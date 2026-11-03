# Student Management API

ASP.NET Core Web API (.NET 10) following Clean Architecture, with EF Core and SQL Server. Documented and testable with Swagger.

## Architecture
- **Domain**: entities (User, Email, Student). No dependencies.
- **Application**: DTOs, interfaces (IStudentService, IStudentRepository), StudentService.
- **Infrastructure**: EF Core AppDbContext, Fluent API configurations, seed data, StudentRepository, migrations.
- **API**: StudentsController, dependency injection setup, Swagger.

## Endpoints
| Method | Route | Description |
|---|---|---|
| GET | /api/students | Get all students |
| GET | /api/students/{studentId} | Get full details of a student (incl. emails) |
| POST | /api/students | Create a student |
| PUT | /api/students/{studentId} | Update a student |
| DELETE | /api/students/{studentId} | Delete a student (and their user and emails) |

## How to run
1. Install the .NET 10 SDK and SQL Server LocalDB (included with Visual Studio).
2. Clone this repository and open the solution in Visual Studio.
3. Open Package Manager Console, set Default project to StudentManagement.Infrastructure, run: `Update-Database`
4. Press F5. Swagger opens at /swagger with 5 seeded students.