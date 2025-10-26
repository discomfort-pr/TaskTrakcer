using Microsoft.EntityFrameworkCore;
using TaskTracker.DbContext;
using TaskTracker.Models;
using TaskTracker.Repositories;
using TaskTracker.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<TaskRepository>();
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    
    
    // var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
    // userRepository.AddUser(new User
    // {
    //     Id = 1,
    //     Name = "John Doe",
    //     Email = "m.akk.@ya.ru",
    //     Password = "12345"
    // });
    // userRepository.UpdateUser(new User
    // {
    //     Id = 1,
    //     Password = "zhuzha"
    // });
    // Console.WriteLine(userRepository.GetUserById(1));
}

app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.MapControllers();

app.Run();