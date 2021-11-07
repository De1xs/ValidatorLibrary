using LibraryUsageImplementation.DataStorage;
using LibraryUsageImplementation.Models;
using System.Collections.Generic;
using System.Linq;
using ValidatorsUnitTests;
using ValidatorsUnitTests.Source.Validators;

namespace LibraryUsageImplementation.Handlers
{
    public class UsersHandler : IUsersHandler
    {
        private readonly string databaseFileName = "MyDatabase.json";

        private readonly MyUsersDatabase myDatabase;
        private readonly IDatabaseProvider _databaseProvider;
        public UsersHandler(IDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
            myDatabase = databaseProvider.GetDatabase(databaseFileName);
        }

        public User CreateUser(User user)
        {
            ValidateUserInfo(user); 


            //save
            user.UserId = myDatabase.CurrentId;
            myDatabase.CurrentId++;

            myDatabase.UserList.Add(user);



            _databaseProvider.SaveFileChanges(databaseFileName, myDatabase);

            return user;
        }

        public User DeleteUser(int userId)
        {
            var user = GetUser(userId);
            if(user != null)
            {
                myDatabase.UserList.Remove(user);

                _databaseProvider.SaveFileChanges(databaseFileName, myDatabase);
                return user;
            }
            else
            {
                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            return myDatabase.UserList;
        }

        public User GetUser(int userId)
        {
            return myDatabase.UserList.Where(u => u.UserId == userId).SingleOrDefault();
        }

        public User ModifyUser(User user)
        {
            var oldUser = GetUser(user.UserId);

            if(oldUser != null)
            {
                ValidateUserInfo(user);

                var indexToChange = myDatabase.UserList.IndexOf(oldUser);
                myDatabase.UserList[indexToChange] = user;

                _databaseProvider.SaveFileChanges(databaseFileName, myDatabase);

                return user;
            }
            else
            {
                return null;
            }
        }

        private static void ValidateUserInfo(User user)
        {
            if (!EmailValidator.isEmailValid(user.Email))
            {
                throw new ValidationException("Email invalid");
            }

            if (!PasswordValidator.IsPasswordValid(user.Password))
            {
                throw new ValidationException("Password invalid");
            }

            if (!PhoneNumberValidator.IsPhoneNumberValid(user.PhoneNumber))
            {
                throw new ValidationException("Phone number invalid");
            }
        }
    }
}
