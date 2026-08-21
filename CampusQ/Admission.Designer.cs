namespace CampusQ
{
    partial class Admission
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
            SuspendLayout();
            // 
            // Admission
            // 
            BackgroundImage = Properties.Resources.staff_reg_bg;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1184, 761);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Admission";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admission - Staff";
            Load += Admission_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}
