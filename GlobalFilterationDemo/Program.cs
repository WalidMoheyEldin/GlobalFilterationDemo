using GlobalFilterationDemo.Data;
using GlobalFilterationDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

ServiceProvider? provider = builder.Services.BuildServiceProvider();
ApplicationDbContext db = provider.GetService<ApplicationDbContext>();
if (!db.Employees.Any())
{
    db.Employees.AddRange(new List<Employee>() {
        new Employee{ Name = "Employee 1", Salary = 10000 },
        new Employee{ Name = "Employee 2", Salary = 20000 },
        new Employee{ Name = "Employee 3", Salary = 30000 },
        new Employee{ Name = "Employee 4", Salary = 40000 },
        new Employee{ Name = "Employee 5", Salary = 50000 },
        new Employee{ Name = "Employee 6", Salary = 60000 },
        new Employee{ Name = "Employee 7", Salary = 70000 },
        new Employee{ Name = "Employee 8", Salary = 80000 },
        new Employee{ Name = "Employee 9", Salary = 90000 },
        new Employee{ Name = "Employee 10", Salary = 100000 }
    });
    db.SaveChanges();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employees}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
