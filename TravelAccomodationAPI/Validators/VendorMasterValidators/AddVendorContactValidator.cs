using FluentValidation;
using TravelAccomodationAPI.ModelClass.RequestModel.VendorMaster;

public class AddVendorContactValidator : AbstractValidator<AddVendorContact>
{
    public AddVendorContactValidator()
    {
        RuleFor(x => x.TenantId)
            .GreaterThan(0)
            .WithMessage("TenantId must be greater than 0");

        RuleFor(x => x.VendorId)
            .GreaterThan(0)
            .WithMessage("VendorId must be greater than 0");

        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required")
            .MaximumLength(100)
            .WithMessage("Full name cannot exceed 100 characters");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .Matches(@"^[0-9]{10}$")
            .WithMessage("Phone number must be 10 digits");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email format")
            .MaximumLength(150);

        RuleFor(x => x.Department)
            .GreaterThan(0)
            .WithMessage("Department is required");

        RuleFor(x => x.Designation)
            .GreaterThan(0)
            .WithMessage("Designation is required");
    }
}