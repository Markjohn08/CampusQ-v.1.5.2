using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using CampusQ.MVP.Data;
using CampusQ.MVP.Models;
using CampusQ.MVP.Views;

namespace CampusQ.MVP.Presenters
{
    public class CashierPresenter
    {
        private readonly ICashierView _view;
        private readonly List<QueueEntry> _masterQueue = new();
        private readonly QueueRepository _queueRepo;

        private static int _nextTicketNumber = 1;

        // =========================================================
        // CASHIER WINDOWS
        // =========================================================

        private const int WindowCount = 4;

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public CashierPresenter(ICashierView view)
        {
            _view = view
                ?? throw new ArgumentNullException(nameof(view));

            DbConfig.EnsureDatabaseAndTables();

            _queueRepo =
                new QueueRepository(
                    DbConfig.ConnectionString
                );

            LoadQueue();
            ApplyFilter();
        }

        // =========================================================
        // ADD TO QUEUE
        // =========================================================

        public void AddToQueue(
            string purpose,
            string service)
        {
            try
            {
                string effectiveService =
                    !string.IsNullOrWhiteSpace(service)
                        ? service
                        : (_view.SelectedService ?? "");

                var entry = new QueueEntry
                {
                    Purpose =
                        string.IsNullOrWhiteSpace(purpose)
                            ? "Unknown"
                            : purpose,

                    Service =
                        NormalizeService(
                            effectiveService
                        ),

                    TimeAdded = DateTime.Now
                };

                _queueRepo.Add(entry);

                _masterQueue.Add(entry);

                ApplyFilter();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierPresenter] AddToQueue failed: {ex}"
                );

                _view.ShowMessage(
                    $"Failed to add queue:\n\n{ex.Message}",
                    "Queue Error",
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // NORMALIZE SERVICE
        // =========================================================

        private static string NormalizeService(
            string? service)
        {
            if (string.IsNullOrWhiteSpace(service))
                return "Cashier";

            string s = service.Trim();

            string lower =
                s.ToLowerInvariant();

            if (lower == "all")
                return "Cashier";

            if (lower.Contains("registr"))
                return "Registrar";

            if (
                lower.Contains("cashier") ||
                lower.Contains("window") ||
                Regex.IsMatch(
                    lower,
                    @"(^|\s)w\s*\d",
                    RegexOptions.IgnoreCase
                )
            )
            {
                return "Cashier";
            }

            return s;
        }

        // =========================================================
        // CHECK CASHIER SERVICE
        // =========================================================

        private static bool IsCashierService(
            string? service)
        {
            if (string.IsNullOrWhiteSpace(service))
                return true;

            string s =
                service.Trim()
                    .ToLowerInvariant();

            if (s == "all")
                return true;

            if (s == "other")
                return true;

            if (s.Contains("cashier"))
                return true;

            if (s.Contains("window"))
                return true;

            if (
                Regex.IsMatch(
                    s,
                    @"(^|\s)w\s*\d",
                    RegexOptions.IgnoreCase
                )
            )
            {
                return true;
            }

            return false;
        }

        // =========================================================
        // GET WINDOW
        // =========================================================

        private static int GetAssignedWindow(
            int sequence)
        {
            if (sequence <= 0)
                sequence = 1;

            return
                ((sequence - 1) % WindowCount) + 1;
        }

        // =========================================================
        // GET WINDOW FOR ENTRY
        // =========================================================

        private static int GetAssignedWindowForEntry(
            QueueEntry entry)
        {
            if (entry == null)
                return 1;

            int sequence;

            // Service ticket number has priority because it is
            // specific to the selected service.
            if (entry.ServiceTicketNumber > 0)
            {
                sequence =
                    entry.ServiceTicketNumber;
            }
            else if (entry.TicketNumber > 0)
            {
                sequence =
                    entry.TicketNumber;
            }
            else
            {
                sequence = 1;
            }

            return GetAssignedWindow(sequence);
        }

        // =========================================================
        // GET SEQUENCE
        // =========================================================

        private static int GetSequenceForEntry(
            QueueEntry entry)
        {
            if (entry == null)
                return int.MaxValue;

            if (entry.ServiceTicketNumber > 0)
                return entry.ServiceTicketNumber;

            if (entry.TicketNumber > 0)
                return entry.TicketNumber;

            return int.MaxValue;
        }

        // =========================================================
        // CLONE FOR DISPLAY
        // =========================================================

        private static QueueEntry CloneForDisplay(
            QueueEntry source,
            int window)
        {
            return new QueueEntry
            {
                TicketNumber =
                    source.TicketNumber,

                ServiceTicketNumber =
                    source.ServiceTicketNumber,

                Purpose =
                    source.Purpose,

                Service =
                    $"Cashier - Window {window}",

                TimeAdded =
                    source.TimeAdded
            };
        }

        // =========================================================
        // APPLY FILTER
        // =========================================================

        private void ApplyFilter()
        {
            try
            {
                var cashierEntries =
                    _masterQueue
                        .Where(q =>
                            IsCashierService(q.Service)
                        )
                        .OrderBy(e =>
                            GetSequenceForEntry(e)
                        )
                        .ToList();

                string selected =
                    (_view.SelectedService ?? "")
                        .Trim();

                int? selectedWindow =
                    GetSelectedWindow(selected);

                IEnumerable<QueueEntry> display;

                // =================================================
                // SPECIFIC WINDOW
                // =================================================

                if (selectedWindow.HasValue)
                {
                    int window =
                        selectedWindow.Value;

                    display =
                        cashierEntries
                            .Where(e =>
                                GetAssignedWindowForEntry(e)
                                == window
                            )
                            .Select(e =>
                                CloneForDisplay(
                                    e,
                                    window
                                )
                            );
                }

                // =================================================
                // ALL WINDOWS
                // =================================================

                else
                {
                    display =
                        cashierEntries
                            .Select(e =>
                                CloneForDisplay(
                                    e,
                                    GetAssignedWindowForEntry(e)
                                )
                            )
                            .OrderBy(e =>
                                GetSequenceForEntry(e)
                            );
                }

                var view =
                    new BindingList<QueueEntry>(
                        display.ToList()
                    );

                Debug.WriteLine(
                    $"[CashierPresenter] " +
                    $"Master={_masterQueue.Count}, " +
                    $"Cashier={cashierEntries.Count}, " +
                    $"Display={view.Count}"
                );

                _view.BindQueue(view);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierPresenter] ApplyFilter failed: {ex}"
                );
            }
        }

