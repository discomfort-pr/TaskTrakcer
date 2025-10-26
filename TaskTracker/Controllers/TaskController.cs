using Microsoft.AspNetCore.Mvc;
using TaskTracker.Repositories;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Controllers;

[Route("[controller]")]
public class TaskController : Controller
{
    private TaskRepository _taskRepository;
    
    public TaskController(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository; 
    }

    [HttpGet("all")]
    public IActionResult TasksPage()
    {
        if (HttpContext.Session.GetInt32("id") == null)
        {
            return RedirectToAction("RegistrationPage", "User");
        }
        
        return View("TasksPage", _taskRepository.GetTasksByUserId(HttpContext.Session.GetInt32("id")));
    }

    [HttpGet("add")]
    public IActionResult AddTaskPage()
    {
        if (HttpContext.Session.GetInt32("id") == null)
        {
            return RedirectToAction("RegistrationPage", "User");
        }

        ViewData["Status"] = "";
        
        return View();
    }
    
    [HttpPost("add")]
    public IActionResult AddTask(Task task)
    {
        if (HttpContext.Session.GetInt32("id") == null)
        {
            return RedirectToAction("RegistrationPage", "User");
        }
        
        task.Status = "New";
        task.UserId = HttpContext.Session.GetInt32("id").Value;
        _taskRepository.AddTask(task);
        // return RedirectToAction("TasksPage");
        ViewData["Status"] = "Задача успешно добавлена!";
        ModelState.Clear();
        // return View("AddTaskPage");
        return View("TasksPage", _taskRepository.GetTasksByUserId(HttpContext.Session.GetInt32("id")));
    }

    public IActionResult RemoveTask(int taskId)
    {
        _taskRepository.DeleteTask(taskId);
        return View("TasksPage", _taskRepository.GetAllTasks());
    }

    // public IActionResult GetTask(int taskId)
    // {
    //     
    // }
}