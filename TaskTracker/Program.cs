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
}

app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.MapControllers();

// using (var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     
//     try
//     {
//         var created = db.Database.EnsureCreated();
//         
//         if (created)
//         {
//             Console.WriteLine("База данных создана");
//             
//             // Добавляем тестовые данные только если база была создана
//             if (!db.Users.Any())
//             {
//                 UserRepository userRepository = new UserRepository(db);
//                 
//                 User tom = new User 
//                 { 
//                     Name = "Tom", 
//                     Email = "m.akkizov@yandex.ru",  
//                     Password = "123456" 
//                 };
//                 
//                 User tom2 = new User 
//                 { 
//                     Name = "Tom2", 
//                     Email = "m.azov@yandex.ru",  
//                     Password = "123456" 
//                 };
//
//                 User tom3 = new User
//                 {
//                     Id = 2,
//                     Name = "Tom3",
//                     Email = "m.akk@yandex.ru",
//                     Password = "123433"
//                 };
//
//
//                 userRepository.AddUser(tom);
//                 Console.WriteLine(string.Join(", ", userRepository.GetAllUsers().Select(u => u.ToString())));
//                 
//                 userRepository.AddUser(tom2);
//                 Console.WriteLine(string.Join(", ", userRepository.GetAllUsers().Select(u => u.ToString())));
//                 
//                 userRepository.DeleteUser(tom.Id);
//                 Console.WriteLine(string.Join(", ", userRepository.GetAllUsers().Select(u => u.ToString())));
//
//                 userRepository.UpdateUser(tom3);
//                 Console.WriteLine(string.Join(", ", userRepository.GetAllUsers().Select(u => u.ToString())));
//                 
//                 // Console.WriteLine("Тестовые данные добавлены");
//             }
//         }
//     }
//     catch (Exception ex)
//     {
//         Console.WriteLine($"Ошибка: {ex.Message}");
//         // Проверьте подключение к PostgreSQL
//         Console.WriteLine("Убедитесь, что PostgreSQL запущен и доступен");
//     }
// }

app.Run();