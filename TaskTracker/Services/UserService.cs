using TaskTracker.Dto;
using TaskTracker.Exceptions;
using TaskTracker.Models;
using TaskTracker.Repositories;

namespace TaskTracker.Services;

public class UserService
{
    private UserRepository _userRepository;
    
    public UserService(UserRepository userRepository) 
    {
        _userRepository = userRepository;
    }
    
    public User AddUser(User user)
    {
        return _userRepository.AddUser(user);
    }

    public User UpdateUser(User user)
    {
        return _userRepository.UpdateUser(user);
    }

    public User DeleteUser(int id)
    {
        return _userRepository.DeleteUser(id);
    }

    public List<User> GetAllUsers()
    {
       return _userRepository.GetAllUsers();
    }
    
    public User GetUserById(int id)
    {
        return _userRepository.GetUserById(id);
    }

    public User GetUserByEmail(string email)
    {
        return _userRepository.GetUserByEmail(email);
    }

    public bool LogIn(LoginData loginData)
    {
        User account = GetUserByEmail(loginData.Email);
        return account.Password == loginData.Password;
    }
}