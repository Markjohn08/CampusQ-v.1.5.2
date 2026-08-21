# CampusQ Configuration Quick Reference

## System Summary

Your CampusQ system is a **three-tier queue management kiosk** with network-enabled QR code ticket scanning.

### Architecture
```
Desktop App (WinForms)  →  Web App (Razor Pages)  →  SQL Database (SQL Express)
	 CampusQ                CampusQ.Web               CampusQ DB
   (Kiosk UI)              (Ticket Details)          (Queue Storage)
```

---

## Configuration Overview

### 1. Network Service IP: `192.168.1.12`
- This is your machine's local network IP
- Used in QR code generation
- Accessible from any device on the same network (192.168.1.x)

### 2. Web Application Port: `5131`
- CampusQ.Web listens on this port
- Full URL: `http://192.168.1.12:5131`
- QR codes point to: `http://192.168.1.12:5131/Ticket/{TicketNumber}`

### 3. Database Configuration
- **Server**: MSI\SQLEXPRESS (Local SQL Server)
- **Database**: CampusQ (Auto-created)
- **Authentication**: Windows Integrated Security

---

## ✅ All Issues Resolved

| # | Issue | Status |
|---|-------|--------|
| 1 | Process lock preventing build | ✅ FIXED |
| 2 | BaseUrl using localhost | ✅ FIXED → `http://192.168.1.12:5131` |
| 3 | Web app bound to localhost | ✅ FIXED → `0.0.0.0:5131` |
| 4 | QR code generation error | ✅ FIXED - Proper bitmap handling |
| 5 | QRCode NuGet package | ✅ INSTALLED - v1.8.0 |

---

## Files Modified

1. **CampusQ.Core\MVP\Data\WebAppConfig.cs**
   - Changed BaseUrl from `https://localhost:7278` → `http://192.168.1.12:5131`

2. **CampusQ.Web\Properties\launchSettings.json**
   - Changed HTTP URL from `http://localhost:5131` → `http://0.0.0.0:5131`
   - Changed HTTPS URL from `https://localhost:7278` → `https://0.0.0.0:7278`

3. **CampusQ\Form1.cs**
   - Fixed `GenerateQRCode()` method with proper resource handling

---

## Pages & Routes

### Web Application (CampusQ.Web)

| Page | Route | Purpose |
|------|-------|---------|
| Index | `/` | Home page |
| QRCode | `/QRCode` | Display current system QR code for sharing |
| Ticket Details | `/Ticket/{ticketNumber}` | Show ticket status from mobile scan |
| Privacy | `/Privacy` | Privacy policy |

---

## How QR Codes Work

1. **Generation** (WinForms Kiosk)
   ```
   GenerateQRCode("http://192.168.1.12:5131/Ticket/123")
   → Creates PNG bitmap
   → Prints on receipt
   ```

2. **Scanning** (Mobile Device)
   ```
   Scan QR code
   → Browser opens "http://192.168.1.12:5131/Ticket/123"
   → Razor Page loads ticket status
   ```

3. **Display** (Web Browser)
   ```
   Shows:
   - Ticket #: 123
   - Status: Waiting
   - Position: 3rd in queue
   - Wait Time: ~15 minutes
   ```

---

## Build Status

```
✅ Solution builds successfully
✅ No compilation errors
✅ All NuGet packages resolved
✅ All projects configured correctly
```

---

## Environment Details

| Item | Value |
|------|-------|
| .NET Target | .NET 8 |
| OS | Windows |
| SQL Server | SQL Server Express |
| Browser Support | Any modern browser (Chrome, Edge, Safari) |
| Network | Local Network (192.168.x.x) |

---

## Next Steps

1. **Start Web App**: `dotnet run --project CampusQ.Web\CampusQ.Web.csproj`
2. **Run Kiosk**: Open CampusQ.sln → Run CampusQ project
3. **Test QR**: Generate a ticket and scan with mobile device
4. **Monitor**: Check `http://192.168.1.12:5131/QRCode` for system QR code

---

## Support

For issues:
- Build errors? → Run `dotnet clean` then rebuild
- Connection errors? → Check SQL Server is running
- QR code not working? → Verify machine IP address with `ipconfig`
- Mobile can't access? → Ensure device is on same network (192.168.1.x)

---

**Last Updated**: January 24, 2025
**Status**: ✅ All Systems Operational
