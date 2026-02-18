# StudySync - Azure Deployment Guide (Optional Reference)

> **Important:** This project was developed with **SQLite only** to avoid Azure costs. This deployment guide is provided as **optional reference material** for future cloud deployment. The application currently runs 100% locally with no Azure services.
>
> **For local development (current setup):** See the "Local deployment (SQLite)" section below or refer to [README.md](README.md).

## Prerequisites

- .NET 8 SDK installed

## Local deployment (SQLite)

If you are using SQLite for development or testing, follow these quick steps:

1. Ensure the .NET 8 SDK is installed.
2. From the project folder run:

```powershell
dotnet ef database update
dotnet run
```

This will create a `StudySync.db` file in the project folder and start the application in development mode using the SQLite connection defined in `appsettings.json`.

If you need more advanced guidance (migrations, backups, or production deployment), ask and I'll expand this section.

## Step 1: Create Azure SQL Database

### Option A: Azure Portal

1. Go to [Azure Portal](https://portal.azure.com)
2. Click "Create a resource" → "Databases" → "SQL Database"
3. Configure:
   - **Resource Group**: Create new or use existing
   - **Database Name**: `StudySyncDb`
   - **Server**: Create new server
     - Server name: `studysync-server-[yourname]`
     - Location: Choose closest region
     - Authentication: SQL authentication
     - Admin login: `sqladmin`
     - Password: Create a strong password
   - **Compute + Storage**: Basic tier is sufficient for development
4. Click "Review + Create" → "Create"
5. Wait for deployment to complete

### Option B: Azure CLI

```bash
# Login to Azure
az login

# Create resource group
az group create --name StudySync-RG --location eastus

# Create SQL Server
az sql server create \
  --name studysync-server-yourname \
  --resource-group StudySync-RG \
  --location eastus \
  --admin-user sqladmin \
  --admin-password YourStrongPassword123!

# Create database
az sql db create \
  --resource-group StudySync-RG \
  --server studysync-server-yourname \
  --name StudySyncDb \
  --service-objective Basic

# Configure firewall to allow Azure services
az sql server firewall-rule create \
  --resource-group StudySync-RG \
  --server studysync-server-yourname \
  --name AllowAzureServices \
  --start-ip-address 0.0.0.0 \
  --end-ip-address 0.0.0.0
```

## Step 2: Configure Connection String

### Get Connection String from Azure Portal

1. Go to your SQL Database in Azure Portal
2. Click "Connection strings" under Settings
3. Copy the ADO.NET connection string
4. Replace `{your_password}` with your actual password

Example:

```
Server=tcp:studysync-server-yourname.database.windows.net,1433;Initial Catalog=StudySyncDb;Persist Security Info=False;User ID=sqladmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

### Update appsettings.json

**Important**: Do NOT commit production passwords to git!

For production, use Azure Key Vault or App Settings in Azure App Service.

For now, you can test locally with the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:studysync-server-yourname.database.windows.net,1433;Initial Catalog=StudySyncDb;Persist Security Info=False;User ID=sqladmin;Password=YourPassword123!;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
```

## Step 3: Apply Database Migrations

### Allow Your IP Address

First, add your local IP to the firewall:

1. Go to SQL Server in Azure Portal
2. Click "Networking" under Security
3. Add your current client IP address
4. Click "Save"

### Run Migrations

```powershell
# From the StudySync project folder
dotnet ef database update
```

This will create all tables in your Azure SQL Database.

## Step 4: Create Azure App Service

### Option A: Azure Portal

1. Go to Azure Portal
2. Click "Create a resource" → "Web App"
3. Configure:
   - **Resource Group**: Use same as database
   - **Name**: `studysync-app-[yourname]`
   - **Publish**: Code
   - **Runtime stack**: .NET 8 (LTS)
   - **Operating System**: Windows or Linux (Windows recommended for .NET)
   - **Region**: Same as database
   - **App Service Plan**: Create new (F1 Free tier for testing)
4. Click "Review + Create" → "Create"

### Option B: Azure CLI

```bash
# Create App Service Plan
az appservice plan create \
  --name StudySync-Plan \
  --resource-group StudySync-RG \
  --sku F1 \
  --location eastus

# Create Web App
az webapp create \
  --name studysync-app-yourname \
  --resource-group StudySync-RG \
  --plan StudySync-Plan \
  --runtime "DOTNET|8.0"
```

## Step 5: Configure App Service Connection String

### In Azure Portal

1. Go to your App Service
2. Click "Configuration" under Settings
3. Click "New connection string"
4. Configure:
   - **Name**: `DefaultConnection`
   - **Value**: Your SQL connection string
   - **Type**: SQLAzure
5. Click "OK" → "Save"

### Using Azure CLI

```bash
az webapp config connection-string set \
  --name studysync-app-yourname \
  --resource-group StudySync-RG \
  --connection-string-type SQLAzure \
  --settings DefaultConnection="your-connection-string-here"
```

## Step 6: Deploy the Application

### Option A: Visual Studio

1. Right-click project → "Publish"
2. Select "Azure" → "Azure App Service (Windows)"
3. Sign in to your Azure account
4. Select your subscription and App Service
5. Click "Publish"

### Option B: VS Code with Azure Extension

1. Install "Azure App Service" extension
2. Open Command Palette (Ctrl+Shift+P)
3. Select "Azure App Service: Deploy to Web App"
4. Select your subscription and App Service
5. Select the project folder
6. Confirm deployment

### Option C: Publish Profile (Manual)

```powershell
# Build and publish
dotnet publish -c Release -o ./publish

# Create a zip file
Compress-Archive -Path ./publish/* -DestinationPath ./app.zip

# Deploy using Azure CLI
az webapp deployment source config-zip \
  --resource-group StudySync-RG \
  --name studysync-app-yourname \
  --src ./app.zip
```

## Step 7: Verify Deployment

1. Go to `https://studysync-app-yourname.azurewebsites.net`
2. Register a new user
3. Create a course
4. Add an assignment
5. Verify all features work

## Security Best Practices

### 1. Use Managed Identity (Recommended)

Instead of SQL authentication, use Managed Identity:

```csharp
// In Program.cs
var credential = new DefaultAzureCredential();
var token = await credential.GetTokenAsync(
    new Azure.Core.TokenRequestContext(new[] { "https://database.windows.net/.default" }));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
        sqlOptions => sqlOptions.EnableRetryOnFailure()));
```

### 2. Use Azure Key Vault

Store connection strings in Key Vault:

1. Create a Key Vault in Azure
2. Add your connection string as a secret
3. Reference in `appsettings.json`:

```json
{
  "KeyVault": {
    "VaultUri": "https://your-keyvault.vault.azure.net/"
  }
}
```

### 3. Enable Application Insights

For monitoring and diagnostics:

```bash
az monitor app-insights component create \
  --app studysync-insights \
  --location eastus \
  --resource-group StudySync-RG
```

## Troubleshooting

### Cannot connect to database

- Verify firewall rules in SQL Server
- Check connection string format
- Ensure "Allow Azure services" is enabled

### Migrations fail

- Ensure your local IP is whitelisted
- Check SQL admin credentials
- Verify network connectivity

### App doesn't start

- Check Application Logs in Azure Portal
- Verify .NET runtime version matches
- Check App Service configuration

### Performance issues

- Upgrade App Service Plan (from F1 to B1 or higher)
- Enable connection pooling
- Add caching for frequently accessed data

## Monitoring

### View Logs

```bash
# Stream logs in real-time
az webapp log tail \
  --name studysync-app-yourname \
  --resource-group StudySync-RG
```

### Enable Diagnostic Logging

1. Go to App Service → "App Service logs"
2. Enable:
   - Application Logging (File System)
   - Web server logging
   - Detailed error messages
3. Click "Save"

## Scaling

### Horizontal Scaling (Multiple Instances)

```bash
az appservice plan update \
  --name StudySync-Plan \
  --resource-group StudySync-RG \
  --number-of-workers 2
```

### Vertical Scaling (Bigger Instance)

```bash
az appservice plan update \
  --name StudySync-Plan \
  --resource-group StudySync-RG \
  --sku B2
```

## Cost Optimization

- Use Free/Basic tiers for development
- Scale up only during high traffic
- Stop App Service when not in use (development)
- Use Azure Cost Management to monitor spending

## Continuous Deployment (Optional)

### GitHub Actions

1. Create `.github/workflows/azure-deploy.yml`:

```yaml
name: Deploy to Azure

on:
  push:
    branches: [main]

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2

      - name: Setup .NET
        uses: actions/setup-dotnet@v1
        with:
          dotnet-version: "8.0.x"

      - name: Build
        run: dotnet build --configuration Release

      - name: Publish
        run: dotnet publish -c Release -o ./publish

      - name: Deploy to Azure
        uses: azure/webapps-deploy@v2
        with:
          app-name: studysync-app-yourname
          publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
          package: ./publish
```

2. Download publish profile from Azure Portal
3. Add as GitHub secret: `AZURE_WEBAPP_PUBLISH_PROFILE`

## Resources

- [Azure App Service Documentation](https://docs.microsoft.com/azure/app-service/)
- [Azure SQL Documentation](https://docs.microsoft.com/azure/azure-sql/)
- [.NET Deployment Guide](https://docs.microsoft.com/aspnet/core/host-and-deploy/azure-apps/)

---

**Created for StudySync - CSE 325**
