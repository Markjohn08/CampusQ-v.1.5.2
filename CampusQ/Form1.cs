using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows.Forms;
using CampusQ.MVP.Views;
using CampusQ.MVP.Presenters;
using System.Drawing;
using System;
using CampusQ.MVP.Data;
using CampusQ.MVP.Models;
using System.Drawing.Printing;
using System.Collections.Generic;
using QRCoder;

namespace CampusQ
{
    public partial class Form1 : Form, IMainView
    {
        private Staff staff;
        private readonly MainPresenter _presenter;

        // Tracks the currently selected purpose button/value for the active department panel.
        private PictureBox? _selectedPurposeButton;
        private string? _selectedPurpose;

        private static readonly Color PurposeDefaultColor = Color.FromArgb(41, 128, 185);
        private static readonly Color PurposeSelectedColor = Color.FromArgb(39, 174, 96);
        private readonly Dictionary<PictureBox, Point> _officeButtonSelectedLocations = new();
        private readonly Dictionary<PictureBox, Point> _officeButtonHomeLocations = new();

        private Label? _lblCashierNow;
        private Label? _lblCashierWait;
        private Label? _lblRegistrarNow;
        private Label? _lblRegistrarWait;
        private Label? _lblAdmissionNow;
        private Label? _lblAdmissionWait;

        private System.Threading.Timer? _queueStatusTimer;
        private System.Threading.Timer? _panelRefreshTimer;
        private const int AverageMinutesPerTicket = 5;

