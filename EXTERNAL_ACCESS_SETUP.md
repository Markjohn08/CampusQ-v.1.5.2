# Quick Setup: External Access for Mobile Data Users

## 🎯 Choose Your Solution

Pick ONE option below:

---

## ⭐ Option 1: Port Forwarding (Free-$50/mo)

### What you need:
- Public static IP from ISP (~$50/mo extra)
- Router access

### Setup (10 minutes):

```bash
# 1. Find your public IP
# Visit: whatismyipaddress.com
# Write down: YOUR_PUBLIC_IP

# 2. In your router settings:
#    Port 5131 (External) → 192.168.1.12:5131 (Internal)
#    Save settings

# 3. Update WebAppConfig.cs:
```

**File**: `CampusQ.Core\MVP\Data\WebAppConfig.cs`

```csharp
public static string ExternalUrl { get; set; } = 
	"http://YOUR_PUBLIC_IP:5131";  // Replace YOUR_PUBLIC_IP
```

**Example:**
```csharp
public static string ExternalUrl { get; set; } = 
	"http://203.0.113.42:5131";  // Your actual public IP
```

### Test:
```bash
# From outside campus network, try:
http://203.0.113.42:5131
```

---

## ⭐⭐ Option 2: Azure Cloud Deployment (Recommended - $15-50/mo)

### What you need:
- Azure account (free tier available)
- 2-3 hours setup time

### Setup Steps:

#### Step 1: Create Azure Account
```
1. Go to: portal.azure.com
2. Sign up (free account)
3. Create resource group: "CampusQ"
```

#### Step 2: Create SQL Database (Cloud)
```
1. Azure → SQL Databases → Create
2. Server: campusq-server
3. Database: CampusQ
4. Get connection string
5. Update appsettings.json
```

#### Step 3: Deploy CampusQ.Web
```powershell
# In Visual Studio:
# Right-click CampusQ.Web → Publish
# Choose: Azure App Service (Create new)
# Name: campusq-[yourschool]
# Publish!
```

#### Step 4: Update WebAppConfig.cs
```csharp
public static string ExternalUrl { get; set; } = 
	"https://campusq-yourschool.azurewebsites.net";
```

### Test:
```
https://campusq-yourschool.azurewebsites.net/Ticket/123
```

---

## ⭐⭐ Option 3: Dynamic DNS (Budget - $0-20/year)

### What you need:
- Free account at no-ip.com or dyn.com
- Access your domain
- Router/server setup

### Setup (30 minutes):

#### Step 1: Register Free Domain
```
1. Go to: no-ip.com
2. Sign up free
3. Add hostname: mycampusq.no-ip.com
4. Create
```

#### Step 2: Configure Dynamic DNS Client
```
1. Download DynDNS client from no-ip.com
2. Install on your server (192.168.1.12)
3. Login with no-ip credentials
4. Select your hostname
5. Start
```

#### Step 3: Update WebAppConfig.cs
```csharp
public static string ExternalUrl { get; set; } = 
	"http://mycampusq.no-ip.com:5131";
```

#### Step 4: Router Port Forwarding
```
Router Settings:
  Port 5131 → 192.168.1.12:5131
```

### Test:
```
http://mycampusq.no-ip.com:5131/Ticket/123
```

---

## 📋 Comparison

| Feature | Port Fwd | Cloud | DynDNS |
|---------|----------|-------|--------|
| Cost | $0-50/mo | $15-50/mo | $0-20/yr |
| Setup Time | 10 min | 2-3 hrs | 30 min |
| Reliability | High | Very High | Medium |
| Security | Medium | High | Low |
| Best For | Tech teams | Production | Budget |

---

## 🧪 Testing After Setup

### Test 1: On Campus WiFi
```
Device: Laptop/Phone
Network: Campus WiFi
URL: http://192.168.1.12:5131
Expected: WORKS (local network)
```

### Test 2: On Mobile Data
```
Device: Mobile Phone
Network: Cellular Data (4G/5G)
URL: http://your-external-url:5131
Expected: WORKS (external access)
```

### Test 3: Full QR Scan
```
1. Generate ticket on kiosk
2. Get QR code receipt
3. Scan with phone on cellular data
4. Should open ticket status page
```

---

## ✅ Validation Checklist

- [ ] Chosen external access solution
- [ ] Updated `ExternalUrl` in WebAppConfig.cs
- [ ] Rebuilt solution (`dotnet build`)
- [ ] Tested from WiFi
- [ ] Tested from cellular data
- [ ] Tested QR code scanning
- [ ] Documented URL for IT team

---

## 🆘 Troubleshooting

**Students can't access from mobile data:**
- [ ] Is `ExternalUrl` correct?
- [ ] Is external service running/online?
- [ ] Port forwarding configured correctly?
- [ ] Firewall blocking port 5131?

**Works on WiFi but not cellular:**
- [ ] WiFi: Using local IP ✅
- [ ] Cellular: Using external URL ✅
- [ ] Check `IsUrlReachable` logic in WebAppConfig

**QR code not scanning:**
- [ ] Test URL manually first
- [ ] Ensure HTTPS/HTTP protocol matches
- [ ] Check ticket number in URL

---

## 📞 Support Resources

- **Port Forwarding Help**: routerpasswords.com
- **Azure Help**: docs.microsoft.com/en-us/azure
- **No-IP Setup**: no-ip.com/support
- **General .NET**: docs.microsoft.com

---

## 🔐 Security Tips

1. **Change default router password** (if port forwarding)
2. **Use HTTPS** with certificate (if possible)
3. **Enable CORS restrictions** for production
4. **Monitor access logs** for suspicious activity
5. **Regular backups** of database

---

**Implementation Status**: Ready to Deploy ✅  
**Next Step**: Choose option above and configure
