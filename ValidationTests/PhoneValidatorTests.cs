using PSP;
using System;
using Xunit;

namespace Validation
{
    public class PhoneValidatorTests
    {
        [Fact]
        public void PhoneValidatorOnlyNumbersTest()
        {
            // Arrange
            var validator = new PhoneValidator();
            string number = "123abc12A3";

            // Act
            var result = validator.ValidatePhoneNumber(number);

            // Assert
            Assert.Equal("0", result);
        }

        [Fact]
        public void PhoneValidatorChangeFirstNumbersTest()
        {
            // Arrange
            var validator = new PhoneValidator();
            string number = "862515327";

            // Act
            var result = validator.ValidatePhoneNumber(number);

            // Assert
            Assert.Equal("+37062515327", result);
        }

        [Fact]
        public void PhoneValidatorAddValidationTest()
        {
            // Arrange
            var validator = new PhoneValidator();

            // Act
            var result = validator.AddCountryValidation();

            // Assert
            Assert.True(result);
        }
    }
}
