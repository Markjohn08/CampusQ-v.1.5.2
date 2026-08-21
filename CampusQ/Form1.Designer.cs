using CampusQ.Properties;

namespace CampusQ
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            btn_cashier = new PictureBox();
            btn_registrar = new PictureBox();
            btn_admission = new PictureBox();

            ((System.ComponentModel.ISupportInitialize)btn_cashier).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btn_registrar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btn_admission).BeginInit();

            SuspendLayout();

            // 
            // btn_cashier
            // 
            btn_cashier.BackColor = Color.Transparent;
            btn_cashier.Cursor = Cursors.Hand;
            btn_cashier.BackgroundImage = Properties.Resources.cashier_btn;
            btn_cashier.Location = new Point(49, 636);
            btn_cashier.Name = "btn_cashier";
            btn_cashier.Size = new Size(500, 120);
            btn_cashier.SizeMode = PictureBoxSizeMode.StretchImage;
            btn_cashier.TabIndex = 1;
            btn_cashier.TabStop = false;

            // IMPORTANT:
            // Use a normal event handler instead of a lambda.
            btn_cashier.MouseClick += btn_cashier_Click;

            // 
            // btn_registrar
            // 
            btn_registrar.BackColor = Color.Transparent;
            btn_registrar.Cursor = Cursors.Hand;
            btn_registrar.BackgroundImage = Properties.Resources.reg_btn;
            btn_registrar.Location = new Point(49, 505);
            btn_registrar.Name = "btn_registrar";
            btn_registrar.Size = new Size(500, 113);
            btn_registrar.SizeMode = PictureBoxSizeMode.Zoom;
            btn_registrar.TabIndex = 3;
            btn_registrar.TabStop = false;

            // IMPORTANT:
            // Use a normal event handler instead of a lambda.
            btn_registrar.MouseClick += btn_registrar_Click_1;

            // 
            // btn_admission
            // 
            btn_admission.BackColor = Color.Transparent;
            btn_admission.Cursor = Cursors.Hand;
            btn_admission.BackgroundImage = Properties.Resources.adm_btn;
            btn_admission.Location = new Point(44, 768);
            btn_admission.Name = "btn_admission";
            btn_admission.Size = new Size(500, 110);
            btn_admission.SizeMode = PictureBoxSizeMode.StretchImage;
            btn_admission.TabIndex = 4;
            btn_admission.TabStop = false;

            // IMPORTANT:
            // Use a normal event handler instead of a lambda.
            btn_admission.MouseClick += btn_admission_Click_1;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Idle_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1920, 1080);
            Controls.Add(btn_admission);
            Controls.Add(btn_registrar);
            Controls.Add(btn_cashier);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";

            Load += Form1_Load;

            ((System.ComponentModel.ISupportInitialize)btn_admission).EndInit();
            ((System.ComponentModel.ISupportInitialize)btn_registrar).EndInit();
            ((System.ComponentModel.ISupportInitialize)btn_cashier).EndInit();

            ResumeLayout(false);
        }

        #endregion

        private PictureBox btn_cashier;
        private PictureBox btn_registrar;
        private PictureBox btn_admission;
    }
}