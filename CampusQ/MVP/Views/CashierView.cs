using CampusQ;
using CampusQ.MVP.Data;
using CampusQ.MVP.Models;
using CampusQ.MVP.Presenters;
using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CampusQ.MVP.Views
{
    public partial class CashierView :
        Form,
        ICashierView
    {
        private readonly CashierPresenter _presenter;
        private BindingSource? _bindingSource;

        private bool _isLoading = false;

        // =========================================================
        // WINDOW STATUS
        // =========================================================

        private bool _window1Active = true;
        private bool _window2Active = true;
        private bool _window3Active = true;
        private bool _window4Active = true;

        // =========================================================
        // STAFF NAMES
        // =========================================================

        private const string Window1Staff =
            "Juan Dela Cruz";

        private const string Window2Staff =
            "Maria Santos";

        private const string Window3Staff =
            "Joshua Gonzales";

        private const string Window4Staff =
            "Pedro Reyes";

        // =========================================================
        // COLORS
        // =========================================================

        private static readonly Color Green =
            Color.FromArgb(
                25,
                125,
                60
            );

        private static readonly Color LightGreen =
            Color.FromArgb(
                220,
                245,
                226
            );

        private static readonly Color PanelGreen =
            Color.FromArgb(
                248,
                252,
                249
            );

        private static readonly Color Gray =
            Color.FromArgb(
                130,
                130,
                130
            );

        private static readonly Color DarkGray =
            Color.FromArgb(
                90,
                100,
                95
            );

        private static readonly Color Red =
            Color.FromArgb(
                210,
                45,
                45
            );

        private static readonly Color LightRed =
            Color.FromArgb(
                245,
                225,
                225
            );

        private static readonly Color ClosedPanel =
            Color.FromArgb(
                248,
                248,
                248
            );

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public CashierView()
        {
            InitializeComponent();
            ApplyRoundedButtons();

            // Configure DataGridView after Designer initialization.
            ConfigureQueueGrid();

            // Presenter
            _presenter =
                new CashierPresenter(this);

            // =====================================================
            // EVENTS
            // =====================================================

            _cmbService.SelectedIndexChanged +=
                CmbService_SelectedIndexChanged;

            _btnServeNext.Click +=
                BtnServeNext_Click;

            _btnRefresh.Click +=
                BtnRefresh_Click;

            _btnServiceWindow.Click +=
                BtnServiceWindow_Click;

            btnWindow1.Click +=
                BtnWindow1_Click;

            btnWindow2.Click +=
                BtnWindow2_Click;

            btnWindow3.Click +=
                BtnWindow3_Click;

            btnWindow4.Click +=
                BtnWindow4_Click;

            // Initial window status
            UpdateAllWindowStatusUI();
        }

        // =========================================================
        // CONFIGURE DATA GRID
        // =========================================================

        private void ConfigureQueueGrid()
        {
            if (_dgvQueue == null)
                return;

            _dgvQueue.AllowUserToAddRows =
                false;

            _dgvQueue.AllowUserToDeleteRows =
                false;

            _dgvQueue.AllowUserToResizeRows =
                false;

            // IMPORTANT:
            // QueueEntry properties generate columns automatically.
            _dgvQueue.AutoGenerateColumns =
                true;

            _dgvQueue.BackgroundColor =
                Color.White;

            _dgvQueue.BorderStyle =
                BorderStyle.None;

            _dgvQueue.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            _dgvQueue.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;

            _dgvQueue.EnableHeadersVisualStyles =
                false;

            // Very light grid
            _dgvQueue.GridColor =
                Color.FromArgb(
                    232,
                    238,
                    234
                );

            _dgvQueue.MultiSelect =
                false;

            _dgvQueue.ReadOnly =
                true;

            _dgvQueue.RowHeadersVisible =
                false;

            _dgvQueue.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            _dgvQueue.ShowCellErrors =
                false;

            _dgvQueue.ShowCellToolTips =
                false;

            _dgvQueue.ShowEditingIcon =
                false;

            _dgvQueue.ShowRowErrors =
                false;

            // =====================================================
            // NORMAL CELL
            // =====================================================

            _dgvQueue.DefaultCellStyle.BackColor =
                Color.White;

            _dgvQueue.DefaultCellStyle.ForeColor =
                Color.FromArgb(
                    40,
                    40,
                    40
                );

            _dgvQueue.DefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9F
                );

            _dgvQueue.DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // =====================================================
            // LIGHT GREEN SELECTION
            // =====================================================

            _dgvQueue.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(
                    220,
                    245,
                    226
                );

            _dgvQueue.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(
                    0,
                    92,
                    48
                );

            // =====================================================
            // HEADER
            // =====================================================

            _dgvQueue.ColumnHeadersDefaultCellStyle.BackColor =
                Color.White;

            _dgvQueue.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(
                    25,
                    35,
                    30
                );

            _dgvQueue.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold
                );

            _dgvQueue.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.White;

            _dgvQueue.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.FromArgb(
                    25,
                    35,
                    30
                );

            _dgvQueue.ColumnHeadersHeight =
                42;

            _dgvQueue.RowTemplate.Height =
                40;
        }

        // =========================================================
        // WINDOW 1
        // =========================================================

        private void BtnWindow1_Click(
            object? sender,
            EventArgs e)
        {
            SetWindowStatus(
                1,
                !_window1Active
            );
        }

        // =========================================================
        // WINDOW 2
        // =========================================================

        private void BtnWindow2_Click(
            object? sender,
            EventArgs e)
        {
            SetWindowStatus(
                2,
                !_window2Active
            );
        }

        // =========================================================
        // WINDOW 3
        // =========================================================

        private void BtnWindow3_Click(
            object? sender,
            EventArgs e)
        {
            SetWindowStatus(
                3,
                !_window3Active
            );
        }

        // =========================================================
        // WINDOW 4
        // =========================================================

        private void BtnWindow4_Click(
            object? sender,
            EventArgs e)
        {
            SetWindowStatus(
                4,
                !_window4Active
            );
        }

        // =========================================================
        // GET STAFF NAME
        // =========================================================

        private static string GetWindowStaffName(
            int windowNumber)
        {
            return windowNumber switch
            {
                1 => Window1Staff,
                2 => Window2Staff,
                3 => Window3Staff,
                4 => Window4Staff,
                _ => "Unassigned"
            };
        }

        // =========================================================
        // SET WINDOW STATUS
        // =========================================================

        private void SetWindowStatus(
            int windowNumber,
            bool isActive)
        {
            switch (windowNumber)
            {
                case 1:
                    _window1Active =
                        isActive;
                    break;

                case 2:
                    _window2Active =
                        isActive;
                    break;

                case 3:
                    _window3Active =
                        isActive;
                    break;

                case 4:
                    _window4Active =
                        isActive;
                    break;
            }

            UpdateWindowStatusUI(
                windowNumber,
                isActive
            );
        }

        // =========================================================
        // GET WINDOW STATUS
        // =========================================================

        public bool IsWindowActive(
            int windowNumber)
        {
            return windowNumber switch
            {
                1 => _window1Active,
                2 => _window2Active,
                3 => _window3Active,
                4 => _window4Active,
                _ => false
            };
        }

        // =========================================================
        // UPDATE WINDOW STATUS UI
        // =========================================================

        private void UpdateWindowStatusUI(
            int windowNumber,
            bool isActive)
        {
            Label? windowLabel = null;
            Label? stateLabel = null;
            Label? descriptionLabel = null;
            Button? button = null;
            Panel? panel = null;

            switch (windowNumber)
            {
                case 1:
                    windowLabel =
                        lblWindow1;

                    stateLabel =
                        lblWindow1State;

                    descriptionLabel =
                        lblWindow1Description;

                    button =
                        btnWindow1;

                    panel =
                        pnlWindow1;

                    break;

                case 2:
                    windowLabel =
                        lblWindow2;

                    stateLabel =
                        lblWindow2State;

                    descriptionLabel =
                        lblWindow2Description;

                    button =
                        btnWindow2;

                    panel =
                        pnlWindow2;

                    break;

                case 3:
                    windowLabel =
                        lblWindow3;

                    stateLabel =
                        lblWindow3State;

                    descriptionLabel =
                        lblWindow3Description;

                    button =
                        btnWindow3;

                    panel =
                        pnlWindow3;

                    break;

                case 4:
                    windowLabel =
                        lblWindow4;

                    stateLabel =
                        lblWindow4State;

                    descriptionLabel =
                        lblWindow4Description;

                    button =
                        btnWindow4;

                    panel =
                        pnlWindow4;

                    break;
            }

            if (
                windowLabel == null ||
                stateLabel == null ||
                descriptionLabel == null ||
                button == null ||
                panel == null
            )
            {
                return;
            }

            // =====================================================
            // STAFF NAME
            // =====================================================

            string staffName =
                isActive
                    ? GetWindowStaffName(
                        windowNumber)
                    : "Unassigned";

            // =====================================================
            // STAFF LABEL
            // =====================================================

            windowLabel.AutoSize =
                false;

            windowLabel.Font =
                new Font(
                    "Segoe UI",
                    9.5F,
                    FontStyle.Bold
                );

            windowLabel.TextAlign =
                ContentAlignment.MiddleLeft;

            // =====================================================
            // STATE LABEL
            // =====================================================

            stateLabel.AutoSize =
                false;

            stateLabel.Font =
                new Font(
                    "Segoe UI",
                    8.25F,
                    FontStyle.Regular
                );

            stateLabel.TextAlign =
                ContentAlignment.MiddleLeft;

            // =====================================================
            // DESCRIPTION
            // =====================================================

            descriptionLabel.AutoSize =
                false;

            descriptionLabel.Font =
                new Font(
                    "Segoe UI",
                    8.25F,
                    FontStyle.Regular
                );

            descriptionLabel.TextAlign =
                ContentAlignment.MiddleLeft;

            descriptionLabel.UseCompatibleTextRendering =
                true;

            // =====================================================
            // ACTIVE
            // =====================================================

            if (isActive)
            {
                windowLabel.Text =
                    $"●  {staffName}";

                windowLabel.ForeColor =
                    Green;

                stateLabel.Text =
                    $"WINDOW {windowNumber}    OPEN";

                stateLabel.ForeColor =
                    Green;

                descriptionLabel.Text =
                    "Available for transaction";

                descriptionLabel.ForeColor =
                    DarkGray;

                button.Text =
                    "ON";

                button.BackColor =
                    LightGreen;

                button.ForeColor =
                    Green;

                button.Font =
                    new Font(
                        "Segoe UI",
                        8.5F,
                        FontStyle.Bold
                    );

                panel.BackColor =
                    PanelGreen;
            }

            // =====================================================
            // CLOSED
            // =====================================================

            else
            {
                windowLabel.Text =
                    "●  Unassigned";

                windowLabel.ForeColor =
                    Red;

                stateLabel.Text =
                    $"WINDOW {windowNumber}    CLOSED";

                stateLabel.ForeColor =
                    Red;

                descriptionLabel.Text =
                    "Temporarily unavailable";

                descriptionLabel.ForeColor =
                    Gray;

                button.Text =
                    "OFF";

                button.BackColor =
                    LightRed;

                button.ForeColor =
                    Red;

                button.Font =
                    new Font(
                        "Segoe UI",
                        8.5F,
                        FontStyle.Bold
                    );

                panel.BackColor =
                    ClosedPanel;
            }

            // =====================================================
            // BUTTON
            // =====================================================

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderSize =
                0;

            button.Cursor =
                Cursors.Hand;

            // =====================================================
            // PADDING
            // =====================================================

            windowLabel.Padding =
                new Padding(0);

            stateLabel.Padding =
                new Padding(0);

            descriptionLabel.Padding =
                new Padding(0);

            // =====================================================
            // REFRESH
            // =====================================================

            panel.Invalidate();
            windowLabel.Invalidate();
            stateLabel.Invalidate();
            descriptionLabel.Invalidate();
            button.Invalidate();
        }

        // =========================================================
        // UPDATE ALL WINDOWS
        // =========================================================

        private void UpdateAllWindowStatusUI()
        {
            UpdateWindowStatusUI(
                1,
                _window1Active
            );

            UpdateWindowStatusUI(
                2,
                _window2Active
            );

            UpdateWindowStatusUI(
                3,
                _window3Active
            );

            UpdateWindowStatusUI(
                4,
                _window4Active
            );
        }

        // =========================================================
        // SERVICE SELECTION
        // =========================================================

        private void CmbService_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            if (_isLoading)
                return;

            try
            {
                _presenter.RefreshQueueView();

                LoadQueueAnalytics();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierView] Service selection error: {ex}"
                );

                ShowMessage(
                    $"Failed to refresh queue:\n\n{ex.Message}",
                    "Error",
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // SERVE NEXT
        // =========================================================

        private void BtnServeNext_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                _presenter.ServeNext();

                LoadQueueAnalytics();

                if (
                    _dgvQueue != null &&
                    !_dgvQueue.IsDisposed
                )
                {
                    _dgvQueue.Refresh();
                }

                if (
                    formsPlot1 != null &&
                    !formsPlot1.IsDisposed
                )
                {
                    formsPlot1.Refresh();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierView] Serve failed: {ex}"
                );

                ShowMessage(
                    $"Serve failed:\n\n{ex.Message}",
                    "Error",
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // REFRESH
        // =========================================================

        private void BtnRefresh_Click(
            object? sender,
            EventArgs e)
        {
            RefreshCashierData();
        }

        private void RefreshCashierData()
        {
            try
            {
                _presenter.RefreshQueueView();

                LoadQueueAnalytics();

                if (
                    _dgvQueue != null &&
                    !_dgvQueue.IsDisposed
                )
                {
                    _dgvQueue.Refresh();
                }

                if (
                    formsPlot1 != null &&
                    !formsPlot1.IsDisposed
                )
                {
                    formsPlot1.Refresh();
                }

                UpdateAllWindowStatusUI();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierView] Refresh failed: {ex}"
                );

                ShowMessage(
                    $"Refresh failed:\n\n{ex.Message}",
                    "Refresh Error",
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // SERVICE WINDOW
        // =========================================================

        private void BtnServiceWindow_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                var svc =
                    new CashierWindows();

                svc.DisplayFromDataGrid(
                    _dgvQueue
                );

                DataGridViewBindingCompleteEventHandler
                    handler = null!;

                handler =
                    (sender2, args) =>
                    {
                        try
                        {
                            if (!svc.IsDisposed)
                            {
                                svc.DisplayFromDataGrid(
                                    _dgvQueue
                                );
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(
                                "[CashierView] " +
                                $"CashierWindows update failed: {ex.Message}"
                            );
                        }
                    };

                _dgvQueue.DataBindingComplete +=
                    handler;

                svc.FormClosed +=
                    (o, args) =>
                    {
                        _dgvQueue.DataBindingComplete -=
                            handler;
                    };

                svc.Show(this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierView] Service window failed: {ex}"
                );

                ShowMessage(
                    $"Failed to open service window:\n\n{ex.Message}",
                    "Error",
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // CURRENT QUEUE
        // =========================================================

        public BindingList<QueueEntry>? CurrentQueue
        {
            get;
            private set;
        }

        // =========================================================
        // QUEUE CHANGED
        // =========================================================

        public event EventHandler? QueueChanged;

        // =========================================================
        // BIND QUEUE
        // =========================================================

        public void BindQueue(
            BindingList<QueueEntry> view)
        {
            if (view == null)
                return;

            // =====================================================
            // FORM SAFETY
            // =====================================================

            if (
                IsDisposed ||
                Disposing
            )
            {
                return;
            }

            // =====================================================
            // IMPORTANT:
            // Don't Invoke before the form handle exists.
            // =====================================================

            if (!IsHandleCreated)
            {
                Debug.WriteLine(
                    "[CashierView] BindQueue skipped because handle is not created."
                );

                return;
            }

            // =====================================================
            // UI THREAD
            // =====================================================

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(
                        new Action(
                            () =>
                                BindQueue(view)
                        )
                    );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(
                        $"[CashierView] BindQueue invoke failed: {ex.Message}"
                    );
                }

                return;
            }

            // =====================================================
            // BIND DATA
            // =====================================================

            try
            {
                if (_bindingSource == null)
                {
                    _bindingSource =
                        new BindingSource();

                    _dgvQueue.DataSource =
                        _bindingSource;
                }

                // IMPORTANT:
                // Assign BindingList to BindingSource.
                _bindingSource.DataSource =
                    view;

                // Store current queue.
                CurrentQueue =
                    view;

                // =================================================
                // AUTO GENERATE COLUMNS
                // =================================================

                _dgvQueue.AutoGenerateColumns =
                    true;

                // =================================================
                // SERVE BUTTON
                // =================================================

                _btnServeNext.Enabled =
                    view.Count > 0;

                // =================================================
                // REFRESH
                // =================================================

                _dgvQueue.Refresh();

                // =================================================
                // QUEUE EVENT
                // =================================================

                QueueChanged?.Invoke(
                    this,
                    EventArgs.Empty
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierView] BindQueue failed: {ex}"
                );

                // No MessageBox here.
                // Prevents repeated Queue Error popups.
            }
        }

        // =========================================================
        // SELECTED SERVICE
        // =========================================================

        public string SelectedService
        {
            get
            {
                if (
                    IsDisposed ||
                    Disposing
                )
                {
                    return "All";
                }

                if (!IsHandleCreated)
                {
                    return
                        _cmbService.SelectedItem?
                            .ToString()
                        ?? "All";
                }

                if (InvokeRequired)
                {
                    try
                    {
                        return (string)Invoke(
                            new Func<string>(
                                () =>
                                    SelectedService
                            )
                        );
                    }
                    catch
                    {
                        return "All";
                    }
                }

                return
                    _cmbService.SelectedItem?
                        .ToString()
                    ?? "All";
            }
        }

        // =========================================================
        // DISPLAY SERVED TICKET
        // =========================================================

        public void DisplayServedTicket(
            QueueEntry entry)
        {
            if (entry == null)
                return;

            ShowMessage(
                $"Now serving {entry.TicketLabel}\n\n" +
                $"Service: {entry.Service}\n" +
                $"Purpose: {entry.Purpose}",
                "Serving Next",
                MessageBoxIcon.Information
            );
        }

        // =========================================================
        // SHOW MESSAGE
        // =========================================================

        public void ShowMessage(
            string text,
            string caption,
            MessageBoxIcon icon)
        {
            if (
                IsDisposed ||
                Disposing
            )
            {
                return;
            }

            if (!IsHandleCreated)
            {
                Debug.WriteLine(
                    $"[CashierView] {caption}: {text}"
                );

                return;
            }

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(
                        new Action(
                            () =>
                                ShowMessage(
                                    text,
                                    caption,
                                    icon
                                )
                        )
                    );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(
                        $"[CashierView] ShowMessage failed: {ex.Message}"
                    );
                }

                return;
            }

            MessageBox.Show(
                this,
                text,
                caption,
                MessageBoxButtons.OK,
                icon
            );
        }

        // =========================================================
        // SET SELECTED SERVICE
        // =========================================================

        public void SetSelectedService(
            string service)
        {
            if (
                string.IsNullOrWhiteSpace(
                    service
                )
            )
            {
                return;
            }

            if (
                IsDisposed ||
                Disposing
            )
            {
                return;
            }

            if (!IsHandleCreated)
                return;

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(
                        new Action(
                            () =>
                                SetSelectedService(
                                    service
                                )
                        )
                    );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(
                        $"[CashierView] SetSelectedService failed: {ex.Message}"
                    );
                }

                return;
            }

            int index =
                _cmbService.FindStringExact(
                    service
                );

            if (index >= 0)
            {
                _cmbService.SelectedIndex =
                    index;
            }
            else
            {
                _cmbService.Text =
                    service;
            }
        }

        // =========================================================
        // REFRESH QUEUE VIEW
        // =========================================================

        public void RefreshQueueView()
        {
            if (
                IsDisposed ||
                Disposing
            )
            {
                return;
            }

            if (!IsHandleCreated)
            {
                Debug.WriteLine(
                    "[CashierView] RefreshQueueView skipped because handle is not ready."
                );

                return;
            }

            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(
                        new Action(
                            () =>
                                RefreshQueueView()
                        )
                    );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(
                        $"[CashierView] RefreshQueueView invoke failed: {ex.Message}"
                    );
                }

                return;
            }

            try
            {
                _presenter.RefreshQueueView();

                LoadQueueAnalytics();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierView] RefreshQueueView failed: {ex}"
                );

                ShowMessage(
                    $"Failed to refresh queue:\n\n{ex.Message}",
                    "Refresh Error",
                    MessageBoxIcon.Error
                );
            }
        }

        // =========================================================
        // QUEUE ANALYTICS
        // =========================================================

        private void LoadQueueAnalytics()
        {
            try
            {
                if (
                    formsPlot1 == null ||
                    formsPlot1.IsDisposed
                )
                {
                    return;
                }

                int window1 = 0;
                int window2 = 0;
                int window3 = 0;
                int window4 = 0;

                // =================================================
                // DATABASE CONNECTION
                // =================================================

                using SqlConnection connection =
                    new SqlConnection(
                        DbConfig.ConnectionString
                    );

                connection.Open();

                string query = @"
                    SELECT
                        Service,
                        TicketNumber,
                        ServiceTicketNumber
                    FROM dbo.QueueHistory
                    ORDER BY ServedAt ASC;
                ";

                using SqlCommand command =
                    new SqlCommand(
                        query,
                        connection
                    );

                using SqlDataReader reader =
                    command.ExecuteReader();

                // =================================================
                // READ HISTORY
                // =================================================

                while (reader.Read())
                {
                    string service =
                        reader["Service"]?
                            .ToString()
                        ?? "";

                    int ticketNumber =
                        0;

                    int serviceTicketNumber =
                        0;

                    if (
                        reader["TicketNumber"] !=
                        DBNull.Value
                    )
                    {
                        int.TryParse(
                            reader["TicketNumber"]
                                .ToString(),
                            out ticketNumber
                        );
                    }

                    if (
                        reader["ServiceTicketNumber"] !=
                        DBNull.Value
                    )
                    {
                        int.TryParse(
                            reader["ServiceTicketNumber"]
                                .ToString(),
                            out serviceTicketNumber
                        );
                    }

                    int window =
                        GetHistoryWindow(
                            service,
                            ticketNumber,
                            serviceTicketNumber
                        );

                    switch (window)
                    {
                        case 1:
                            window1++;
                            break;

                        case 2:
                            window2++;
                            break;

                        case 3:
                            window3++;
                            break;

                        case 4:
                            window4++;
                            break;
                    }
                }

                // =================================================
                // CLEAR GRAPH
                // =================================================

                formsPlot1.Plot.Clear();

                // =================================================
                // VALUES
                // =================================================

                double[] values =
                {
                    window1,
                    window2,
                    window3,
                    window4
                };

                // =================================================
                // ADD BARS
                // =================================================

                var bars =
                    formsPlot1.Plot.Add.Bars(
                        values
                    );

                // =================================================
                // BAR SETTINGS
                // =================================================

                for (
                    int i = 0;
                    i < bars.Bars.Count;
                    i++
                )
                {
                    bars.Bars[i].Position =
                        i;

                    bars.Bars[i].Size =
                        0.60;

                    // GREEN
                    bars.Bars[i].FillColor =
                        ScottPlot.Colors.Green;

                    bars.Bars[i].LineColor =
                        ScottPlot.Colors.Green;

                    // Remove bar outline
                    bars.Bars[i].LineWidth =
                        0;
                }

                // =================================================
                // X LIMIT
                // =================================================

                formsPlot1.Plot.Axes.SetLimitsX(
                    -0.5,
                    3.5
                );

                // =================================================
                // Y LIMIT
                // FIXED 0 - 100
                // =================================================

                formsPlot1.Plot.Axes.SetLimitsY(
                    0,
                    100
                );

                // =================================================
                // Y AXIS TICKS
                // 0, 20, 40, 60, 80, 100
                // =================================================

                formsPlot1.Plot.Axes.Left.TickGenerator =
                    new ScottPlot.TickGenerators
                        .NumericFixedInterval(
                            20
                        );

                // =================================================
                // X AXIS TICKS
                // =================================================

                formsPlot1.Plot.Axes.Bottom.TickGenerator =
                    new ScottPlot.TickGenerators
                        .NumericManual(
                            new double[]
                            {
                                0,
                                1,
                                2,
                                3
                            },
                            new string[]
                            {
                                "W1",
                                "W2",
                                "W3",
                                "W4"
                            }
                        );

                // =================================================
                // TITLE
                // =================================================

                formsPlot1.Plot.Title(
                    "Customers Served"
                );

                // =================================================
                // X LABEL
                // =================================================

                formsPlot1.Plot.XLabel(
                    "Service Window"
                );

                // =================================================
                // REMOVE Y LABEL
                // =================================================

                formsPlot1.Plot.YLabel(
                    ""
                );

                // =================================================
                // REMOVE GRAPH GRID
                // =================================================

                formsPlot1.Plot.HideGrid();

                // =================================================
                // REFRESH
                // =================================================

                formsPlot1.Refresh();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierView] Chart error: {ex}"
                );
            }
        }

        // =========================================================
        // HISTORY WINDOW
        // =========================================================

        private static int GetHistoryWindow(
            string service,
            int ticketNumber,
            int serviceTicketNumber)
        {
            string normalized =
                (service ?? "")
                    .Trim()
                    .ToLowerInvariant();

            // =====================================================
            // WINDOW 1-4
            // =====================================================

            Match match =
                Regex.Match(
                    normalized,
                    @"window\s*([1-4])",
                    RegexOptions.IgnoreCase
                );

            if (
                match.Success &&
                int.TryParse(
                    match.Groups[1].Value,
                    out int explicitWindow
                )
            )
            {
                return explicitWindow;
            }

            // =====================================================
            // W1-W4
            // =====================================================

            match =
                Regex.Match(
                    normalized,
                    @"\bw\s*([1-4])\b",
                    RegexOptions.IgnoreCase
                );

            if (
                match.Success &&
                int.TryParse(
                    match.Groups[1].Value,
                    out int w
                )
            )
            {
                return w;
            }

            // =====================================================
            // CASHIER
            // =====================================================

            if (
                normalized.Contains(
                    "cashier"
                ) ||
                normalized == "cashier"
            )
            {
                int sequence =
                    serviceTicketNumber > 0
                        ? serviceTicketNumber
                        : ticketNumber;

                if (sequence <= 0)
                {
                    sequence = 1;
                }

                return
                    ((sequence - 1) % 4) + 1;
            }

            return 0;
        }

        // =========================================================
        // FORM LOAD
        // =========================================================

        private void CashierView_Load(
            object? sender,
            EventArgs e)
        {
            try
            {
                _isLoading =
                    true;

                // =================================================
                // DEFAULT WINDOW STATUS
                // =================================================

                _window1Active =
                    true;

                _window2Active =
                    true;

                _window3Active =
                    true;

                _window4Active =
                    true;

                UpdateAllWindowStatusUI();

                // =================================================
                // DEFAULT SERVICE
                // =================================================

                if (
                    _cmbService.Items.Count > 0
                )
                {
                    _cmbService.SelectedIndex =
                        0;
                }

                // =================================================
                // LOAD QUEUE
                // =================================================

                _presenter.RefreshQueueView();

                // =================================================
                // LOAD ANALYTICS
                // =================================================

                LoadQueueAnalytics();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"[CashierView] Initial load failed: {ex}"
                );

                ShowMessage(
                    $"Failed to load queue:\n\n{ex.Message}",
                    "Cashier Error",
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                _isLoading =
                    false;

                // Restore window UI.
                UpdateAllWindowStatusUI();
            }
        }
    }
}