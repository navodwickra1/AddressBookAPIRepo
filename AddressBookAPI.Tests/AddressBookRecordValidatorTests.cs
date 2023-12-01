using AddressBookAPI.Data.Models;
using AddressBookAPI.Validation;
using FluentValidation.TestHelper;


namespace AddressBookAPI.Tests
{

    public class AddressBookRecordValidatorTests
    {
        private readonly AddressBookRecordValidator _validator;

        public AddressBookRecordValidatorTests()
        {
            _validator = new AddressBookRecordValidator();
        }

        [Fact]
        public void Should_have_error_when_FirstName_is_null()
        {
            var model = new AddressBookRecord {FirstName = null, LastName = "Wickra", Email = "navodwickra@gmail.com", Phone = "07912040291"};
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(r => r.FirstName);
        }

        [Fact]
        public void Should_have_error_when_FirstName_is_empty()
        {
            var model = new AddressBookRecord { FirstName = string.Empty, LastName = "Wickra", Email = "navodwickra@gmail.com", Phone = "07912040291" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(r => r.FirstName);
        }

        [Fact]
        public void Should_have_error_when_LastName_is_null()
        {
            var model = new AddressBookRecord { FirstName = "Navod", LastName = null, Email = "navodwickra@gmail.com", Phone = "07912040291" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(r => r.LastName);
        }

        [Fact]
        public void Should_have_error_when_LastName_is_empty()
        {
            var model = new AddressBookRecord{FirstName = "Navod", LastName = string.Empty, Email = "navodwickra@gmail.com", Phone = "07912040291" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(r => r.LastName);
        }

        [Fact]
        public void Should_have_error_when_Email_is_not_in_correctformat()
        {
            var model = new AddressBookRecord { FirstName = "Navod", LastName = "Wickra", Email = "navodwickragmail.com", Phone = "07912040291" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(r => r.Email);
        }

        [Fact]
        public void Should_have_error_when_Phone_is_not_in_correctformat()
        {
            var model = new AddressBookRecord { FirstName = "Navod", LastName = "Wickra", Email = "navodwickra@gmail.com", Phone = "079120401" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(r => r.Phone);
        }

        [Fact]
        public void Should_have_no_errors_when_data_is_in_correctformat()
        {
            var model = new AddressBookRecord { FirstName = "Navod", LastName = "Wickra", Email = "navodwickra@gmail.com", Phone = "07912040290" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(r => r.FirstName);
            result.ShouldNotHaveValidationErrorFor(r => r.LastName);
            result.ShouldNotHaveValidationErrorFor(r => r.Email);
            result.ShouldNotHaveValidationErrorFor(r => r.Phone);
        }
    }
}
