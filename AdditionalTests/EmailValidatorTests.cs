namespace AdditionalTests
{
    using PSP;
    using Xunit;
    public class EmailValidatorTests
    {
        private EmailValidator validator;
        public EmailValidatorTests()
        {
            validator = new EmailValidator();
        }

        [Theory]
        [InlineData("testEmail@gmail.com")]
        [InlineData("testEmail@yahoo.com")]
        [InlineData("te3st2Email@mail.ru")]
        public void EmailValidator_Success(string email)
        {
            Assert.True(validator.IsValidEmail(email));
        }

        [Theory]
        [InlineData("testEmail@notgood.com")]
        [InlineData("testEmail@illegal.org")]
        public void EmailValidator_ReturnsFalse_WhenDomainIsNotValid(string email)
        {
            Assert.False(validator.IsValidEmail(email));
        }

        [Fact]
        public void EmailValidator_ReturnsFalse_WhenEmpty()
        {
            Assert.False(validator.IsValidEmail(""));
        }
    }
}
