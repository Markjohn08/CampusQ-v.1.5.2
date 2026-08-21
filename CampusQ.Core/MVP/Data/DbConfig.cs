using System;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace CampusQ.MVP.Data
{
    public static class DbConfig
    {
        public static string ConnectionString { get; set; } =
            "Data Source=MSI;Initial Catalog=CampusQ;Integrated Security=True;TrustServerCertificate=True";

        public static void EnsureDatabaseAndTables()
        {
            try
            {
                var builder =
                    new SqlConnectionStringBuilder(
                        ConnectionString
                    );

                var database =
                    builder.InitialCatalog;

                if (string.IsNullOrWhiteSpace(database))
                {
                    database = "CampusQ";

                    builder.InitialCatalog =
                        database;

                    ConnectionString =
                        builder.ConnectionString;
                }

                // =====================================================
                // CREATE DATABASE IF IT DOES NOT EXIST
                // =====================================================

                var masterBuilder =
                    new SqlConnectionStringBuilder(
                        ConnectionString
                    )
                    {
                        InitialCatalog = "master"
                    };

                using (var conn =
                    new SqlConnection(
                        masterBuilder.ConnectionString
                    ))
                {
                    conn.Open();

                    using var cmd =
                        conn.CreateCommand();

                    cmd.CommandText =
                        $"IF DB_ID(N'{database}') IS NULL " +
                        $"CREATE DATABASE [{database}];";

                    cmd.ExecuteNonQuery();
                }

                // =====================================================
                // CONNECT TO CAMPUSQ DATABASE
                // =====================================================

                var targetBuilder =
                    new SqlConnectionStringBuilder(
                        ConnectionString
                    )
                    {
                        InitialCatalog = database
                    };

                using (var conn =
                    new SqlConnection(
                        targetBuilder.ConnectionString
                    ))
                {
                    conn.Open();

                    using var cmd =
                        conn.CreateCommand();

                    // =================================================
                    // USERS TABLE
                    // =================================================

                    cmd.CommandText = @"
IF OBJECT_ID(N'dbo.Users') IS NULL
BEGIN
    CREATE TABLE dbo.Users
    (
        Username NVARCHAR(100) PRIMARY KEY,
        PasswordHash NVARCHAR(MAX) NOT NULL,
        Salt NVARCHAR(200) NOT NULL,
        Role NVARCHAR(50) NOT NULL,
        CreatedAt DATETIME2 NOT NULL
    );
END";

                    cmd.ExecuteNonQuery();

                    // =================================================
                    // QUEUE TABLE
                    // =================================================

                    cmd.CommandText = @"
IF OBJECT_ID(N'dbo.Queue') IS NULL
BEGIN
    CREATE TABLE dbo.Queue
    (
        TicketNumber INT IDENTITY(1,1) PRIMARY KEY,
        ServiceTicketNumber INT NOT NULL DEFAULT(0),
        Purpose NVARCHAR(200) NOT NULL,
        Service NVARCHAR(100) NOT NULL,
        TimeAdded DATETIME2 NOT NULL
    );
END";

                    cmd.ExecuteNonQuery();

                    // =================================================
                    // QUEUE HISTORY TABLE
                    // =================================================

                    cmd.CommandText = @"
IF OBJECT_ID(N'dbo.QueueHistory') IS NULL
BEGIN
    CREATE TABLE dbo.QueueHistory
    (
        TicketNumber INT PRIMARY KEY,
        ServiceTicketNumber INT NOT NULL DEFAULT(0),
        Purpose NVARCHAR(200) NOT NULL,
        Service NVARCHAR(100) NOT NULL,
        TimeAdded DATETIME2 NOT NULL,
        ServedAt DATETIME2 NOT NULL
    );
END";

                    cmd.ExecuteNonQuery();

                    // =================================================
                    // SERVICE WINDOWS TABLE
                    // =================================================
                    //
                    // WindowNumber:
                    //     1 = Window 1
                    //     2 = Window 2
                    //     3 = Window 3
                    //     4 = Window 4
                    //
                    // IsActive:
                    //     1 = ON / Available
                    //     0 = OFF / Temporarily Unavailable
                    //
                    // UpdatedAt:
                    //     Stores the last time the status was changed.
                    //
                    // The table is created only if it does not exist.
                    // Existing window statuses will NOT be overwritten.
                    // =================================================

                    cmd.CommandText = @"
IF OBJECT_ID(N'dbo.ServiceWindows') IS NULL
BEGIN
    CREATE TABLE dbo.ServiceWindows
    (
        WindowNumber INT PRIMARY KEY,
        IsActive BIT NOT NULL DEFAULT(1),
        UpdatedAt DATETIME2 NOT NULL DEFAULT(GETDATE())
    );

    INSERT INTO dbo.ServiceWindows
    (
        WindowNumber,
        IsActive,
        UpdatedAt
    )
    VALUES
    (
        1,
        1,
        GETDATE()
    ),
    (
        2,
        1,
        GETDATE()
    ),
    (
        3,
        1,
        GETDATE()
    ),
    (
        4,
        1,
        GETDATE()
    );
END";

                    cmd.ExecuteNonQuery();

                    // =================================================
                    // ENSURE ALL FOUR WINDOWS EXIST
                    // =================================================
                    //
                    // This is useful if ServiceWindows was created
                    // previously but one of the four records is missing.
                    //
                    // Existing ON/OFF values are preserved.
                    // =================================================

                    cmd.CommandText = @"
IF NOT EXISTS
(
    SELECT 1
    FROM dbo.ServiceWindows
    WHERE WindowNumber = 1
)
BEGIN
    INSERT INTO dbo.ServiceWindows
    (
        WindowNumber,
        IsActive,
        UpdatedAt
    )
    VALUES
    (
        1,
        1,
        GETDATE()
    );
END;

IF NOT EXISTS
(
    SELECT 1
    FROM dbo.ServiceWindows
    WHERE WindowNumber = 2
)
BEGIN
    INSERT INTO dbo.ServiceWindows
    (
        WindowNumber,
        IsActive,
        UpdatedAt
    )
    VALUES
    (
        2,
        1,
        GETDATE()
    );
END;

IF NOT EXISTS
(
    SELECT 1
    FROM dbo.ServiceWindows
    WHERE WindowNumber = 3
)
BEGIN
    INSERT INTO dbo.ServiceWindows
    (
        WindowNumber,
        IsActive,
        UpdatedAt
    )
    VALUES
    (
        3,
        1,
        GETDATE()
    );
END;

IF NOT EXISTS
(
    SELECT 1
    FROM dbo.ServiceWindows
    WHERE WindowNumber = 4
)
BEGIN
    INSERT INTO dbo.ServiceWindows
    (
        WindowNumber,
        IsActive,
        UpdatedAt
    )
    VALUES
    (
        4,
        1,
        GETDATE()
    );
END";

                    cmd.ExecuteNonQuery();

                    // =================================================
                    // CREATE PERFORMANCE INDICES
                    // =================================================

                    CreateIndices(conn);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"EnsureDatabaseAndTables failed: {ex}"
                );

                Trace.TraceError(
                    $"EnsureDatabaseAndTables failed: {ex}"
                );
            }
        }

        // ============================================================
        // CREATE INDICES
        // ============================================================

        /// <summary>
        /// Creates performance indices on Queue and QueueHistory
        /// tables for Service, Purpose, and TimeAdded filtering.
        /// Indices help optimize queries for admission, cashier,
        /// and registrar queue filtering.
        /// </summary>
        private static void CreateIndices(
            SqlConnection conn)
        {
            try
            {
                using var cmd =
                    conn.CreateCommand();

                // =================================================
                // QUEUE - SERVICE INDEX
                // =================================================

                cmd.CommandText = @"
IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Queue_Service'
    AND object_id = OBJECT_ID('dbo.Queue')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_Queue_Service
    ON dbo.Queue (Service)
    INCLUDE
    (
        ServiceTicketNumber,
        Purpose,
        TimeAdded
    );
END";

                cmd.ExecuteNonQuery();

                // =================================================
                // QUEUE - SERVICE + PURPOSE INDEX
                // =================================================

                cmd.CommandText = @"
IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Queue_Service_Purpose'
    AND object_id = OBJECT_ID('dbo.Queue')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_Queue_Service_Purpose
    ON dbo.Queue
    (
        Service,
        Purpose
    )
    INCLUDE
    (
        ServiceTicketNumber,
        TimeAdded
    );
END";

                cmd.ExecuteNonQuery();

                // =================================================
                // QUEUE - TIME ADDED INDEX
                // =================================================

                cmd.CommandText = @"
IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_Queue_TimeAdded'
    AND object_id = OBJECT_ID('dbo.Queue')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_Queue_TimeAdded
    ON dbo.Queue (TimeAdded)
    INCLUDE
    (
        Service,
        Purpose,
        ServiceTicketNumber
    );
END";

                cmd.ExecuteNonQuery();

                // =================================================
                // QUEUE HISTORY - SERVICE INDEX
                // =================================================

                cmd.CommandText = @"
IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_QueueHistory_Service'
    AND object_id = OBJECT_ID('dbo.QueueHistory')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_QueueHistory_Service
    ON dbo.QueueHistory (Service)
    INCLUDE
    (
        ServiceTicketNumber,
        Purpose,
        TimeAdded,
        ServedAt
    );
END";

                cmd.ExecuteNonQuery();

                // =================================================
                // QUEUE HISTORY - SERVED AT INDEX
                // =================================================

                cmd.CommandText = @"
IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_QueueHistory_ServedAt'
    AND object_id = OBJECT_ID('dbo.QueueHistory')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_QueueHistory_ServedAt
    ON dbo.QueueHistory (ServedAt DESC)
    INCLUDE
    (
        Service,
        Purpose,
        TimeAdded
    );
END";

                cmd.ExecuteNonQuery();

                // =================================================
                // SERVICE WINDOWS - ACTIVE STATUS INDEX
                // =================================================

                cmd.CommandText = @"
IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = 'IX_ServiceWindows_IsActive'
    AND object_id = OBJECT_ID('dbo.ServiceWindows')
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_ServiceWindows_IsActive
    ON dbo.ServiceWindows (IsActive)
    INCLUDE
    (
        WindowNumber,
        UpdatedAt
    );
END";

                cmd.ExecuteNonQuery();

                Debug.WriteLine(
                    "Database indices created successfully."
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"CreateIndices failed: {ex}"
                );

                // Non-critical error.
                // Do not throw.
            }
        }
    }
}