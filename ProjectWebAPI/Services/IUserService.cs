using ProjectWebAPI.Entity;

namespace ProjectWebAPI.Services
{
    public interface IUserService
    {
        void CreateUser(User user); //create new user
        List<User> GetAllUsers(); // get all users
        User GetUser(string userId);
        void EditUser(User user);
        void DeleteUser(string userId);
        User ValidteUser(string email, string password);
    }
}