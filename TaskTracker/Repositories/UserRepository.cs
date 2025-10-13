using TaskTracker.DbContext;
using TaskTracker.Exceptions;
using TaskTracker.Models;

namespace TaskTracker.Repositories;

public class UserRepository
{
    protected ApplicationDbContext Db;

    public UserRepository(ApplicationDbContext db)
    {
        Db = db;
    }

    public User AddUser(User user)
    {
        CheckUniqueEmail(user.Email);
        var savedUser = Db.Users.Add(user).Entity;
        Db.SaveChanges();
        
        return savedUser;
    }

    public User UpdateUser(User user)
    {
        var updatedUser = GetUserById(user.Id);
        Db.Entry(updatedUser).CurrentValues.SetValues(user);
        Db.SaveChanges();
        
        return updatedUser;
    }

    public User DeleteUser(int id)
    {
        var deletedUser = GetUserById(id);
        Db.Users.Remove(deletedUser);
        Db.SaveChanges();
        
        return deletedUser;
    }

    public List<User> GetAllUsers()
    {
        return Db.Users.ToList();
    }
    
    public User GetUserById(int id)
    {
        var user = Db.Users.Find(id);
        return user ?? throw new NotFoundException($"User with id {id} not found");
    }

    public User GetUserByEmail(string email)
    {
        var user = Db.Users.FirstOrDefault(u => u.Email == email);
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
}