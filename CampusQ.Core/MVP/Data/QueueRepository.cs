using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Diagnostics;
using CampusQ.MVP.Models;

namespace CampusQ.MVP.Data
{
    public class QueueRepository
    {
        private readonly string _conn;

        public QueueRepository(string connectionString)
        {
            _conn = connectionString;
        }

        // =========================================================
        // ADD QUEUE
        // =========================================================

        public void Add(QueueEntry entry)
        {
            using var conn =
                new SqlConnection(_conn);

            conn.Open();

            using var tran =
                conn.BeginTransaction(
                    IsolationLevel.Serializable);

            using var cmd =
                conn.CreateCommand();

            cmd.Transaction =
                tran;

            cmd.CommandText =
                "SELECT ISNULL(MAX(ServiceTicketNumber), 0) + 1 " +
                "FROM dbo.Queue " +
                "WHERE Service = @s AND Purpose = @p";

            cmd.Parameters.AddWithValue(
                "@s",
                entry.Service ?? "");

            cmd.Parameters.AddWithValue(
                "@p",
                entry.Purpose ?? "");

            var nextServiceNumber =
                Convert.ToInt32(
                    cmd.ExecuteScalar() ?? 1);

            cmd.Parameters.Clear();

            cmd.CommandText =
                "INSERT INTO dbo.Queue " +
                "(ServiceTicketNumber, Purpose, Service, TimeAdded) " +
                "VALUES (@stn, @p, @s, @t); " +
                "SELECT CAST(SCOPE_IDENTITY() as int);";

            cmd.Parameters.AddWithValue(
                "@stn",
                nextServiceNumber);

            cmd.Parameters.AddWithValue(
                "@p",
                entry.Purpose ?? "");

            cmd.Parameters.AddWithValue(
                "@s",
                entry.Service ?? "");

            cmd.Parameters.AddWithValue(
                "@t",
                entry.TimeAdded);

            var id =
                cmd.ExecuteScalar();

            if (
                id != null &&
                int.TryParse(
                    id.ToString(),
                    out var ticket))
            {
                entry.TicketNumber =
                    ticket;
            }

            entry.ServiceTicketNumber =
                nextServiceNumber;

            tran.Commit();
        }

        // =========================================================
        // GET ALL QUEUE
        // =========================================================

        public List<QueueEntry> GetAll()
        {
            var list =
                new List<QueueEntry>();

            using var conn =
                new SqlConnection(_conn);

            conn.Open();

            using var cmd =
                conn.CreateCommand();

            cmd.CommandText =
                "SELECT TicketNumber, ServiceTicketNumber, Purpose, " +
                "Service, TimeAdded " +
                "FROM dbo.Queue " +
                "ORDER BY TicketNumber";

            using var reader =
                cmd.ExecuteReader();

            while (reader.Read())
            {
                var entry =
                    new QueueEntry
                    {
                        TicketNumber =
                            reader.GetInt32(0),

                        ServiceTicketNumber =
                            reader.IsDBNull(1)
                                ? 0
                                : reader.GetInt32(1),

                        Purpose =
                            reader.IsDBNull(2)
                                ? ""
                                : reader.GetString(2),

                        Service =
                            reader.IsDBNull(3)
                                ? ""
                                : reader.GetString(3),

                        TimeAdded =
                            reader.GetDateTime(4)
                    };

                list.Add(entry);
            }

            Debug.WriteLine(
                $"QueueRepository.GetAll returned {list.Count} rows from database.");

            return list;
        }

        // =========================================================
        // GET ALL BY SERVICE
        // =========================================================

        public List<QueueEntry> GetAllByService(
            string service)
        {
            var list =
                new List<QueueEntry>();

            using var conn =
                new SqlConnection(_conn);

            conn.Open();

            using var cmd =
                conn.CreateCommand();

            cmd.CommandText =
                "SELECT TicketNumber, ServiceTicketNumber, Purpose, " +
                "Service, TimeAdded " +
                "FROM dbo.Queue " +
                "WHERE Service = @s " +
                "ORDER BY TicketNumber";

            cmd.Parameters.AddWithValue(
                "@s",
                service ?? "");

            using var reader =
                cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(
                    new QueueEntry
                    {
                        TicketNumber =
                            reader.GetInt32(0),

                        ServiceTicketNumber =
                            reader.IsDBNull(1)
                                ? 0
                                : reader.GetInt32(1),

                        Purpose =
                            reader.IsDBNull(2)
                                ? ""
                                : reader.GetString(2),

                        Service =
                            reader.IsDBNull(3)
                                ? ""
                                : reader.GetString(3),

                        TimeAdded =
                            reader.GetDateTime(4)
                    });
            }

