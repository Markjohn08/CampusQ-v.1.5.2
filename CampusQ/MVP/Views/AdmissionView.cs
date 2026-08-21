using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using CampusQ.MVP.Data;
using CampusQ.MVP.Models;
using CampusQ.MVP.Presenters;

namespace CampusQ.MVP.Views
{
    public partial class AdmissionView : Form, IAdmissionView
    {
        // =========================================================
        // PRESENTER
        // =========================================================

        private readonly AdmissionPresenter _presenter;

        // =========================================================
        // REPOSITORY
        // =========================================================

        private readonly QueueRepository _queueRepo;

        // =========================================================
        // BINDING
        // =========================================================

        private BindingSource? _bindingSource;

        private BindingList<QueueEntry>? _currentQueue;

        // =========================================================
        // ADMISSION STAFF NAMES
        // =========================================================

        private const string Window1Staff =
            "Alyssa Marie Santos";

        private const string Window2Staff =
            "Kevin John Mendoza";

        // =========================================================
        // WINDOW STATUS
        // =========================================================

        private bool _window1Active = true;
        private bool _window2Active = true;

        // =========================================================
        // COLORS
        // =========================================================

        private static readonly Color Green =
            Color.FromArgb(0, 105, 55);

        private static readonly Color DarkGreen =
            Color.FromArgb(0, 92, 48);

        private static readonly Color LightGreen =
            Color.FromArgb(220, 245, 226);

        private static readonly Color Red =
            Color.FromArgb(205, 45, 45);

        private static readonly Color LightRed =
            Color.FromArgb(250, 225, 225);

        private static readonly Color Gray =
            Color.FromArgb(90, 100, 95);

        // =========================================================
        // IADMISSIONVIEW
        // =========================================================

        public BindingList<QueueEntry>? CurrentQueue
        {
            get
            {
                return _currentQueue;
            }
        }

        public event EventHandler? QueueChanged;

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public AdmissionView()
        {
            InitializeComponent();

            // =====================================================
            // DATABASE
            // =====================================================

            DbConfig.EnsureDatabaseAndTables();

            _queueRepo =
                new QueueRepository(
                    DbConfig.ConnectionString);

            // =====================================================
            // PRESENTER
            // =====================================================

            _presenter =
                new AdmissionPresenter(this);

            // =====================================================
            // CONFIGURE UI
            // =====================================================

            ConfigureDataGrid();

            ConfigureButtons();

            ConfigureActiveStatus();

            // =====================================================
            // LOAD SAVED WINDOW STATUS
            // =====================================================

            LoadWindowStatuses();

            // =====================================================
            // EVENTS
            // =====================================================

            _cmbService.SelectedIndexChanged +=
                CmbService_SelectedIndexChanged;

            _btnRefresh.Click +=
                BtnRefresh_Click;

            _btnServeNext.Click +=
                BtnServeNext_Click;

            _btnServiceWindow.Click +=
                BtnServiceWindow_Click;

            btnWindow1.Click +=
                BtnWindow1_Click;

            btnWindow2.Click +=
                BtnWindow2_Click;

            // =====================================================
            // FORM RESIZE
            // =====================================================

            Resize +=
                AdmissionView_Resize;

            // =====================================================
            // ANALYTICS
            // =====================================================

            LoadQueueAnalytics();

            RemoveButtonFocus();
        }

        // =========================================================
        // FORM LOAD
        // =========================================================

