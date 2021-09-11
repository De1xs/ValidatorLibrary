using Xunit;

namespace ValidatorLibraryTests.PasswordChecker
{
    public class PasswordChecker
    {
        private ValidatorLibrary.PasswordChecker.PasswordChecker _passwordChecker;

        public PasswordChecker()
        {
            _passwordChecker = new ValidatorLibrary.PasswordChecker.PasswordChecker(8);
        }

        [Fact]
        public void PasswordChecker_ReturnsTrue()
        {
            var password = AppendPassword("@T", 2);

            Assert.True(_passwordChecker.Validate(password));
        }

        [Fact]
        public void PasswordChecker_ReturnsFalse_WhenTooShort()
        {
            var password = AppendPassword("@T",3);

            Assert.True(_passwordChecker.Validate(password));
        }

        [Fact]
        public void PasswordChecker_ReturnsFalse_WhenMissingSpecialSymbol()
        {
            var password = AppendPassword("TT", 2);

            Assert.True(_passwordChecker.Validate(password));
        }

        [Fact]
        public void PasswordChecker_ReturnsFalse_WhenMissingUppercase()
        {
            var password = AppendPassword("@@", 2);

            Assert.True(_passwordChecker.Validate(password));
        }

        [Fact]
        public void PasswordChecker_ReturnsFalse_WhenEmpty()
        {
            Assert.True(_passwordChecker.Validate(""));
        }

        private string AppendPassword(string password, int lengthToSubtract)
        {
            return password + new string('s', (_passwordChecker._passwordLength - lengthToSubtract));
        }
    }
}
