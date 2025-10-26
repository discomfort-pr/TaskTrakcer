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
        return View();
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
                HttpContext.Session.SetInt32("id", _userService.GetUserByEmail(loginData.Email).Id);
                // return View("LoggedIn", _userService.GetUserByEmail(loginData.Email));
                return View("Main");
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