using FakeItEasy;
using LibraryUsageImplementation.DataStorage;
using LibraryUsageImplementation.Handlers;
using LibraryUsageImplementation.Models;
using System.Collections.Generic;
using Xunit;

namespace LibraryUsageImplementationTests
{
    public class UsersHandlerTests
    {
        private readonly IDatabaseProvider _mockDbProvider;
        private readonly UsersHandler _handler;
        private readonly User _validUser;
        private readonly User _updatedValidUser;
        private readonly User _validUser2;
        private readonly User _invalidUser;
        private readonly MyUsersDatabase _myDb;

        public UsersHandlerTests()
        {
            _mockDbProvider = A.Fake<IDatabaseProvider>();

            _myDb = new MyUsersDatabase() { CurrentId = 0, UserList = new List<User>() };

            A.CallTo(() => _mockDbProvider.GetDatabase(A<string>._))
                .Returns(_myDb);

            _handler = new UsersHandler(_mockDbProvider);

            _validUser = new User()
            {
                UserId = 0,
                Name = "testName",
                LastName = "testLastName",
                PhoneNumber = "867744448",
                Email = "testmail@gmail.com",
                Address = "testAddress",
                Password = "Kas514wss"
            };

            _updatedValidUser = new User()
            {
                UserId = 0,
                Name = "updatedTestName",
                LastName = "updatedTestLastName",
                PhoneNumber = "867744448",
                Email = "testmail@gmail.com",
                Address = "testAddress",
                Password = "Kas514wss"
            };

            _validUser2 = new User()
            {
                UserId = 1,
                Name = "testName2",
                LastName = "testLastName2",
                PhoneNumber = "867744449",
                Email = "testmail2@gmail.com",
                Address = "testAddress2",
                Password = "Kas514wss2"
            };

            _invalidUser = new User()
            {
                UserId = 0,
                Name = "testName",
                LastName = "testLastName",
                PhoneNumber = "867744448",
                Email = "testmail@gmail.com",
                Address = "testAddress",
                Password = "456"
            };
        }

        [Fact]
        public void CreateUser_Successful()
        {
            var result = _handler.CreateUser(_validUser);

            A.CallTo(() => _mockDbProvider.SaveFileChanges(A<string>._, A<MyUsersDatabase>._))
                .MustHaveHappenedOnceExactly();

            Assert.Equal(1, _myDb.CurrentId);

            Assert.Equal(_validUser.Name, result.Name);
        }

        [Fact]
        public void CreateUser_ThrowsException_WhenDataInvalid()
        {
            var message = Assert.Throws<ValidationException>(() => _handler.CreateUser(_invalidUser))
                .Message;

            Assert.Equal("Password invalid", message);

            A.CallTo(() => _mockDbProvider.SaveFileChanges(A<string>._, A<MyUsersDatabase>._))
                .MustNotHaveHappened();

            Assert.Equal(0, _myDb.CurrentId);
        }

        [Fact]
        public void GetUser_Successful()
        {
            _myDb.UserList.Add(_validUser);

            var result = _handler.GetUser(0);

            Assert.Equal(_validUser, result);
        }

        [Fact]
        public void GetUser_ReturnsNull_WhenUserNotFound()
        {
            var result = _handler.GetUser(0);

            Assert.Null(result);
        }

        [Fact]
        public void DeleteUser_Successful()
        {
            _myDb.UserList.Add(_validUser);

            var result = _handler.DeleteUser(0);

            A.CallTo(() => _mockDbProvider.SaveFileChanges(A<string>._, A<MyUsersDatabase>._))
                .MustHaveHappenedOnceExactly();

            Assert.Equal(_validUser, result);
        }

        [Fact]
        public void DeleteUser_ReturnsNull_WhenUserNotFound()
        {
            var result = _handler.DeleteUser(0);

            A.CallTo(() => _mockDbProvider.SaveFileChanges(A<string>._, A<MyUsersDatabase>._))
                .MustNotHaveHappened();

            Assert.Null(result);
        }

        [Fact]
        public void GetAllUsers_Successful()
        {
            _myDb.UserList.Add(_validUser);
            _myDb.UserList.Add(_validUser2);

            var expectedList = new List<User> { _validUser, _validUser2 };
            var result = _handler.GetAllUsers();

            Assert.Equal(expectedList, result);
        }

        [Fact]
        public void GetAllUsers_ReturnsEmptyList_WhenNoUsers()
        {
            var result = _handler.GetAllUsers();

            Assert.Empty(result);
        }

        [Fact]
        public void ModifyUser_Successful()
        {
            _myDb.UserList.Add(_validUser);

            var result = _handler.ModifyUser(_updatedValidUser);

            A.CallTo(() => _mockDbProvider.SaveFileChanges(A<string>._, A<MyUsersDatabase>._))
                .MustHaveHappenedOnceExactly();

            Assert.Equal(_updatedValidUser, result);
        }

        [Fact]
        public void ModifyUser_ReturnsNull_WhenUserNotFound()
        {
            var result = _handler.ModifyUser(_updatedValidUser);

            A.CallTo(() => _mockDbProvider.SaveFileChanges(A<string>._, A<MyUsersDatabase>._))
                .MustNotHaveHappened();

            Assert.Null(result);
        }

        [Fact]
        public void ModifyUser_ThrowsException_WhenDataInvalid()
        {
            _myDb.UserList.Add(_validUser);

            var message = Assert.Throws<ValidationException>(() => _handler.ModifyUser(_invalidUser))
                .Message;

            Assert.Equal("Password invalid", message);

            A.CallTo(() => _mockDbProvider.SaveFileChanges(A<string>._, A<MyUsersDatabase>._))
                .MustNotHaveHappened();

            Assert.Equal(0, _myDb.CurrentId);
        }
    }
}
