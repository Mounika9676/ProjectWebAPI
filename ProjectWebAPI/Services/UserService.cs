using Microsoft.EntityFrameworkCore;
using ProjectWebAPI.Entity;
using ProjectWebAPI.Services;

namespace ProjectWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly oExamDbContext _context;

        public UserService(oExamDbContext context)
        {
            _context = context;
        }

        //public UserService()
        //{
        //    _context = new MyContext();
        //}
        public void CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteUser(string userId)
        {
            User user = _context.Users.Find(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public void EditUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(string userId)
        {
            return _context.Users.Find(userId);
        }

        public User ValidteUser(string email, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }

    }

}
