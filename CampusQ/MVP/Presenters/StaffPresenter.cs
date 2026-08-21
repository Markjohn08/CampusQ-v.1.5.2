using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using CampusQ.MVP.Data;
using CampusQ.MVP.Models;
using CampusQ.MVP.Views;

namespace CampusQ.MVP.Presenters
{
    public class StaffPresenter
    {
        private readonly IStaffView _view;

        private readonly List<QueueEntry> _masterQueue = new();

        private readonly QueueRepository _queueRepo;

        private static int _nextTicketNumber = 1;

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public StaffPresenter(IStaffView view)
        {
            _view = view;

            // Make sure database and tables exist
            DbConfig.EnsureDatabaseAndTables();

            _queueRepo =
                new QueueRepository(
                    DbConfig.ConnectionString);

            // Load current active queue
            LoadQueue();

            // Apply current filter
            ApplyFilter();
        }

        // =========================================================
        // ADD TO QUEUE
        // =========================================================

        public void AddToQueue(
            string purpose,
            string service)
        {
            string effectiveService =
                !string.IsNullOrWhiteSpace(service)
                    ? service
                    : (_view.SelectedService ?? "");

            // IMPORTANT:
            // Keep Registrar as Registrar.
            //
            // Do NOT convert:
            // Registrar - W1
            // into:
            // Registrar
            //
            // The Staff dashboard uses the Registrar queue
            // and assigns the pending ticket to W1-W4 based
            // on ServiceTicketNumber.

            string normalizedService =
                NormalizeService(effectiveService);

            var entry =
                new QueueEntry
                {
                    Purpose =
                        string.IsNullOrWhiteSpace(purpose)
                            ? "Unknown"
                            : purpose.Trim(),

                    Service =
                        normalizedService,

                    TimeAdded =
                        DateTime.Now
                };

            // Save to database
            _queueRepo.Add(entry);

            // Add to memory
            _masterQueue.Add(entry);

            // Refresh
            ApplyFilter();
        }

        // =========================================================
        // NORMALIZE SERVICE
        // =========================================================

        private static string NormalizeService(
            string? service)
        {
            if (string.IsNullOrWhiteSpace(service))
                return "Other";

            string s =
                service.Trim();

            string lower =
                s.ToLowerInvariant();

            // =====================================================
            // CASHIER
            // =====================================================

            if (lower.Contains("cashier") ||
                lower.Equals("cash") ||
                lower.StartsWith("cashier "))
            {
                return "Cashier";
            }

            // =====================================================
            // REGISTRAR
            // =====================================================

            if (lower.Contains("registr") ||
                lower.Equals("reg"))
            {
                return "Registrar";
            }

            // =====================================================
            // ADMISSION
            // =====================================================

            if (lower.Contains("admission") ||
                lower.Equals("adm"))
            {
                return "Admission";
            }

            // =====================================================
            // OTHER
            // =====================================================

            if (lower.Equals("other"))
                return "Other";

            return "Other";
        }

        // =========================================================
        // CHECK RC / CREDENTIAL REQUEST
        // =========================================================

