using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace CampusQ.MVP.Data
{
    /// <summary>
    /// Database verification utility to test connectivity, schema, and indices.
    /// Can be run at startup to ensure database is properly configured.
    /// </summary>
    public static class DbVerification
    {
        public static bool VerifyDatabase(string connectionString)
        {
            try
            {
                Debug.WriteLine("=== Database Verification Started ===");

                // Ensure database exists first
                DbConfig.EnsureDatabaseAndTables();
                Debug.WriteLine("✓ Database and tables initialized");

                // Test connection
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Debug.WriteLine("✓ Database connection successful");

                    // Verify Queue table structure
                    if (!VerifyTableExists(conn, "Queue"))
                    {
                        Debug.WriteLine("✗ Queue table not found");
                        return false;
                    }
                    Debug.WriteLine("✓ Queue table exists");

                    // Verify QueueHistory table structure
                    if (!VerifyTableExists(conn, "QueueHistory"))
                    {
                        Debug.WriteLine("✗ QueueHistory table not found");
                        return false;
                    }
                    Debug.WriteLine("✓ QueueHistory table exists");

                    // Verify Users table structure
                    if (!VerifyTableExists(conn, "Users"))
                    {
                        Debug.WriteLine("✗ Users table not found");
                        return false;
                    }
                    Debug.WriteLine("✓ Users table exists");

                    // Verify indices
                    var indices = GetTableIndices(conn, "Queue");
                    Debug.WriteLine($"✓ Queue table has {indices.Count} indices: {string.Join(", ", indices)}");

                    var historyIndices = GetTableIndices(conn, "QueueHistory");
                    Debug.WriteLine($"✓ QueueHistory table has {historyIndices.Count} indices: {string.Join(", ", historyIndices)}");

                    // Test service-specific queries
                    if (!TestServiceQueries(conn))
                    {
                        Debug.WriteLine("✗ Service queries failed");
                        return false;
                    }
                    Debug.WriteLine("✓ Service queries (Admission, Cashier, Registrar) verified");
                }

                Debug.WriteLine("=== Database Verification Completed Successfully ===");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"=== Database Verification Failed ===");
                Debug.WriteLine($"Error: {ex.Message}");
                Debug.WriteLine(ex.StackTrace);
                return false;
            }
        }

        private static bool VerifyTableExists(SqlConnection conn, string tableName)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
            var result = cmd.ExecuteScalar();
            return result != null;
        }

        private static List<string> GetTableIndices(SqlConnection conn, string tableName)
        {
            var indices = new List<string>();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = $@"SELECT name FROM sys.indexes 
                                 WHERE object_id = OBJECT_ID('dbo.{tableName}') 
                                 AND name IS NOT NULL 
                                 ORDER BY name";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                indices.Add(reader.GetString(0));
            }
            return indices;
        }

        private static bool TestServiceQueries(SqlConnection conn)
        {
            try
            {
                var services = new[] { "Admission", "Cashier", "Registrar" };

                foreach (var service in services)
                {
                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT COUNT(*) FROM dbo.Queue WHERE Service = @service";
                    cmd.Parameters.AddWithValue("@service", service);
                    var count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    Debug.WriteLine($"  - {service} queue count: {count}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Service query test failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets database statistics for monitoring and reporting.
        /// </summary>
        public static DatabaseStats GetDatabaseStats(string connectionString)
        {
            var stats = new DatabaseStats();

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Total queue entries
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM dbo.Queue";
                        stats.TotalActiveQueueEntries = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }

                    // Queue entries by service
                    foreach (var service in new[] { "Admission", "Cashier", "Registrar" })
                    {
                        using (var cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT COUNT(*) FROM dbo.Queue WHERE Service = @service";
                            cmd.Parameters.AddWithValue("@service", service);
                            var count = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                            stats.QueueCountByService[service] = count;
                        }
                    }

                    // Historical entries
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM dbo.QueueHistory";
                        stats.TotalHistoricalEntries = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }

                    // User accounts
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM dbo.Users";
                        stats.TotalUserAccounts = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }

                    stats.LastVerifiedAt = DateTime.Now;
                    stats.IsHealthy = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetDatabaseStats failed: {ex.Message}");
                stats.IsHealthy = false;
                stats.LastError = ex.Message;
            }

            return stats;
        }
    }

    public class DatabaseStats
    {
        public int TotalActiveQueueEntries { get; set; }
        public Dictionary<string, int> QueueCountByService { get; set; } = new Dictionary<string, int>();
        public int TotalHistoricalEntries { get; set; }
        public int TotalUserAccounts { get; set; }
        public DateTime LastVerifiedAt { get; set; }
        public bool IsHealthy { get; set; }
        public string LastError { get; set; }

        public override string ToString()
        {
            return $@"Database Statistics (as of {LastVerifiedAt:g}):
  Health Status: {(IsHealthy ? "✓ Healthy" : "✗ Unhealthy")}
  Active Queue Entries: {TotalActiveQueueEntries}
    - Admission: {QueueCountByService.GetValueOrDefault("Admission", 0)}
    - Cashier: {QueueCountByService.GetValueOrDefault("Cashier", 0)}
    - Registrar: {QueueCountByService.GetValueOrDefault("Registrar", 0)}
  Historical Entries: {TotalHistoricalEntries}
  User Accounts: {TotalUserAccounts}
{(string.IsNullOrEmpty(LastError) ? "" : $"  Last Error: {LastError}")}";
        }
    }
}