            return list;
        }

        // =========================================================
        // GET BY TICKET NUMBER
        // =========================================================

        public QueueEntry? GetByTicketNumber(
            int ticketNumber)
        {
            using var conn =
                new SqlConnection(_conn);

            conn.Open();

            using var cmd =
                conn.CreateCommand();

            cmd.CommandText =
                "SELECT TicketNumber, ServiceTicketNumber, Purpose, " +
                "Service, TimeAdded " +
                "FROM dbo.Queue " +
                "WHERE TicketNumber = @t";

            cmd.Parameters.AddWithValue(
                "@t",
                ticketNumber);

            using var reader =
                cmd.ExecuteReader();

            if (reader.Read())
            {
                return new QueueEntry
                {
                    TicketNumber =
                        reader.GetInt32(0),

                    ServiceTicketNumber =
                        reader.IsDBNull(1)
                            ? 0
                            : reader.GetInt32(1),

                    Purpose =
                        reader.IsDBNull(2)
                            ? ""
                            : reader.GetString(2),

                    Service =
                        reader.IsDBNull(3)
                            ? ""
                            : reader.GetString(3),

                    TimeAdded =
                        reader.GetDateTime(4)
                };
            }

            return null;
        }

        // =========================================================
        // COUNT AHEAD
        // =========================================================

        public int CountAhead(
            string service,
            string purpose,
            int ticketNumber)
        {
            using var conn =
                new SqlConnection(_conn);

            conn.Open();

            using var cmd =
                conn.CreateCommand();

            cmd.CommandText =
                "SELECT COUNT(*) " +
                "FROM dbo.Queue " +
                "WHERE Service = @s " +
                "AND Purpose = @p " +
                "AND TicketNumber < @t";

            cmd.Parameters.AddWithValue(
                "@s",
                service ?? "");

            cmd.Parameters.AddWithValue(
                "@p",
                purpose ?? "");

            cmd.Parameters.AddWithValue(
                "@t",
                ticketNumber);

            return Convert.ToInt32(
                cmd.ExecuteScalar() ?? 0);
        }

        // =========================================================
        // GET HISTORY BY TICKET
        // =========================================================

        public QueuePersistDto? GetHistoryByTicketNumber(
            int ticketNumber)
        {
            using var conn =
                new SqlConnection(_conn);

            conn.Open();

            using var cmd =
                conn.CreateCommand();

            cmd.CommandText =
                "SELECT TicketNumber, ServiceTicketNumber, Purpose, " +
                "Service, TimeAdded, ServedAt " +
                "FROM dbo.QueueHistory " +
                "WHERE TicketNumber = @t";

            cmd.Parameters.AddWithValue(
                "@t",
                ticketNumber);

            using var reader =
                cmd.ExecuteReader();

            if (reader.Read())
            {
                return new QueuePersistDto
                {
                    TicketNumber =
                        reader.GetInt32(0),

                    ServiceTicketNumber =
                        reader.IsDBNull(1)
                            ? 0
                            : reader.GetInt32(1),

                    Purpose =
                        reader.IsDBNull(2)
                            ? ""
                            : reader.GetString(2),

                    Service =
                        reader.IsDBNull(3)
                            ? ""
                            : reader.GetString(3),

                    TimeAdded =
                        reader.IsDBNull(4)
                            ? DateTime.MinValue
                            : reader.GetDateTime(4),

                    ServedAt =
                        reader.IsDBNull(5)
                            ? (DateTime?)null
                            : reader.GetDateTime(5)
                };
            }

            return null;
        }

        // =========================================================
        // GET ALL HISTORY
        // =========================================================

