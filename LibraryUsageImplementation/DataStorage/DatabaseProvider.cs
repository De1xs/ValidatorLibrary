using LibraryUsageImplementation.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LibraryUsageImplementation.DataStorage
{
    public class DatabaseProvider : IDatabaseProvider
    {
        public MyUsersDatabase GetDatabase(string fileName)
        {
            MyUsersDatabase myDatabase;

            if (!File.Exists(fileName))
            {
                myDatabase = new MyUsersDatabase() { CurrentId = 0, UserList = new List<User>() };
                string jsonString = JsonSerializer.Serialize(myDatabase);
                File.WriteAllText(fileName, jsonString);
            }
            else
            {
                var jsonString = File.ReadAllText(fileName);
                myDatabase = JsonSerializer.Deserialize<MyUsersDatabase>(jsonString);
                if (myDatabase.UserList == null)
                {
                    myDatabase = new MyUsersDatabase() { CurrentId = 0, UserList = new List<User>() { } };
                }
            }

            return myDatabase;
        }

        public void SaveFileChanges(string fileName, MyUsersDatabase myDatabase)
        {
            string jsonString = JsonSerializer.Serialize(myDatabase);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