        public Form1()
        {
            InitializeComponent();
            _presenter = new MainPresenter(this);

            _officeButtonHomeLocations[btn_cashier] = btn_cashier.Location;
            _officeButtonHomeLocations[btn_registrar] = btn_registrar.Location;
            _officeButtonHomeLocations[btn_admission] = btn_admission.Location;

            // Initialize default selected locations
            _officeButtonSelectedLocations[btn_cashier] = new Point(35, 466);
            _officeButtonSelectedLocations[btn_registrar] = new Point(35, 475);
            _officeButtonSelectedLocations[btn_admission] = new Point(35, 473);

            CreateQueueStatusLabels();

            _queueStatusTimer = new System.Threading.Timer(
                _ => RefreshQueueStatus(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(3));

            FormClosed += (s, e) =>
            {
                _queueStatusTimer?.Dispose();
                _queueStatusTimer = null;
            };
        }

        public void SetOfficeButtonSelectedLocations(
            Point cashierSelectedLocation,
            Point registrarSelectedLocation,
            Point admissionSelectedLocation)
        {
            _officeButtonSelectedLocations[btn_cashier] = cashierSelectedLocation;
            _officeButtonSelectedLocations[btn_registrar] = registrarSelectedLocation;
            _officeButtonSelectedLocations[btn_admission] = admissionSelectedLocation;
        }

        private void CreateQueueStatusLabels()
        {
            var font = new Font(
                "Segoe UI",
                24F,
                FontStyle.Bold,
                GraphicsUnit.Point);

            (_lblRegistrarNow, _lblRegistrarWait) =
                CreateOfficeStatusLabelPair(
                    new Point(1110, 170),
                    new Point(1610, 170),
                    font);

            (_lblCashierNow, _lblCashierWait) =
                CreateOfficeStatusLabelPair(
                    new Point(1110, 305),
                    new Point(1610, 305),
                    font);

            (_lblAdmissionNow, _lblAdmissionWait) =
                CreateOfficeStatusLabelPair(
                    new Point(1110, 425),
                    new Point(1610, 425),
                    font);
        }

        private (Label now, Label wait) CreateOfficeStatusLabelPair(
            Point nowLocation,
            Point waitLocation,
            Font font)
        {
            var nowLabel = new Label
            {
                AutoSize = true,
                Location = nowLocation,
                Font = font,
                ForeColor = Color.DarkSeaGreen,
                BackColor = Color.White,
                Padding = new Padding(6, 3, 6, 3),
                Text = "",
            };

            var waitLabel = new Label
            {
                AutoSize = true,
                Location = waitLocation,
                Font = font,
                ForeColor = Color.DarkSeaGreen,
                BackColor = Color.White,
                Padding = new Padding(6, 3, 6, 3),
                Text = "",
            };

            this.Controls.Add(nowLabel);
            this.Controls.Add(waitLabel);

            nowLabel.BringToFront();
            waitLabel.BringToFront();

            return (nowLabel, waitLabel);
        }

        // Refreshes the "Now Serving" / "Est. Wait" labels
        // for window 1 of each office.
        private void RefreshQueueStatus()
        {
            try
            {
                DbConfig.EnsureDatabaseAndTables();

                var repo = new QueueRepository(
                    DbConfig.ConnectionString);

                var all =
                    repo.GetAll() ??
                    new List<QueueEntry>();

                var cashierWindow1 = all
                    .Where(q =>
                        string.Equals(
                            q.Service,
                            "Cashier",
                            StringComparison.OrdinalIgnoreCase))
                    .Where(q =>
                        GetCashierWindowIndex(q) == 1)
                    .OrderBy(q => q.TicketNumber)
                    .ToList();

                var registrarAll = all
                    .Where(q =>
                        string.Equals(
                            q.Service,
                            "Registrar",
                            StringComparison.OrdinalIgnoreCase))
                    .OrderBy(q => q.TicketNumber)
                    .ToList();

                var registrarWindow1 =
                    GetRegistrarWindow1Queue(registrarAll);

                var admissionQueue = all
                    .Where(q =>
                        string.Equals(
                            q.Service,
                            "Admission",
                            StringComparison.OrdinalIgnoreCase))
                    .OrderBy(q => q.TicketNumber)
                    .ToList();

                if (IsHandleCreated && !IsDisposed)
                {
                    BeginInvoke(new Action(() =>
                    {
                        UpdateOfficeStatusLabels(
                            _lblCashierNow,
                            _lblCashierWait,
                            cashierWindow1);

                        UpdateOfficeStatusLabels(
                            _lblRegistrarNow,
                            _lblRegistrarWait,
                            registrarWindow1);

                        UpdateOfficeStatusLabels(
                            _lblAdmissionNow,
                            _lblAdmissionWait,
                            admissionQueue);
                    }));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Form1.RefreshQueueStatus failed: {ex}");
            }
        }

        private static void UpdateOfficeStatusLabels(
            Label? nowLabel,
            Label? waitLabel,
            List<QueueEntry> windowQueue)
        {
            if (nowLabel == null || waitLabel == null)
                return;

            var now =
                windowQueue.ElementAtOrDefault(0);

            nowLabel.Text =
                $"{(now?.TicketLabel ?? "-")}";

            int waitingAhead =
                Math.Max(windowQueue.Count - 1, 0);

            int estMinutes =
                waitingAhead * AverageMinutesPerTicket;

            waitLabel.Text =
                $"{estMinutes} min";
        }

        private static int GetCashierWindowIndex(
            QueueEntry entry)
        {
            var stn =
                entry.ServiceTicketNumber > 0
                    ? entry.ServiceTicketNumber
                    : entry.TicketNumber;

            return ((stn - 1) % 4) + 1;
        }

        private static List<QueueEntry> GetRegistrarWindow1Queue(
            List<QueueEntry> registrarAll)
        {
            if (!registrarAll.Any())
                return new List<QueueEntry>();

            int baseParity =
                registrarAll[0].TicketNumber % 2;

            return registrarAll
                .Where(q =>
                    (q.TicketNumber % 2) == baseParity &&
                    !IsRCRequest(q))
                .OrderBy(q => q.TicketNumber)
                .ToList();
        }

        private void SetOfficeButtonsVisible(
            bool visible,
            PictureBox? keepVisible = null)
        {
            foreach (var btn in new[]
            {
                btn_cashier,
                btn_registrar,
                btn_admission
            })
            {
                btn.Visible =
                    visible || btn == keepVisible;

                if (btn == keepVisible)
                {
                    if (_officeButtonSelectedLocations
                        .TryGetValue(
                            btn,
                            out var selectedLocation))
                    {
                        btn.Location =
                            selectedLocation;
                    }
                }
                else if (
                    visible &&
                    _officeButtonHomeLocations
                        .TryGetValue(
                            btn,
                            out var homeLocation))
                {
                    btn.Location =
                        homeLocation;
                }
            }

            bool showIdleLabels = visible;

            _lblCashierNow!.Visible =
                showIdleLabels;

            _lblCashierWait!.Visible =
                showIdleLabels;

            _lblRegistrarNow!.Visible =
                showIdleLabels;

            _lblRegistrarWait!.Visible =
                showIdleLabels;

            _lblAdmissionNow!.Visible =
                showIdleLabels;

            _lblAdmissionWait!.Visible =
                showIdleLabels;
        }

        /// <summary>
        /// Maps a purpose to its corresponding button image
        /// based on the office/department.
        /// </summary>
        private Image? GetPurposeButtonImage(
            string department,
            string purpose)
        {
            return department switch
            {
                "Cashier" => purpose switch
                {
                    "Tuition Fee" =>
                        Properties.Resources.tuition_btn,

                    "Miscellaneous Fee" =>
                        Properties.Resources.misc_fee_btn,

                    "Other Payments" =>
                        Properties.Resources.other_trans_btn,

                    _ => null
                },

                "Registrar" => purpose switch
                {
                    "Enrollment" =>
                        Properties.Resources.enroll_btn,

                    "Credentials" =>
                        Properties.Resources.cred_btn,

                    "Other Inquiries" =>
                        Properties.Resources.other_inq_btn,

                    _ => null
                },

                "Admission" => purpose switch
                {
                    "Application Status" =>
                        Properties.Resources.appfrm_btn,

                    "Document Verification" =>
                        Properties.Resources.ref_btn,

                    "General Inquiry" =>
                        Properties.Resources.inq_btn,

                    _ => null
                },

                _ => null
            };
        }

        private Image? GetPurposeHighlightImage(
            string department,
            string purpose)
        {
            return department switch
            {
                "Cashier" => purpose switch
                {
                    "Tuition Fee" =>
                        Properties.Resources.tuition_hl_btn,

                    "Miscellaneous Fee" =>
                        Properties.Resources.misc_hl_btn,

                    "Other Payments" =>
                        Properties.Resources.transaction_hl_btn,

                    _ => null
                },

                "Registrar" => purpose switch
                {
                    "Enrollment" =>
                        Properties.Resources.enroll_hl_btn,

                    "Credentials" =>
                        Properties.Resources.cred_hl_btn,

                    "Other Inquiries" =>
                        Properties.Resources.other_inq_hl_btn,

                    _ => null
                },

                "Admission" => purpose switch
                {
                    "Application Status" =>
                        Properties.Resources.app_hl_btn,

                    "Document Verification" =>
                        Properties.Resources.ref_hl_btn,

                    "General Inquiry" =>
                        Properties.Resources.inq_hl_btn,

                    _ => null
                },

                _ => null
            };
        }

        private void CreateDepartmentPanel(
            string backgroundImage,
            string[] purposes,
            PictureBox officeButton,
            int startX = -1,
            int startY = -1,
            int buttonSpacing = -1)
        {
            // Reset selection state for the new panel
            _selectedPurposeButton = null;
            _selectedPurpose = null;

            Panel panel = new Panel()
            {
                Size = new Size(1920, 1080),
                Location = new Point(0, 0),
                BackgroundImageLayout =
                    ImageLayout.Stretch,
            };

            if (backgroundImage == "cash")
            {
                panel.BackgroundImage =
                    Properties.Resources.kiosk_cash_bg;

                panel.Tag = "Cashier";
            }
            else if (backgroundImage == "reg")
            {
                panel.BackgroundImage =
                    Properties.Resources.kiosk_reg_bg;

                panel.Tag = "Registrar";
            }
            else if (backgroundImage == "adm")
            {
                panel.BackgroundImage =
                    Properties.Resources.kiosk_adm_bg;

                panel.Tag = "Admission";
            }

            var controlFont =
                new Font(
                    "Segoe UI",
                    16F,
                    FontStyle.Regular,
                    GraphicsUnit.Point);

            int buttonHeight = 120;
            int buttonWidth = 538;

            if (startX == -1)
                startX = officeButton.Location.X;

            if (startY == -1)
                startY =
                    officeButton.Location.Y +
                    officeButton.Height +
                    30;

            if (buttonSpacing == -1)
                buttonSpacing = 150;

            for (int i = 0;
                 i < purposes.Length;
                 i++)
            {
                string department =
                    panel.Tag?.ToString() ?? "";

                Image? purposeImage =
                    GetPurposeButtonImage(
                        department,
                        purposes[i]);

                PictureBox purposeBtn =
                    CreateImageReadyPictureBox(
                        text: purposes[i],
                        name: $"btnPurpose_{i}",
                        location: new Point(
                            startX,
                            startY +
                            (i * buttonSpacing)),
                        size: new Size(
                            buttonWidth,
                            buttonHeight),
                        font: controlFont,
                        image: purposeImage,
                        backColor:
                            PurposeDefaultColor);

                purposeBtn.Tag =
                    purposes[i];

                purposeBtn.MouseClick +=
                    (sender, e) =>
                        PurposeButton_Click(
                            sender,
                            e,
                            panel);

                panel.Controls.Add(
                    purposeBtn);
            }

            PictureBox confirmBtn =
                CreateImageReadyPictureBox(
                    text: "Confirm",
                    name: "btnConfirm",
                    location:
                        new Point(680, 878),
                    size:
                        new Size(562, 130),
                    font: controlFont,
                    image:
                        Properties.Resources.confirm_btn,
                    backColor:
                        Color.FromArgb(
                            39,
                            174,
                            96));

            confirmBtn.MouseClick +=
                (sender, e) =>
                    ConfirmButton_Click(panel);

            confirmBtn.Visible = false;

            panel.Controls.Add(confirmBtn);

            PictureBox backBtn =
                CreateImageReadyPictureBox(
                    text: "Back",
                    name: "btnBack",
                    location:
                        new Point(1359, 868),
                    size:
                        new Size(562, 130),
                    font: controlFont,
                    image:
                        Properties.Resources.back_btn,
                    backColor:
                        Color.FromArgb(
                            192,
                            57,
                            43));

            backBtn.MouseClick +=
                (sender, e) =>
                    BackButton_Click(panel);

            panel.Controls.Add(backBtn);

            foreach (
                Control c in
                this.Controls.OfType<Panel>())
            {
                c.Visible = false;
            }

            this.Controls.Add(panel);

            panel.Refresh();

            StartPanelRefresh(panel);
        }

        private static Button CreateImageReadyButton(
            string text,
            string name,
            Point location,
            Size size,
            Font font,
            Image? image,
            Color backColor)
        {
            var button = new Button
            {
                Size = size,
                Location = location,
                FlatStyle =
                    FlatStyle.Popup,
                BackColor =
                    Color.Transparent,
                ForeColor =
                    Color.White,
                Font = font,
                Text = text,
                Name = name,
                BackgroundImage = image,
                ImageAlign =
                    ContentAlignment.MiddleCenter,
                BackgroundImageLayout =
                    ImageLayout.Stretch,
                UseVisualStyleBackColor =
                    false,
            };

            button.FlatAppearance
                .BorderSize = 0;

            if (image != null)
            {
                button.BackgroundImage =
                    image;

                button.Text =
                    string.Empty;
            }

            return button;
        }

        private static PictureBox CreateImageReadyPictureBox(
            string text,
            string name,
            Point location,
            Size size,
            Font font,
            Image? image,
            Color backColor)
        {
            var pictureBox =
                new PictureBox
                {
                    Size = size,
                    Location = location,

                    BackColor =
                        image == null
                            ? backColor
                            : Color.Transparent,

                    Image = image,

                    SizeMode =
                        image != null
                            ? PictureBoxSizeMode.StretchImage
                            : PictureBoxSizeMode.Normal,

                    Name = name,
                    Cursor = Cursors.Hand,
                    TabIndex = 0,
                    TabStop = false,
                };

            // If there's text and no image,
            // add a label on top.
            if (!string.IsNullOrEmpty(text) &&
                image == null)
            {
                var textLabel =
                    new Label
                    {
                        Text = text,
                        ForeColor = Color.White,
                        Font = font,
                        Location = new Point(0, 0),
                        Size = size,
                        TextAlign =
                            ContentAlignment.MiddleCenter,
                        BackColor =
                            Color.Transparent,
                        AutoSize = false,
                        Margin =
                            new Padding(0),
                        TabIndex = 0,
                        TabStop = false,
                    };

                pictureBox.Controls.Add(
                    textLabel);
            }

            return pictureBox;
        }

        private void btn_cashier_Click(
            object sender,
            EventArgs e)
        {
            SetOfficeButtonsVisible(
                false,
                keepVisible: btn_cashier);

            CreateDepartmentPanel(
                "cash",
                new[]
                {
                    "Tuition Fee",
                    "Miscellaneous Fee",
                    "Other Payments"
                },
                btn_cashier,
                startX: 33,
                startY: 650,
                buttonSpacing: 130);
        }

        private void btn_registrar_Click_1(
            object sender,
            EventArgs e)
        {
            SetOfficeButtonsVisible(
                false,
                keepVisible: btn_registrar);

            CreateDepartmentPanel(
                "reg",
                new[]
                {
                    "Enrollment",
                    "Credentials",
                    "Other Inquiries"
                },
                btn_registrar,
                startX: 33,
                startY: 650,
                buttonSpacing: 130);
        }

        private void btn_admission_Click_1(
            object sender,
            EventArgs e)
        {
            SetOfficeButtonsVisible(
                false,
                keepVisible: btn_admission);

            CreateDepartmentPanel(
                "adm",
                new[]
                {
                    "Application Status",
                    "Document Verification",
                    "General Inquiry"
                },
                btn_admission,
                startX: 33,
                startY: 650,
                buttonSpacing: 130);
        }

        private void PurposeButton_Click(
            object sender,
            EventArgs e,
            Panel activePanel)
        {
            if (sender is not PictureBox pictureBox)
                return;

            string purpose =
                pictureBox.Tag?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(purpose))
            {
                MessageBox.Show(
                    "Invalid selection. Please try again.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string department =
                activePanel.Tag?.ToString() ?? "";

            // Restore previously selected button
            // back to its normal image/color.
            if (_selectedPurposeButton != null &&
                _selectedPurposeButton != pictureBox)
            {
                var prevPurpose =
                    _selectedPurposeButton.Tag?
                        .ToString() ?? "";

                var prevNormalImage =
                    GetPurposeButtonImage(
                        department,
                        prevPurpose);

                if (prevNormalImage != null)
                {
                    _selectedPurposeButton.Image =
                        prevNormalImage;
                }
                else
                {
                    _selectedPurposeButton.BackColor =
                        PurposeDefaultColor;
                }
            }

            // Highlight newly selected button.
            var highlightImage =
                GetPurposeHighlightImage(
                    department,
                    purpose);

            if (highlightImage != null)
            {
                pictureBox.Image =
                    highlightImage;
            }
            else
            {
                pictureBox.BackColor =
                    PurposeSelectedColor;
            }

            _selectedPurposeButton =
                pictureBox;

            _selectedPurpose =
                purpose;

            var confirmBtn =
                activePanel.Controls[
                    "btnConfirm"];

            if (confirmBtn != null)
            {
                confirmBtn.Visible =
                    true;
            }
        }

        private void ConfirmButton_Click(
            Panel activePanel)
        {
            string service =
                activePanel.Tag?.ToString()
                ?? "Other";

            if (string.IsNullOrWhiteSpace(
                _selectedPurpose))
            {
                MessageBox.Show(
                    "Please select a purpose before confirming.",
                    "Selection Required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            // =====================================================
            // IMPORTANT:
            // This saves the ticket exactly as before.
            // We DO NOT create another ticket here.
            // =====================================================

            _presenter.Submit(
                _selectedPurpose,
                service);

            try
            {
                // Existing printing behavior.
                PrintLastInsertedTicket();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Printing failed: {ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            // =====================================================
            // NEW:
            // Show the SAME saved ticket in a popup.
            // =====================================================

            ShowTicketPopup();

            // Return to kiosk home only AFTER
            // the user closes the popup.
            ClosePanelAndReturnHome(
                activePanel);
        }

        private void BackButton_Click(
            Panel activePanel)
        {
            ClosePanelAndReturnHome(
                activePanel);
        }

        private void ClosePanelAndReturnHome(
            Panel activePanel)
        {
            _selectedPurposeButton = null;
            _selectedPurpose = null;

            this.Controls.Remove(
                activePanel);

            activePanel.Dispose();

            StopPanelRefresh();

            SetOfficeButtonsVisible(
                true);

            this.Refresh();
        }

        private void StartPanelRefresh(
            Panel panel)
        {
            Dictionary<string, Point[]> defaultPositions =
                new()
                {
                    {
                        "Cashier",
                        new[]
                        {
                            new Point(1350, 220),
                            new Point(1350, 380),
                            new Point(1350, 540),
                            new Point(1350, 700)
                        }
                    },

                    {
                        "Registrar",
                        new[]
                        {
                            new Point(1350, 220),
                            new Point(1350, 380)
                        }
                    },

                    {
                        "Admission",
                        new[]
                        {
                            new Point(1350, 220)
                        }
                    }
                };

            StartPanelRefresh(
                panel,
                defaultPositions);
        }

        /// <summary>
        /// Creates window number labels for the
        /// department panel with custom positions.
        /// </summary>
        private void StartPanelRefresh(
            Panel panel,
            Dictionary<string, Point[]> windowPositions)
        {
            StopPanelRefresh();

            var service =
                panel.Tag?.ToString()
                ?? string.Empty;

            if (windowPositions.TryGetValue(
                service,
                out var positions))
            {
                float fontSize =
                    service switch
                    {
                        "Cashier" => 36F,
                        "Registrar" => 36F,
                        "Admission" => 36F,
                        _ => 36F
                    };

                for (int i = 0;
                     i < positions.Length;
                     i++)
                {
                    var lbl =
                        new Label
                        {
                            Name =
                                $"lbl_{service}_W{i + 1}",

                            AutoSize = true,

                            Location =
                                positions[i],

                            Font =
                                new Font(
                                    "Segoe UI",
                                    fontSize,
                                    FontStyle.Bold,
                                    GraphicsUnit.Point),

                            ForeColor =
                                Color.DarkSeaGreen,

                            BackColor =
                                Color.Transparent,

                            Text = "-"
                        };

                    panel.Controls.Add(lbl);

                    lbl.BringToFront();
                }
            }

            _panelRefreshTimer =
                new System.Threading.Timer(
                    _ =>
                        RefreshPanelWindowNowLabels(
                            panel),
                    null,
                    TimeSpan.Zero,
                    TimeSpan.FromSeconds(2));
        }

        private void StopPanelRefresh()
        {
            try
            {
                _panelRefreshTimer?.Dispose();
            }
            catch
            {
            }

            _panelRefreshTimer = null;
        }

        private void RefreshPanelWindowNowLabels(
            Panel panel)
        {
            try
            {
                DbConfig.EnsureDatabaseAndTables();

                var repo =
                    new QueueRepository(
                        DbConfig.ConnectionString);

                var all =
                    repo.GetAll()
                    ?? new List<QueueEntry>();

                var service =
                    panel.Tag?.ToString()
                    ?? string.Empty;

                if (string.Equals(
                    service,
                    "Cashier",
                    StringComparison.OrdinalIgnoreCase))
                {
                    for (int i = 1;
                         i <= 4;
                         i++)
                    {
                        var lbl =
                            panel.Controls.Find(
                                $"lbl_{service}_W{i}",
                                true)
                            .FirstOrDefault()
                            as Label;

                        if (lbl == null)
                            continue;

                        var windowQueue =
                            all.Where(q =>
                                    string.Equals(
                                        q.Service,
                                        "Cashier",
                                        StringComparison.OrdinalIgnoreCase))
                               .Where(q =>
                                    GetCashierWindowIndex(q) == i)
                               .OrderBy(q =>
                                    q.TicketNumber)
                               .ToList();

                        var now =
                            windowQueue.ElementAtOrDefault(0);

                        var text =
                            now?.TicketLabel ?? "-";

                        if (lbl.IsHandleCreated &&
                            !lbl.IsDisposed)
                        {
                            try
                            {
                                lbl.BeginInvoke(
                                    new Action(() =>
                                        lbl.Text =
                                            $"{text}"));
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                else if (string.Equals(
                    service,
                    "Registrar",
                    StringComparison.OrdinalIgnoreCase))
                {
                    var reg =
                        all.Where(q =>
                                string.Equals(
                                    q.Service,
                                    "Registrar",
                                    StringComparison.OrdinalIgnoreCase))
                           .OrderBy(q =>
                                q.TicketNumber)
                           .ToList();

                    List<QueueEntry> rcRequests =
                        reg.Where(IsRCRequest)
                           .OrderBy(q =>
                                q.TicketNumber)
                           .ToList();

                    List<QueueEntry> nonRc =
                        reg.Where(q =>
                                !IsRCRequest(q))
                           .OrderBy(q =>
                                q.TicketNumber)
                           .ToList();

                    var now1 =
                        nonRc.ElementAtOrDefault(0)?
                            .TicketLabel ?? "-";

                    var now2 =
                        rcRequests.ElementAtOrDefault(0)?
                            .TicketLabel ?? "-";

                    var lbl1 =
                        panel.Controls.Find(
                            $"lbl_{service}_W1",
                            true)
                        .FirstOrDefault()
                        as Label;

                    var lbl2 =
                        panel.Controls.Find(
                            $"lbl_{service}_W2",
                            true)
                        .FirstOrDefault()
                        as Label;

                    if (lbl1 != null &&
                        lbl1.IsHandleCreated &&
                        !lbl1.IsDisposed)
                    {
                        try
                        {
                            lbl1.BeginInvoke(
                                new Action(() =>
                                    lbl1.Text =
                                        $"{now1}"));
                        }
                        catch
                        {
                        }
                    }

                    if (lbl2 != null &&
                        lbl2.IsHandleCreated &&
                        !lbl2.IsDisposed)
                    {
                        try
                        {
                            lbl2.BeginInvoke(
                                new Action(() =>
                                    lbl2.Text =
                                        $"{now2}"));
                        }
                        catch
                        {
                        }
                    }
                }
                else if (string.Equals(
                    service,
                    "Admission",
                    StringComparison.OrdinalIgnoreCase))
                {
                    var admissionQueue =
                        all.Where(q =>
                                string.Equals(
                                    q.Service,
                                    "Admission",
                                    StringComparison.OrdinalIgnoreCase))
                           .OrderBy(q =>
                                q.TicketNumber)
                           .ToList();

                    var now =
                        admissionQueue.ElementAtOrDefault(0)?
                            .TicketLabel ?? "-";

                    var lbl =
                        panel.Controls.Find(
                            $"lbl_{service}_W1",
                            true)
                        .FirstOrDefault()
                        as Label;

                    if (lbl != null &&
                        lbl.IsHandleCreated &&
                        !lbl.IsDisposed)
                    {
                        try
                        {
                            lbl.BeginInvoke(
                                new Action(() =>
                                    lbl.Text = now));
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"RefreshPanelWindowNowLabels failed: {ex}");
            }
        }

        // =========================================================
        // EXISTING PRINT TICKET
        // =========================================================

        private void PrintLastInsertedTicket()
        {
            DbConfig.EnsureDatabaseAndTables();

            var repo =
                new QueueRepository(
                    DbConfig.ConnectionString);

            var all =
                repo.GetAll()
                ?? new List<QueueEntry>();

            if (!all.Any())
                return;

            var last =
                all.OrderByDescending(
                    x => x.TicketNumber)
                   .First();

            string assignedWindow =
                DetermineAssignedWindow(
                    last,
                    all);

            // PRINT LAYOUT
            // Existing print behavior preserved.
            var lines = new List<string>
            {
                "CampusQ - Queue Ticket",
                "======================",
                $"Ticket #: {last.TicketNumber}",
                $"Office: {last.Service} {assignedWindow}",
                $"Purpose: {last.Purpose}",
                $"Time: {last.TimeAdded:g}",
                "",
                "Please wait for your ticket to be called.",
            };

            // Generate QR code with existing ticket info URL.
            string ticketUrl =
                GenerateTicketInfoUrl(
                    last.TicketNumber);

            Bitmap? qrCodeBitmap =
                GenerateQRCode(ticketUrl);

            PrintDocument pd =
                new PrintDocument();

            pd.DocumentName =
                $"Ticket_{last.TicketNumber}";

            PaperSize paperSize =
                new PaperSize(
                    "58mm Thermal",
                    219,
                    350);

            PaperSource paperSource =
                new PaperSource
                {
                    RawKind =
                        (int)PaperSourceKind.Custom
                };

            pd.DefaultPageSettings.PaperSize =
                paperSize;

            pd.DefaultPageSettings.PaperSource =
                paperSource;

            pd.DefaultPageSettings.Margins =
                new Margins(5, 5, 5, 5);

            pd.PrintPage += (s, e) =>
            {
                var g = e.Graphics;

                var fontTitle =
                    new Font(
                        "Arial",
                        12,
                        FontStyle.Bold);

                var font =
                    new Font(
                        "Arial",
                        9);

                var fontSmall =
                    new Font(
                        "Arial",
                        7);

                float x = 10;
                float y = 10;

                float maxWidth = 199;

                var centerFormat =
                    new StringFormat
                    {
                        Alignment =
                            StringAlignment.Center
                    };

                g.DrawString(
                    lines[0],
                    fontTitle,
                    Brushes.Black,
                    x + maxWidth / 2,
                    y,
                    centerFormat);

                y += 25;

                g.DrawString(
                    lines[1],
                    font,
                    Brushes.Black,
                    x + maxWidth / 2,
                    y,
                    centerFormat);

                y += 18;

                foreach (var ln in lines.Skip(2))
                {
                    if (string.IsNullOrWhiteSpace(ln))
                    {
                        y += 8;
                    }
                    else
                    {
                        g.DrawString(
                            ln,
                            font,
                            Brushes.Black,
                            x,
                            y);

                        y += 18;
                    }
                }

                if (qrCodeBitmap != null)
                {
                    y += 10;

                    int qrSize = 80;

                    int qrX =
                        (int)(
                            x +
                            (maxWidth - qrSize) / 2);

                    int qrY =
                        (int)y;

                    g.DrawImage(
                        qrCodeBitmap,
                        qrX,
                        qrY,
                        qrSize,
                        qrSize);

                    y +=
                        qrSize +
                        5;

                    g.DrawString(
                        "Scan for info",
                        fontSmall,
                        Brushes.Black,
                        x + maxWidth / 2,
                        y,
                        centerFormat);
                }
            };

            using (var dlg =
                   new PrintDialog())
            {
                dlg.Document = pd;

                if (dlg.ShowDialog() ==
                    DialogResult.OK)
                {
                    pd.Print();
                }
            }

            qrCodeBitmap?.Dispose();
        }

        // =========================================================
        // NEW TICKET POPUP
        // =========================================================

        private void ShowTicketPopup()
        {
            try
            {
                DbConfig.EnsureDatabaseAndTables();

                var repo =
                    new QueueRepository(
                        DbConfig.ConnectionString);

                var all =
                    repo.GetAll()
                    ?? new List<QueueEntry>();

                if (!all.Any())
                {
                    MessageBox.Show(
                        "Ticket was saved, but the ticket information could not be loaded.",
                        "CampusQ",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                // IMPORTANT:
                // Get the SAME ticket that was already saved.
                // We are NOT creating a new ticket.
                var last =
                    all.OrderByDescending(
                        x => x.TicketNumber)
                       .First();

                string ticketNumber =
                    !string.IsNullOrWhiteSpace(
                        last.TicketLabel)
                        ? last.TicketLabel
                        : last.TicketNumber.ToString();

                string assignedWindow =
                    DetermineAssignedWindow(
                        last,
                        all);

                string officeText =
                    $"{last.Service} {assignedWindow}".Trim();

                string purposeText =
                    last.Purpose ?? "";

                // Use the existing QR generation.
                string ticketUrl =
                    GenerateTicketInfoUrl(
                        last.TicketNumber);

                Bitmap? qrBitmap =
                    GenerateQRCode(
                        ticketUrl,
                        8);

                using (Form popup =
                       new Form())
                {
                    popup.StartPosition =
                        FormStartPosition.CenterParent;

                    popup.Size =
                        new Size(620, 760);

                    popup.FormBorderStyle =
                        FormBorderStyle.FixedDialog;

                    popup.MaximizeBox = false;
                    popup.MinimizeBox = false;

                    popup.ShowInTaskbar = false;

                    popup.Text =
                        "CampusQ - Your Ticket";

                    popup.BackColor =
                        Color.White;

                    // =================================================
                    // TITLE
                    // =================================================

                    Label title =
                        new Label
                        {
                            Text =
                                "YOUR TICKET",

                            Font =
                                new Font(
                                    "Segoe UI",
                                    25F,
                                    FontStyle.Bold),

                            ForeColor =
                                Color.FromArgb(
                                    15,
                                    75,
                                    45),

                            AutoSize = false,

                            TextAlign =
                                ContentAlignment.MiddleCenter,

                            Location =
                                new Point(
                                    20,
                                    25),

                            Size =
                                new Size(
                                    560,
                                    55)
                        };

                    popup.Controls.Add(title);

                    // =================================================
                    // SMALL MESSAGE
                    // =================================================

                    Label message =
                        new Label
                        {
                            Text =
                                "Please keep this ticket number.",

                            Font =
                                new Font(
                                    "Segoe UI",
                                    12F),

                            ForeColor =
                                Color.FromArgb(
                                    90,
                                    90,
                                    90),

                            AutoSize = false,

                            TextAlign =
                                ContentAlignment.MiddleCenter,

                            Location =
                                new Point(
                                    20,
                                    80),

                            Size =
                                new Size(
                                    560,
                                    35)
                        };

                    popup.Controls.Add(message);

                    // =================================================
                    // TICKET NUMBER
                    // =================================================

                    Label ticketLabel =
                        new Label
                        {
                            Text =
                                ticketNumber,

                            Font =
                                new Font(
                                    "Segoe UI",
                                    58F,
                                    FontStyle.Bold),

                            ForeColor =
                                Color.FromArgb(
                                    15,
                                    85,
                                    50),

                            AutoSize = false,

                            TextAlign =
                                ContentAlignment.MiddleCenter,

                            Location =
                                new Point(
                                    20,
                                    115),

                            Size =
                                new Size(
                                    560,
                                    105)
                        };

                    popup.Controls.Add(
                        ticketLabel);

                    // =================================================
                    // OFFICE
                    // =================================================

                    Label officeLabel =
                        new Label
                        {
                            Text =
                                officeText.ToUpper(),

                            Font =
                                new Font(
                                    "Segoe UI",
                                    17F,
                                    FontStyle.Bold),

                            ForeColor =
                                Color.White,

                            BackColor =
                                Color.FromArgb(
                                    39,
                                    174,
                                    96),

                            AutoSize = false,

                            TextAlign =
                                ContentAlignment.MiddleCenter,

                            Location =
                                new Point(
                                    100,
                                    220),

                            Size =
                                new Size(
                                    420,
                                    50)
                        };

                    popup.Controls.Add(
                        officeLabel);

                    // =================================================
                    // PURPOSE
                    // =================================================

                    Label purposeLabel =
                        new Label
                        {
                            Text =
                                purposeText,

                            Font =
                                new Font(
                                    "Segoe UI",
                                    13F,
                                    FontStyle.Regular),

                            ForeColor =
                                Color.FromArgb(
                                    40,
                                    40,
                                    40),

                            AutoSize = false,

                            TextAlign =
                                ContentAlignment.MiddleCenter,

                            Location =
                                new Point(
                                    20,
                                    275),

                            Size =
                                new Size(
                                    560,
                                    40)
                        };

                    popup.Controls.Add(
                        purposeLabel);

                    // =================================================
                    // QR CODE
                    // =================================================

                    PictureBox qr =
                        new PictureBox
                        {
                            Size =
                                new Size(
                                    250,
                                    250),

                            SizeMode =
                                PictureBoxSizeMode.Zoom,

                            Location =
                                new Point(
                                    185,
                                    325),

                            BackColor =
                                Color.White,

                            BorderStyle =
                                BorderStyle.FixedSingle,

                            Image =
                                qrBitmap
                        };

                    popup.Controls.Add(qr);

                    // =================================================
                    // QR MESSAGE
                    // =================================================

                    Label qrMessage =
                        new Label
                        {
                            Text =
                                "Scan the QR code for ticket information",

                            Font =
                                new Font(
                                    "Segoe UI",
                                    10F),

                            ForeColor =
                                Color.FromArgb(
                                    100,
                                    100,
                                    100),

                            AutoSize = false,

                            TextAlign =
                                ContentAlignment.MiddleCenter,

                            Location =
                                new Point(
                                    20,
                                    580),

                            Size =
                                new Size(
                                    560,
                                    30)
                        };

                    popup.Controls.Add(
                        qrMessage);

                    // =================================================
                    // OK BUTTON
                    // =================================================

                    Button okButton =
                        new Button
                        {
                            Text =
                                "OK",

                            Font =
                                new Font(
                                    "Segoe UI",
                                    12F,
                                    FontStyle.Bold),

                            BackColor =
                                Color.FromArgb(
                                    39,
                                    174,
                                    96),

                            ForeColor =
                                Color.White,

                            FlatStyle =
                                FlatStyle.Flat,

                            Location =
                                new Point(
                                    190,
                                    625),

                            Size =
                                new Size(
                                    240,
                                    55),

                            Cursor =
                                Cursors.Hand
                        };

                    okButton.FlatAppearance
                        .BorderSize = 0;

                    okButton.Click +=
                        (s, e) =>
                        {
                            popup.Close();
                        };

                    popup.Controls.Add(
                        okButton);

                    popup.AcceptButton =
                        okButton;

                    // Make sure the popup appears
                    // above the kiosk form.
                    popup.ShowDialog(this);

                    // Dispose QR bitmap after
                    // popup has been closed.
                    if (qrBitmap != null)
                    {
                        qrBitmap.Dispose();
                        qrBitmap = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to display ticket:\n\n{ex.Message}",
                    "CampusQ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private static string DetermineAssignedWindow(
            QueueEntry entry,
            List<QueueEntry> all)
        {
            if (entry == null)
                return string.Empty;

            if (string.Equals(
                entry.Service,
                "Cashier",
                StringComparison.OrdinalIgnoreCase))
            {
                var stn =
                    entry.ServiceTicketNumber > 0
                        ? entry.ServiceTicketNumber
                        : entry.TicketNumber;

                var idx =
                    ((stn - 1) % 4) + 1;

                return
                    $"- Window {idx}";
            }

            if (string.Equals(
                entry.Service,
                "Registrar",
                StringComparison.OrdinalIgnoreCase))
            {
                var reg =
                    all.Where(q =>
                            string.Equals(
                                q.Service,
                                "Registrar",
                                StringComparison.OrdinalIgnoreCase))
                       .OrderBy(q =>
                            q.TicketNumber)
                       .ToList();

                if (!reg.Any())
                    return string.Empty;

                int baseParity =
                    reg[0].TicketNumber % 2;

                bool isRC =
                    IsRCRequest(entry);

                if (
                    (entry.TicketNumber % 2)
                    == baseParity
                    &&
                    !isRC)
                {
                    return "- W1";
                }

                return "- W2";
            }

            return string.Empty;
        }

        private static bool IsRCRequest(
            QueueEntry? entry)
        {
            if (entry == null)
                return false;

            var label =
                entry.TicketLabel;

            if (!string.IsNullOrWhiteSpace(label) &&
                label.StartsWith(
                    "RC",
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            var purpose =
                entry.Purpose ?? string.Empty;

            if (
                purpose.IndexOf(
                    "credential",
                    StringComparison.OrdinalIgnoreCase)
                >= 0)
            {
                return true;
            }

            var p =
                purpose.ToLowerInvariant();

            var separators =
                new[]
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

            var tokens =
                p.Split(
                    separators,
                    StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Any(
                t => t == "rc"))
            {
                return true;
            }

            return false;
        }

        private void Form1_Load(
            object sender,
            EventArgs e)
        {
        }

        public void ShowMessage(
            string text,
            string caption,
            MessageBoxIcon icon)
        {
            MessageBox.Show(
                text,
                caption,
                MessageBoxButtons.OK,
                icon);
        }

        public void AddToQueue(
            string purpose,
            string service)
        {
            if (staff == null ||
                staff.IsDisposed)
            {
                staff = new Staff();
            }

            staff.AddToQueue(
                purpose,
                service);
        }

        private string GenerateTicketInfoUrl(
            int ticketNumber)
        {
            var baseUrl =
                WebAppConfig.BaseUrl.TrimEnd('/');

            return
                $"{baseUrl}/Ticket/{ticketNumber}";
        }

        private Bitmap? GenerateQRCode(
            string text,
            int pixelPerModule = 10)
        {
            try
            {
                using (
                    var qrGenerator =
                        new QRCoder.QRCodeGenerator())
                {
                    var qrCodeData =
                        qrGenerator.CreateQrCode(
                            text,
                            QRCoder.QRCodeGenerator
                                .ECCLevel.Q);

                    using (
                        var qrCode =
                            new QRCoder.QRCode(
                                qrCodeData))
                    {
                        Bitmap qrCodeBitmap =
                            qrCode.GetGraphic(
                                pixelPerModule);

                        return qrCodeBitmap;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error generating QR code: {ex.Message}",
                    "QR Code Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return null;
            }
        }
    }
}