        public List<QueuePersistDto> GetHistoryAll()
        {
            var list =
                new List<QueuePersistDto>();

            using var conn =
                new SqlConnection(_conn);

            conn.Open();

            using var cmd =
                conn.CreateCommand();

            cmd.CommandText =
                "SELECT TicketNumber, ServiceTicketNumber, Purpose, " +
                "Service, TimeAdded, ServedAt " +
                "FROM dbo.QueueHistory " +
                "ORDER BY TicketNumber";

            using var reader =
                cmd.ExecuteReader();

            while (reader.Read())
            {
                var dto =
                    new QueuePersistDto
                    {
                        TicketNumber =
                            reader.GetInt32(0),

                        ServiceTicketNumber =
                            reader.IsDBNull(1)
                                ? 0
                                : reader.GetInt32(1),

                        Purpose =
                            reader.IsDBNull(2)
                                ? ""
                                : reader.GetString(2),

                        Service =
                            reader.IsDBNull(3)
                                ? ""
                                : reader.GetString(3),

                        TimeAdded =
                            reader.IsDBNull(4)
                                ? DateTime.MinValue
                                : reader.GetDateTime(4),

                        ServedAt =
                            reader.IsDBNull(5)
                                ? (DateTime?)null
                                : reader.GetDateTime(5)
                    };

                list.Add(dto);
            }

            Debug.WriteLine(
                $"QueueRepository.GetHistoryAll returned {list.Count} rows from database.");

            return list;
        }

        // =========================================================
        // DAILY ADMISSION ANALYTICS
        // =========================================================
        //
        // IMPORTANT:
        // Admission records served using:
        //
        // Admission - Window 1
        // Admission - Window 2
        //
        // This allows analytics to use the ACTUAL window
        // instead of calculating the window from the ticket number.
        //
        // Old records that only contain "Admission" will still
        // use the old ticket-number calculation as fallback.
        // =========================================================

        public Dictionary<int, int>
            GetTodayAdmissionServedByWindow()
        {
            var result =
                new Dictionary<int, int>
                {
                    { 1, 0 },
                    { 2, 0 }
                };

            try
            {
                using var conn =
                    new SqlConnection(_conn);

                conn.Open();

                using var cmd =
                    conn.CreateCommand();

                DateTime today =
                    DateTime.Today;

                DateTime tomorrow =
                    today.AddDays(1);

                cmd.CommandText =
                    @"SELECT ServiceTicketNumber, Service
                      FROM dbo.QueueHistory
                      WHERE ServedAt >= @today
                        AND ServedAt < @tomorrow
                        AND Service LIKE '%Admission%'
                      ORDER BY ServedAt";

                cmd.Parameters.AddWithValue(
                    "@today",
                    today);

                cmd.Parameters.AddWithValue(
                    "@tomorrow",
                    tomorrow);

                using var reader =
                    cmd.ExecuteReader();

                while (reader.Read())
                {
                    int serviceTicketNumber =
                        reader.IsDBNull(0)
                            ? 0
                            : reader.GetInt32(0);

                    string service =
                        reader.IsDBNull(1)
                            ? ""
                            : reader.GetString(1);

                    int window =
                        GetAdmissionWindowFromHistory(
                            service,
                            serviceTicketNumber);

                    if (window == 1)
                    {
                        result[1]++;
                    }
                    else if (window == 2)
                    {
                        result[2]++;
                    }
                }

                Debug.WriteLine(
                    "[QueueRepository] Today's Admission Analytics: " +
                    $"W1={result[1]}, W2={result[2]}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "[QueueRepository] " +
                    $"GetTodayAdmissionServedByWindow failed: {ex}");
            }

            return result;
        }

        // =========================================================
        // GET ACTUAL ADMISSION WINDOW FROM HISTORY
        // =========================================================

