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
        [InlineData("1", "1234567890")]
        [InlineData("44", "2071234567")]
        [InlineData("91", "9876543210")]
        public void Phone_ValidInputs_CreatesPhone(string countryCode, string number)
        {
            var phone = new Phone(countryCode, number);
            Assert.Equal(countryCode, phone.CountryCode);
            Assert.Equal(number, phone.Number);
        }

        [Theory]
        [InlineData("", "1234567890")]
        [InlineData(null, "1234567890")]
        [InlineData("1", "")]
        [InlineData("1", null)]
        public void Phone_InvalidInputs_ThrowsArgumentException(string? countryCode, string? number)
        {
            Assert.Throws<ArgumentException>(() => new Phone(countryCode, number));
        }
    }
}
