# StudySync

A web application designed to help university students organize their academic life efficiently.

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

1. Clone the repository
2. Navigate to the project folder
3. Update connection string in `appsettings.json` if needed (default uses LocalDB)
4. Create the database:
   ```powershell
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
5. Run the application:
   ```powershell
   dotnet run
   ```
6. Navigate to `http://localhost:5000` in your browser

## Database Setup

The application uses SQL Server with Entity Framework Core migrations.

To create the database:

```powershell
dotnet ef migrations add InitialCreate
dotnet ef database update
```

To reset the database:

```powershell
dotnet ef database drop
dotnet ef database update
```

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
