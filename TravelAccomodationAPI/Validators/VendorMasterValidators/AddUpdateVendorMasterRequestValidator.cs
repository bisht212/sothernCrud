using FluentValidation;
using TravelAccomodationAPI.ModelClass.RequestModel.VendorMaster;

namespace TravelAccomodationAPI.Validators.VendorMasterValidators
{
    public class AddUpdateVendorMasterRequestValidator : AbstractValidator<AddUpdateVendorMasterRequest>
    {
        public AddUpdateVendorMasterRequestValidator()
        {
            RuleFor(x => x.TenantId)
                .GreaterThan(0).WithMessage("TenantId is required");

            RuleFor(x => x.Business_Name)
                .NotEmpty().WithMessage("Business Name is required");

            RuleFor(x => x.Legal_Name)
                       .NotEmpty().WithMessage("Legal Name is required.")
                       .MaximumLength(150);

            RuleFor(x => x.Services)
                .NotEmpty().WithMessage("Services are required.")
                .MaximumLength(250);

            RuleFor(x => x.Star_Rating)
                .InclusiveBetween((byte)1, (byte)5)
                .WithMessage("Star Rating must be between 1 and 5.");

            RuleFor(x => x.AddressLine1)
                .NotEmpty().WithMessage("Address Line 1 is required.")
                .MaximumLength(250);

            RuleFor(x => x.AddressLine2)
                .MaximumLength(250)
                .When(x => !string.IsNullOrWhiteSpace(x.AddressLine2));

            RuleFor(x => x.City)
                .GreaterThan(0).WithMessage("City is required.");

            RuleFor(x => x.State)
                .GreaterThan(0).WithMessage("State is required.");

            RuleFor(x => x.Country)
                .GreaterThan(0).WithMessage("Country is required.");

            RuleFor(x => x.Pin_Code)
                .NotEmpty().WithMessage("Pin Code is required.")
                .Matches(@"^\d{4,10}$")
                .WithMessage("Pin Code must be numeric and between 4 to 10 digits.");

            RuleFor(x => x.Business_Type)
                .GreaterThan(0).WithMessage("Business Type is required.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(100);

        }
    }
}
