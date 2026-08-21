namespace CampusQ
{
    partial class AdminDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Button btnOpenMain;
        private Button btnOpenStaff;
        private Button btnOpenLogin;
        private Button btnRefresh;
        private Button btnClearPersist;
        private DataGridView dgvQueue;
        private Label lblTotals;

        private Label lblAccounts;
        private DataGridView dgvAccounts;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtConfirm;
        private ComboBox cmbRole;
        private Button btnCreateAccount;
        private Button btnDeleteAccount;

        // Panel-based chart for daily queue statistics (avoids external chart package)
        private Panel chartDaily;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnOpenMain = new Button();
            btnOpenStaff = new Button();
            btnOpenLogin = new Button();
            btnRefresh = new Button();
            btnClearPersist = new Button();
            lblTotals = new Label();
            dgvQueue = new DataGridView();
            lblAccounts = new Label();
            dgvAccounts = new DataGridView();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            txtConfirm = new TextBox();
            cmbRole = new ComboBox();
            btnCreateAccount = new Button();
            btnDeleteAccount = new Button();
            chartDaily = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvQueue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).BeginInit();
            SuspendLayout();
            //
            // btnOpenMain
            //
            btnOpenMain.Text = "Open Main Form";
            btnOpenMain.Location = new Point(20, 20);
            btnOpenMain.Size = new Size(160, 36);
            btnOpenMain.Name = "btnOpenMain";
            btnOpenMain.Click += BtnOpenMain_Click;
            //
            // btnOpenStaff
            //
            btnOpenStaff.Text = "Open Staff";
            btnOpenStaff.Location = new Point(200, 20);
            btnOpenStaff.Size = new Size(160, 36);
            btnOpenStaff.Name = "btnOpenStaff";
            btnOpenStaff.Click += BtnOpenStaff_Click;
            //
            // btnOpenLogin
            //
            btnOpenLogin.Text = "Open Login";
            btnOpenLogin.Location = new Point(380, 20);
            btnOpenLogin.Size = new Size(160, 36);
            btnOpenLogin.Name = "btnOpenLogin";
            btnOpenLogin.Click += BtnOpenLogin_Click;
            //
            // btnRefresh
            //
            btnRefresh.Text = "Refresh Queue";
            btnRefresh.Location = new Point(560, 20);
            btnRefresh.Size = new Size(120, 36);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Click += BtnRefresh_Click;
            //
            // btnClearPersist
            //
            btnClearPersist.Text = "Clear Persisted Queue";
            btnClearPersist.Location = new Point(700, 20);
            btnClearPersist.Size = new Size(180, 36);
            btnClearPersist.Name = "btnClearPersist";
            btnClearPersist.Click += BtnClearPersist_Click;
            //
            // lblTotals
            //
            lblTotals.Text = "Totals: (loading...)";
            lblTotals.Location = new Point(20, 70);
            lblTotals.AutoSize = true;
            lblTotals.Name = "lblTotals";
            //
            // dgvQueue
            //
            dgvQueue.Location = new Point(20, 100);
            dgvQueue.Size = new Size(630, 480);
            dgvQueue.ReadOnly = true;
            dgvQueue.AllowUserToAddRows = false;
            dgvQueue.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvQueue.AutoGenerateColumns = false;
            dgvQueue.Name = "dgvQueue";
            dgvQueue.Columns.Add(new DataGridViewTextBoxColumn { Name = "TicketLabel", DataPropertyName = "TicketLabel", HeaderText = "Ticket", Width = 70 });
            dgvQueue.Columns.Add(new DataGridViewTextBoxColumn { Name = "Purpose", DataPropertyName = "Purpose", HeaderText = "Purpose", Width = 140 });
            dgvQueue.Columns.Add(new DataGridViewTextBoxColumn { Name = "Service", DataPropertyName = "Service", HeaderText = "Service", Width = 90 });
            dgvQueue.Columns.Add(new DataGridViewTextBoxColumn { Name = "Added", DataPropertyName = "TimeAdded", HeaderText = "Added", Width = 120, DefaultCellStyle = { Format = "g" } });
            //
            // lblAccounts
            //
            lblAccounts.Text = "Staff Accounts";
            lblAccounts.Location = new Point(720, 70);
            lblAccounts.AutoSize = true;
            lblAccounts.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
            lblAccounts.Name = "lblAccounts";
            //
            // dgvAccounts
            //
            dgvAccounts.Location = new Point(720, 100);
            dgvAccounts.Size = new Size(400, 200);
            dgvAccounts.ReadOnly = true;
            dgvAccounts.AllowUserToAddRows = false;
            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccounts.AutoGenerateColumns = false;
            dgvAccounts.Name = "dgvAccounts";
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Username", DataPropertyName = "Username", HeaderText = "Username", Width = 140 });
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn { Name = "Role", DataPropertyName = "Role", HeaderText = "Role", Width = 80 });
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn { Name = "CreatedAt", DataPropertyName = "CreatedAt", HeaderText = "Created", Width = 110, DefaultCellStyle = { Format = "g" } });
            //
            // txtUsername
            //
            txtUsername.Location = new Point(720, 320);
            txtUsername.Size = new Size(220, 26);
            txtUsername.PlaceholderText = "username";
            txtUsername.Name = "txtUsername";
            //
            // txtPassword
            //
            txtPassword.Location = new Point(720, 360);
            txtPassword.Size = new Size(220, 26);
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.PlaceholderText = "password";
            txtPassword.Name = "txtPassword";
            //
            // txtConfirm
            //
            txtConfirm.Location = new Point(720, 400);
            txtConfirm.Size = new Size(220, 26);
            txtConfirm.UseSystemPasswordChar = true;
            txtConfirm.PlaceholderText = "confirm password";
            txtConfirm.Name = "txtConfirm";
            //
            // cmbRole
            //
            cmbRole.Location = new Point(720, 440);
            cmbRole.Size = new Size(140, 26);
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Name = "cmbRole";
            cmbRole.Items.AddRange(new object[] { "Staff", "Admin" });
            cmbRole.SelectedIndex = 0;
            //
            // btnCreateAccount
            //
            btnCreateAccount.Text = "Create Account";
            btnCreateAccount.Location = new Point(720, 480);
            btnCreateAccount.Size = new Size(140, 36);
            btnCreateAccount.Name = "btnCreateAccount";
            btnCreateAccount.Click += BtnCreateAccount_Click;
            //
            // btnDeleteAccount
            //
            btnDeleteAccount.Text = "Delete Selected";
            btnDeleteAccount.Location = new Point(870, 480);
            btnDeleteAccount.Size = new Size(140, 36);
            btnDeleteAccount.Name = "btnDeleteAccount";
            btnDeleteAccount.Click += BtnDeleteAccount_Click;
            //
            // chartDaily
            //
            chartDaily.Location = new Point(20, 600);
            chartDaily.Size = new Size(1120, 240);
            chartDaily.BorderStyle = BorderStyle.FixedSingle;
            chartDaily.Name = "chartDaily";
            chartDaily.Paint += ChartDaily_Paint;
            chartDaily.Resize += ChartDaily_Resize;
            //
            // AdminDashboard
            //
            Text = "Admin Dashboard";
            Size = new Size(1200, 920);
            StartPosition = FormStartPosition.CenterScreen;
            BackgroundImage = Properties.Resources.Purple_and_White_Minimalist_Modern_Computer_Repair_Logo__23_386_x_16_535_in___8_5_x_22_cm___500_x_500_px___11_7_x_8_27_in_;
            Name = "AdminDashboard";
            Controls.Add(btnOpenMain);
            Controls.Add(btnOpenStaff);
            Controls.Add(btnOpenLogin);
            Controls.Add(btnRefresh);
            Controls.Add(btnClearPersist);
            Controls.Add(lblTotals);
            Controls.Add(dgvQueue);
            Controls.Add(lblAccounts);
            Controls.Add(dgvAccounts);
            Controls.Add(txtUsername);
            Controls.Add(txtPassword);
            Controls.Add(txtConfirm);
            Controls.Add(cmbRole);
            Controls.Add(btnCreateAccount);
            Controls.Add(btnDeleteAccount);
            Controls.Add(chartDaily);
            Load += AdminDashboard_Load;
            ((System.ComponentModel.ISupportInitialize)dgvQueue).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
