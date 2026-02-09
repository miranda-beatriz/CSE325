# StudySync

A web application designed to help university students organize their academic life efficiently.

## 🚀 Quick Start (First Time Setup)

**Important:** The database is not included in this repository. Follow these steps:

1. **Clone and restore**

   ```powershell
   git clone https://github.com/YOUR-USERNAME/StudySync.git
   cd StudySync
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
- **Database**: SQL Server (Azure-ready)
- **Authentication**: ASP.NET Core Identity
- **ORM**: Entity Framework Core

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB for development, or Azure SQL for production)

### Installation

1. **Clone the repository**

   ```powershell
   git clone https://github.com/YOUR-USERNAME/StudySync.git
   cd StudySync
   ```

2. **Restore packages**

   ```powershell
   dotnet restore
   ```

3. **Create the database**

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

## Deployment to Azure

1. Create an Azure SQL Database
2. Update the connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=tcp:yourserver.database.windows.net,1433;Database=StudySync;..."
     }
   }
   ```
3. Run migrations against Azure SQL:
   ```powershell
   dotnet ef database update
   ```
4. Deploy to Azure App Service

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

## License

This project is for educational purposes.
