using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CampusQ.MVP.Views;
using CampusQ.MVP.Models;

namespace CampusQ
{

    public partial class Cashier : Form
    {
        private readonly CashierView _mvpView;

        public Cashier()
        {
            InitializeComponent();

            _mvpView = new CashierView
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            Controls.Add(_mvpView);
            _mvpView.Show();
            // Default to show all cashier entries so DB items appear
            _mvpView.SetSelectedService("All");
            _mvpView.RefreshQueueView();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Also refresh when the host is shown (handles cases where DB was updated while the app was hidden)
            _mvpView.RefreshQueueView();
        }

        // Removed AddToQueue: cashier no longer exposes the ability to add tickets to the queue.

        public void RefreshQueueView()
            => _mvpView.RefreshQueueView();

        // Expose selection/state operations if other code relies on IStaffView on Cashier.
        public void BindQueue(BindingList<QueueEntry> view)
            => _mvpView.BindQueue(view);

        public string SelectedService => _mvpView.SelectedService;

        public void ShowMessage(string text, string caption, MessageBoxIcon icon)
            => _mvpView.ShowMessage(text, caption, icon);

        public void SetSelectedService(string service)
            => _mvpView.SetSelectedService(service);

        private void Cashier_Load(object sender, EventArgs e)
        {

        }
    }
}