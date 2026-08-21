namespace CampusQ
{
    partial class Staff
    {
        private System.ComponentModel.IContainer components = null;

        // =========================================================
        // EXISTING CONTROLS
        // =========================================================

        private System.Windows.Forms.ComboBox comboBoxService;
        private System.Windows.Forms.Label labelService;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonServeNext;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.DataGridView dataGridViewQueue;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.Button btn_service;

        // =========================================================
        // LIVE QUEUE
        // =========================================================

        private System.Windows.Forms.Panel pnlLiveQueue;
        private System.Windows.Forms.Label lblLiveQueueIcon;
        private System.Windows.Forms.Label lblLiveQueueTitle;

        // =========================================================
        // QUEUE ANALYTICS CONTAINER
        // =========================================================

        private System.Windows.Forms.Panel pnlAnalytics;
        private System.Windows.Forms.Label lblAnalyticsIcon;
        private System.Windows.Forms.Label lblAnalyticsTitle;
        private System.Windows.Forms.Label lblAnalyticsNote;

        // =========================================================
        // ACTIVE STATUS
        // =========================================================

        private System.Windows.Forms.Panel pnlActiveStatus;
        private System.Windows.Forms.Label lblActiveStatusIcon;
        private System.Windows.Forms.Label lblActiveStatusTitle;

        // =========================================================
        // WINDOW 1
        // =========================================================

        private System.Windows.Forms.Panel pnlWindow1;
        private System.Windows.Forms.Label lblWindow1Dot;
        private System.Windows.Forms.Label lblWindow1Staff;
        private System.Windows.Forms.Label lblWindow1State;
        private System.Windows.Forms.Label lblWindow1Description;
        private System.Windows.Forms.Button btnWindow1;

        // =========================================================
        // WINDOW 2
        // =========================================================

        private System.Windows.Forms.Panel pnlWindow2;
        private System.Windows.Forms.Label lblWindow2Dot;
        private System.Windows.Forms.Label lblWindow2Staff;
        private System.Windows.Forms.Label lblWindow2State;
        private System.Windows.Forms.Label lblWindow2Description;
        private System.Windows.Forms.Button btnWindow2;

        // =========================================================
        // WINDOW 3
        // =========================================================

        private System.Windows.Forms.Panel pnlWindow3;
        private System.Windows.Forms.Label lblWindow3Dot;
        private System.Windows.Forms.Label lblWindow3Staff;
        private System.Windows.Forms.Label lblWindow3State;
        private System.Windows.Forms.Label lblWindow3Description;
        private System.Windows.Forms.Button btnWindow3;

        // =========================================================
        // WINDOW 4
        // =========================================================

        private System.Windows.Forms.Panel pnlWindow4;
        private System.Windows.Forms.Label lblWindow4Dot;
        private System.Windows.Forms.Label lblWindow4Staff;
        private System.Windows.Forms.Label lblWindow4State;
        private System.Windows.Forms.Label lblWindow4Description;
        private System.Windows.Forms.Button btnWindow4;

        // =========================================================
        // DISPOSE
        // =========================================================

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Staff));
            comboBoxService = new ComboBox();
            labelService = new Label();
            buttonRefresh = new Button();
            buttonServeNext = new Button();
            labelTotal = new Label();
            dataGridViewQueue = new DataGridView();
            trayIcon = new NotifyIcon(components);
            btn_service = new Button();
            pnlLiveQueue = new Panel();
            lblLiveQueueIcon = new Label();
            lblLiveQueueTitle = new Label();
            pnlAnalytics = new Panel();
            lblAnalyticsIcon = new Label();
            lblAnalyticsTitle = new Label();
            lblAnalyticsNote = new Label();
            pnlActiveStatus = new Panel();
            lblActiveStatusIcon = new Label();
            lblActiveStatusTitle = new Label();
            pnlWindow1 = new Panel();
            lblWindow1Dot = new Label();
            lblWindow1Staff = new Label();
            lblWindow1State = new Label();
            lblWindow1Description = new Label();
            btnWindow1 = new Button();
            pnlWindow2 = new Panel();
            lblWindow2Dot = new Label();
            lblWindow2Staff = new Label();
            lblWindow2State = new Label();
            lblWindow2Description = new Label();
            btnWindow2 = new Button();
            pnlWindow3 = new Panel();
            lblWindow3Dot = new Label();
            lblWindow3Staff = new Label();
            lblWindow3State = new Label();
            lblWindow3Description = new Label();
            btnWindow3 = new Button();
            pnlWindow4 = new Panel();
            lblWindow4Dot = new Label();
            lblWindow4Staff = new Label();
            lblWindow4State = new Label();
            lblWindow4Description = new Label();
            btnWindow4 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewQueue).BeginInit();
            pnlLiveQueue.SuspendLayout();
            pnlAnalytics.SuspendLayout();
            pnlActiveStatus.SuspendLayout();
            pnlWindow1.SuspendLayout();
            pnlWindow2.SuspendLayout();
            pnlWindow3.SuspendLayout();
            pnlWindow4.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxService
            // 
            comboBoxService.BackColor = Color.White;
            comboBoxService.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxService.FlatStyle = FlatStyle.Flat;
            comboBoxService.Font = new Font("Segoe UI", 9F);
            comboBoxService.FormattingEnabled = true;
            comboBoxService.Location = new Point(205, 132);
            comboBoxService.Name = "comboBoxService";
            comboBoxService.Size = new Size(150, 23);
            comboBoxService.TabIndex = 0;
            // 
            // labelService
            // 
            labelService.AutoSize = true;
            labelService.BackColor = Color.Transparent;
            labelService.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelService.ForeColor = Color.FromArgb(0, 92, 48);
            labelService.Location = new Point(122, 137);
            labelService.Name = "labelService";
            labelService.Size = new Size(56, 15);
            labelService.TabIndex = 1;
            labelService.Text = "SERVICE:";
            // 
            // buttonRefresh
            // 
            buttonRefresh.BackColor = Color.FromArgb(0, 110, 55);
            buttonRefresh.Cursor = Cursors.Hand;
            buttonRefresh.FlatAppearance.BorderSize = 0;
            buttonRefresh.FlatStyle = FlatStyle.Flat;
            buttonRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonRefresh.ForeColor = Color.White;
            buttonRefresh.Location = new Point(452, 115);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(145, 42);
            buttonRefresh.TabIndex = 2;
            buttonRefresh.Text = "↻   Refresh Queue";
            buttonRefresh.UseVisualStyleBackColor = false;
            // 
            // buttonServeNext
            // 
            buttonServeNext.BackColor = Color.FromArgb(0, 120, 58);
            buttonServeNext.Cursor = Cursors.Hand;
            buttonServeNext.FlatAppearance.BorderSize = 0;
            buttonServeNext.FlatStyle = FlatStyle.Flat;
            buttonServeNext.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonServeNext.ForeColor = Color.White;
            buttonServeNext.Location = new Point(612, 115);
            buttonServeNext.Name = "buttonServeNext";
            buttonServeNext.Size = new Size(145, 42);
            buttonServeNext.TabIndex = 3;
            buttonServeNext.Text = "▶   Serve Next";
            buttonServeNext.UseVisualStyleBackColor = false;
            // 
            // labelTotal
            // 
            labelTotal.AutoSize = true;
            labelTotal.BackColor = Color.White;
            labelTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelTotal.ForeColor = Color.FromArgb(0, 92, 48);
            labelTotal.Location = new Point(960, 36);
            labelTotal.Name = "labelTotal";
            labelTotal.Size = new Size(56, 15);
            labelTotal.TabIndex = 4;
            labelTotal.Text = "TOTAL: 0";
            // 
            // dataGridViewQueue
            // 
            dataGridViewQueue.AllowUserToAddRows = false;
            dataGridViewQueue.AllowUserToDeleteRows = false;
            dataGridViewQueue.AllowUserToResizeRows = false;
            dataGridViewQueue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewQueue.BackgroundColor = Color.White;
            dataGridViewQueue.BorderStyle = BorderStyle.None;
            dataGridViewQueue.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewQueue.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewQueue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewQueue.EnableHeadersVisualStyles = false;
            dataGridViewQueue.GridColor = Color.FromArgb(232, 238, 234);
            dataGridViewQueue.Location = new Point(16, 68);
            dataGridViewQueue.MultiSelect = false;
            dataGridViewQueue.Name = "dataGridViewQueue";
            dataGridViewQueue.ReadOnly = true;
            dataGridViewQueue.RowHeadersVisible = false;
            dataGridViewQueue.RowTemplate.Height = 40;
            dataGridViewQueue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewQueue.Size = new Size(448, 392);
            dataGridViewQueue.TabIndex = 5;
            dataGridViewQueue.CellContentClick += dataGridViewQueue_CellContentClick;
            // 
            // trayIcon
            // 
            trayIcon.Text = "CampusQ Registrar Dashboard";
            trayIcon.MouseDoubleClick += trayIcon_MouseDoubleClick;
            // 
            // btn_service
            // 
            btn_service.BackColor = Color.White;
            btn_service.Cursor = Cursors.Hand;
            btn_service.FlatAppearance.BorderColor = Color.FromArgb(0, 110, 55);
            btn_service.FlatAppearance.BorderSize = 0;
            btn_service.FlatStyle = FlatStyle.Flat;
            btn_service.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btn_service.ForeColor = Color.FromArgb(30, 30, 30);
            btn_service.Location = new Point(772, 115);
            btn_service.Name = "btn_service";
            btn_service.Size = new Size(145, 42);
            btn_service.TabIndex = 6;
            btn_service.Text = "✓   Service Window";
            btn_service.UseVisualStyleBackColor = false;
            btn_service.Click += btn_service_Click;
            // 
            // pnlLiveQueue
            // 
            pnlLiveQueue.BackColor = Color.White;
            pnlLiveQueue.BorderStyle = BorderStyle.FixedSingle;
            pnlLiveQueue.Controls.Add(dataGridViewQueue);
            pnlLiveQueue.Controls.Add(lblLiveQueueIcon);
            pnlLiveQueue.Controls.Add(lblLiveQueueTitle);
            pnlLiveQueue.Location = new Point(76, 167);
            pnlLiveQueue.Name = "pnlLiveQueue";
            pnlLiveQueue.Size = new Size(482, 478);
            pnlLiveQueue.TabIndex = 2;
            // 
            // lblLiveQueueIcon
            // 
            lblLiveQueueIcon.AutoSize = true;
            lblLiveQueueIcon.BackColor = Color.Transparent;
            lblLiveQueueIcon.Font = new Font("Segoe UI Symbol", 16F, FontStyle.Bold);
            lblLiveQueueIcon.ForeColor = Color.FromArgb(0, 92, 48);
            lblLiveQueueIcon.Location = new Point(20, 16);
            lblLiveQueueIcon.Name = "lblLiveQueueIcon";
            lblLiveQueueIcon.Size = new Size(0, 30);
            lblLiveQueueIcon.TabIndex = 6;
            // 
            // lblLiveQueueTitle
            // 
            lblLiveQueueTitle.AutoSize = true;
            lblLiveQueueTitle.BackColor = Color.Transparent;
            lblLiveQueueTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLiveQueueTitle.ForeColor = Color.FromArgb(0, 92, 48);
            lblLiveQueueTitle.Location = new Point(16, 13);
            lblLiveQueueTitle.Name = "lblLiveQueueTitle";
            lblLiveQueueTitle.Size = new Size(145, 25);
            lblLiveQueueTitle.TabIndex = 7;
            lblLiveQueueTitle.Text = "🔊 LIVE QUEUE";
            // 
            // pnlAnalytics
            // 
            pnlAnalytics.BackColor = Color.White;
            pnlAnalytics.BorderStyle = BorderStyle.FixedSingle;
            pnlAnalytics.Controls.Add(lblAnalyticsIcon);
            pnlAnalytics.Controls.Add(lblAnalyticsTitle);
            pnlAnalytics.Controls.Add(lblAnalyticsNote);
            pnlAnalytics.Location = new Point(571, 167);
            pnlAnalytics.Name = "pnlAnalytics";
            pnlAnalytics.Size = new Size(468, 267);
            pnlAnalytics.TabIndex = 1;
            // 
            // lblAnalyticsIcon
            // 
            lblAnalyticsIcon.AutoSize = true;
            lblAnalyticsIcon.BackColor = Color.Transparent;
            lblAnalyticsIcon.Font = new Font("Segoe UI Symbol", 16F, FontStyle.Bold);
            lblAnalyticsIcon.ForeColor = Color.FromArgb(0, 92, 48);
            lblAnalyticsIcon.Location = new Point(20, 13);
            lblAnalyticsIcon.Name = "lblAnalyticsIcon";
            lblAnalyticsIcon.Size = new Size(36, 30);
            lblAnalyticsIcon.TabIndex = 0;
            lblAnalyticsIcon.Text = "📶";
            // 
            // lblAnalyticsTitle
            // 
            lblAnalyticsTitle.AutoSize = true;
            lblAnalyticsTitle.BackColor = Color.Transparent;
            lblAnalyticsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblAnalyticsTitle.ForeColor = Color.FromArgb(0, 92, 48);
            lblAnalyticsTitle.Location = new Point(51, 17);
            lblAnalyticsTitle.Name = "lblAnalyticsTitle";
            lblAnalyticsTitle.Size = new Size(181, 25);
            lblAnalyticsTitle.TabIndex = 1;
            lblAnalyticsTitle.Text = "QUEUE ANALYTICS";
            // 
            // lblAnalyticsNote
            // 
            lblAnalyticsNote.AutoSize = true;
            lblAnalyticsNote.BackColor = Color.Transparent;
            lblAnalyticsNote.Font = new Font("Segoe UI", 8F);
            lblAnalyticsNote.ForeColor = Color.FromArgb(100, 110, 105);
            lblAnalyticsNote.Location = new Point(28, 238);
            lblAnalyticsNote.Name = "lblAnalyticsNote";
            lblAnalyticsNote.Size = new Size(124, 13);
            lblAnalyticsNote.TabIndex = 2;
            lblAnalyticsNote.Text = "▦   Data for today only";
            // 
            // pnlActiveStatus
            // 
            pnlActiveStatus.BackColor = Color.White;
            pnlActiveStatus.BorderStyle = BorderStyle.FixedSingle;
            pnlActiveStatus.Controls.Add(lblActiveStatusIcon);
            pnlActiveStatus.Controls.Add(lblActiveStatusTitle);
            pnlActiveStatus.Controls.Add(pnlWindow1);
            pnlActiveStatus.Controls.Add(pnlWindow2);
            pnlActiveStatus.Controls.Add(pnlWindow3);
            pnlActiveStatus.Controls.Add(pnlWindow4);
            pnlActiveStatus.Location = new Point(571, 443);
            pnlActiveStatus.Name = "pnlActiveStatus";
            pnlActiveStatus.Size = new Size(468, 202);
            pnlActiveStatus.TabIndex = 0;
            // 
            // lblActiveStatusIcon
            // 
            lblActiveStatusIcon.AutoSize = true;
            lblActiveStatusIcon.BackColor = Color.Transparent;
            lblActiveStatusIcon.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblActiveStatusIcon.ForeColor = Color.FromArgb(0, 120, 58);
            lblActiveStatusIcon.Location = new Point(22, 10);
            lblActiveStatusIcon.Name = "lblActiveStatusIcon";
            lblActiveStatusIcon.Size = new Size(23, 25);
            lblActiveStatusIcon.TabIndex = 0;
            lblActiveStatusIcon.Text = "●";
            // 
            // lblActiveStatusTitle
            // 
            lblActiveStatusTitle.AutoSize = true;
            lblActiveStatusTitle.BackColor = Color.Transparent;
            lblActiveStatusTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblActiveStatusTitle.ForeColor = Color.FromArgb(0, 92, 48);
            lblActiveStatusTitle.Location = new Point(49, 13);
            lblActiveStatusTitle.Name = "lblActiveStatusTitle";
            lblActiveStatusTitle.Size = new Size(151, 25);
            lblActiveStatusTitle.TabIndex = 1;
            lblActiveStatusTitle.Text = "ACTIVE STATUS";
            // 
            // pnlWindow1
            // 
            pnlWindow1.BackColor = Color.FromArgb(248, 252, 249);
            pnlWindow1.Controls.Add(lblWindow1Dot);
            pnlWindow1.Controls.Add(lblWindow1Staff);
            pnlWindow1.Controls.Add(lblWindow1State);
            pnlWindow1.Controls.Add(lblWindow1Description);
            pnlWindow1.Controls.Add(btnWindow1);
            pnlWindow1.Location = new Point(18, 43);
            pnlWindow1.Name = "pnlWindow1";
            pnlWindow1.Size = new Size(430, 32);
            pnlWindow1.TabIndex = 2;
            // 
            // lblWindow1Dot
            // 
            lblWindow1Dot.AutoSize = true;
            lblWindow1Dot.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblWindow1Dot.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow1Dot.Location = new Point(8, 3);
            lblWindow1Dot.Name = "lblWindow1Dot";
            lblWindow1Dot.Size = new Size(17, 19);
            lblWindow1Dot.TabIndex = 0;
            lblWindow1Dot.Text = "●";
            // 
            // lblWindow1Staff
            // 
            lblWindow1Staff.AutoSize = true;
            lblWindow1Staff.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblWindow1Staff.ForeColor = Color.FromArgb(0, 92, 48);
            lblWindow1Staff.Location = new Point(25, 2);
            lblWindow1Staff.Name = "lblWindow1Staff";
            lblWindow1Staff.Size = new Size(88, 15);
            lblWindow1Staff.TabIndex = 1;
            lblWindow1Staff.Text = "Juan Dela Cruz";
            // 
            // lblWindow1State
            // 
            lblWindow1State.AutoSize = true;
            lblWindow1State.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            lblWindow1State.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow1State.Location = new Point(25, 17);
            lblWindow1State.Name = "lblWindow1State";
            lblWindow1State.Size = new Size(96, 12);
            lblWindow1State.TabIndex = 2;
            lblWindow1State.Text = "WINDOW 1   OPEN";
            // 
            // lblWindow1Description
            // 
            lblWindow1Description.AutoSize = true;
            lblWindow1Description.Font = new Font("Segoe UI", 7.5F);
            lblWindow1Description.ForeColor = Color.FromArgb(100, 110, 105);
            lblWindow1Description.Location = new Point(174, 9);
            lblWindow1Description.Name = "lblWindow1Description";
            lblWindow1Description.Size = new Size(109, 12);
            lblWindow1Description.TabIndex = 3;
            lblWindow1Description.Text = "Available for transaction";
            // 
            // btnWindow1
            // 
            btnWindow1.BackColor = Color.FromArgb(220, 245, 226);
            btnWindow1.Cursor = Cursors.Hand;
            btnWindow1.FlatAppearance.BorderSize = 0;
            btnWindow1.FlatStyle = FlatStyle.Flat;
            btnWindow1.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            btnWindow1.ForeColor = Color.FromArgb(0, 120, 58);
            btnWindow1.Location = new Point(382, 5);
            btnWindow1.Name = "btnWindow1";
            btnWindow1.Size = new Size(40, 22);
            btnWindow1.TabIndex = 0;
            btnWindow1.Text = "ON";
            btnWindow1.UseVisualStyleBackColor = false;
            // 
            // pnlWindow2
            // 
            pnlWindow2.BackColor = Color.FromArgb(248, 252, 249);
            pnlWindow2.Controls.Add(lblWindow2Dot);
            pnlWindow2.Controls.Add(lblWindow2Staff);
            pnlWindow2.Controls.Add(lblWindow2State);
            pnlWindow2.Controls.Add(lblWindow2Description);
            pnlWindow2.Controls.Add(btnWindow2);
            pnlWindow2.Location = new Point(18, 80);
            pnlWindow2.Name = "pnlWindow2";
            pnlWindow2.Size = new Size(430, 32);
            pnlWindow2.TabIndex = 3;
            // 
            // lblWindow2Dot
            // 
            lblWindow2Dot.AutoSize = true;
            lblWindow2Dot.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblWindow2Dot.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow2Dot.Location = new Point(8, 3);
            lblWindow2Dot.Name = "lblWindow2Dot";
            lblWindow2Dot.Size = new Size(17, 19);
            lblWindow2Dot.TabIndex = 0;
            lblWindow2Dot.Text = "●";
            // 
            // lblWindow2Staff
            // 
            lblWindow2Staff.AutoSize = true;
            lblWindow2Staff.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblWindow2Staff.ForeColor = Color.FromArgb(0, 92, 48);
            lblWindow2Staff.Location = new Point(25, 2);
            lblWindow2Staff.Name = "lblWindow2Staff";
            lblWindow2Staff.Size = new Size(78, 15);
            lblWindow2Staff.TabIndex = 1;
            lblWindow2Staff.Text = "Maria Santos";
            // 
            // lblWindow2State
            // 
            lblWindow2State.AutoSize = true;
            lblWindow2State.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            lblWindow2State.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow2State.Location = new Point(25, 17);
            lblWindow2State.Name = "lblWindow2State";
            lblWindow2State.Size = new Size(96, 12);
            lblWindow2State.TabIndex = 2;
            lblWindow2State.Text = "WINDOW 2   OPEN";
            // 
            // lblWindow2Description
            // 
            lblWindow2Description.AutoSize = true;
            lblWindow2Description.Font = new Font("Segoe UI", 7.5F);
            lblWindow2Description.ForeColor = Color.FromArgb(100, 110, 105);
            lblWindow2Description.Location = new Point(174, 9);
            lblWindow2Description.Name = "lblWindow2Description";
            lblWindow2Description.Size = new Size(109, 12);
            lblWindow2Description.TabIndex = 3;
            lblWindow2Description.Text = "Available for transaction";
            // 
            // btnWindow2
            // 
            btnWindow2.BackColor = Color.FromArgb(220, 245, 226);
            btnWindow2.Cursor = Cursors.Hand;
            btnWindow2.FlatAppearance.BorderSize = 0;
            btnWindow2.FlatStyle = FlatStyle.Flat;
            btnWindow2.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            btnWindow2.ForeColor = Color.FromArgb(0, 120, 58);
            btnWindow2.Location = new Point(382, 5);
            btnWindow2.Name = "btnWindow2";
            btnWindow2.Size = new Size(40, 22);
            btnWindow2.TabIndex = 1;
            btnWindow2.Text = "ON";
            btnWindow2.UseVisualStyleBackColor = false;
            // 
            // pnlWindow3
            // 
            pnlWindow3.BackColor = Color.FromArgb(248, 252, 249);
            pnlWindow3.Controls.Add(lblWindow3Dot);
            pnlWindow3.Controls.Add(lblWindow3Staff);
            pnlWindow3.Controls.Add(lblWindow3State);
            pnlWindow3.Controls.Add(lblWindow3Description);
            pnlWindow3.Controls.Add(btnWindow3);
            pnlWindow3.Location = new Point(18, 117);
            pnlWindow3.Name = "pnlWindow3";
            pnlWindow3.Size = new Size(430, 32);
            pnlWindow3.TabIndex = 4;
            // 
            // lblWindow3Dot
            // 
            lblWindow3Dot.AutoSize = true;
            lblWindow3Dot.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblWindow3Dot.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow3Dot.Location = new Point(8, 3);
            lblWindow3Dot.Name = "lblWindow3Dot";
            lblWindow3Dot.Size = new Size(17, 19);
            lblWindow3Dot.TabIndex = 0;
            lblWindow3Dot.Text = "●";
            // 
            // lblWindow3Staff
            // 
            lblWindow3Staff.AutoSize = true;
            lblWindow3Staff.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblWindow3Staff.ForeColor = Color.FromArgb(0, 92, 48);
            lblWindow3Staff.Location = new Point(25, 2);
            lblWindow3Staff.Name = "lblWindow3Staff";
            lblWindow3Staff.Size = new Size(97, 15);
            lblWindow3Staff.TabIndex = 1;
            lblWindow3Staff.Text = "Joshua Gonzales";
            // 
            // lblWindow3State
            // 
            lblWindow3State.AutoSize = true;
            lblWindow3State.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            lblWindow3State.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow3State.Location = new Point(25, 17);
            lblWindow3State.Name = "lblWindow3State";
            lblWindow3State.Size = new Size(96, 12);
            lblWindow3State.TabIndex = 2;
            lblWindow3State.Text = "WINDOW 3   OPEN";
            // 
            // lblWindow3Description
            // 
            lblWindow3Description.AutoSize = true;
            lblWindow3Description.Font = new Font("Segoe UI", 7.5F);
            lblWindow3Description.ForeColor = Color.FromArgb(100, 110, 105);
            lblWindow3Description.Location = new Point(174, 9);
            lblWindow3Description.Name = "lblWindow3Description";
            lblWindow3Description.Size = new Size(109, 12);
            lblWindow3Description.TabIndex = 3;
            lblWindow3Description.Text = "Available for transaction";
            // 
            // btnWindow3
            // 
            btnWindow3.BackColor = Color.FromArgb(220, 245, 226);
            btnWindow3.Cursor = Cursors.Hand;
            btnWindow3.FlatAppearance.BorderSize = 0;
            btnWindow3.FlatStyle = FlatStyle.Flat;
            btnWindow3.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            btnWindow3.ForeColor = Color.FromArgb(0, 120, 58);
            btnWindow3.Location = new Point(382, 5);
            btnWindow3.Name = "btnWindow3";
            btnWindow3.Size = new Size(40, 22);
            btnWindow3.TabIndex = 2;
            btnWindow3.Text = "ON";
            btnWindow3.UseVisualStyleBackColor = false;
            // 
            // pnlWindow4
            // 
            pnlWindow4.BackColor = Color.FromArgb(248, 252, 249);
            pnlWindow4.Controls.Add(lblWindow4Dot);
            pnlWindow4.Controls.Add(lblWindow4Staff);
            pnlWindow4.Controls.Add(lblWindow4State);
            pnlWindow4.Controls.Add(lblWindow4Description);
            pnlWindow4.Controls.Add(btnWindow4);
            pnlWindow4.Location = new Point(18, 154);
            pnlWindow4.Name = "pnlWindow4";
            pnlWindow4.Size = new Size(430, 32);
            pnlWindow4.TabIndex = 5;
            // 
            // lblWindow4Dot
            // 
            lblWindow4Dot.AutoSize = true;
            lblWindow4Dot.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblWindow4Dot.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow4Dot.Location = new Point(8, 3);
            lblWindow4Dot.Name = "lblWindow4Dot";
            lblWindow4Dot.Size = new Size(17, 19);
            lblWindow4Dot.TabIndex = 0;
            lblWindow4Dot.Text = "●";
            // 
            // lblWindow4Staff
            // 
            lblWindow4Staff.AutoSize = true;
            lblWindow4Staff.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblWindow4Staff.ForeColor = Color.FromArgb(0, 92, 48);
            lblWindow4Staff.Location = new Point(25, 2);
            lblWindow4Staff.Name = "lblWindow4Staff";
            lblWindow4Staff.Size = new Size(76, 15);
            lblWindow4Staff.TabIndex = 1;
            lblWindow4Staff.Text = "Pedro Reyes";
            // 
            // lblWindow4State
            // 
            lblWindow4State.AutoSize = true;
            lblWindow4State.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            lblWindow4State.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow4State.Location = new Point(25, 17);
            lblWindow4State.Name = "lblWindow4State";
            lblWindow4State.Size = new Size(96, 12);
            lblWindow4State.TabIndex = 2;
            lblWindow4State.Text = "WINDOW 4   OPEN";
            // 
            // lblWindow4Description
            // 
            lblWindow4Description.AutoSize = true;
            lblWindow4Description.Font = new Font("Segoe UI", 7.5F);
            lblWindow4Description.ForeColor = Color.FromArgb(100, 110, 105);
            lblWindow4Description.Location = new Point(174, 9);
            lblWindow4Description.Name = "lblWindow4Description";
            lblWindow4Description.Size = new Size(109, 12);
            lblWindow4Description.TabIndex = 3;
            lblWindow4Description.Text = "Available for transaction";
            // 
            // btnWindow4
            // 
            btnWindow4.BackColor = Color.FromArgb(220, 245, 226);
            btnWindow4.Cursor = Cursors.Hand;
            btnWindow4.FlatAppearance.BorderSize = 0;
            btnWindow4.FlatStyle = FlatStyle.Flat;
            btnWindow4.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            btnWindow4.ForeColor = Color.FromArgb(0, 120, 58);
            btnWindow4.Location = new Point(382, 5);
            btnWindow4.Name = "btnWindow4";
            btnWindow4.Size = new Size(40, 22);
            btnWindow4.TabIndex = 3;
            btnWindow4.Text = "ON";
            btnWindow4.UseVisualStyleBackColor = false;
            // 
            // Staff
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1047, 665);
            Controls.Add(pnlActiveStatus);
            Controls.Add(pnlAnalytics);
            Controls.Add(pnlLiveQueue);
            Controls.Add(btn_service);
            Controls.Add(labelTotal);
            Controls.Add(buttonServeNext);
            Controls.Add(buttonRefresh);
            Controls.Add(labelService);
            Controls.Add(comboBoxService);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MaximumSize = new Size(1063, 704);
            MinimumSize = new Size(1063, 704);
            Name = "Staff";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Registrar - Staff";
            Load += Staff_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewQueue).EndInit();
            pnlLiveQueue.ResumeLayout(false);
            pnlLiveQueue.PerformLayout();
            pnlAnalytics.ResumeLayout(false);
            pnlAnalytics.PerformLayout();
            pnlActiveStatus.ResumeLayout(false);
            pnlActiveStatus.PerformLayout();
            pnlWindow1.ResumeLayout(false);
            pnlWindow1.PerformLayout();
            pnlWindow2.ResumeLayout(false);
            pnlWindow2.PerformLayout();
            pnlWindow3.ResumeLayout(false);
            pnlWindow3.PerformLayout();
            pnlWindow4.ResumeLayout(false);
            pnlWindow4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}