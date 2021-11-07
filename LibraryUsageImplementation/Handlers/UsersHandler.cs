using LibraryUsageImplementation.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ValidatorsUnitTests;
using ValidatorsUnitTests.Source.Validators;

namespace LibraryUsageImplementation.Handlers
{
    public class UsersHandler : IUsersHandler
    {
        private string databaseFileName = "MyDatabase.json";

        private MyUsersDatabase myDatabase;
        public UsersHandler()
        {
            if (!File.Exists(databaseFileName))
            {
                myDatabase = new MyUsersDatabase() { currentId = 0, userList = new List<User>() };
                string jsonString = JsonSerializer.Serialize(myDatabase);
                File.WriteAllText(databaseFileName, jsonString);
            }
            else
            {
                var jsonString = File.ReadAllText(databaseFileName);
                myDatabase = JsonSerializer.Deserialize<MyUsersDatabase>(jsonString);
                if(myDatabase.userList == null)
                {
                    myDatabase = new MyUsersDatabase() { currentId = 0, userList = new List<User>() { } };
                }
            }

            
        }

        public User CreateUser(User user)
        {
            ValidateUserInfo(user); 


            //save
            user.userId = myDatabase.currentId;
            myDatabase.currentId++;

            myDatabase.userList.Add(user);

            

            SaveFileChanges();

            return user;
        }

        public User DeleteUser(int userId)
        {
            var user = GetUser(userId);
            if(user != null)
            {
                myDatabase.userList.Remove(user);

                SaveFileChanges();
                return user;
            }
            else
            {
                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            return myDatabase.userList;
        }

        public User GetUser(int userId)
        {
            return myDatabase.userList.Where(u => u.userId == userId).SingleOrDefault();
        }

        public User ModifyUser(User user)
        {
            var oldUser = GetUser(user.userId);

            if(oldUser != null)
            {
                ValidateUserInfo(user);

                var indexToChange = myDatabase.userList.IndexOf(oldUser);
                myDatabase.userList[indexToChange] = user;

                SaveFileChanges();

                return oldUser;
            }
            else
            {
                return null;
            }
        }

        public void ValidateUserInfo(User user)
        {
            if (!EmailValidator.isEmailValid(user.email))
            {
                throw new ValidationException("Email invalid");
            }

            if (!PasswordValidator.IsPasswordValid(user.password))
            {
                throw new ValidationException("Password invalid");
            }

            if (!PhoneNumberValidator.IsPhoneNumberValid(user.phoneNumber))
            {
                throw new ValidationException("Phone number invalid");
            }
        }

        private void SaveFileChanges()
        {
            string jsonString = JsonSerializer.Serialize(myDatabase);
            File.WriteAllText(databaseFileName, jsonString);
        }
    }
}
