# StudySync - Quick Start Guide

## ✅ Project Status

The project is **100% complete** and ready to use. It includes:

### Phase 1: Infrastructure ✓

- [x] Blazor Server project configured
- [x] SQL Server database configured
- [x] Models: Users, Courses, Assignments
- [x] UI/Layout with consistent branding
- [x] Responsive NavMenu
- [x] Custom color palette

### Phase 2: Authentication ✓

- [x] ASP.NET Identity implemented
- [x] User registration
- [x] Login/Logout
- [x] Route protection (authenticated users only)

### Phase 3: CRUD Functionality ✓

- [x] **Courses**: Create, Read, Update, Delete
- [x] **Assignments**: Create, Read, Update, Delete
- [x] Validation with DataAnnotations
- [x] Filter by status (To Do, In Progress, Done)
- [x] Priority system (High, Medium, Low)

### Phase 4: Dashboard & Business Logic ✓

- [x] **Enhanced Dashboard** with multiple views
- [x] **Due Soon Filter** (< 3 days) - Usability requirement
- [x] **Filter by Course** - Usability requirement
- [x] **Optimized check-off logic** - No page reload (Performance requirement)
- [x] **Parallel data loading** - Better performance
- [x] Overdue assignments tracking

## 🚀 How to Run

### Local Development

1. Open terminal in the project folder
2. Run:
   ```powershell
   dotnet run
   ```
3. Open browser at: `http://localhost:5000`

### First Time Setup (Database)

If you need to create the database:

```powershell
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## 📝 How to Use

### 1. Register

- Go to `http://localhost:5000`
- Click on "Register"
- Create account with email and password (minimum 6 characters, must include uppercase, lowercase, and digit)

### 2. Add Courses

- After login, go to "My Courses"
- Click "+ Add Course"
- Fill in: Name (e.g., "CSE 325"), Code (optional), Description (optional)
- Click "Create Course"

### 3. Add Assignments

- Go to "Assignments"
- Click "+ Add Assignment"
- Fill in:
  - **Title**: Assignment name (e.g., "Homework Chapter 5")
  - **Description**: Optional details
  - **Course**: Select from your courses
  - **Due Date**: Deadline
  - **Priority**: Low/Medium/High
  - **Status**: To Do/In Progress/Done
- Click "Create Assignment"

### 4. Dashboard Features

#### View Filters

The dashboard now has three powerful view options:

- **📅 Upcoming (7 days)**: All assignments due in the next week
- **⚡ Due Soon (3 days)**: Urgent assignments due in ≤ 3 days
- **⚠️ Overdue**: Past due assignments that need attention

#### Course Filter

- Select a specific course from the dropdown to see only its assignments
- Or select "All Courses" to see everything

#### Performance Optimization

- Click "✓ Complete" to mark tasks as done
- **No page reload** - instant UI update
- Data loads in parallel for faster performance
- Smart caching reduces unnecessary database calls

### 5. Statistics

The dashboard shows:

- Number of active courses
- Tasks due soon (next 3 days)
- Overdue tasks count

## 📊 Database Structure

### Users (Identity)

- Id, Email, PasswordHash, etc.

### Courses

- Id, Name, Code, Description, UserId, CreatedAt
- Relationship: One user has many courses

### Assignments

- Id, Title, Description, DueDate, Status, Priority
- CourseId (FK), UserId (FK), CreatedAt, CompletedAt
- Relationship: One course has many assignments

## 🎨 Design Features

- **Primary Colors**:
  - Primary: #4F46E5 (Indigo)
  - Success: #10B981 (Green)
  - Danger: #EF4444 (Red)
  - Warning: #F59E0B (Amber)

- **Responsive**: Works on desktop and mobile
- **Icons**: Emojis for better visual experience
- **Visual States**:
  - Overdue tasks: Red border
  - Completed tasks: Reduced opacity
  - Priorities: Distinctive colors
  - Due soon: Orange warning badge

## 🔧 Technical Features

### Validation

- All required fields marked with \*
- Valid email required
- Password minimum 6 characters (includes uppercase, lowercase, digit)
- StringLength on text fields

