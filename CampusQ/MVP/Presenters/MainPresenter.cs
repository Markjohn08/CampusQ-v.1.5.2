using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Globalization;
using CampusQ.MVP.Views;

namespace CampusQ.MVP.Presenters
{
    public class MainPresenter
    {
        private readonly IMainView _view;

        public MainPresenter(IMainView view)
        {
            _view = view;
        }

        // =====================================================
        // SUBMIT TICKET
        // =====================================================
        public void Submit(string purposeText, string service)
        {
            var purposePattern =
                new Regex(
                    @"^[A-Za-z \&\-]{2,60}$",
                    RegexOptions.Compiled);

            // Validate purpose
            if (!purposePattern.IsMatch(purposeText))
            {
                _view.ShowMessage(
                    "Please select a valid purpose from the list.",
                    "Validation Error",
                    MessageBoxIcon.Warning);

                return;
            }

            // Normalize service
            var svc =
                NormalizeService(service);

            // =================================================
            // SAVE TICKET
            // =================================================
            _view.AddToQueue(
                purposeText,
                svc);

            // =================================================
            // IMPORTANT:
            // DO NOT SHOW "THANK YOU FOR USING CAMPUSQ"
            //
            // Form1 will handle the next step:
            //
            // Save
            //   ↓
            // Print
            //   ↓
            // QR Ticket Popup
            //   ↓
            // OK
            //   ↓
            // Home
            // =================================================
        }

        // =====================================================
        // NORMALIZE SERVICE
        // =====================================================
        private static string NormalizeService(
            string? service)
        {
            if (string.IsNullOrWhiteSpace(service))
                return "Other";

            var s =
                service.Trim();

            var lower =
                s.ToLowerInvariant();

            if (
                lower.Contains("cash") ||
                lower.Contains("window") ||
                Regex.IsMatch(
                    lower,
                    @"(^|\s)w\s*\d",
                    RegexOptions.IgnoreCase))
            {
                return "Cashier";
            }

            if (
                lower.Contains("registr") ||
                lower.Equals(
                    "reg",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Registrar";
            }

            if (
                string.Equals(
                    s,
                    "other",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Other";
            }

            // Fallback:
            // Convert the provided service to Title Case.
            return CultureInfo
                .CurrentCulture
                .TextInfo
                .ToTitleCase(
                    s.ToLowerInvariant());
        }
    }
}