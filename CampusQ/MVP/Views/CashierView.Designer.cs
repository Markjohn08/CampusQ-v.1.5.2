namespace CampusQ.MVP.Views
{
    partial class CashierView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // =========================================================
        // TOP CONTROLS
        // =========================================================

        private System.Windows.Forms.Label lblService;
        private System.Windows.Forms.ComboBox _cmbService;

        private System.Windows.Forms.Button _btnRefresh;
        private System.Windows.Forms.Button _btnServeNext;
        private System.Windows.Forms.Button _btnServiceWindow;

        private System.Windows.Forms.Label lblTotalCaption;
        private System.Windows.Forms.Label lblTotalValue;

        private System.Windows.Forms.Button btnNav1;
        private System.Windows.Forms.Button btnNav2;
        private System.Windows.Forms.Button btnNav3;

        // =========================================================
        // LIVE QUEUE
        // =========================================================

        private System.Windows.Forms.Panel pnlLiveQueue;
        private System.Windows.Forms.Label lblLiveQueueTitle;

        private System.Windows.Forms.DataGridView _dgvQueue;

        // =========================================================
        // ANALYTICS
        // =========================================================

        private System.Windows.Forms.Panel pnlAnalytics;

        private System.Windows.Forms.Label lblAnalyticsIcon;
        private System.Windows.Forms.Label lblAnalyticsTitle;

        private ScottPlot.WinForms.FormsPlot formsPlot1;

        private System.Windows.Forms.Label lblAnalyticsNote;

        // =========================================================
        // ACTIVE STATUS
        // =========================================================

        private System.Windows.Forms.Panel pnlActiveStatus;

        private System.Windows.Forms.Label lblActiveStatusIcon;
        private System.Windows.Forms.Label lblActiveStatusTitle;

        private System.Windows.Forms.Panel pnlWindow1;
        private System.Windows.Forms.Panel pnlWindow2;
        private System.Windows.Forms.Panel pnlWindow3;
        private System.Windows.Forms.Panel pnlWindow4;

        private System.Windows.Forms.Label lblWindow1;
        private System.Windows.Forms.Label lblWindow1State;
        private System.Windows.Forms.Label lblWindow1Description;
        private System.Windows.Forms.Button btnWindow1;

        private System.Windows.Forms.Label lblWindow2;
        private System.Windows.Forms.Label lblWindow2State;
        private System.Windows.Forms.Label lblWindow2Description;
        private System.Windows.Forms.Button btnWindow2;

        private System.Windows.Forms.Label lblWindow3;
        private System.Windows.Forms.Label lblWindow3State;
        private System.Windows.Forms.Label lblWindow3Description;
        private System.Windows.Forms.Button btnWindow3;

        private System.Windows.Forms.Label lblWindow4;
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

        /// <summary>
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashierView));
            lblService = new Label();
            _cmbService = new ComboBox();
            _btnRefresh = new Button();
            _btnServeNext = new Button();
            _btnServiceWindow = new Button();
            lblTotalCaption = new Label();
            lblTotalValue = new Label();
            btnNav1 = new Button();
            btnNav2 = new Button();
            btnNav3 = new Button();
            pnlLiveQueue = new Panel();
            lblLiveQueueTitle = new Label();
            _dgvQueue = new DataGridView();
            pnlAnalytics = new Panel();
            lblAnalyticsIcon = new Label();
            lblAnalyticsTitle = new Label();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            lblAnalyticsNote = new Label();
            pnlActiveStatus = new Panel();
            lblActiveStatusIcon = new Label();
            lblActiveStatusTitle = new Label();
            pnlWindow1 = new Panel();
            lblWindow1 = new Label();
            lblWindow1State = new Label();
            lblWindow1Description = new Label();
            btnWindow1 = new Button();
            pnlWindow2 = new Panel();
            lblWindow2 = new Label();
            lblWindow2State = new Label();
            lblWindow2Description = new Label();
            btnWindow2 = new Button();
            pnlWindow3 = new Panel();
            lblWindow3 = new Label();
            lblWindow3State = new Label();
            lblWindow3Description = new Label();
            btnWindow3 = new Button();
            pnlWindow4 = new Panel();
            lblWindow4 = new Label();
            lblWindow4State = new Label();
            lblWindow4Description = new Label();
            btnWindow4 = new Button();
            pnlLiveQueue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvQueue).BeginInit();
            pnlAnalytics.SuspendLayout();
            pnlActiveStatus.SuspendLayout();
            pnlWindow1.SuspendLayout();
            pnlWindow2.SuspendLayout();
            pnlWindow3.SuspendLayout();
            pnlWindow4.SuspendLayout();
            SuspendLayout();
            // 
            // lblService
            // 
            lblService.AutoSize = true;
            lblService.BackColor = Color.Transparent;
            lblService.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblService.ForeColor = Color.FromArgb(0, 92, 48);
            lblService.Location = new Point(130, 134);
            lblService.Name = "lblService";
            lblService.Size = new Size(66, 19);
            lblService.TabIndex = 2;
            lblService.Text = "SERVICE:";
            // 
            // _cmbService
            // 
            _cmbService.BackColor = Color.White;
            _cmbService.DropDownStyle = ComboBoxStyle.DropDownList;
            _cmbService.Font = new Font("Segoe UI", 10F);
            _cmbService.FormattingEnabled = true;
            _cmbService.Items.AddRange(new object[] { "All", "Window 1", "Window 2", "Window 3", "Window 4" });
            _cmbService.Location = new Point(202, 132);
            _cmbService.Name = "_cmbService";
            _cmbService.Size = new Size(149, 25);
            _cmbService.TabIndex = 3;
            // 
            // _btnRefresh
            // 
            _btnRefresh.BackColor = Color.FromArgb(0, 120, 58);
            _btnRefresh.FlatAppearance.BorderSize = 0;
            _btnRefresh.FlatStyle = FlatStyle.Flat;
            _btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnRefresh.ForeColor = Color.White;
            _btnRefresh.Location = new Point(446, 111);
            _btnRefresh.Name = "_btnRefresh";
            _btnRefresh.Size = new Size(149, 42);
            _btnRefresh.TabIndex = 4;
            _btnRefresh.Text = "↻   Refresh Queue";
            _btnRefresh.UseVisualStyleBackColor = false;
            // 
            // _btnServeNext
            // 
            _btnServeNext.BackColor = Color.FromArgb(0, 120, 58);
            _btnServeNext.FlatAppearance.BorderSize = 0;
            _btnServeNext.FlatStyle = FlatStyle.Flat;
            _btnServeNext.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnServeNext.ForeColor = Color.White;
            _btnServeNext.Location = new Point(605, 111);
            _btnServeNext.Name = "_btnServeNext";
            _btnServeNext.Size = new Size(149, 42);
            _btnServeNext.TabIndex = 5;
            _btnServeNext.Text = "▶   Serve Next";
            _btnServeNext.UseVisualStyleBackColor = false;
            // 
            // _btnServiceWindow
            // 
            _btnServiceWindow.BackColor = Color.White;
            _btnServiceWindow.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 58);
            _btnServiceWindow.FlatAppearance.BorderSize = 0;
            _btnServiceWindow.FlatStyle = FlatStyle.Flat;
            _btnServiceWindow.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnServiceWindow.ForeColor = Color.FromArgb(30, 30, 30);
            _btnServiceWindow.Location = new Point(764, 111);
            _btnServiceWindow.Name = "_btnServiceWindow";
            _btnServiceWindow.Size = new Size(149, 42);
            _btnServiceWindow.TabIndex = 6;
            _btnServiceWindow.Text = "✓   Service Window";
            _btnServiceWindow.UseVisualStyleBackColor = false;
            // 
            // lblTotalCaption
            // 
            lblTotalCaption.AutoSize = true;
            lblTotalCaption.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalCaption.ForeColor = Color.FromArgb(0, 92, 48);
            lblTotalCaption.Location = new Point(952, 33);
            lblTotalCaption.Name = "lblTotalCaption";
            lblTotalCaption.Size = new Size(46, 15);
            lblTotalCaption.TabIndex = 7;
            lblTotalCaption.Text = "TOTAL:";
            // 
            // lblTotalValue
            // 
            lblTotalValue.AutoSize = true;
            lblTotalValue.BackColor = Color.White;
            lblTotalValue.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.FromArgb(0, 92, 48);
            lblTotalValue.Location = new Point(954, 45);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(24, 28);
            lblTotalValue.TabIndex = 8;
            lblTotalValue.Text = "1";
            // 
            // btnNav1
            // 
            btnNav1.BackColor = Color.White;
            btnNav1.FlatAppearance.BorderColor = Color.FromArgb(200, 220, 207);
            btnNav1.FlatStyle = FlatStyle.Flat;
            btnNav1.Font = new Font("Segoe UI Symbol", 17F, FontStyle.Bold);
            btnNav1.ForeColor = Color.FromArgb(0, 92, 48);
            btnNav1.Location = new Point(10, 164);
            btnNav1.Name = "btnNav1";
            btnNav1.Size = new Size(54, 68);
            btnNav1.TabIndex = 0;
            btnNav1.Text = "Cah";
            btnNav1.UseVisualStyleBackColor = false;
            // 
            // btnNav2
            // 
            btnNav2.BackColor = Color.FromArgb(232, 246, 236);
            btnNav2.FlatAppearance.BorderColor = Color.FromArgb(200, 220, 207);
            btnNav2.FlatStyle = FlatStyle.Flat;
            btnNav2.Font = new Font("Segoe UI Symbol", 17F, FontStyle.Bold);
            btnNav2.ForeColor = Color.FromArgb(0, 92, 48);
            btnNav2.Location = new Point(10, 312);
            btnNav2.Name = "btnNav2";
            btnNav2.Size = new Size(54, 68);
            btnNav2.TabIndex = 1;
            btnNav2.Text = "▣";
            btnNav2.UseVisualStyleBackColor = false;
            // 
            // btnNav3
            // 
            btnNav3.BackColor = Color.White;
            btnNav3.FlatAppearance.BorderColor = Color.FromArgb(200, 220, 207);
            btnNav3.FlatStyle = FlatStyle.Flat;
            btnNav3.Font = new Font("Segoe UI Symbol", 17F, FontStyle.Bold);
            btnNav3.ForeColor = Color.FromArgb(0, 92, 48);
            btnNav3.Location = new Point(10, 238);
            btnNav3.Name = "btnNav3";
            btnNav3.Size = new Size(54, 68);
            btnNav3.TabIndex = 2;
            btnNav3.Text = "◆";
            btnNav3.UseVisualStyleBackColor = false;
            // 
            // pnlLiveQueue
            // 
            pnlLiveQueue.BackColor = Color.White;
            pnlLiveQueue.BorderStyle = BorderStyle.FixedSingle;
            pnlLiveQueue.Controls.Add(lblLiveQueueTitle);
            pnlLiveQueue.Controls.Add(_dgvQueue);
            pnlLiveQueue.Location = new Point(74, 164);
            pnlLiveQueue.Name = "pnlLiveQueue";
            pnlLiveQueue.Size = new Size(482, 478);
            pnlLiveQueue.TabIndex = 12;
            // 
            // lblLiveQueueTitle
            // 
            lblLiveQueueTitle.AutoSize = true;
            lblLiveQueueTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblLiveQueueTitle.ForeColor = Color.FromArgb(0, 92, 48);
            lblLiveQueueTitle.Location = new Point(16, 20);
            lblLiveQueueTitle.Name = "lblLiveQueueTitle";
            lblLiveQueueTitle.Size = new Size(157, 28);
            lblLiveQueueTitle.TabIndex = 1;
            lblLiveQueueTitle.Text = "🔊 LIVE QUEUE";
            // 
            // _dgvQueue
            // 
            _dgvQueue.AllowUserToAddRows = false;
            _dgvQueue.AllowUserToDeleteRows = false;
            _dgvQueue.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(252, 254, 253);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(45, 55, 50);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(225, 246, 231);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(0, 92, 48);
            _dgvQueue.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            _dgvQueue.BackgroundColor = Color.White;
            _dgvQueue.BorderStyle = BorderStyle.None;
            _dgvQueue.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            _dgvQueue.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(35, 45, 40);
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(35, 45, 40);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            _dgvQueue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            _dgvQueue.ColumnHeadersHeight = 42;
            _dgvQueue.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(45, 55, 50);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(225, 246, 231);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(0, 92, 48);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            _dgvQueue.DefaultCellStyle = dataGridViewCellStyle3;
            _dgvQueue.EnableHeadersVisualStyles = false;
            _dgvQueue.GridColor = Color.FromArgb(232, 238, 234);
            _dgvQueue.Location = new Point(16, 68);
            _dgvQueue.MultiSelect = false;
            _dgvQueue.Name = "_dgvQueue";
            _dgvQueue.ReadOnly = true;
            _dgvQueue.RowHeadersVisible = false;
            _dgvQueue.RowTemplate.Height = 42;
            _dgvQueue.RowTemplate.Resizable = DataGridViewTriState.False;
            _dgvQueue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvQueue.ShowCellErrors = false;
            _dgvQueue.ShowCellToolTips = false;
            _dgvQueue.ShowEditingIcon = false;
            _dgvQueue.ShowRowErrors = false;
            _dgvQueue.Size = new Size(448, 394);
            _dgvQueue.TabIndex = 2;
            // 
            // pnlAnalytics
            // 
            pnlAnalytics.BackColor = Color.White;
            pnlAnalytics.BorderStyle = BorderStyle.FixedSingle;
            pnlAnalytics.Controls.Add(lblAnalyticsIcon);
            pnlAnalytics.Controls.Add(lblAnalyticsTitle);
            pnlAnalytics.Controls.Add(formsPlot1);
            pnlAnalytics.Controls.Add(lblAnalyticsNote);
            pnlAnalytics.Location = new Point(569, 164);
            pnlAnalytics.Name = "pnlAnalytics";
            pnlAnalytics.Size = new Size(468, 267);
            pnlAnalytics.TabIndex = 13;
            // 
            // lblAnalyticsIcon
            // 
            lblAnalyticsIcon.AutoSize = true;
            lblAnalyticsIcon.Font = new Font("Segoe UI Symbol", 17F, FontStyle.Bold);
            lblAnalyticsIcon.ForeColor = Color.FromArgb(0, 92, 48);
            lblAnalyticsIcon.Location = new Point(17, 5);
            lblAnalyticsIcon.Name = "lblAnalyticsIcon";
            lblAnalyticsIcon.Size = new Size(35, 31);
            lblAnalyticsIcon.TabIndex = 0;
            lblAnalyticsIcon.Text = "▥";
            // 
            // lblAnalyticsTitle
            // 
            lblAnalyticsTitle.AutoSize = true;
            lblAnalyticsTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblAnalyticsTitle.ForeColor = Color.FromArgb(0, 92, 48);
            lblAnalyticsTitle.Location = new Point(47, 7);
            lblAnalyticsTitle.Name = "lblAnalyticsTitle";
            lblAnalyticsTitle.Size = new Size(189, 28);
            lblAnalyticsTitle.TabIndex = 1;
            lblAnalyticsTitle.Text = "QUEUE ANALYTICS";
            // 
            // formsPlot1
            // 
            formsPlot1.BackColor = Color.White;
            formsPlot1.Location = new Point(12, 46);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(442, 186);
            formsPlot1.TabIndex = 2;
            // 
            // lblAnalyticsNote
            // 
            lblAnalyticsNote.AutoSize = true;
            lblAnalyticsNote.Font = new Font("Segoe UI", 8.5F);
            lblAnalyticsNote.ForeColor = Color.FromArgb(85, 95, 90);
            lblAnalyticsNote.Location = new Point(38, 238);
            lblAnalyticsNote.Name = "lblAnalyticsNote";
            lblAnalyticsNote.Size = new Size(127, 15);
            lblAnalyticsNote.TabIndex = 3;
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
            pnlActiveStatus.Location = new Point(569, 437);
            pnlActiveStatus.Name = "pnlActiveStatus";
            pnlActiveStatus.Size = new Size(468, 214);
            pnlActiveStatus.TabIndex = 14;
            // 
            // lblActiveStatusIcon
            // 
            lblActiveStatusIcon.AutoSize = true;
            lblActiveStatusIcon.Font = new Font("Segoe UI Symbol", 17F, FontStyle.Bold);
            lblActiveStatusIcon.ForeColor = Color.FromArgb(0, 92, 48);
            lblActiveStatusIcon.Location = new Point(16, 0);
            lblActiveStatusIcon.Name = "lblActiveStatusIcon";
            lblActiveStatusIcon.Size = new Size(35, 31);
            lblActiveStatusIcon.TabIndex = 0;
            lblActiveStatusIcon.Text = "●";
            // 
            // lblActiveStatusTitle
            // 
            lblActiveStatusTitle.AutoSize = true;
            lblActiveStatusTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblActiveStatusTitle.ForeColor = Color.FromArgb(0, 92, 48);
            lblActiveStatusTitle.Location = new Point(47, 3);
            lblActiveStatusTitle.Name = "lblActiveStatusTitle";
            lblActiveStatusTitle.Size = new Size(158, 28);
            lblActiveStatusTitle.TabIndex = 1;
            lblActiveStatusTitle.Text = "ACTIVE STATUS";
            // 
            // pnlWindow1
            // 
            pnlWindow1.BackColor = Color.FromArgb(248, 252, 249);
            pnlWindow1.Controls.Add(lblWindow1);
            pnlWindow1.Controls.Add(lblWindow1State);
            pnlWindow1.Controls.Add(lblWindow1Description);
            pnlWindow1.Controls.Add(btnWindow1);
            pnlWindow1.Location = new Point(16, 34);
            pnlWindow1.Name = "pnlWindow1";
            pnlWindow1.Size = new Size(437, 42);
            pnlWindow1.TabIndex = 2;
            // 
            // lblWindow1
            // 
            lblWindow1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblWindow1.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow1.Location = new Point(12, 2);
            lblWindow1.Name = "lblWindow1";
            lblWindow1.Size = new Size(150, 19);
            lblWindow1.TabIndex = 0;
            lblWindow1.Text = "●  Juan Dela Cruz";
            lblWindow1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblWindow1State
            // 
            lblWindow1State.Font = new Font("Segoe UI", 8F);
            lblWindow1State.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow1State.Location = new Point(12, 20);
            lblWindow1State.Name = "lblWindow1State";
            lblWindow1State.Size = new Size(150, 17);
            lblWindow1State.TabIndex = 1;
            lblWindow1State.Text = "WINDOW 1    OPEN";
            lblWindow1State.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblWindow1Description
            // 
            lblWindow1Description.Font = new Font("Segoe UI", 8F);
            lblWindow1Description.ForeColor = Color.FromArgb(70, 80, 75);
            lblWindow1Description.Location = new Point(174, 9);
            lblWindow1Description.Name = "lblWindow1Description";
            lblWindow1Description.Size = new Size(175, 25);
            lblWindow1Description.TabIndex = 2;
            lblWindow1Description.Text = "Available for transaction";
            lblWindow1Description.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnWindow1
            // 
            btnWindow1.BackColor = Color.FromArgb(225, 246, 231);
            btnWindow1.FlatAppearance.BorderSize = 0;
            btnWindow1.FlatStyle = FlatStyle.Flat;
            btnWindow1.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnWindow1.ForeColor = Color.FromArgb(0, 120, 58);
            btnWindow1.Location = new Point(385, 4);
            btnWindow1.Name = "btnWindow1";
            btnWindow1.Size = new Size(40, 22);
            btnWindow1.TabIndex = 3;
            btnWindow1.Text = "ON";
            btnWindow1.UseVisualStyleBackColor = false;
            // 
            // pnlWindow2
            // 
            pnlWindow2.BackColor = Color.FromArgb(248, 252, 249);
            pnlWindow2.Controls.Add(lblWindow2);
            pnlWindow2.Controls.Add(lblWindow2State);
            pnlWindow2.Controls.Add(lblWindow2Description);
            pnlWindow2.Controls.Add(btnWindow2);
            pnlWindow2.Location = new Point(16, 76);
            pnlWindow2.Name = "pnlWindow2";
            pnlWindow2.Size = new Size(437, 42);
            pnlWindow2.TabIndex = 3;
            // 
            // lblWindow2
            // 
            lblWindow2.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblWindow2.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow2.Location = new Point(12, 2);
            lblWindow2.Name = "lblWindow2";
            lblWindow2.Size = new Size(150, 19);
            lblWindow2.TabIndex = 0;
            lblWindow2.Text = "●  Maria Santos";
            lblWindow2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblWindow2State
            // 
            lblWindow2State.Font = new Font("Segoe UI", 8F);
            lblWindow2State.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow2State.Location = new Point(12, 20);
            lblWindow2State.Name = "lblWindow2State";
            lblWindow2State.Size = new Size(150, 17);
            lblWindow2State.TabIndex = 1;
            lblWindow2State.Text = "WINDOW 2    OPEN";
            lblWindow2State.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblWindow2Description
            // 
            lblWindow2Description.Font = new Font("Segoe UI", 8F);
            lblWindow2Description.ForeColor = Color.FromArgb(70, 80, 75);
            lblWindow2Description.Location = new Point(174, 9);
            lblWindow2Description.Name = "lblWindow2Description";
            lblWindow2Description.Size = new Size(175, 25);
            lblWindow2Description.TabIndex = 2;
            lblWindow2Description.Text = "Available for transaction";
            lblWindow2Description.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnWindow2
            // 
            btnWindow2.BackColor = Color.FromArgb(225, 246, 231);
            btnWindow2.FlatAppearance.BorderSize = 0;
            btnWindow2.FlatStyle = FlatStyle.Flat;
            btnWindow2.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnWindow2.ForeColor = Color.FromArgb(0, 120, 58);
            btnWindow2.Location = new Point(385, 4);
            btnWindow2.Name = "btnWindow2";
            btnWindow2.Size = new Size(40, 22);
            btnWindow2.TabIndex = 3;
            btnWindow2.Text = "ON";
            btnWindow2.UseVisualStyleBackColor = false;
            // 
            // pnlWindow3
            // 
            pnlWindow3.BackColor = Color.FromArgb(248, 252, 249);
            pnlWindow3.Controls.Add(lblWindow3);
            pnlWindow3.Controls.Add(lblWindow3State);
            pnlWindow3.Controls.Add(lblWindow3Description);
            pnlWindow3.Controls.Add(btnWindow3);
            pnlWindow3.Location = new Point(16, 118);
            pnlWindow3.Name = "pnlWindow3";
            pnlWindow3.Size = new Size(437, 42);
            pnlWindow3.TabIndex = 4;
            // 
            // lblWindow3
            // 
            lblWindow3.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblWindow3.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow3.Location = new Point(12, 2);
            lblWindow3.Name = "lblWindow3";
            lblWindow3.Size = new Size(150, 19);
            lblWindow3.TabIndex = 0;
            lblWindow3.Text = "●  Joshua Gonzales";
            lblWindow3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblWindow3State
            // 
            lblWindow3State.Font = new Font("Segoe UI", 8F);
            lblWindow3State.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow3State.Location = new Point(12, 20);
            lblWindow3State.Name = "lblWindow3State";
            lblWindow3State.Size = new Size(150, 17);
            lblWindow3State.TabIndex = 1;
            lblWindow3State.Text = "WINDOW 3    OPEN";
            lblWindow3State.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblWindow3Description
            // 
            lblWindow3Description.Font = new Font("Segoe UI", 8F);
            lblWindow3Description.ForeColor = Color.FromArgb(70, 80, 75);
            lblWindow3Description.Location = new Point(174, 9);
            lblWindow3Description.Name = "lblWindow3Description";
            lblWindow3Description.Size = new Size(175, 25);
            lblWindow3Description.TabIndex = 2;
            lblWindow3Description.Text = "Available for transaction";
            lblWindow3Description.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnWindow3
            // 
            btnWindow3.BackColor = Color.FromArgb(225, 246, 231);
            btnWindow3.FlatAppearance.BorderSize = 0;
            btnWindow3.FlatStyle = FlatStyle.Flat;
            btnWindow3.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnWindow3.ForeColor = Color.FromArgb(0, 120, 58);
            btnWindow3.Location = new Point(385, 4);
            btnWindow3.Name = "btnWindow3";
            btnWindow3.Size = new Size(40, 22);
            btnWindow3.TabIndex = 3;
            btnWindow3.Text = "ON";
            btnWindow3.UseVisualStyleBackColor = false;
            // 
            // pnlWindow4
            // 
            pnlWindow4.BackColor = Color.FromArgb(248, 252, 249);
            pnlWindow4.Controls.Add(lblWindow4);
            pnlWindow4.Controls.Add(lblWindow4State);
            pnlWindow4.Controls.Add(lblWindow4Description);
            pnlWindow4.Controls.Add(btnWindow4);
            pnlWindow4.Location = new Point(16, 160);
            pnlWindow4.Name = "pnlWindow4";
            pnlWindow4.Size = new Size(437, 42);
            pnlWindow4.TabIndex = 5;
            // 
            // lblWindow4
            // 
            lblWindow4.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblWindow4.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow4.Location = new Point(12, 2);
            lblWindow4.Name = "lblWindow4";
            lblWindow4.Size = new Size(150, 19);
            lblWindow4.TabIndex = 0;
            lblWindow4.Text = "●  Pedro Reyes";
            lblWindow4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblWindow4State
            // 
            lblWindow4State.Font = new Font("Segoe UI", 8F);
            lblWindow4State.ForeColor = Color.FromArgb(0, 120, 58);
            lblWindow4State.Location = new Point(12, 20);
            lblWindow4State.Name = "lblWindow4State";
            lblWindow4State.Size = new Size(150, 17);
            lblWindow4State.TabIndex = 1;
            lblWindow4State.Text = "WINDOW 4    OPEN";
            lblWindow4State.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblWindow4Description
            // 
            lblWindow4Description.Font = new Font("Segoe UI", 8F);
            lblWindow4Description.ForeColor = Color.FromArgb(70, 80, 75);
            lblWindow4Description.Location = new Point(174, 9);
            lblWindow4Description.Name = "lblWindow4Description";
            lblWindow4Description.Size = new Size(175, 25);
            lblWindow4Description.TabIndex = 2;
            lblWindow4Description.Text = "Available for transaction";
            lblWindow4Description.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnWindow4
            // 
            btnWindow4.BackColor = Color.FromArgb(225, 246, 231);
            btnWindow4.FlatAppearance.BorderSize = 0;
            btnWindow4.FlatStyle = FlatStyle.Flat;
            btnWindow4.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            btnWindow4.ForeColor = Color.FromArgb(0, 120, 58);
            btnWindow4.Location = new Point(385, 4);
            btnWindow4.Name = "btnWindow4";
            btnWindow4.Size = new Size(40, 22);
            btnWindow4.TabIndex = 3;
            btnWindow4.Text = "ON";
            btnWindow4.UseVisualStyleBackColor = false;
            // 
            // CashierView
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1063, 704);
            Controls.Add(btnNav1);
            Controls.Add(btnNav2);
            Controls.Add(btnNav3);
            Controls.Add(lblService);
            Controls.Add(_cmbService);
            Controls.Add(_btnRefresh);
            Controls.Add(_btnServeNext);
            Controls.Add(_btnServiceWindow);
            Controls.Add(lblTotalCaption);
            Controls.Add(lblTotalValue);
            Controls.Add(pnlLiveQueue);
            Controls.Add(pnlAnalytics);
            Controls.Add(pnlActiveStatus);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MaximumSize = new Size(1079, 743);
            MinimumSize = new Size(1079, 743);
            Name = "CashierView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cashier - Staff";
            Load += CashierView_Load;
            pnlLiveQueue.ResumeLayout(false);
            pnlLiveQueue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvQueue).EndInit();
            pnlAnalytics.ResumeLayout(false);
            pnlAnalytics.PerformLayout();
            pnlActiveStatus.ResumeLayout(false);
            pnlActiveStatus.PerformLayout();
            pnlWindow1.ResumeLayout(false);
            pnlWindow2.ResumeLayout(false);
            pnlWindow3.ResumeLayout(false);
            pnlWindow4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // =========================================================
        // ROUNDED BUTTON HELPER
        // =========================================================

        private void ApplyRoundedButtons()
        {
            SetRoundedButton(
                _btnRefresh,
                12
            );

            SetRoundedButton(
                _btnServeNext,
                12
            );

            SetRoundedButton(
                _btnServiceWindow,
                12
            );

            SetRoundedButton(
                btnWindow1,
                8
            );

            SetRoundedButton(
                btnWindow2,
                8
            );

            SetRoundedButton(
                btnWindow3,
                8
            );

            SetRoundedButton(
                btnWindow4,
                8
            );
        }

        // =========================================================
        // ROUNDED BUTTON
        // =========================================================

        private void SetRoundedButton(
            System.Windows.Forms.Button button,
            int radius)
        {
            if (button == null)
                return;

            System.Drawing.Drawing2D.GraphicsPath path =
                new System.Drawing.Drawing2D.GraphicsPath();

            int width =
                button.Width;

            int height =
                button.Height;

            int diameter =
                radius * 2;

            // Safety
            if (width <= 0 || height <= 0)
                return;

            if (diameter > width)
                diameter = width;

            if (diameter > height)
                diameter = height;

            path.AddArc(
                0,
                0,
                diameter,
                diameter,
                180,
                90
            );

            path.AddArc(
                width - diameter,
                0,
                diameter,
                diameter,
                270,
                90
            );

            path.AddArc(
                width - diameter,
                height - diameter,
                diameter,
                diameter,
                0,
                90
            );

            path.AddArc(
                0,
                height - diameter,
                diameter,
                diameter,
                90,
                90
            );

            path.CloseFigure();

            button.Region =
                new System.Drawing.Region(
                    path
                );
        }
    }
}