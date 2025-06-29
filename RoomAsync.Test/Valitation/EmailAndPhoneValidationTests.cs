using Domain.ContactWay;

namespace RoomAsync.Test.Valitation
{
    public class EmailAndPhoneValidationTests
    {
        [Theory]
        [InlineData("user@example.com")]
        [InlineData("user.name+tag@sub.domain.com")]
        [InlineData("user_name@domain.co.uk")]
        [InlineData("\"quoted@local\"@domain.com")]
        public void Email_IsValid_ValidEmails_ReturnsTrue(string email)
        {
            Assert.True(Email.IsValid(email));
            var e = new Email(email);
            Assert.Equal(email, e.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("plainaddress")]
        [InlineData("user@.com")]
        [InlineData("user@domain")]
        [InlineData("user@domain..com")]
        [InlineData(null)]
        public void Email_IsValid_InvalidEmails_ReturnsFalse(string? email)
        {
            Assert.False(Email.IsValid(email));
            Assert.Throws<ArgumentException>(() => new Email(email));
        }

        [Theory]
        [InlineData(CountryCode.US, "1234567890")]
        [InlineData(CountryCode.GB, "2071234567")]
        [InlineData(CountryCode.IND, "9876543210")]
        public void Phone_ValidInputs_CreatesPhone(CountryCode countryCode, string number)
        {
            var phone = new Phone(countryCode, number);
            Assert.Equal(countryCode, phone.CountryCode);
            Assert.Equal(number, phone.Number);
        }

        [Theory]
        [InlineData(CountryCode.None, "1234567890")]
        [InlineData(null, "1234567890")]
        [InlineData(CountryCode.US, "")]
        [InlineData(CountryCode.US, null)]
        public void Phone_InvalidInputs_ThrowsArgumentException(CountryCode? countryCode, string? number)
        {
            Assert.Throws<ArgumentException>(() => new Phone(countryCode ?? CountryCode.None, number));
        }
    }
}