        private static int GetAdmissionWindowFromHistory(
            string service,
            int serviceTicketNumber)
        {
            if (!string.IsNullOrWhiteSpace(service))
            {
                // -------------------------------------------------
                // NEW FORMAT
                // -------------------------------------------------

                if (
                    service.IndexOf(
                        "Admission - Window 1",
                        StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return 1;
                }

                if (
                    service.IndexOf(
                        "Admission - Window 2",
                        StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return 2;
                }

                // Also support:
                //
                // Admission Window 1
                // Admission Window 2

                if (
                    service.IndexOf(
                        "Admission Window 1",
                        StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return 1;
                }

                if (
                    service.IndexOf(
                        "Admission Window 2",
                        StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return 2;
                }
            }

            // -----------------------------------------------------
            // FALLBACK FOR OLD HISTORY RECORDS
            // -----------------------------------------------------
            //
            // Existing records were previously assigned using
            // ServiceTicketNumber.
            //
            // We keep this fallback so old analytics won't break.
            // -----------------------------------------------------

            if (serviceTicketNumber > 0)
            {
                return
                    ((serviceTicketNumber - 1) % 2) + 1;
            }

            return 0;
        }

        // =========================================================
        // REMOVE / ARCHIVE QUEUE
        // =========================================================
        //
        // EXISTING METHOD
        //
        // Used by Registrar / Cashier / older logic.
        //
        // DO NOT REMOVE.
        // =========================================================

        public void Remove(
            int ticketNumber)
        {
            using var conn =
                new SqlConnection(_conn);

            conn.Open();

            using var cmd =
                conn.CreateCommand();

            cmd.CommandText =
                @"INSERT INTO dbo.QueueHistory
                  (
                      TicketNumber,
                      ServiceTicketNumber,
                      Purpose,
                      Service,
                      TimeAdded,
                      ServedAt
                  )
                  SELECT
                      TicketNumber,
                      ServiceTicketNumber,
                      Purpose,
                      Service,
                      TimeAdded,
                      @servedAt
                  FROM dbo.Queue
                  WHERE TicketNumber = @t;

                  DELETE FROM dbo.Queue
                  WHERE TicketNumber = @t;";

            cmd.Parameters.AddWithValue(
                "@t",
                ticketNumber);

            cmd.Parameters.AddWithValue(
                "@servedAt",
                DateTime.Now);

            cmd.ExecuteNonQuery();
        }

        // =========================================================
        // ADMISSION REMOVE / ARCHIVE WITH ACTUAL WINDOW
        // =========================================================
        //
        // NEW METHOD
        //
        // Example:
        //
        // Remove(15, 2)
        //
        // QueueHistory.Service becomes:
        //
        // Admission - Window 2
        //
        // This is what makes the Admission analytics accurate.
        // =========================================================

        public void Remove(
            int ticketNumber,
            int servedWindow)
        {
            // -----------------------------------------------------
            // Safety check
            // -----------------------------------------------------

            if (
                servedWindow < 1 ||
                servedWindow > 2)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(servedWindow),
                    "Admission window must be 1 or 2.");
            }

            using var conn =
                new SqlConnection(_conn);

            conn.Open();

            using var cmd =
                conn.CreateCommand();

            cmd.CommandText =
                @"INSERT INTO dbo.QueueHistory
                  (
                      TicketNumber,
                      ServiceTicketNumber,
                      Purpose,
                      Service,
                      TimeAdded,
                      ServedAt
                  )
                  SELECT
                      TicketNumber,
                      ServiceTicketNumber,
                      Purpose,
                      @service,
                      TimeAdded,
                      @servedAt
                  FROM dbo.Queue
                  WHERE TicketNumber = @t;

                  DELETE FROM dbo.Queue
                  WHERE TicketNumber = @t;";

            cmd.Parameters.AddWithValue(
                "@t",
                ticketNumber);

            cmd.Parameters.AddWithValue(
                "@service",
                $"Admission - Window {servedWindow}");

            cmd.Parameters.AddWithValue(
                "@servedAt",
                DateTime.Now);

            int affected =
                cmd.ExecuteNonQuery();

            Debug.WriteLine(
                $"[QueueRepository] " +
                $"Admission ticket {ticketNumber} " +
                $"served at Window {servedWindow}. " +
                $"Rows={affected}");
        }

        // =========================================================
        // CLEAR ALL QUEUE
        // =========================================================

        public void ClearAll()
        {
            using var conn =
                new SqlConnection(_conn);

            conn.Open();

            using var cmd =
                conn.CreateCommand();

            cmd.CommandText =
                "DELETE FROM dbo.Queue";

            cmd.ExecuteNonQuery();

            cmd.CommandText =
                "DBCC CHECKIDENT ('dbo.Queue', RESEED, 0);";

            cmd.ExecuteNonQuery();
        }

        // =========================================================
        // CLEAR HISTORY
        // =========================================================

        public void ClearHistory()
        {
            using var conn =
                new SqlConnection(_conn);

            conn.Open();

            using var cmd =
                conn.CreateCommand();

            cmd.CommandText =
                "DELETE FROM dbo.QueueHistory";

            cmd.ExecuteNonQuery();
        }

        // =========================================================
        // SERVICE WINDOW STATUS
        // =========================================================
        //
        // Shared by Registrar / Cashier / Admission.
        //
        // Admission uses ONLY Window 1 and Window 2.
        // Registrar / Cashier can still use 1-4.
        // =========================================================

        public bool GetWindowStatus(
            int windowNumber)
        {
            if (
                windowNumber < 1 ||
                windowNumber > 4)
            {
                return false;
            }

            try
            {
                using var conn =
                    new SqlConnection(_conn);

                conn.Open();

                using var cmd =
                    conn.CreateCommand();

                cmd.CommandText =
                    @"SELECT IsActive
                      FROM dbo.ServiceWindows
                      WHERE WindowNumber = @windowNumber";

                cmd.Parameters.AddWithValue(
                    "@windowNumber",
                    windowNumber);

                object? result =
                    cmd.ExecuteScalar();

                if (
                    result == null ||
                    result == DBNull.Value)
                {
                    return false;
                }

                return Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[QueueRepository] " +
                    $"GetWindowStatus({windowNumber}) failed: {ex}");

                return false;
            }
        }

        // =========================================================
        // SET WINDOW STATUS
        // =========================================================

        public bool SetWindowStatus(
            int windowNumber,
            bool isActive)
        {
            if (
                windowNumber < 1 ||
                windowNumber > 4)
            {
                return false;
            }

            try
            {
                using var conn =
                    new SqlConnection(_conn);

                conn.Open();

                using var cmd =
                    conn.CreateCommand();

                cmd.CommandText =
                    @"UPDATE dbo.ServiceWindows
                      SET
                          IsActive = @isActive,
                          UpdatedAt = @updatedAt
                      WHERE WindowNumber = @windowNumber";

                cmd.Parameters.AddWithValue(
                    "@isActive",
                    isActive);

                cmd.Parameters.AddWithValue(
                    "@updatedAt",
                    DateTime.Now);

                cmd.Parameters.AddWithValue(
                    "@windowNumber",
                    windowNumber);

                int affected =
                    cmd.ExecuteNonQuery();

                Debug.WriteLine(
                    $"[QueueRepository] " +
                    $"Window {windowNumber} " +
                    $"status changed to " +
                    $"{(isActive ? "ON" : "OFF")}. " +
                    $"Rows={affected}");

                return affected > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[QueueRepository] " +
                    $"SetWindowStatus({windowNumber}) failed: {ex}");

                return false;
            }
        }

