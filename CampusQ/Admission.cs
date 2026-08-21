using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CampusQ.MVP.Views;
using CampusQ.MVP.Models;

namespace CampusQ
{

    public partial class Admission : Form
    {
        private readonly AdmissionView _mvpView;

        public Admission()
        {
            InitializeComponent();

            _mvpView = new AdmissionView
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };

            Controls.Add(_mvpView);
            _mvpView.Show();
            _mvpView.SetSelectedService("All");
            _mvpView.RefreshQueueView();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Also refresh when the host is shown (handles cases where DB was updated while the app was hidden)
            _mvpView.RefreshQueueView();
        }

        // Removed AddToQueue: admission no longer exposes the ability to add tickets to the queue.

        public void RefreshQueueView()
            => _mvpView.RefreshQueueView();

        // Expose selection/state operations if other code relies on IStaffView on Admission.
        public void BindQueue(BindingList<QueueEntry> view)
            => _mvpView.BindQueue(view);

        public string SelectedService => _mvpView.SelectedService;

        public void ShowMessage(string text, string caption, MessageBoxIcon icon)
            => _mvpView.ShowMessage(text, caption, icon);


        public void SetSelectedService(string service)
            => _mvpView.SetSelectedService(service);

        private void Admission_Load(object sender, EventArgs e)
        {

        }
    }
}
