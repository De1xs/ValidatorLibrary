using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryUsageImplementation.DataStorage
{
    public interface IDatabaseProvider
    {
        public MyUsersDatabase GetDatabase(string fileName);

        public void SaveFileChanges(string fileName, MyUsersDatabase myDatabase);
    }
}
