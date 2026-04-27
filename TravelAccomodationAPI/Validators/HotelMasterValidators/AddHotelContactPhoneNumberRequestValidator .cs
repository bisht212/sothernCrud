using FluentValidation;
using TravelAccomodationAPI.ModelClass.RequestModel;

namespace TravelAccomodationAPI.Validators.HotelMasterValidators
{
    public class AddHotelContactPhoneNumberRequestValidator : AbstractValidator<AddHotelContactPhoneNumberRequest>
    {

        public AddHotelContactPhoneNumberRequestValidator()
        {
            RuleFor(x => x.ContactId)
                .GreaterThan(0)
                .WithMessage("ContactId must be greater than zero.");

            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .WithMessage("Country code is required.")
                .MaximumLength(10)
                .WithMessage("Country code cannot exceed 10 characters.")
                .Matches(@"^\+?[0-9]+$")
                .WithMessage("Country code must contain only digits and optional '+'.");

            RuleFor(x => x.Phone_Number)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .MaximumLength(20)
                .WithMessage("Phone number cannot exceed 20 characters.")
                .Matches(@"^[0-9]+$")
                .WithMessage("Phone number must contain digits only.");

            RuleFor(x => x.PhoneType_Id)
                .GreaterThan(0)
                .WithMessage("Phone type is required.");
        }

    }
}
