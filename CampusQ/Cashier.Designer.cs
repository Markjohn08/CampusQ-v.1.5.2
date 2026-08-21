namespace CampusQ
{
    partial class Cashier
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
            // Cashier
            //
            Text = "Cashier - Staff";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1200, 800);
            BackgroundImage = Properties.Resources.Purple_and_White_Minimalist_Modern_Computer_Repair_Logo__23_386_x_16_535_in___8_5_x_22_cm___500_x_500_px___11_7_x_8_27_in_;
            Name = "Cashier";
            Load += Cashier_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}