        private void AdmissionView_Load(
            object sender,
            EventArgs e)
        {
            try
            {
                LoadWindowStatuses();

                _presenter.RefreshQueueView();

                LoadQueueAnalytics();

                ApplyAllRoundedCorners();

                RemoveButtonFocus();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Failed to load Admission Dashboard.\n\n" +
                    ex.Message,
                    "Admission Error",
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // DATA GRID
        // =========================================================

        private void ConfigureDataGrid()
        {
            if (_dgvQueue == null)
                return;

            // No manual timeCol.
            _dgvQueue.AutoGenerateColumns = true;

            _dgvQueue.AllowUserToAddRows =
                false;

            _dgvQueue.AllowUserToDeleteRows =
                false;

            _dgvQueue.AllowUserToResizeRows =
                false;

            _dgvQueue.ReadOnly =
                true;

            _dgvQueue.RowHeadersVisible =
                false;

            _dgvQueue.MultiSelect =
                false;

            _dgvQueue.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            _dgvQueue.EnableHeadersVisualStyles =
                false;

            _dgvQueue.BackgroundColor =
                Color.White;

            _dgvQueue.BorderStyle =
                BorderStyle.None;

            _dgvQueue.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            _dgvQueue.GridColor =
                Color.FromArgb(
                    230,
                    235,
                    232);

            _dgvQueue.ColumnHeadersHeight =
                40;

            _dgvQueue.RowTemplate.Height =
                38;

            // =====================================================
            // HEADER
            // =====================================================

            _dgvQueue.ColumnHeadersDefaultCellStyle =
                new DataGridViewCellStyle
                {
                    BackColor =
                        Color.White,

                    ForeColor =
                        Color.FromArgb(
                            25,
                            35,
                            30),

                    Font =
                        new Font(
                            "Segoe UI",
                            8.5F,
                            FontStyle.Bold),

                    Alignment =
                        DataGridViewContentAlignment.MiddleLeft,

                    SelectionBackColor =
                        Color.White,

                    SelectionForeColor =
                        Color.FromArgb(
                            25,
                            35,
                            30)
                };

            // =====================================================
            // CELLS
            // =====================================================

            _dgvQueue.DefaultCellStyle =
                new DataGridViewCellStyle
                {
                    BackColor =
                        Color.White,

                    ForeColor =
                        Color.FromArgb(
                            45,
                            55,
                            50),

                    Font =
                        new Font(
                            "Segoe UI",
                            8.5F),

                    SelectionBackColor =
                        LightGreen,

                    SelectionForeColor =
                        DarkGreen,

                    Padding =
                        new Padding(
                            5,
                            0,
                            3,
                            0)
                };

            // =====================================================
            // ALTERNATING ROWS
            // =====================================================

            _dgvQueue.AlternatingRowsDefaultCellStyle =
                new DataGridViewCellStyle
                {
                    BackColor =
                        Color.FromArgb(
                            252,
                            253,
                            252),

                    ForeColor =
                        Color.FromArgb(
                            45,
                            55,
                            50),

                    SelectionBackColor =
                        LightGreen,

                    SelectionForeColor =
                        DarkGreen
                };
        }

        // =========================================================
        // BUTTON CONFIGURATION
        // =========================================================

        private void ConfigureButtons()
        {
            ConfigureMainButton(
                _btnRefresh,
                true);

            ConfigureMainButton(
                _btnServeNext,
                true);

            ConfigureMainButton(
                _btnServiceWindow,
                false);

            ConfigureStatusButton(
                btnWindow1);

            ConfigureStatusButton(
                btnWindow2);

            ApplyAllRoundedCorners();
        }

        // =========================================================
        // ACTIVE STATUS CONFIGURATION
        // =========================================================

        private void ConfigureActiveStatus()
        {
            ConfigureStatusButton(
                btnWindow1);

            ConfigureStatusButton(
                btnWindow2);

            UpdateWindow1Status();

            UpdateWindow2Status();

            RemoveButtonFocusStyle(
                btnWindow1);

            RemoveButtonFocusStyle(
                btnWindow2);

            ApplyAllRoundedCorners();
        }

        // =========================================================
        // MAIN BUTTON STYLE
        // =========================================================

        private void ConfigureMainButton(
            Button button,
            bool filled)
        {
            button.FlatStyle =
                FlatStyle.Flat;

            button.Font =
                new Font(
                    "Segoe UI",
                    9F,
                    FontStyle.Bold);

            button.Cursor =
                Cursors.Hand;

            button.TabStop =
                false;

            button.UseVisualStyleBackColor =
                false;

            // No outline
            button.FlatAppearance.BorderSize =
                0;

            if (filled)
            {
                button.BackColor =
                    Green;

                button.ForeColor =
                    Color.White;

                button.FlatAppearance.MouseOverBackColor =
                    Green;

                button.FlatAppearance.MouseDownBackColor =
                    Green;
            }
            else
            {
                button.BackColor =
                    Color.White;

                button.ForeColor =
                    DarkGreen;

                button.FlatAppearance.MouseOverBackColor =
                    Color.White;

                button.FlatAppearance.MouseDownBackColor =
                    Color.White;
            }

            ApplyRoundedCorners(
                button,
                10);
        }

        // =========================================================
        // STATUS BUTTON STYLE
        // =========================================================

        private void ConfigureStatusButton(
            Button button)
        {
            button.FlatStyle =
                FlatStyle.Flat;

            // IMPORTANT:
            // No border/focus outline.
            button.FlatAppearance.BorderSize =
                0;

            button.UseVisualStyleBackColor =
                false;

            button.TabStop =
                false;

            button.Cursor =
                Cursors.Hand;

            button.FlatAppearance.MouseOverBackColor =
                button.BackColor;

            button.FlatAppearance.MouseDownBackColor =
                button.BackColor;

            button.FlatAppearance.CheckedBackColor =
                button.BackColor;

            ApplyRoundedCorners(
                button,
                8);
        }

        // =========================================================
        // ROUND ALL BUTTONS
        // =========================================================

        private void ApplyAllRoundedCorners()
        {
            if (btnWindow1 != null)
            {
                ApplyRoundedCorners(
                    btnWindow1,
                    8);
            }

            if (btnWindow2 != null)
            {
                ApplyRoundedCorners(
                    btnWindow2,
                    8);
            }

            if (_btnRefresh != null)
            {
                ApplyRoundedCorners(
                    _btnRefresh,
                    10);
            }

            if (_btnServeNext != null)
            {
                ApplyRoundedCorners(
                    _btnServeNext,
                    10);
            }

            if (_btnServiceWindow != null)
            {
                ApplyRoundedCorners(
                    _btnServiceWindow,
                    10);
            }
        }

        // =========================================================
        // ROUNDED CORNERS
        // =========================================================

        private void ApplyRoundedCorners(
            Button button,
            int radius)
        {
            if (button == null)
                return;

            if (button.Width <= 0 ||
                button.Height <= 0)
                return;

            int diameter =
                radius * 2;

            if (diameter >
                button.Width)
            {
                diameter =
                    button.Width;
            }

            if (diameter >
                button.Height)
            {
                diameter =
                    button.Height;
            }

            Rectangle rect =
                new Rectangle(
                    0,
                    0,
                    button.Width,
                    button.Height);

            using GraphicsPath path =
                new GraphicsPath();

            path.StartFigure();

            path.AddArc(
                rect.X,
                rect.Y,
                diameter,
                diameter,
                180,
                90);

            path.AddArc(
                rect.Right - diameter,
                rect.Y,
                diameter,
                diameter,
                270,
                90);

            path.AddArc(
                rect.Right - diameter,
                rect.Bottom - diameter,
                diameter,
                diameter,
                0,
                90);

            path.AddArc(
                rect.X,
                rect.Bottom - diameter,
                diameter,
                diameter,
                90,
                90);

            path.CloseFigure();

            button.Region =
                new Region(path);
        }

        // =========================================================
        // FORM RESIZE
        // =========================================================

        private void AdmissionView_Resize(
            object? sender,
            EventArgs e)
        {
            ApplyAllRoundedCorners();
        }

        // =========================================================
        // SERVICE FILTER
        // =========================================================

        private void CmbService_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            try
            {
                _presenter.RefreshQueueView();

                LoadQueueAnalytics();

                RemoveButtonFocus();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Failed to filter the queue.\n\n" +
                    ex.Message,
                    "Filter Error",
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // REFRESH
        // =========================================================

        private void BtnRefresh_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                LoadWindowStatuses();

                _presenter.RefreshQueueView();

                LoadQueueAnalytics();

                RemoveButtonFocus();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Failed to refresh the queue.\n\n" +
                    ex.Message,
                    "Refresh Error",
                    MessageBoxIcon.Error);
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

                _presenter.RefreshQueueView();

                LoadQueueAnalytics();

                RemoveButtonFocus();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Failed to serve the next customer.\n\n" +
                    ex.Message,
                    "Serve Error",
                    MessageBoxIcon.Error);
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
                var serviceWindow =
                    new AdmissionWindows();

                serviceWindow.Show();

                RemoveButtonFocus();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Failed to open Service Window.\n\n" +
                    ex.Message,
                    "Service Window Error",
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // LOAD WINDOW STATUS
        // =========================================================

        private void LoadWindowStatuses()
        {
            try
            {
                _window1Active =
                    _queueRepo.GetWindowStatus(1);

                _window2Active =
                    _queueRepo.GetWindowStatus(2);

                UpdateWindow1Status();

                UpdateWindow2Status();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "LoadWindowStatuses error: " +
                    ex.Message);

                // Default
                _window1Active =
                    true;

                _window2Active =
                    true;

                UpdateWindow1Status();

                UpdateWindow2Status();
            }
        }

