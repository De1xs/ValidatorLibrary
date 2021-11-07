using LibraryUsageImplementation.Models;
using System.Collections.Generic;

namespace LibraryUsageImplementation.DataStorage
{
    public class MyUsersDatabase
    {
        public int CurrentId { get; set; }
        public List<User> UserList { get; set; }
    }
}
