using EmployeeApplication.Data;
using EmployeeApplication.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
#if DEBUG
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<EmployeeContext>(
    options => options.UseSqlServer("Server=.;Database=Employee;Integrated Security=True;TrustServerCertificate=True;"));
#endif
builder.Services.AddScoped<EmployeeRepository, EmployeeRepository>();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();


app.MapDefaultControllerRoute();

app.Run();
