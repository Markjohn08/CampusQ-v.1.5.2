using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

using CampusQ.MVP.Data;
using CampusQ.MVP.Models;
using CampusQ.MVP.Views;

namespace CampusQ.MVP.Presenters
{
    public class AdmissionPresenter
    {
        private readonly IAdmissionView _view;
        private readonly List<QueueEntry> _masterQueue = new();
        private readonly QueueRepository _queueRepo;

        private static int _nextTicketNumber = 1;

        // =========================================================
        // ADMISSION HAS ONLY 2 WINDOWS
        // =========================================================

        private const int WindowCount = 2;

        public AdmissionPresenter(IAdmissionView view)
        {
            _view =
                view ?? throw new ArgumentNullException(nameof(view));

            DbConfig.EnsureDatabaseAndTables();

            _queueRepo =
                new QueueRepository(
                    DbConfig.ConnectionString);

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
            string effectiveService =
                !string.IsNullOrWhiteSpace(service)
                    ? service
                    : (_view.SelectedService ?? "");

            QueueEntry entry =
                new QueueEntry
                {
                    TicketNumber =
                        GetNextTicket(),

                    Purpose =
                        string.IsNullOrWhiteSpace(purpose)
                            ? "Unknown"
                            : purpose.Trim(),

                    Service =
                        NormalizeService(
                            effectiveService),

                    TimeAdded =
                        DateTime.Now
                };

            try
            {
                _queueRepo.Add(entry);

                _masterQueue.Add(entry);

                ApplyFilter();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "[AdmissionPresenter] AddToQueue failed: "
                    + ex);

                _view.ShowMessage(
                    "Failed to add customer to the admission queue.\n\n"
                    + ex.Message,
                    "Queue Error",
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // NORMALIZE SERVICE
        // =========================================================

        private static string NormalizeService(
            string? service)
        {
            if (string.IsNullOrWhiteSpace(service))
            {
                return "Admission";
            }

            string value =
                service.Trim();

            if (value.Equals(
                "All",
                StringComparison.OrdinalIgnoreCase))
            {
                return "Admission";
            }

            if (value.IndexOf(
                "admission",
                StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return "Admission";
            }

            return value;
        }

        // =========================================================
        // CHECK IF ADMISSION
        // =========================================================

        private static bool IsAdmissionService(
            string? service)
        {
            if (string.IsNullOrWhiteSpace(service))
            {
                return false;
            }

            string value =
                service.Trim();

            if (value.Equals(
                "Registrar",
                StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (value.Equals(
                "Cashier",
                StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (value.Equals(
                "Other",
                StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return value.IndexOf(
                "admission",
                StringComparison.OrdinalIgnoreCase) >= 0;
        }

        // =========================================================
        // GET WINDOW STATUS
        // =========================================================

        private bool IsWindowActive(
            int window)
        {
            if (window < 1 ||
                window > WindowCount)
            {
                return false;
            }

            try
            {
                return _queueRepo.GetWindowStatus(
                    window);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "[AdmissionPresenter] " +
                    $"GetWindowStatus({window}) failed: {ex}");

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
                window++)
            {
                if (IsWindowActive(window))
                {
                    activeWindows.Add(window);
                }
            }

            return activeWindows;
        }

        // =========================================================
        // GET WINDOW FROM TICKET SEQUENCE
        //
        // Used ONLY when both windows are active.
        //
        // W1 ON + W2 ON:
        // Ticket 1 -> W1
        // Ticket 2 -> W2
        // Ticket 3 -> W1
        // Ticket 4 -> W2
        //
        // When a window is OFF, assignment is handled by
        // GetAvailableWindowForEntry().
        // =========================================================

        private static int GetAssignedWindow(
            int sequence)
        {
            if (sequence <= 0)
            {
                sequence = 1;
            }

            return
                ((sequence - 1) % WindowCount) + 1;
        }

        // =========================================================
        // GET ASSIGNED WINDOW FOR QUEUE ENTRY
        //
        // IMPORTANT:
        //
        // This now considers Active Status.
        //
        // W1 ON / W2 ON
        //     -> normal alternating assignment
        //
        // W1 OFF / W2 ON
        //     -> W2
        //
        // W1 ON / W2 OFF
        //     -> W1
        //
        // W1 OFF / W2 OFF
        //     -> 0
        // =========================================================

        private int GetAssignedWindowForEntry(
            QueueEntry entry)
        {
            if (entry == null)
            {
                return 0;
            }

            List<int> activeWindows =
                GetActiveWindows();

            if (activeWindows.Count == 0)
            {
                return 0;
            }

            // -----------------------------------------------------
            // ONLY ONE WINDOW ACTIVE
            // -----------------------------------------------------

            if (activeWindows.Count == 1)
            {
                return activeWindows[0];
            }

            // -----------------------------------------------------
            // BOTH WINDOWS ACTIVE
            // -----------------------------------------------------

            int sequence;

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
        // GET SORTING SEQUENCE
        // =========================================================

        private static int GetSequenceForEntry(
            QueueEntry entry)
        {
            if (entry == null)
            {
                return int.MaxValue;
            }

            if (entry.ServiceTicketNumber > 0)
            {
                return entry.ServiceTicketNumber;
            }

            if (entry.TicketNumber > 0)
            {
                return entry.TicketNumber;
            }

            return int.MaxValue;
        }

        // =========================================================
        // CREATE DISPLAY COPY
        // =========================================================

        private static QueueEntry CloneForDisplay(
            QueueEntry source,
            int assignedWindow)
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
                    assignedWindow > 0
                        ? "Admission - Window "
                            + assignedWindow
                        : "Admission",

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
                List<QueueEntry> admissionEntries =
                    _masterQueue
                        .Where(q =>
                            IsAdmissionService(q.Service))
                        .OrderBy(
                            GetSequenceForEntry)
                        .ToList();

                string selected =
                    (_view.SelectedService ?? "")
                        .Trim();

                Debug.WriteLine(
                    "[AdmissionPresenter] ApplyFilter"
                    + " | Selected="
                    + selected
                    + " | MasterCount="
                    + _masterQueue.Count
                    + " | AdmissionCount="
                    + admissionEntries.Count);

                int? selectedWindow =
                    GetSelectedWindow(
                        selected);

                IEnumerable<QueueEntry> display;

                // =================================================
                // WINDOW SELECTED
                // =================================================

                if (selectedWindow.HasValue)
                {
                    int window =
                        selectedWindow.Value;

                    // If selected window is OFF,
                    // show an empty queue for that window.
                    if (!IsWindowActive(window))
                    {
                        display =
                            Enumerable.Empty<QueueEntry>();
                    }
                    else
                    {
                        display =
                            admissionEntries
                                .Where(e =>
                                    GetAssignedWindowForEntry(e)
                                    == window)
                                .Select(e =>
                                    CloneForDisplay(
                                        e,
                                        window));
                    }
                }

                // =================================================
                // ALL
                // =================================================

                else
                {
                    display =
                        admissionEntries
                            .Select(e =>
                            {
                                int window =
                                    GetAssignedWindowForEntry(e);

                                return new
                                {
                                    Entry = e,
                                    Window = window
                                };
                            })
                            .Where(x =>
                                x.Window > 0)
                            .Select(x =>
                                CloneForDisplay(
                                    x.Entry,
                                    x.Window))
                            .OrderBy(
                                GetSequenceForEntry);
                }

                BindingList<QueueEntry> view =
                    new BindingList<QueueEntry>(
                        display.ToList());

                _view.BindQueue(
                    view);

                Debug.WriteLine(
                    "[AdmissionPresenter] DisplayCount="
                    + view.Count);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "[AdmissionPresenter] ApplyFilter failed: "
                    + ex);

                _view.ShowMessage(
                    "Failed to refresh the admission queue.\n\n"
                    + ex.Message,
                    "Queue Error",
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // GET SELECTED WINDOW
        //
        // Supports:
        // Window1
        // Window2
        // W1
        // W2
        // =========================================================

        private static int? GetSelectedWindow(
            string selected)
        {
            if (string.IsNullOrWhiteSpace(selected))
            {
                return null;
            }

            if (selected.Equals(
                "All",
                StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            bool containsWindow =
                selected.IndexOf(
                    "Window",
                    StringComparison.OrdinalIgnoreCase) >= 0;

            bool containsW =
                Regex.IsMatch(
                    selected,
                    @"\bW\s*\d",
                    RegexOptions.IgnoreCase);

            if (!containsWindow &&
                !containsW)
            {
                return null;
            }

            Match match =
                Regex.Match(
                    selected,
                    @"\d+");

            if (!match.Success)
            {
                return null;
            }

            if (!int.TryParse(
                match.Value,
                out int window))
            {
                return null;
            }

            if (window < 1 ||
                window > WindowCount)
            {
                return null;
            }

            return window;
        }

        // =========================================================
        // GET NEXT AVAILABLE WINDOW FOR SERVING
        //
        // This is used by ServeNext().
        //
        // If a specific window is selected:
        //     selected window must be ON.
        //
        // If All:
        //     both ON  -> normal assignment
        //     W1 only  -> W1
        //     W2 only  -> W2
        // =========================================================

        private int GetServingWindow(
            QueueEntry entry,
            int? selectedWindow)
        {
            // -----------------------------------------------------
            // SPECIFIC WINDOW SELECTED
            // -----------------------------------------------------

            if (selectedWindow.HasValue)
            {
                int selected =
                    selectedWindow.Value;

                if (!IsWindowActive(selected))
                {
                    return 0;
                }

                return selected;
            }

            // -----------------------------------------------------
            // ALL WINDOWS
            // -----------------------------------------------------

            return GetAssignedWindowForEntry(
                entry);
        }

        // =========================================================
        // SERVE NEXT
        // =========================================================

        public void ServeNext()
        {
            try
            {
                // =================================================
                // REFRESH DATABASE QUEUE
                // =================================================

                LoadQueue();

                string selected =
                    (_view.SelectedService ?? "")
                        .Trim();

                List<QueueEntry> admissionEntries =
                    _masterQueue
                        .Where(q =>
                            IsAdmissionService(q.Service))
                        .OrderBy(
                            GetSequenceForEntry)
                        .ToList();

                if (admissionEntries.Count == 0)
                {
                    _view.ShowMessage(
                        "No one is currently in the admission queue.",
                        "Queue Empty",
                        MessageBoxIcon.Information);

                    return;
                }

                int? selectedWindow =
                    GetSelectedWindow(
                        selected);

                QueueEntry? next =
                    null;

                int servingWindow =
                    0;

                // =================================================
                // SPECIFIC WINDOW SELECTED
                // =================================================

                if (selectedWindow.HasValue)
                {
                    int window =
                        selectedWindow.Value;

                    if (!IsWindowActive(window))
                    {
                        _view.ShowMessage(
                            $"Window {window} is currently OFF.\n\n"
                            + "Please turn the window ON before serving.",
                            "Window Unavailable",
                            MessageBoxIcon.Warning);

                        return;
                    }

                    next =
                        admissionEntries
                            .FirstOrDefault(
                                e =>
                                    GetAssignedWindowForEntry(e)
                                    == window);

                    // -------------------------------------------------
                    // IMPORTANT:
                    //
                    // If only this window is active, all tickets belong
                    // to this window.
                    // -------------------------------------------------

                    if (next == null)
                    {
                        List<int> activeWindows =
                            GetActiveWindows();

                        if (
                            activeWindows.Count == 1 &&
                            activeWindows[0] == window)
                        {
                            next =
                                admissionEntries
                                    .FirstOrDefault();
                        }
                    }

                    servingWindow =
                        window;

                    Debug.WriteLine(
                        "[AdmissionPresenter] "
                        + "Selected Window = "
                        + window);
                }

                // =================================================
                // ALL WINDOWS
                // =================================================

                else
                {
                    next =
                        admissionEntries
                            .FirstOrDefault();

                    if (next != null)
                    {
                        servingWindow =
                            GetServingWindow(
                                next,
                                null);
                    }
                }

                // =================================================
                // NO ACTIVE WINDOW
                // =================================================

                if (servingWindow <= 0)
                {
                    _view.ShowMessage(
                        "All admission service windows are currently OFF.\n\n"
                        + "Please turn on at least one window before serving.",
                        "No Available Window",
                        MessageBoxIcon.Warning);

                    return;
                }

                // =================================================
                // NO QUEUE FOR SELECTED WINDOW
                // =================================================

                if (next == null)
                {
                    _view.ShowMessage(
                        "No one is currently in the selected admission window.",
                        "Queue Empty",
                        MessageBoxIcon.Information);

                    return;
                }

                // =================================================
                // REMOVE FROM DATABASE
                //
                // IMPORTANT:
                // Save the ACTUAL window.
                // =================================================

                _queueRepo.Remove(
                    next.TicketNumber,
                    servingWindow);

                // =================================================
                // REMOVE FROM LOCAL QUEUE
                // =================================================

                _masterQueue.Remove(
                    next);

                // =================================================
                // CREATE SERVED DISPLAY COPY
                // =================================================

                QueueEntry servedEntry =
                    CloneForDisplay(
                        next,
                        servingWindow);

                // =================================================
                // REFRESH DISPLAY
                // =================================================

                ApplyFilter();

                // =================================================
                // INFORM VIEW
                // =================================================

                _view.DisplayServedTicket(
                    servedEntry);

                Debug.WriteLine(
                    "[AdmissionPresenter] "
                    + "Served Ticket = "
                    + next.TicketLabel
                    + " | Window = "
                    + servingWindow);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "[AdmissionPresenter] ServeNext failed: "
                    + ex);

                _view.ShowMessage(
                    "Failed to serve the next customer.\n\n"
                    + ex.Message,
                    "Serve Error",
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // GET NEXT TICKET
        // =========================================================

        private static int GetNextTicket()
        {
            return
                Interlocked.Increment(
                    ref _nextTicketNumber) - 1;
        }

        // =========================================================
        // LOAD QUEUE FROM DATABASE
        // =========================================================

        private void LoadQueue()
        {
            try
            {
                List<QueueEntry>? list =
                    _queueRepo.GetAll();

                _masterQueue.Clear();

                if (list != null)
                {
                    _masterQueue.AddRange(
                        list);
                }

                if (_masterQueue.Count > 0)
                {
                    int maxTicket =
                        _masterQueue
                            .Max(
                                q =>
                                    q.TicketNumber);

                    _nextTicketNumber =
                        maxTicket + 1;
                }
                else
                {
                    _nextTicketNumber =
                        1;
                }

                Debug.WriteLine(
                    "[AdmissionPresenter] "
                    + "Loaded "
                    + _masterQueue.Count
                    + " queue records.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "[AdmissionPresenter] LoadQueue failed: "
                    + ex);

                _view.ShowMessage(
                    "Failed to load the admission queue from the database.\n\n"
                    + ex.Message,
                    "Database Error",
                    MessageBoxIcon.Error);
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
        // BIND QUEUE
        // =========================================================

        public void BindQueue(
            BindingList<QueueEntry> queue)
        {
            _view.BindQueue(
                queue);
        }

        // =========================================================
        // SET SELECTED SERVICE
        // =========================================================

        public void SetSelectedService(
            string service)
        {
            _view.SetSelectedService(
                service);
        }
    }
}