### Security

- Hashed passwords (Identity)
- Users only see their own data
- Routes protected with `[Authorize]`
- Anti-forgery tokens in forms

### Performance Optimizations

- **Parallel data loading**: All dashboard queries run concurrently
- **Optimized check-off**: Updates UI without full page reload
- **Smart queries**: EF Core `Include()` prevents N+1 queries
- **Priority sorting**: Assignments ordered by due date and priority
- **Client-side filtering**: Course filter doesn't hit database

### Usability Features

- **3-day Due Soon filter**: Quickly identify urgent tasks
- **Course-based filtering**: Focus on specific class assignments
- **Visual urgency indicators**: Color-coded warnings for overdue/urgent items
- **Tab-based navigation**: Easy switching between views
- **Empty states**: Helpful messages when no data

## 📁 Important Files

```
StudySync/
├── Data/                          # Models and DbContext
│   ├── ApplicationUser.cs
│   ├── ApplicationDbContext.cs
│   ├── Course.cs
│   └── Assignment.cs
├── Services/                      # Business logic
│   ├── CourseService.cs          # Enhanced with filters
│   └── AssignmentService.cs      # New: GetDueSoonAsync, GetOverdueAsync
├── Components/
│   ├── Account/Pages/            # Login/Register
│   ├── Pages/                    # Main pages
│   │   ├── Home.razor            # Enhanced Dashboard with filters
│   │   ├── Courses/              # CRUD Courses
│   │   └── Assignments/          # CRUD Assignments
│   └── Layout/                   # MainLayout, NavMenu
├── wwwroot/
│   └── app.css                   # Custom styles + new filter styles
├── Program.cs                    # Configuration (SQL Server)
└── appsettings.json             # Connection string
```

## 🐛 Troubleshooting

### Application won't start

```powershell
dotnet build
# If errors, review and report them
```

### Database errors

```powershell
dotnet ef database drop
dotnet ef database update
```

### Port already in use

- Change port in `launchSettings.json`
- Or use: `dotnet run --urls "http://localhost:5001"`

## 🚀 Deployment to Azure (Optional - Not Currently Used)

> **Note:** This project uses SQLite locally and does not require Azure. This section is for reference only.

### Prerequisites

- Azure account (with active subscription and billing)
- Azure SQL Database created

### Steps

1. Update `appsettings.json` with Azure SQL connection string:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=tcp:server.database.windows.net,1433;Database=StudySync;User ID=youradmin;Password=yourpassword;Encrypt=true;Connection Timeout=30;"
     }
   }
   ```

2. Apply migrations to Azure:

   ```powershell
   dotnet ef database update
   ```

3. Publish to Azure App Service:

   ```powershell
   dotnet publish -c Release
   ```

4. Deploy using Visual Studio, VS Code Azure extension, or Azure CLI

## 📚 Future Enhancements (Optional)

- [ ] Export assignments to PDF
- [ ] Email notifications
- [ ] Calendar view
- [ ] Custom tags
- [ ] Productivity statistics
- [ ] Dark mode
- [ ] Advanced search

## 🎓 Project Requirements (Completed)

✅ **User Authentication**: Identity implemented with secure login/registration  
✅ **CRUD Functionality**: Full CRUD for Courses and Assignments  
✅ **Application Design Standards**: DataAnnotations validation, proper error handling  
✅ **Branding**: Consistent color palette, cohesive UI design  
✅ **Database**: SQLite with EF Core, proper relationships (no cloud/server required)  
✅ **Security**: Hashed passwords, user-specific data isolation  
✅ **Usability**: Due Soon filter (< 3 days), Course filtering, clear visual indicators  
✅ **Performance**: Optimized check-off (no reload), parallel data loading, efficient queries

## 📧 Support

For questions or issues:

1. Review this guide
2. Check terminal for errors
3. Verify database connection string
4. Review `README.md` for technical details

---

**Project created for CSE 325 - February 2026**  
**Built with SQLite for local development (no cloud costs)**