        // =========================================================
        // GET SELECTED WINDOW
        // =========================================================

        private static int? GetSelectedWindow(
            string selected)
        {
            if (string.IsNullOrWhiteSpace(selected))
                return null;

            bool isWindow =
                selected.IndexOf(
                    "Window",
                    StringComparison.OrdinalIgnoreCase
                ) >= 0
                ||
                Regex.IsMatch(
                    selected,
                    @"\bW\s*\d",
                    RegexOptions.IgnoreCase
                );

            if (!isWindow)
                return null;

            Match match =
                Regex.Match(
                    selected,
                    @"\d+"
                );

            if (!match.Success)
                return null;

            if (!int.TryParse(
                match.Value,
                out int window))
            {
                return null;
            }

            if (
                window < 1 ||
                window > WindowCount
            )
            {
                return null;
            }

            return window;
        }

        // =========================================================
        // CHECK WINDOW ACTIVE
        // =========================================================

        private bool IsWindowActive(
            int window)
        {
            if (
                window < 1 ||
                window > WindowCount
            )
            {
                return false;
            }

            try
            {
                return _view.IsWindowActive(
                    window
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierPresenter] " +
                    $"Unable to read Window {window}: {ex}"
                );

                return false;
            }
        }

        // =========================================================
        // GET ACTIVE WINDOWS
        // =========================================================

        private List<int> GetActiveWindows()
        {
            var activeWindows =
                new List<int>();

            for (
                int window = 1;
                window <= WindowCount;
                window++
            )
            {
                if (IsWindowActive(window))
                {
                    activeWindows.Add(window);
                }
            }

            return activeWindows;
        }

        // =========================================================
        // SERVE NEXT
        // =========================================================

        public void ServeNext()
        {
            try
            {
                // Reload latest database queue.
                LoadQueue();

                string selected =
                    (_view.SelectedService ?? "")
                        .Trim();

                var cashierEntries =
                    _masterQueue
                        .Where(q =>
                            IsCashierService(q.Service)
                        )
                        .OrderBy(e =>
                            GetSequenceForEntry(e)
                        )
                        .ToList();

                if (!cashierEntries.Any())
                {
                    _view.ShowMessage(
                        "No one is currently in the cashier queue.",
                        "Queue Empty",
                        MessageBoxIcon.Information
                    );

                    return;
                }

                int? selectedWindow =
                    GetSelectedWindow(selected);

                QueueEntry? next = null;
                int assignedWindow;

                // =================================================
                // SPECIFIC WINDOW
                // =================================================

                if (selectedWindow.HasValue)
                {
                    int window =
                        selectedWindow.Value;

                    if (!IsWindowActive(window))
                    {
                        _view.ShowMessage(
                            $"Window {window} is currently CLOSED.\n\n" +
                            "Set the window to ON before serving the next customer.",
                            "Window Unavailable",
                            MessageBoxIcon.Warning
                        );

                        return;
                    }

                    next =
                        cashierEntries
                            .FirstOrDefault(e =>
                                GetAssignedWindowForEntry(e)
                                == window
                            );

                    assignedWindow =
                        window;
                }

                // =================================================
                // ALL WINDOWS
                // =================================================

                else
                {
                    var activeWindows =
                        GetActiveWindows();

                    if (!activeWindows.Any())
                    {
                        _view.ShowMessage(
                            "All cashier windows are currently CLOSED.\n\n" +
                            "Please turn ON at least one window before serving.",
                            "No Active Windows",
                            MessageBoxIcon.Warning
                        );

                        return;
                    }

                    next =
                        cashierEntries
                            .FirstOrDefault(e =>
                                activeWindows.Contains(
                                    GetAssignedWindowForEntry(e)
                                )
                            );

                    if (next == null)
                    {
                        _view.ShowMessage(
                            "The waiting customers are assigned to closed windows.\n\n" +
                            "Open the required window to continue serving.",
                            "No Available Window",
                            MessageBoxIcon.Warning
                        );

                        return;
                    }

                    assignedWindow =
                        GetAssignedWindowForEntry(next);
                }

                // =================================================
                // NO CUSTOMER
                // =================================================

                if (next == null)
                {
                    _view.ShowMessage(
                        $"No customer is currently waiting for Window {assignedWindow}.",
                        "Queue Empty",
                        MessageBoxIcon.Information
                    );

                    return;
                }

                // =================================================
                // FINAL WINDOW CHECK
                // =================================================

                if (!IsWindowActive(assignedWindow))
                {
                    _view.ShowMessage(
                        $"Window {assignedWindow} was closed and cannot serve this customer.",
                        "Window Unavailable",
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                // =================================================
                // SAVE HISTORY FIRST
                // =================================================

                bool saved =
                    SaveToQueueHistory(
                        next,
                        assignedWindow
                    );

                if (!saved)
                {
                    _view.ShowMessage(
                        "The customer was NOT removed from the queue because the served transaction could not be saved to QueueHistory.",
                        "Serve Error",
                        MessageBoxIcon.Error
                    );

                    return;
                }

                // =================================================
                // REMOVE ACTIVE QUEUE
                // =================================================

                try
                {
                    _queueRepo.Remove(
                        next.TicketNumber
                    );
                }
                catch (Exception repoEx)
                {
                    Debug.WriteLine(
                        $"[CashierPresenter] " +
                        $"QueueRepository.Remove failed: {repoEx}"
                    );

                    _view.ShowMessage(
                        $"The transaction was saved to history, " +
                        $"but the active queue could not be removed.\n\n" +
                        $"{repoEx.Message}",
                        "Queue Warning",
                        MessageBoxIcon.Warning
                    );
                }

                _masterQueue.Remove(next);

                // =================================================
                // UPDATE VIEW
                // =================================================

                ApplyFilter();

                var displayEntry =
                    CloneForDisplay(
                        next,
                        assignedWindow
                    );

                try
                {
                    _view.DisplayServedTicket(
                        displayEntry
                    );
                }
                catch (Exception uiEx)
                {
                    Debug.WriteLine(
                        $"[CashierPresenter] " +
                        $"DisplayServedTicket failed: {uiEx}"
                    );
                }

                Debug.WriteLine(
                    $"[CashierPresenter] SERVED " +
                    $"Ticket={next.TicketNumber} " +
                    $"STN={next.ServiceTicketNumber} " +
                    $"Window={assignedWindow}"
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierPresenter] ServeNext failed: {ex}"
                );

                _view.ShowMessage(
                    $"Serve failed:\n\n{ex.Message}",
                    "Serve Error",
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // SAVE TO QUEUE HISTORY
        // =========================================================

        private bool SaveToQueueHistory(
            QueueEntry entry,
            int assignedWindow)
        {
            try
            {
                string historyService =
                    $"Cashier - Window {assignedWindow}";

                using SqlConnection connection =
                    new SqlConnection(
                        DbConfig.ConnectionString
                    );

                connection.Open();

                // =================================================
                // CHECK EXISTING HISTORY
                // =================================================

                string checkQuery = @"
                    SELECT COUNT(*)
                    FROM dbo.QueueHistory
                    WHERE TicketNumber = @TicketNumber;
                ";

                using SqlCommand checkCommand =
                    new SqlCommand(
                        checkQuery,
                        connection
                    );

                checkCommand.Parameters.AddWithValue(
                    "@TicketNumber",
                    entry.TicketNumber
                );

                int existing =
                    Convert.ToInt32(
                        checkCommand.ExecuteScalar()
                    );

                // =================================================
                // UPDATE EXISTING
                // =================================================

                if (existing > 0)
                {
                    string updateQuery = @"
                        UPDATE dbo.QueueHistory
                        SET
                            ServiceTicketNumber = @ServiceTicketNumber,
                            Purpose = @Purpose,
                            Service = @Service,
                            TimeAdded = @TimeAdded,
                            ServedAt = @ServedAt
                        WHERE TicketNumber = @TicketNumber;
                    ";

                    using SqlCommand updateCommand =
                        new SqlCommand(
                            updateQuery,
                            connection
                        );

                    updateCommand.Parameters.AddWithValue(
                        "@TicketNumber",
                        entry.TicketNumber
                    );

                    updateCommand.Parameters.AddWithValue(
                        "@ServiceTicketNumber",
                        entry.ServiceTicketNumber
                    );

                    updateCommand.Parameters.AddWithValue(
                        "@Purpose",
                        entry.Purpose ?? ""
                    );

                    updateCommand.Parameters.AddWithValue(
                        "@Service",
                        historyService
                    );

                    updateCommand.Parameters.AddWithValue(
                        "@TimeAdded",
                        entry.TimeAdded
                    );

                    updateCommand.Parameters.AddWithValue(
                        "@ServedAt",
                        DateTime.Now
                    );

                    return
                        updateCommand.ExecuteNonQuery() > 0;
                }

                // =================================================
                // INSERT NEW HISTORY
                // =================================================

                string insertQuery = @"
                    INSERT INTO dbo.QueueHistory
                    (
                        TicketNumber,
                        ServiceTicketNumber,
                        Purpose,
                        Service,
                        TimeAdded,
                        ServedAt
                    )
                    VALUES
                    (
                        @TicketNumber,
                        @ServiceTicketNumber,
                        @Purpose,
                        @Service,
                        @TimeAdded,
                        @ServedAt
                    );
                ";

                using SqlCommand command =
                    new SqlCommand(
                        insertQuery,
                        connection
                    );

                command.Parameters.AddWithValue(
                    "@TicketNumber",
                    entry.TicketNumber
                );

                command.Parameters.AddWithValue(
                    "@ServiceTicketNumber",
                    entry.ServiceTicketNumber
                );

                command.Parameters.AddWithValue(
                    "@Purpose",
                    entry.Purpose ?? ""
                );

                command.Parameters.AddWithValue(
                    "@Service",
                    historyService
                );

                command.Parameters.AddWithValue(
                    "@TimeAdded",
                    entry.TimeAdded
                );

                command.Parameters.AddWithValue(
                    "@ServedAt",
                    DateTime.Now
                );

                int affected =
                    command.ExecuteNonQuery();

                return affected > 0;
            }
            catch (SqlException sqlEx)
            {
                Debug.WriteLine(
                    $"[CashierPresenter] QueueHistory SQL error: {sqlEx}"
                );

                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierPresenter] QueueHistory error: {ex}"
                );

                return false;
            }
        }

        // =========================================================
        // LOAD QUEUE
        // =========================================================

        private void LoadQueue()
        {
            try
            {
                var list =
                    GetAllQueueSafe();

                _masterQueue.Clear();

                if (list != null)
                {
                    _masterQueue.AddRange(list);
                }

                if (_masterQueue.Any())
                {
                    _nextTicketNumber =
                        _masterQueue.Max(
                            q => q.TicketNumber
                        ) + 1;
                }
                else
                {
                    _nextTicketNumber = 1;
                }

                Debug.WriteLine(
                    $"[CashierPresenter] " +
                    $"LoadQueue = {_masterQueue.Count}"
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierPresenter] LoadQueue failed: {ex}"
                );
            }
        }

        // =========================================================
        // SAFE GET ALL
        // =========================================================

        private List<QueueEntry>? GetAllQueueSafe()
        {
            try
            {
                return _queueRepo.GetAll();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierPresenter] " +
                    $"QueueRepository.GetAll failed: {ex}"
                );

                return new List<QueueEntry>();
            }
        }

        // =========================================================
        // REFRESH
        // =========================================================

        public void RefreshQueueView()
        {
            try
            {
                LoadQueue();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierPresenter] " +
                    $"RefreshQueueView failed: {ex}"
                );
            }
        }

        // =========================================================
        // GET NEXT TICKET
        // =========================================================

        private static int GetNextTicket()
        {
            return
                Interlocked.Increment(
                    ref _nextTicketNumber
                ) - 1;
        }
    }
}