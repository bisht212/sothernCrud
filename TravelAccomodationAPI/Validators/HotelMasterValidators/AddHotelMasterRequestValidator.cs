using FluentValidation;
using TravelAccomodationAPI.ModelClass.RequestModel;

namespace TravelAccomodationAPI.Validators.HotelMasterValidators
{
    public class AddHotelMasterRequestValidator : AbstractValidator<AddHotelMasterRequest>
    {
        public AddHotelMasterRequestValidator()
        {
            RuleFor(x => x.TeanatId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Teanat Id is required");

            RuleFor(x => x.Hotel_Name)
                .NotEmpty()
                .WithMessage("Hotel Name is required");

            RuleFor(x => x.Property_Type_Id)
               .NotEmpty()
               .GreaterThan(0)
               .WithMessage("Property Type Id is required");

            RuleFor(x => x.Star_Rating_Id)
               .NotEmpty()
               .GreaterThan(0)
               .WithMessage("Star Rating Type Id is required");

            RuleFor(x => x.Owner_Name)
               .NotEmpty()
               .WithMessage("Owner Name is required");

            RuleFor(x => x.Owner_Phone)
             .NotEmpty()
             .WithMessage("Owner Phone number is required");

            RuleFor(x => x.ChainStandId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Chain Stand Id is required");

            RuleFor(x => x.AddressLine1)
                .NotEmpty()
                .WithMessage("AddressLine1 is required");

            RuleFor(x => x.City)
              .NotEmpty()
              .GreaterThan(0)
              .WithMessage("City is required");

            RuleFor(x => x.State)
             .NotEmpty()
             .GreaterThan(0)
             .WithMessage("State is required");

            RuleFor(x => x.Country)
              .NotEmpty()
              .GreaterThan(0)
              .WithMessage("Country is required");

            RuleFor(x => x.PinCode)
              .NotEmpty()
              .WithMessage("PinCode is required");


            RuleFor(x => x.Year_of_construction)
             .NotEmpty()
             .GreaterThan(0)
             .WithMessage("Year of Construction is required");

            RuleFor(x => x.Check_In_Time)
               .Must(t => t != TimeSpan.Zero)
               .WithMessage("Check-In Time is required");

            RuleFor(x => x.Check_Out_Time)
                .Must(t => t != TimeSpan.Zero)
                .WithMessage("Check-Out Time is required");

            RuleFor(x => x.IsPublish)
             .NotEmpty()
             .WithMessage("IsPublish is required");

            RuleFor(x => x.IsDraft)
                .NotEmpty()
                .WithMessage("IsDraft is required");

            RuleFor(x => x.IsDeleted)
             .NotEmpty()
             .WithMessage("IsDeleted is required");
        }
    }
}
