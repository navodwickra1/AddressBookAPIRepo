using AddressBookAPI.Data.Models;
using FluentValidation;

namespace AddressBookAPI.Validation
{
    public class AddressBookRecordValidator : AbstractValidator<AddressBookRecord>
    {
        public AddressBookRecordValidator()
        {
            RuleFor(r => r.FirstName).NotNull().NotEmpty().WithMessage("First name can not be empty");
            RuleFor(r => r.LastName).NotNull().NotEmpty().WithMessage("Last name can not be empty");
            RuleFor(r => r.Email).NotNull().NotEmpty().
                Matches(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$").
                WithMessage("Invalid Email address format");
            RuleFor(r => r.Phone).NotNull().NotEmpty().
                Matches(@"^(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3}\s?\d{3})|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4})|((\+44\s?\d{2}|\(?0\d{2}\)?)\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$").
                WithMessage("Invalid phone number format");
        }
    }
}
