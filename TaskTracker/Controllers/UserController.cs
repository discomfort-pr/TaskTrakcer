using Microsoft.AspNetCore.Mvc;
using TaskTracker.Dto;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.Services;

namespace TaskTracker.Controllers;

[Route("[controller]")]
public class UserController : Controller
{
    private UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    // [HttpGet]
    // public IActionResult Create()
    // {
    //     return View();
    // }
    //
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public IActionResult Create(User user)
    // {
    //     HttpContext.Session.SetInt32("UserId", user.Id);
    //
    //     if (!ModelState.IsValid)
    //     {
    //         Console.WriteLine("ошибка валидации");
    //         return View(user);
    //     }
    //     // Добавляем в статический список (в реальном приложении НЕ использовать!)
    //     _users.Add(user);
    //         
    //     ViewData["Message"] = $"Пользователь сохранен! Всего пользователей: {_users.Count}";
    //     return View("Success", user);
    //     // return RedirectToAction("Success", new { userId = user.Id });
    // }
    //
    // [HttpGet]
    // [Route("account")]
    // public string GetUserPage()
    // {
    //     var accountId = HttpContext.Session.GetInt32("UserId");
    //
    //     if (accountId == null)
    //     {
    //         return "not found";
    //     }
    //
    //     return $"welcome, {accountId}";
    // }



    [HttpGet]
    [Route("register")]
    public IActionResult RegistrationPage()
    {
        ViewData["Notification"] = "";
        return View();
    }

    [HttpPost]
    [Route("register")]
    [ValidateAntiForgeryToken]
    public IActionResult RegisterAccount(User user)
    {
        try
        {
            _userService.AddUser(user);
        }
        catch (AlreadyExistsException exception)
        {
            ViewData["Notification"] = exception.Message;
            return View("RegistrationPage");
        }
        return View("Success", user);
    }

    [HttpGet]
    [Route("login")]
    public IActionResult LoginPage()
    {
        return View("LoginPage");
    }

    [HttpPost]
    [Route("login")]
    [ValidateAntiForgeryToken]
    public IActionResult LogIn(LoginData loginData)
    {
        try
        {
            if (_userService.LogIn(loginData))
            {
                return View("LoggedIn", _userService.GetUserByEmail(loginData.Email));
            }

            ViewData["Notification"] = "Введен неверный пароль";
            return View("LoginPage");
        }
        catch (NotFoundException exception)
        {
            ViewData["Notification"] = exception.Message;
            return View("LoginPage");
        }
    }
}