        private static bool IsRCRequest(
            QueueEntry? entry)
        {
            if (entry == null)
                return false;

            string label =
                entry.TicketLabel ?? "";

            if (label.StartsWith(
                    "RC",
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            string purpose =
                entry.Purpose ?? "";

            if (purpose.IndexOf(
                    "credential",
                    StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }

            string p =
                purpose.ToLowerInvariant();

            char[] separators =
            {
                ' ',
                '\t',
                '/',
                '\\',
                ',',
                ';',
                '-',
                '_',
                '.',
                '(',
                ')',
                '[',
                ']'
            };

            string[] tokens =
                p.Split(
                    separators,
                    StringSplitOptions.RemoveEmptyEntries);

            return tokens.Any(
                t => t == "rc");
        }

        // =========================================================
        // GET REGISTRAR WINDOW
        // =========================================================
        //
        // We do not need to add a WindowNumber column to Queue.
        //
        // Registrar tickets are assigned using:
        //
        // Ticket 1 -> W1
        // Ticket 2 -> W2
        // Ticket 3 -> W3
        // Ticket 4 -> W4
        // Ticket 5 -> W1
        // ...
        //
        // This also allows QueueHistory to calculate analytics
        // without changing the existing database structure.
        //
        // =========================================================

        private static int GetRegistrarWindow(
            QueueEntry entry)
        {
            int number =
                entry.ServiceTicketNumber;

            if (number <= 0)
            {
                number =
                    entry.TicketNumber;
            }

            if (number <= 0)
                return 1;

            return
                ((number - 1) % 4) + 1;
        }

        // =========================================================
        // GET SELECTED WINDOW
        // =========================================================

        private static int? GetSelectedWindow(
            string? selectedService)
        {
            string selected =
                selectedService?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(selected))
                return null;

            // =====================================================
            // W1
            // =====================================================

            if (Regex.IsMatch(
                    selected,
                    @"\bW\s*1\b",
                    RegexOptions.IgnoreCase))
            {
                return 1;
            }

            // =====================================================
            // W2
            // =====================================================

            if (Regex.IsMatch(
                    selected,
                    @"\bW\s*2\b",
                    RegexOptions.IgnoreCase))
            {
                return 2;
            }

            // =====================================================
            // W3
            // =====================================================

            if (Regex.IsMatch(
                    selected,
                    @"\bW\s*3\b",
                    RegexOptions.IgnoreCase))
            {
                return 3;
            }

            // =====================================================
            // W4
            // =====================================================

            if (Regex.IsMatch(
                    selected,
                    @"\bW\s*4\b",
                    RegexOptions.IgnoreCase))
            {
                return 4;
            }

            return null;
        }

        // =========================================================
        // IS REGISTRAR
        // =========================================================

        private static bool IsRegistrar(
            QueueEntry? entry)
        {
            if (entry == null)
                return false;

            return string.Equals(
                entry.Service,
                "Registrar",
                StringComparison.OrdinalIgnoreCase);
        }

        // =========================================================
        // APPLY FILTER
        // =========================================================

        private void ApplyFilter()
        {
            // =====================================================
            // ONLY REGISTRAR
            // =====================================================

            List<QueueEntry> registrarQueue =
                _masterQueue
                    .Where(IsRegistrar)
                    .OrderBy(q => q.TicketNumber)
                    .ToList();

            IEnumerable<QueueEntry> filtered =
                registrarQueue;

            string selected =
                (_view.SelectedService ?? "")
                .Trim();

            // =====================================================
            // ALL REGISTRAR
            // =====================================================

            if (string.Equals(
                    selected,
                    "All",
                    StringComparison.OrdinalIgnoreCase))
            {
                filtered =
                    registrarQueue;
            }

            // =====================================================
            // REGISTRAR WINDOW
            // =====================================================

            else
            {
                int? window =
                    GetSelectedWindow(selected);

                if (window.HasValue)
                {
                    int selectedWindow =
                        window.Value;

                    filtered =
                        registrarQueue
                            .Where(q =>
                                GetRegistrarWindow(q)
                                == selectedWindow)
                            .OrderBy(q =>
                                q.TicketNumber);
                }
                else
                {
                    // If nothing specific is selected,
                    // show all Registrar queue.
                    filtered =
                        registrarQueue;
                }
            }

            // =====================================================
            // BIND
            // =====================================================

            var view =
                new BindingList<QueueEntry>(
                    filtered.ToList());

            _view.BindQueue(view);
        }

        // =========================================================
        // SERVE NEXT
        // =========================================================

        public void ServeNext()
        {
            string selected =
                _view.SelectedService ??
                "Registrar - W1";

            int? selectedWindow =
                GetSelectedWindow(selected);

            // =====================================================
            // IF A WINDOW IS SELECTED
            // =====================================================

            if (selectedWindow.HasValue)
            {
                ServeNextForWindow(
                    selectedWindow.Value);

                return;
            }

            // =====================================================
            // ALL / DEFAULT
            // =====================================================

            QueueEntry? next =
                _masterQueue
                    .Where(IsRegistrar)
                    .OrderBy(q => q.TicketNumber)
                    .FirstOrDefault();

            if (next == null)
            {
                _view.ShowMessage(
                    "No one is currently in the Registrar queue.",
                    "Queue Empty",
                    MessageBoxIcon.Information);

                return;
            }

            ServeEntry(
                next,
                0);
        }

        // =========================================================
        // SERVE NEXT FOR WINDOW
        // =========================================================

        private void ServeNextForWindow(
            int window)
        {
            // =====================================================
            // CHECK WINDOW STATUS
            // =====================================================

            if (!GetWindowStatus(window))
            {
                _view.ShowMessage(
                    $"Registrar Window {window} is currently OFF.\n\n" +
                    "Turn the window ON before serving the next customer.",
                    "Window Unavailable",
                    MessageBoxIcon.Warning);

                return;
            }

            // =====================================================
            // FIND PENDING TICKET FOR THIS WINDOW
            // =====================================================

            QueueEntry? next =
                _masterQueue
                    .Where(IsRegistrar)
                    .Where(q =>
                        GetRegistrarWindow(q)
                        == window)
                    .OrderBy(q =>
                        q.TicketNumber)
                    .FirstOrDefault();

            // =====================================================
            // EMPTY
            // =====================================================

            if (next == null)
            {
                _view.ShowMessage(
                    $"No pending Registrar ticket for Window {window}.",
                    "Queue Empty",
                    MessageBoxIcon.Information);

                return;
            }

            // =====================================================
            // SERVE
            // =====================================================

            ServeEntry(
                next,
                window);
        }

        // =========================================================
        // SERVE ENTRY
        // =========================================================

        private void ServeEntry(
            QueueEntry next,
            int window)
        {
            try
            {
                // =================================================
                // SAVE TO HISTORY
                // =================================================

                _queueRepo.Remove(
                    next.TicketNumber);

                // =================================================
                // REMOVE FROM MEMORY
                // =================================================

                _masterQueue.Remove(
                    next);

                // =================================================
                // REFRESH
                // =================================================

                ApplyFilter();

                // =================================================
                // MESSAGE
                // =================================================

                string windowText =
                    window > 0
                        ? $"Window: {window}\n"
                        : "";

                _view.ShowMessage(
                    $"Now serving Ticket #{next.ServiceTicketNumber}\n" +
                    $"{windowText}" +
                    $"Service: {next.Service}\n" +
                    $"Purpose: {next.Purpose}",
                    "Serving Next",
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _view.ShowMessage(
                    $"Unable to serve ticket.\n\n{ex.Message}",
                    "Serve Error",
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // LOAD ACTIVE QUEUE
        // =========================================================

        private void LoadQueue()
        {
            try
            {
                List<QueueEntry> list =
                    _queueRepo.GetAll();

                _masterQueue.Clear();

                if (list != null)
                {
                    _masterQueue.AddRange(
                        list);
                }

                // Continue numbering
                _nextTicketNumber =
                    _masterQueue.Any()
                        ? _masterQueue.Max(
                            q => q.TicketNumber) + 1
                        : 1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Failed to load queue: {ex}");
            }
        }

        // =========================================================
        // REFRESH QUEUE
        // =========================================================

        public void RefreshQueueView()
        {
            LoadQueue();

            ApplyFilter();
        }

        // =========================================================
        // TODAY ANALYTICS
        // =========================================================
        //
        // Returns:
        //
        // [0] = W1
        // [1] = W2
        // [2] = W3
        // [3] = W4
        //
        // ONLY TODAY.
        //
        // =========================================================

        public int[] GetWeeklyQueueAnalytics()
        {
            int[] result =
            {
                0,
                0,
                0,
                0
            };

            try
            {
                List<QueuePersistDto> history =
                    _queueRepo.GetHistoryAll();

                DateTime today =
                    DateTime.Today;

                DateTime tomorrow =
                    today.AddDays(1);

                foreach (QueuePersistDto entry in history)
                {
                    DateTime servedDate;

                    if (entry.ServedAt.HasValue)
                    {
                        servedDate =
                            entry.ServedAt.Value;
                    }
                    else
                    {
                        servedDate =
                            entry.TimeAdded;
                    }

                    if (servedDate ==
                        DateTime.MinValue)
                    {
                        continue;
                    }

                    // =================================================
                    // TODAY ONLY
                    // =================================================

                    if (servedDate < today ||
                        servedDate >= tomorrow)
                    {
                        continue;
                    }

                    // =================================================
                    // ONLY REGISTRAR
                    // =================================================

                    if (!string.Equals(
                            entry.Service,
                            "Registrar",
                            StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // =================================================
                    // DETERMINE WINDOW
                    // =================================================

                    int ticketNumber =
                        entry.ServiceTicketNumber;

                    if (ticketNumber <= 0)
                    {
                        ticketNumber =
                            entry.TicketNumber;
                    }

                    if (ticketNumber <= 0)
                        continue;

                    int window =
                        ((ticketNumber - 1) % 4) + 1;

                    if (window >= 1 &&
                        window <= 4)
                    {
                        result[window - 1]++;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Today's Registrar analytics error: {ex}");
            }

            return result;
        }

        // =========================================================
        // TODAY TOTAL
        // =========================================================

        public int GetMonthlyServedCount()
        {
            try
            {
                int[] values =
                    GetWeeklyQueueAnalytics();

                return values.Sum();
            }
            catch
            {
                return 0;
            }
        }

        // =========================================================
        // TOTAL HISTORY
        // =========================================================

        public int GetTotalHistoryCount()
        {
            try
            {
                return
                    _queueRepo
                        .GetHistoryAll()
                        .Count;
            }
            catch
            {
                return 0;
            }
        }

        // =========================================================
        // GET WINDOW STATUS
        // =========================================================

        public bool GetWindowStatus(
            int windowNumber)
        {
            return _queueRepo.GetWindowStatus(
                windowNumber);
        }

        // =========================================================
        // SET WINDOW STATUS
        // =========================================================

        public bool SetWindowStatus(
            int windowNumber,
            bool isActive)
        {
            return _queueRepo.SetWindowStatus(
                windowNumber,
                isActive);
        }

        // =========================================================
        // GET ALL WINDOW STATUSES
        // =========================================================

        public Dictionary<int, bool>
            GetAllWindowStatuses()
        {
            return
                _queueRepo.GetAllWindowStatuses();
        }

        // =========================================================
        // SERVICE WINDOW
        // =========================================================

        public void ServiceWindow()
        {
            Form service =
                new ServiceWindow();

            service.Show();
        }
    }
}