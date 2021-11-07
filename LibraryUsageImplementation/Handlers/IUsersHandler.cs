using LibraryUsageImplementation.Models;
using System.Collections.Generic;

namespace LibraryUsageImplementation.Handlers
{
    public interface IUsersHandler
    {
        public User CreateUser(User user);
        public User GetUser(int userId);
        public List<User> GetAllUsers();
        public User DeleteUser(int userId);
        public User ModifyUser(User user);
    }
}
