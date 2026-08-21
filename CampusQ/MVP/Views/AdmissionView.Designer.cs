namespace CampusQ.MVP.Views
{
    partial class AdmissionView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // =========================================================
        // MAIN CONTROLS
        // =========================================================

        private DataGridView _dgvQueue;
        private ComboBox _cmbService;

        private Button _btnServeNext;
        private Button _btnRefresh;
        private Button _btnServiceWindow;

        private ScottPlot.WinForms.FormsPlot _formsPlotAnalytics;

        // =========================================================
        // LABELS
        // =========================================================

        private Label lblService;
        private Label lblTotal;

        private Label lblLiveQueue;
        private Label lblQueueAnalytics;
        private Label lblActiveStatus;

        // =========================================================
        // PANELS
        // =========================================================

        private Panel panelQueue;
        private Panel panelAnalytics;
        private Panel panelActiveStatus;

        // =========================================================
        // ACTIVE STATUS - WINDOW 1
        // =========================================================

        private Label lblStatusDot1;
        private Label lblWindow1Name;
        private Label lblWindow1Info;
        private Label lblWindow1Description;

        private Button btnWindow1;

        // =========================================================
        // ACTIVE STATUS - WINDOW 2
        // =========================================================

        private Label lblStatusDot2;
        private Label lblWindow2Name;
        private Label lblWindow2Info;
        private Label lblWindow2Description;

        private Button btnWindow2;

        // =========================================================
        // DISPOSE
        // =========================================================

        protected override void Dispose(bool disposing)
        {
            if (disposing &&
                (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdmissionView));
            _cmbService = new ComboBox();
            _btnServeNext = new Button();
            _btnRefresh = new Button();
            _btnServiceWindow = new Button();
            _dgvQueue = new DataGridView();
            _formsPlotAnalytics = new ScottPlot.WinForms.FormsPlot();
            lblService = new Label();
            lblTotal = new Label();
            lblLiveQueue = new Label();
            lblQueueAnalytics = new Label();
            lblActiveStatus = new Label();
            panelQueue = new Panel();
            panelAnalytics = new Panel();
            panelActiveStatus = new Panel();
            lblStatusDot1 = new Label();
            lblWindow1Name = new Label();
            lblWindow1Info = new Label();
            lblWindow1Description = new Label();
            btnWindow1 = new Button();
            lblStatusDot2 = new Label();
            lblWindow2Name = new Label();
            lblWindow2Info = new Label();
            lblWindow2Description = new Label();
            btnWindow2 = new Button();
            ((System.ComponentModel.ISupportInitialize)_dgvQueue).BeginInit();
            panelQueue.SuspendLayout();
            panelAnalytics.SuspendLayout();
            panelActiveStatus.SuspendLayout();
            SuspendLayout();
            // 
            // _cmbService
            // 
            _cmbService.DropDownStyle = ComboBoxStyle.DropDownList;
            _cmbService.FormattingEnabled = true;
            _cmbService.Items.AddRange(new object[] { "All", "Window1", "Window2" });
            _cmbService.Location = new Point(210, 152);
            _cmbService.Name = "_cmbService";
            _cmbService.Size = new Size(150, 23);
            _cmbService.TabIndex = 0;
            // 
            // _btnServeNext
            // 
            _btnServeNext.BackColor = Color.FromArgb(0, 105, 55);
            _btnServeNext.FlatAppearance.BorderSize = 0;
            _btnServeNext.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 105, 55);
            _btnServeNext.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 105, 55);
            _btnServeNext.FlatStyle = FlatStyle.Flat;
            _btnServeNext.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _btnServeNext.ForeColor = Color.White;
            _btnServeNext.Location = new Point(614, 137);
            _btnServeNext.Name = "_btnServeNext";
            _btnServeNext.Size = new Size(145, 42);
            _btnServeNext.TabIndex = 2;
            _btnServeNext.Text = "▶   Serve Next";
            _btnServeNext.UseVisualStyleBackColor = false;
            // 
            // _btnRefresh
            // 
            _btnRefresh.BackColor = Color.FromArgb(0, 105, 55);
            _btnRefresh.FlatAppearance.BorderSize = 0;
            _btnRefresh.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 105, 55);
            _btnRefresh.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 105, 55);
            _btnRefresh.FlatStyle = FlatStyle.Flat;
            _btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _btnRefresh.ForeColor = Color.White;
            _btnRefresh.Location = new Point(458, 136);
            _btnRefresh.Name = "_btnRefresh";
            _btnRefresh.Size = new Size(145, 42);
            _btnRefresh.TabIndex = 1;
            _btnRefresh.Text = "↻   Refresh Queue";
            _btnRefresh.UseVisualStyleBackColor = false;
            // 
            // _btnServiceWindow
            // 
            _btnServiceWindow.BackColor = Color.White;
            _btnServiceWindow.FlatAppearance.BorderColor = Color.FromArgb(0, 105, 55);
            _btnServiceWindow.FlatAppearance.BorderSize = 0;
            _btnServiceWindow.FlatAppearance.MouseDownBackColor = Color.White;
            _btnServiceWindow.FlatAppearance.MouseOverBackColor = Color.White;
            _btnServiceWindow.FlatStyle = FlatStyle.Flat;
            _btnServiceWindow.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _btnServiceWindow.ForeColor = Color.FromArgb(0, 92, 48);
            _btnServiceWindow.Location = new Point(766, 137);
            _btnServiceWindow.Name = "_btnServiceWindow";
            _btnServiceWindow.Size = new Size(145, 42);
            _btnServiceWindow.TabIndex = 3;
            _btnServiceWindow.Text = "✓   Service";
            _btnServiceWindow.UseVisualStyleBackColor = false;
            // 
            // _dgvQueue
            // 
            _dgvQueue.AllowUserToAddRows = false;
            _dgvQueue.AllowUserToDeleteRows = false;
            _dgvQueue.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(252, 253, 252);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 8.5F);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(45, 55, 50);
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(220, 245, 226);
            dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(0, 92, 48);
            _dgvQueue.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            _dgvQueue.BackgroundColor = Color.White;
            _dgvQueue.BorderStyle = BorderStyle.None;
            _dgvQueue.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            _dgvQueue.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(25, 35, 30);
            dataGridViewCellStyle2.SelectionBackColor = Color.White;
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(25, 35, 30);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            _dgvQueue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            _dgvQueue.ColumnHeadersHeight = 40;
            _dgvQueue.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 8.5F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(45, 55, 50);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(220, 245, 226);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(0, 92, 48);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            _dgvQueue.DefaultCellStyle = dataGridViewCellStyle3;
            _dgvQueue.EnableHeadersVisualStyles = false;
            _dgvQueue.GridColor = Color.FromArgb(230, 235, 232);
            _dgvQueue.Location = new Point(20, 67);
            _dgvQueue.MultiSelect = false;
            _dgvQueue.Name = "_dgvQueue";
            _dgvQueue.ReadOnly = true;
            _dgvQueue.RowHeadersVisible = false;
            _dgvQueue.RowTemplate.Height = 38;
            _dgvQueue.ScrollBars = ScrollBars.Horizontal;
            _dgvQueue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvQueue.Size = new Size(443, 383);
            _dgvQueue.TabIndex = 4;
            _dgvQueue.CellContentClick += _dgvQueue_CellContentClick;
            // 
            // _formsPlotAnalytics
            // 
            _formsPlotAnalytics.BackColor = Color.White;
            _formsPlotAnalytics.Location = new Point(15, 47);
            _formsPlotAnalytics.Name = "_formsPlotAnalytics";
            _formsPlotAnalytics.Size = new Size(433, 200);
            _formsPlotAnalytics.TabIndex = 5;
            // 
            // lblService
            // 
            lblService.AutoSize = true;
            lblService.BackColor = Color.Transparent;
            lblService.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblService.ForeColor = Color.FromArgb(0, 92, 48);
            lblService.Location = new Point(148, 156);
            lblService.Name = "lblService";
            lblService.Size = new Size(56, 15);
            lblService.TabIndex = 10;
            lblService.Text = "SERVICE:";
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotal.AutoSize = true;
            lblTotal.BackColor = Color.White;
            lblTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(0, 92, 48);
            lblTotal.Location = new Point(958, 58);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(47, 15);
            lblTotal.TabIndex = 11;
            lblTotal.Text = "Total: 0";
            // 
            // lblLiveQueue
            // 
            lblLiveQueue.AutoSize = true;
            lblLiveQueue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblLiveQueue.ForeColor = Color.FromArgb(0, 92, 48);
            lblLiveQueue.Location = new Point(20, 16);
            lblLiveQueue.Name = "lblLiveQueue";
            lblLiveQueue.Size = new Size(123, 20);
            lblLiveQueue.TabIndex = 0;
            lblLiveQueue.Text = "🔊  LIVE QUEUE";
            // 
            // lblQueueAnalytics
            // 
            lblQueueAnalytics.AutoSize = true;
            lblQueueAnalytics.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblQueueAnalytics.ForeColor = Color.FromArgb(0, 92, 48);
            lblQueueAnalytics.Location = new Point(20, 16);
            lblQueueAnalytics.Name = "lblQueueAnalytics";
            lblQueueAnalytics.Size = new Size(177, 20);
            lblQueueAnalytics.TabIndex = 0;
            lblQueueAnalytics.Text = "📶   QUEUE ANALYTICS";
            // 
            // lblActiveStatus
            // 
            lblActiveStatus.AutoSize = true;
            lblActiveStatus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblActiveStatus.ForeColor = Color.FromArgb(0, 92, 48);
            lblActiveStatus.Location = new Point(20, 12);
            lblActiveStatus.Name = "lblActiveStatus";
            lblActiveStatus.Size = new Size(136, 20);
            lblActiveStatus.TabIndex = 0;
            lblActiveStatus.Text = "●  ACTIVE STATUS";
            // 
            // panelQueue
            // 
            panelQueue.BackColor = Color.White;
            panelQueue.BorderStyle = BorderStyle.FixedSingle;
            panelQueue.Controls.Add(_dgvQueue);
            panelQueue.Controls.Add(lblLiveQueue);
            panelQueue.Location = new Point(74, 190);
            panelQueue.Name = "panelQueue";
            panelQueue.Size = new Size(485, 477);
            panelQueue.TabIndex = 2;
            // 
            // panelAnalytics
            // 
            panelAnalytics.BackColor = Color.White;
            panelAnalytics.BorderStyle = BorderStyle.FixedSingle;
            panelAnalytics.Controls.Add(_formsPlotAnalytics);
            panelAnalytics.Controls.Add(lblQueueAnalytics);
            panelAnalytics.Location = new Point(571, 190);
            panelAnalytics.Name = "panelAnalytics";
            panelAnalytics.Size = new Size(465, 265);
            panelAnalytics.TabIndex = 1;
            // 
            // panelActiveStatus
            // 
            panelActiveStatus.BackColor = Color.White;
            panelActiveStatus.BorderStyle = BorderStyle.FixedSingle;
            panelActiveStatus.Controls.Add(lblActiveStatus);
            panelActiveStatus.Controls.Add(lblStatusDot1);
            panelActiveStatus.Controls.Add(lblWindow1Name);
            panelActiveStatus.Controls.Add(lblWindow1Info);
            panelActiveStatus.Controls.Add(lblWindow1Description);
            panelActiveStatus.Controls.Add(btnWindow1);
            panelActiveStatus.Controls.Add(lblStatusDot2);
            panelActiveStatus.Controls.Add(lblWindow2Name);
            panelActiveStatus.Controls.Add(lblWindow2Info);
            panelActiveStatus.Controls.Add(lblWindow2Description);
            panelActiveStatus.Controls.Add(btnWindow2);
            panelActiveStatus.Location = new Point(571, 463);
            panelActiveStatus.Name = "panelActiveStatus";
            panelActiveStatus.Size = new Size(465, 204);
            panelActiveStatus.TabIndex = 0;
            // 
            // lblStatusDot1
            // 
            lblStatusDot1.AutoSize = true;
            lblStatusDot1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatusDot1.ForeColor = Color.FromArgb(0, 125, 55);
            lblStatusDot1.Location = new Point(20, 49);
            lblStatusDot1.Name = "lblStatusDot1";
            lblStatusDot1.Size = new Size(14, 15);
            lblStatusDot1.TabIndex = 1;
            lblStatusDot1.Text = "●";
            // 
            // lblWindow1Name
            // 
            lblWindow1Name.AutoSize = true;
            lblWindow1Name.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblWindow1Name.ForeColor = Color.FromArgb(0, 92, 48);
            lblWindow1Name.Location = new Point(39, 45);
            lblWindow1Name.Name = "lblWindow1Name";
            lblWindow1Name.Size = new Size(70, 15);
            lblWindow1Name.TabIndex = 2;
            lblWindow1Name.Text = "Unassigned";
            // 
            // lblWindow1Info
            // 
            lblWindow1Info.AutoSize = true;
            lblWindow1Info.Font = new Font("Segoe UI", 7.5F);
            lblWindow1Info.ForeColor = Color.FromArgb(0, 105, 55);
            lblWindow1Info.Location = new Point(39, 62);
            lblWindow1Info.Name = "lblWindow1Info";
            lblWindow1Info.Size = new Size(91, 12);
            lblWindow1Info.TabIndex = 3;
            lblWindow1Info.Text = "WINDOW 1   OPEN";
            // 
            // lblWindow1Description
            // 
            lblWindow1Description.AutoSize = true;
            lblWindow1Description.Font = new Font("Segoe UI", 7.5F);
            lblWindow1Description.ForeColor = Color.FromArgb(90, 100, 95);
            lblWindow1Description.Location = new Point(205, 54);
            lblWindow1Description.Name = "lblWindow1Description";
            lblWindow1Description.Size = new Size(109, 12);
            lblWindow1Description.TabIndex = 4;
            lblWindow1Description.Text = "Available for transaction";
            // 
            // btnWindow1
            // 
            btnWindow1.BackColor = Color.FromArgb(220, 245, 226);
            btnWindow1.FlatAppearance.BorderSize = 0;
            btnWindow1.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 245, 226);
            btnWindow1.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 245, 226);
            btnWindow1.FlatStyle = FlatStyle.Flat;
            btnWindow1.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            btnWindow1.ForeColor = Color.FromArgb(0, 125, 55);
            btnWindow1.Location = new Point(390, 45);
            btnWindow1.Name = "btnWindow1";
            btnWindow1.Size = new Size(45, 25);
            btnWindow1.TabIndex = 5;
            btnWindow1.Text = "ON";
            btnWindow1.UseVisualStyleBackColor = false;
            // 
            // lblStatusDot2
            // 
            lblStatusDot2.AutoSize = true;
            lblStatusDot2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatusDot2.ForeColor = Color.FromArgb(0, 125, 55);
            lblStatusDot2.Location = new Point(20, 102);
            lblStatusDot2.Name = "lblStatusDot2";
            lblStatusDot2.Size = new Size(14, 15);
            lblStatusDot2.TabIndex = 6;
            lblStatusDot2.Text = "●";
            // 
            // lblWindow2Name
            // 
            lblWindow2Name.AutoSize = true;
            lblWindow2Name.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblWindow2Name.ForeColor = Color.FromArgb(0, 92, 48);
            lblWindow2Name.Location = new Point(39, 98);
            lblWindow2Name.Name = "lblWindow2Name";
            lblWindow2Name.Size = new Size(70, 15);
            lblWindow2Name.TabIndex = 7;
            lblWindow2Name.Text = "Unassigned";
            // 
            // lblWindow2Info
            // 
            lblWindow2Info.AutoSize = true;
            lblWindow2Info.Font = new Font("Segoe UI", 7.5F);
            lblWindow2Info.ForeColor = Color.FromArgb(0, 105, 55);
            lblWindow2Info.Location = new Point(39, 115);
            lblWindow2Info.Name = "lblWindow2Info";
            lblWindow2Info.Size = new Size(91, 12);
            lblWindow2Info.TabIndex = 8;
            lblWindow2Info.Text = "WINDOW 2   OPEN";
            // 
            // lblWindow2Description
            // 
            lblWindow2Description.AutoSize = true;
            lblWindow2Description.Font = new Font("Segoe UI", 7.5F);
            lblWindow2Description.ForeColor = Color.FromArgb(90, 100, 95);
            lblWindow2Description.Location = new Point(205, 107);
            lblWindow2Description.Name = "lblWindow2Description";
            lblWindow2Description.Size = new Size(109, 12);
            lblWindow2Description.TabIndex = 9;
            lblWindow2Description.Text = "Available for transaction";
            // 
            // btnWindow2
            // 
            btnWindow2.BackColor = Color.FromArgb(220, 245, 226);
            btnWindow2.FlatAppearance.BorderSize = 0;
            btnWindow2.FlatAppearance.MouseDownBackColor = Color.FromArgb(220, 245, 226);
            btnWindow2.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 245, 226);
            btnWindow2.FlatStyle = FlatStyle.Flat;
            btnWindow2.Font = new Font("Segoe UI", 7.5F, FontStyle.Bold);
            btnWindow2.ForeColor = Color.FromArgb(0, 125, 55);
            btnWindow2.Location = new Point(390, 98);
            btnWindow2.Name = "btnWindow2";
            btnWindow2.Size = new Size(45, 25);
            btnWindow2.TabIndex = 10;
            btnWindow2.Text = "ON";
            btnWindow2.UseVisualStyleBackColor = false;
            // 
            // AdmissionView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1080, 700);
            Controls.Add(panelActiveStatus);
            Controls.Add(panelAnalytics);
            Controls.Add(panelQueue);
            Controls.Add(lblTotal);
            Controls.Add(lblService);
            Controls.Add(_cmbService);
            Controls.Add(_btnRefresh);
            Controls.Add(_btnServeNext);
            Controls.Add(_btnServiceWindow);
            Name = "AdmissionView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admission - Queue Management";
            Load += AdmissionView_Load;
            ((System.ComponentModel.ISupportInitialize)_dgvQueue).EndInit();
            panelQueue.ResumeLayout(false);
            panelQueue.PerformLayout();
            panelAnalytics.ResumeLayout(false);
            panelAnalytics.PerformLayout();
            panelActiveStatus.ResumeLayout(false);
            panelActiveStatus.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}