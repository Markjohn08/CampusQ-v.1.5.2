namespace CampusQ.MVP.Data
{
    public static class WebAppConfig
    {
        /// <summary>
        /// Base URL where CampusQ.Web is publicly reachable (via Cloudflare Tunnel).
        /// This single URL works for students on campus WiFi AND on mobile/cellular data,
        /// since the Cloudflare Tunnel is accessible from anywhere on the internet.
        /// NOTE: Quick Tunnels (trycloudflare.com) are temporary and change each time cloudflared restarts.
        /// Update this value whenever the tunnel is restarted with a new URL.
        /// </summary>
        public static string BaseUrl { get; set; } = "https://mandatory-marilyn-advisor-rev.trycloudflare.com";
    }
}
