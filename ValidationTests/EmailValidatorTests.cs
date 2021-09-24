using Xunit;
using PSP;

namespace Validation
{
    public class EmailValidatorTests
    {
        [Fact]
        public void EmailValidatorContainsEtaTest()
        {
            // Arrange
            var validator = new EmailValidator();
            string email = "lalalalalgmail.com";

            // Act
            var result = validator.IsValidEmail(email);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EmailValidatorNoForbiddenSymbolsTest()
        {
            // Arrange
            var validator = new EmailValidator();
            string email = "12Av3!*^%&@gmail.com";

            // Act
            var result = validator.IsValidEmail(email);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EmailValidatorCorrectDomainAndTLD()
        {
            // Arrange
            var validator = new EmailValidator();
            string email = "12Av3!*^%&@gmail.woaoqowo";

            // Act
            var result = validator.IsValidEmail(email);

            // Assert
            Assert.False(result);
        }
    }
}
