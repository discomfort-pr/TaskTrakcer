using TaskTracker.DbContext;
using TaskTracker.Exceptions;
using TaskTracker.Models;

namespace TaskTracker.Repositories;

public class UserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public User AddUser(User user)
    {
        CheckUniqueEmail(user.Email);
        var savedUser = _db.Users.Add(user).Entity;
        _db.SaveChanges();
        
        return savedUser;
    }

    public User UpdateUser(User user)
    {
        var updatedUser = GetUserById(user.Id);
        _db.Entry(updatedUser).CurrentValues.SetValues(user);
        _db.SaveChanges();
        
        return updatedUser;
    }

    public User DeleteUser(int id)
    {
        var deletedUser = GetUserById(id);
        _db.Users.Remove(deletedUser);
        _db.SaveChanges();
        
        return deletedUser;
    }

    public List<User> GetAllUsers()
    {
        return _db.Users.ToList();
    }
    
    public User GetUserById(int id)
    {
        var user = _db.Users.Find(id);
        return user ?? throw new NotFoundException($"User with id {id} not found");
    }

    public User GetUserByEmail(string email)
    {
        var user = _db.Users.FirstOrDefault(u => u.Email == email);
        return user ?? throw new NotFoundException($"User with email {email} not found");
    }

    private void CheckUniqueEmail(string email)
    {
        try
        {
            GetUserByEmail(email);
        }
        catch (NotFoundException)
        {
            return;
        }
        throw new AlreadyExistsException($"User with email {email} already exists");
    }

    private void PrepareToUpdate(User updated, User userData)
    {
        
    }
}