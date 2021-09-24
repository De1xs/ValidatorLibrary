namespace ValidatorLibraryTests.PhoneValidator
{
    using Xunit;
    using PSP;
    public class PhoneValidatorTests
    {
        private PhoneValidator phoneValidator;

        public PhoneValidatorTests()
        {
            phoneValidator = new PhoneValidator();
        }

        [Theory]
        [InlineData("864443333", "+37064443333")]
        [InlineData("+37064443333", "+37064443333")]
        [InlineData("+442222333333", "+442222333333")]
        public void PhoneValidator_Succeeds(string number, string expectedResult)
        {
            Assert.Equal(expectedResult, phoneValidator.ValidatePhoneNumber(number));
        }

        [Theory]
        [InlineData("864%$333A")]
        [InlineData("+3706!44333B")]
        [InlineData("+44222@33333C")]
        public void PhoneValidator_ReturnsZero_WhenExtraSymbols(string number)
        {
            Assert.Equal("0", phoneValidator.ValidatePhoneNumber(number));
        }


        [Theory]
        [InlineData("8644433339")]
        [InlineData("+3706444333399")]
        [InlineData("+442222333333999")]
        public void PhoneValidator_ReturnsZero_WhenExtraNumbers(string number)
        {
            Assert.Equal("0", phoneValidator.ValidatePhoneNumber(number));
        }

        [Theory]
        [InlineData("86444333")]
        [InlineData("+3706444333")]
        [InlineData("+44222233333")]
        public void PhoneValidator_ReturnsZero_WhenMissingNumbers(string number)
        {
            Assert.Equal("0", phoneValidator.ValidatePhoneNumber(number));
        }

        [Theory]
        [InlineData("!7777777777")]
        [InlineData("-0000000000")]
        [InlineData("66666666666")]
        public void PhoneValidator_ReturnsZero_WhenUnrecognizedPrefix(string number)
        {
            Assert.Equal("0", phoneValidator.ValidatePhoneNumber(number));
        }

        [Fact]
        public void PhoneValidator_ReturnsZero_WhenEmpty()
        {
            Assert.Equal("0", phoneValidator.ValidatePhoneNumber(""));
        }

    }
}
