using TaskTracker.DbContext;
using TaskTracker.Exceptions;
using Task = TaskTracker.Models.Task;

namespace TaskTracker.Repositories;

public class TaskRepository
{
    private readonly ApplicationDbContext _db;
    
    public TaskRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public Task AddTask(Task task)
    {
        task.Status = "New";
        var savedTask = _db.Tasks.Add(task).Entity;
        _db.SaveChanges();
        
        return savedTask;
    }
    
    public Task UpdateTask(Task task)
    {
        var updatedTask = GetTaskById(task.Id);
        _db.Entry(updatedTask).CurrentValues.SetValues(task);
        _db.SaveChanges();
        
        return updatedTask;
    }

    public Task DeleteTask(int id)
    {
        var deletedTask = GetTaskById(id);
        _db.Tasks.Remove(deletedTask);
        _db.SaveChanges();
        
        return deletedTask;
    }

    public List<Task> GetAllTasks()
    {
        return _db.Tasks.ToList();
    }
    
    public Task GetTaskById(int id)
    {
        var task = _db.Tasks.Find(id);
        return task ?? throw new NotFoundException($"Task with id {id} not found");
    }

    public List<Task> GetTasksByUserId(int? userId)
    {
        if (userId == null)
        {
            throw new NotFoundException($"User with id {userId} not found");
        }
        return _db.Tasks.Where(x => x.UserId == userId).ToList();
    }
}