        // =========================================================
        // WINDOW 1 BUTTON
        // =========================================================

        private void BtnWindow1_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                bool newStatus =
                    !_window1Active;

                bool success =
                    _queueRepo.SetWindowStatus(
                        1,
                        newStatus);

                if (!success)
                {
                    ShowMessage(
                        "Unable to update Window 1 status.",
                        "Status Error",
                        MessageBoxIcon.Error);

                    return;
                }

                _window1Active =
                    newStatus;

                UpdateWindow1Status();

                _presenter.RefreshQueueView();

                LoadQueueAnalytics();

                RemoveButtonFocus();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Failed to update Window 1 status.\n\n" +
                    ex.Message,
                    "Status Error",
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // WINDOW 2 BUTTON
        // =========================================================

        private void BtnWindow2_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                bool newStatus =
                    !_window2Active;

                bool success =
                    _queueRepo.SetWindowStatus(
                        2,
                        newStatus);

                if (!success)
                {
                    ShowMessage(
                        "Unable to update Window 2 status.",
                        "Status Error",
                        MessageBoxIcon.Error);

                    return;
                }

                _window2Active =
                    newStatus;

                UpdateWindow2Status();

                _presenter.RefreshQueueView();

                LoadQueueAnalytics();

                RemoveButtonFocus();
            }
            catch (Exception ex)
            {
                ShowMessage(
                    "Failed to update Window 2 status.\n\n" +
                    ex.Message,
                    "Status Error",
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // WINDOW 1 UI
        // =========================================================

        private void UpdateWindow1Status()
        {
            if (_window1Active)
            {
                // =================================================
                // ON
                // =================================================

                lblStatusDot1.ForeColor =
                    Green;

                lblWindow1Name.Text =
                    Window1Staff;

                lblWindow1Name.ForeColor =
                    DarkGreen;

                lblWindow1Info.Text =
                    "WINDOW 1   OPEN";

                lblWindow1Info.ForeColor =
                    Green;

                lblWindow1Description.Text =
                    "Available for transaction";

                lblWindow1Description.ForeColor =
                    Gray;

                btnWindow1.Text =
                    "ON";

                btnWindow1.BackColor =
                    LightGreen;

                btnWindow1.ForeColor =
                    Green;
            }
            else
            {
                // =================================================
                // OFF
                // =================================================

                lblStatusDot1.ForeColor =
                    Red;

                lblWindow1Name.Text =
                    "Unassigned";

                lblWindow1Name.ForeColor =
                    Red;

                lblWindow1Info.Text =
                    "WINDOW 1   CLOSED";

                lblWindow1Info.ForeColor =
                    Red;

                lblWindow1Description.Text =
                    "Temporarily unavailable";

                lblWindow1Description.ForeColor =
                    Color.FromArgb(
                        165,
                        90,
                        90);

                btnWindow1.Text =
                    "OFF";

                btnWindow1.BackColor =
                    LightRed;

                btnWindow1.ForeColor =
                    Red;
            }

            // Keep rounded shape after color change.
            ConfigureStatusButton(
                btnWindow1);

            RemoveButtonFocusStyle(
                btnWindow1);
        }

        // =========================================================
        // WINDOW 2 UI
        // =========================================================

        private void UpdateWindow2Status()
        {
            if (_window2Active)
            {
                // =================================================
                // ON
                // =================================================

                lblStatusDot2.ForeColor =
                    Green;

                lblWindow2Name.Text =
                    Window2Staff;

                lblWindow2Name.ForeColor =
                    DarkGreen;

                lblWindow2Info.Text =
                    "WINDOW 2   OPEN";

                lblWindow2Info.ForeColor =
                    Green;

                lblWindow2Description.Text =
                    "Available for transaction";

                lblWindow2Description.ForeColor =
                    Gray;

                btnWindow2.Text =
                    "ON";

                btnWindow2.BackColor =
                    LightGreen;

                btnWindow2.ForeColor =
                    Green;
            }
            else
            {
                // =================================================
                // OFF
                // =================================================

                lblStatusDot2.ForeColor =
                    Red;

                lblWindow2Name.Text =
                    "Unassigned";

                lblWindow2Name.ForeColor =
                    Red;

                lblWindow2Info.Text =
                    "WINDOW 2   CLOSED";

                lblWindow2Info.ForeColor =
                    Red;

                lblWindow2Description.Text =
                    "Temporarily unavailable";

                lblWindow2Description.ForeColor =
                    Color.FromArgb(
                        165,
                        90,
                        90);

                btnWindow2.Text =
                    "OFF";

                btnWindow2.BackColor =
                    LightRed;

                btnWindow2.ForeColor =
                    Red;
            }

            // Keep rounded shape after color change.
            ConfigureStatusButton(
                btnWindow2);

            RemoveButtonFocusStyle(
                btnWindow2);
        }

        // =========================================================
        // REMOVE BUTTON OUTLINE
        // =========================================================

        private void RemoveButtonFocusStyle(
            Button button)
        {
            button.FlatStyle =
                FlatStyle.Flat;

            // NO OUTLINE
            button.FlatAppearance.BorderSize =
                0;

            button.UseVisualStyleBackColor =
                false;

            button.TabStop =
                false;

            button.FlatAppearance.MouseOverBackColor =
                button.BackColor;

            button.FlatAppearance.MouseDownBackColor =
                button.BackColor;

            button.FlatAppearance.CheckedBackColor =
                button.BackColor;

            // Re-apply curve.
            if (button == btnWindow1 ||
                button == btnWindow2)
            {
                ApplyRoundedCorners(
                    button,
                    8);
            }
            else
            {
                ApplyRoundedCorners(
                    button,
                    10);
            }
        }

        // =========================================================
        // REMOVE FOCUS
        // =========================================================

        private void RemoveButtonFocus()
        {
            try
            {
                ActiveControl = null;

                if (_dgvQueue != null)
                {
                    _dgvQueue.ClearSelection();
                }
            }
            catch
            {
                // Ignore.
            }
        }

        // =========================================================
        // QUEUE ANALYTICS
        // =========================================================

        private void LoadQueueAnalytics()
        {
            try
            {
                if (_formsPlotAnalytics == null)
                    return;

                var counts =
                    _queueRepo
                        .GetTodayAdmissionServedByWindow();

                int window1 =
                    counts.TryGetValue(
                        1,
                        out int w1)
                        ? w1
                        : 0;

                int window2 =
                    counts.TryGetValue(
                        2,
                        out int w2)
                        ? w2
                        : 0;

                double[] values =
                {
                    window1,
                    window2
                };

                // =================================================
                // CLEAR
                // =================================================

                _formsPlotAnalytics.Plot.Clear();

                // =================================================
                // GREEN BARS
                // =================================================

                var bars =
                    _formsPlotAnalytics.Plot.Add.Bars(
                        values);

                bars.Color =
                    ScottPlot.Color.FromColor(
                        Green);

                // =================================================
                // X AXIS
                // =================================================

                _formsPlotAnalytics.Plot
                    .Axes
                    .Bottom
                    .SetTicks(
                        new double[]
                        {
                            0,
                            1
                        },
                        new string[]
                        {
                            "W1",
                            "W2"
                        });

                // =================================================
                // Y AXIS
                // =================================================

                _formsPlotAnalytics.Plot
                    .Axes
                    .Left
                    .SetTicks(
                        new double[]
                        {
                            0,
                            20,
                            40,
                            60,
                            80,
                            100
                        },
                        new string[]
                        {
                            "0",
                            "20",
                            "40",
                            "60",
                            "80",
                            "100"
                        });

                // =================================================
                // NO GRID LINES
                // =================================================

                _formsPlotAnalytics.Plot
                    .Grid
                    .MajorLineWidth =
                    0;

                _formsPlotAnalytics.Plot
                    .Grid
                    .MinorLineWidth =
                    0;

                // =================================================
                // NO TICK MARKS
                // =================================================

                _formsPlotAnalytics.Plot
                    .Axes
                    .Left
                    .MajorTickStyle
                    .Length =
                    0;

                _formsPlotAnalytics.Plot
                    .Axes
                    .Bottom
                    .MajorTickStyle
                    .Length =
                    0;

                // =================================================
                // LIMITS
                // =================================================

                _formsPlotAnalytics.Plot
                    .Axes
                    .SetLimits(
                        -0.5,
                        1.5,
                        0,
                        100);

                // =================================================
                // REMOVE LEFT "CUSTOMERS" LABEL
                // =================================================

                _formsPlotAnalytics.Plot
                    .Axes
                    .Left
                    .Label
                    .Text =
                    "";

                // =================================================
                // X AXIS LABEL
                // =================================================

                _formsPlotAnalytics.Plot
                    .Axes
                    .Bottom
                    .Label
                    .Text =
                    "Service Window";

                // =================================================
                // REFRESH
                // =================================================

                _formsPlotAnalytics.Refresh();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "LoadQueueAnalytics error: " +
                    ex.Message);
            }
        }

        // =========================================================
        // BIND QUEUE
        // =========================================================

        public void BindQueue(
            BindingList<QueueEntry> view)
        {
            if (InvokeRequired)
            {
                Invoke(
                    new Action(
                        () =>
                            BindQueue(view)));

                return;
            }

            try
            {
                // =================================================
                // CURRENT QUEUE
                // =================================================

                _currentQueue =
                    view;

                // =================================================
                // BINDING SOURCE
                // =================================================

                if (_bindingSource == null)
                {
                    _bindingSource =
                        new BindingSource();

                    _dgvQueue.DataSource =
                        _bindingSource;
                }

                _bindingSource.DataSource =
                    view;

                // =================================================
                // TOTAL
                // =================================================

                if (lblTotal != null)
                {
                    lblTotal.Text =
                        $"Total: {view.Count}";
                }

                // =================================================
                // CLEAR SELECTION
                // =================================================

                if (_dgvQueue.Rows.Count > 0)
                {
                    _dgvQueue.ClearSelection();
                }

                // =================================================
                // EVENT
                // =================================================

                QueueChanged?.Invoke(
                    this,
                    EventArgs.Empty);

                // =================================================
                // ANALYTICS
                // =================================================

                LoadQueueAnalytics();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "BindQueue error: " +
                    ex.Message);
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

            try
            {
                Debug.WriteLine(
                    "Admission served ticket: " +
                    entry.TicketLabel);

                QueueChanged?.Invoke(
                    this,
                    EventArgs.Empty);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    "DisplayServedTicket error: " +
                    ex.Message);
            }
        }

