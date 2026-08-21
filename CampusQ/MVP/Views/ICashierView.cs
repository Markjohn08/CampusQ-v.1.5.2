using System;
using System.ComponentModel;
using System.Windows.Forms;
using CampusQ.MVP.Models;

namespace CampusQ.MVP.Views
{
    public interface ICashierView : IStaffView
    {
        // =========================================================
        // CURRENT QUEUE
        // =========================================================

        BindingList<QueueEntry>? CurrentQueue { get; }

        // =========================================================
        // QUEUE CHANGED
        // =========================================================

        event EventHandler? QueueChanged;

        // =========================================================
        // DISPLAY SERVED TICKET
        // =========================================================

        void DisplayServedTicket(QueueEntry entry);

        // =========================================================
        // WINDOW ACTIVE STATUS
        // =========================================================

        bool IsWindowActive(int windowNumber);
    }
}