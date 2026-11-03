using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;
using StudentManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ---------- 1. Register services (Dependency Injection) ----------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddInfrastructure(connectionString);              // DbContext + repository
builder.Services.AddScoped<IStudentService, StudentService>();     // application service

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();   // lets Swagger discover the endpoints
builder.Services.AddSwaggerGen();             // generates the OpenAPI (Swagger) document

var app = builder.Build();

// ---------- 2. Configure the HTTP request pipeline (middleware) ----------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();     // serves /swagger/v1/swagger.json
    app.UseSwaggerUI();   // serves the interactive page at /swagger
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();