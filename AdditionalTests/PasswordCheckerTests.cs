namespace AdditionalTests
{
    using Xunit;
    using PSP;
    public class PasswordCheckerTests
    {
        private PasswordChecker passwordChecker;
        public PasswordCheckerTests()
        {
            passwordChecker = new PasswordChecker();
        }

        [Fact]
        public void PasswordChecker_ReturnsTrue()
        {
            var password = "dddddddddd@T";

            Assert.True(passwordChecker.IsValidPassword(password));
        }

        [Fact]
        public void PasswordChecker_ReturnsFalse_WhenEmpty()
        {
            Assert.False(passwordChecker.IsValidPassword(""));
        }
    }
}
