using Exam.App;
using Exam.App.Domain;
using Exam.App.Infrastructure;
using Exam.App.Infrastructure.Database;
using Exam.App.Mappers;
using Exam.App.Middleware;
using Exam.App.Repository;
using Exam.App.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Startup.AddLogging(builder);
Startup.AddSwagger(builder);
Startup.AddCors(builder);
Startup.AddAuthenticationAndAuthorization(builder);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Service and Repository Dependency Injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();
builder.Services.AddScoped<IPatientsService, PatientsService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAnimalsRepository, AnimalsRepository>();
builder.Services.AddScoped<IAnimalsService, AnimalsService>();
builder.Services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
builder.Services.AddScoped<IAppointmentsService, AppointmentsService>();
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();
builder.Services.AddScoped<IReportsService, ReportsService>();




var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();