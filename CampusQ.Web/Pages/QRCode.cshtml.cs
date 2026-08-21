using CampusQ.MVP.Data;
using CampusQ.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace CampusQ.Web.Pages
{
    public class QRCodeModel : PageModel
    {
        public string QrCodeImageUrl { get; set; }
        public string ShareUrl { get; set; }
        public string? Office { get; set; }
        public IReadOnlyList<string> KnownOffices => OfficeQueueService.KnownOffices;

        public void OnGet(string? office)
        {
            Office = office;

            // Use the smart hybrid URL (local network if reachable, otherwise the Cloudflare tunnel/external URL).
            // When an office is selected, point the QR code straight at that office's live queue page.
            ShareUrl = string.IsNullOrWhiteSpace(office)
                ? WebAppConfig.BaseUrl
                : $"{WebAppConfig.BaseUrl.TrimEnd('/')}/Office/{Uri.EscapeDataString(office)}";

            try
            {
                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(ShareUrl, QRCodeGenerator.ECCLevel.Q);
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        Bitmap qrCodeImage = qrCode.GetGraphic(20);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            qrCodeImage.Save(ms, ImageFormat.Png);
                            byte[] qrCodeBytes = ms.ToArray();
                            QrCodeImageUrl = "data:image/png;base64," + Convert.ToBase64String(qrCodeBytes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating QR code: {ex.Message}");
                QrCodeImageUrl = null;
            }
        }
    }
}