        // =========================================================
        // GET ALL WINDOW STATUSES
        // =========================================================

        public Dictionary<int, bool>
            GetAllWindowStatuses()
        {
            var result =
                new Dictionary<int, bool>
                {
                    { 1, true },
                    { 2, true },
                    { 3, true },
                    { 4, true }
                };

            try
            {
                using var conn =
                    new SqlConnection(_conn);

                conn.Open();

                using var cmd =
                    conn.CreateCommand();

                cmd.CommandText =
                    @"SELECT WindowNumber, IsActive
                      FROM dbo.ServiceWindows
                      ORDER BY WindowNumber";

                using var reader =
                    cmd.ExecuteReader();

                while (reader.Read())
                {
                    int windowNumber =
                        reader.GetInt32(0);

                    bool isActive =
                        reader.GetBoolean(1);

                    if (
                        windowNumber >= 1 &&
                        windowNumber <= 4)
                    {
                        result[windowNumber] =
                            isActive;
                    }
                }

                Debug.WriteLine(
                    "[QueueRepository] " +
                    "Window statuses loaded: " +
                    $"W1={result[1]}, " +
                    $"W2={result[2]}, " +
                    $"W3={result[3]}, " +
                    $"W4={result[4]}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[QueueRepository] " +
                    $"GetAllWindowStatuses failed: {ex}");
            }

            return result;
        }
    }
}