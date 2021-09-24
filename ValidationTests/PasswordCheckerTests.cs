using PSP;
using System;
using Xunit;

namespace Validation
{
    public class UserTests
    {
        [Fact]
        public void PasswordCheckerLengthTest()
        {
            // Arrange
            var validator = new PasswordChecker();
            string password = "Ab*12";

            // Act
            var result = validator.IsValidPassword(password);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void PasswordCheckerUppercaseTest()
        {
            // Arrange
            var validator = new PasswordChecker();
            string password = "a*123lal$asdoqw123";

            // Act
            var result = validator.IsValidPassword(password);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void PasswordCheckerSpecialCharTest()
        {
            // Arrange
            var validator = new PasswordChecker();
            string password = "Ab123LoA4213";

            // Act
            var result = validator.IsValidPassword(password);

            // Assert
            Assert.False(result);
        }
    }
}