        // =========================================================
        // SELECTED SERVICE
        // =========================================================

        public string SelectedService
        {
            get
            {
                return
                    _cmbService
                        ?.SelectedItem?
                        .ToString()
                    ?? "All";
            }
        }

        // =========================================================
        // SET SELECTED SERVICE
        // =========================================================

        public void SetSelectedService(
            string service)
        {
            if (_cmbService == null)
                return;

            if (string.IsNullOrWhiteSpace(service))
            {
                service =
                    "All";
            }

            _cmbService.SelectedItem =
                service;
        }

        // =========================================================
        // REFRESH QUEUE VIEW
        // =========================================================

        public void RefreshQueueView()
        {
            _presenter.RefreshQueueView();

            LoadQueueAnalytics();
        }

        // =========================================================
        // SHOW MESSAGE
        // =========================================================

        public void ShowMessage(
            string text,
            string caption,
            MessageBoxIcon icon)
        {
            if (InvokeRequired)
            {
                Invoke(
                    new Action(
                        () =>
                            ShowMessage(
                                text,
                                caption,
                                icon)));

                return;
            }

            MessageBox.Show(
                this,
                text,
                caption,
                MessageBoxButtons.OK,
                icon);
        }

        // =========================================================
        // DATAGRID CLICK
        // =========================================================

        private void _dgvQueue_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            // Intentionally empty.
        }

        // =========================================================
        // FORM CLOSING
        // =========================================================

        protected override void OnFormClosing(
            FormClosingEventArgs e)
        {
            try
            {
                if (_bindingSource != null)
                {
                    _bindingSource.Dispose();

                    _bindingSource =
                        null;
                }
            }
            catch
            {
                // Ignore cleanup errors.
            }

            base.OnFormClosing(e);
        }
    }
}