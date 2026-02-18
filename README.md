# StudySync

A web application designed to help university students organize their academic life efficiently.

## 🚀 Quick Start (First Time Setup)

**Important:** The database is not included in this repository. Follow these steps:

1. **Clone and restore**

   ```powershell
   git clone https://github.com/miranda-beatriz/CSE325.git
   cd CSE325
   dotnet restore
   ```

2. **Create database**

   ```powershell
   dotnet ef database update
   ```

3. **Run application**

   ```powershell
   dotnet run
   ```

4. **Create your account**
   - Open browser at `http://localhost:5000`
   - Click **"Register"**
   - Use any email (e.g., `student@example.com`)
   - Password must have: 6+ chars, uppercase, lowercase, digit (e.g., `Password123`)

**There is no default username/password. You must register first!**

---

## Features

- 🔐 **User Authentication** - Secure sign up and login with ASP.NET Identity
- 📚 **Course Management** - Add, edit, and delete courses
- 📝 **Assignment Tracking** - CRUD functionality for tasks with due dates
- ✅ **Task Organization** - Categorize assignments (To Do, In Progress, Done)
- 🎯 **Priority System** - Mark assignments as High, Medium, or Low priority
- 📅 **Enhanced Dashboard** - Multiple views with smart filtering
- ⚡ **Due Soon Filter** - Quickly identify assignments due in ≤ 3 days
- 📊 **Course Filtering** - Filter assignments by specific course
- 🔍 **Status Filtering** - Filter by To Do, In Progress, or Done
- ⚙️ **Performance Optimized** - No page reload on check-off, parallel data loading

## Project Requirements Met

✅ **User Authentication**: Implemented with ASP.NET Core Identity  
✅ **CRUD Functionality**: Full CRUD for Courses and Assignments  
✅ **Application Design Standards**: DataAnnotations validation throughout  
✅ **Branding**: Consistent color palette and UI design  
✅ **Usability**: Due Soon filter (< 3 days), Course filtering, clear visual feedback  
✅ **Performance**: Optimized check-off logic (no page reload), parallel queries

## Technologies

- **Framework**: Blazor Server (.NET 8)
- **Database**: SQLite (local database file - no cloud services required)
- **Authentication**: ASP.NET Core Identity
- **ORM**: Entity Framework Core

## Getting Started

### Prerequisites

- .NET 8 SDK only
- No database installation needed - SQLite is file-based and included with .NET

### Installation

1. **Clone the repository**

   ```powershell
   git clone https://github.com/miranda-beatriz/CSE325.git
   cd CSE325
   ```

2. **Restore packages**

   ```powershell
   dotnet restore
   ```

3. **Create the database (local SQLite)**

   ```powershell
   dotnet ef database update
   ```

   This will create a `StudySync.db` SQLite database in the project folder.

4. **Run the application**

   ```powershell
   dotnet run
   ```

5. **First Time Setup - Create Your Account**
   - Navigate to `http://localhost:5000` in your browser
   - Click on **"Register"** (top right or link at bottom of login page)
   - Create your account with:
     - Email: any valid email format (e.g., `student@example.com`)
     - Password: minimum 6 characters with uppercase, lowercase, and digit (e.g., `Password123`)
   - After registration, you'll be automatically logged in!

> **Note:** The database is NOT included in the repository. Each developer needs to create their own local database using the commands above.

## Database Setup

The application uses **SQLite** for local development (no SQL Server installation needed).

**Creating the database for the first time:**

```powershell
dotnet ef database update
```

This creates `StudySync.db` in your project folder.

**If you need to reset the database:**

```powershell
dotnet ef database drop
dotnet ef database update
```

> ⚠️ **Note:** After resetting, you'll need to register again as all users are deleted.

## Troubleshooting

### Application won't start

```powershell
dotnet build
```

Check for compilation errors. If successful, try:

```powershell
dotnet clean
dotnet restore
dotnet run
```

### Database errors

If you see "no such table" or migration errors:

```powershell
dotnet ef database drop
dotnet ef database update
```

### Port already in use

If port 5000 is busy, specify a different port:

```powershell
dotnet run --urls "http://localhost:5001"
```

Or edit `Properties/launchSettings.json` to change the default port.

### EF Core tools not found

Install Entity Framework Core tools:

```powershell
dotnet tool install --global dotnet-ef
```

Or update if already installed:

```powershell
dotnet tool update --global dotnet-ef
```

## Deployment to Azure (Optional - Not Implemented)

> **Note:** This project currently uses **SQLite only** to avoid Azure costs. The Azure deployment steps below are provided for reference if you want to deploy to cloud infrastructure in the future.

To deploy to Azure with Azure SQL Database:

1. Create an Azure SQL Database (requires Azure subscription with billing)
2. Update `appsettings.json` with Azure SQL connection string
3. Run migrations against Azure SQL
4. Deploy to Azure App Service

**For detailed Azure deployment instructions, see [DEPLOYMENT.md](DEPLOYMENT.md)**

**Current setup:** The application runs entirely locally with SQLite - no cloud services or costs required.

## Project Structure

- **/Data** - Database models and DbContext
- **/Services** - Business logic services
- **/Components/Pages** - Razor pages for UI
- **/Components/Account** - Authentication pages
- **/Components/Layout** - Layout components

## Usage

1. **Register** - Create a new account
2. **Add Courses** - Start by adding your courses (e.g., "CSE 325")
3. **Add Assignments** - Create assignments linked to courses with due dates
4. **Track Progress** - Mark assignments as In Progress or Done
5. **View Dashboard** - See upcoming deadlines at a glance

## Target Users

University students who need a straightforward way to track courses and tasks without the complexity of a full Learning Management System (LMS).

## Additional Resources

- **[DEPLOYMENT.md](DEPLOYMENT.md)** - Complete Azure deployment guide
- **[GUIDE.md](GUIDE.md)** - Detailed feature guide and usage instructions
- [ASP.NET Core Blazor Documentation](https://learn.microsoft.com/aspnet/core/blazor/)
- [Entity Framework Core Documentation](https://learn.microsoft.com/ef/core/)

## Contributing

This is an educational project. If you want to extend it:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/YourFeature`)
3. Commit your changes (`git commit -m 'Add YourFeature'`)
4. Push to the branch (`git push origin feature/YourFeature`)
5. Open a Pull Request

## License

This project is for educational purposes as part of CSE 325 - February 2026.
