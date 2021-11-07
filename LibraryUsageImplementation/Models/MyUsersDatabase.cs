using System.Collections.Generic;

namespace LibraryUsageImplementation.Models
{
    public class MyUsersDatabase
    {
        public int currentId { get; set; }
        public List<User> userList { get; set; }
    }
}
