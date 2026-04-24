using FluentValidation;
using TravelAccomodationAPI.ModelClass.RequestModel;

namespace TravelAccomodationAPI.Validators.HotelMasterValidators
{
    public class AddRestaurantsOnPropertyValidator
      : AbstractValidator<AddRestaurantsOnPropertyRequest>
    {
        public AddRestaurantsOnPropertyValidator()
        {
            RuleFor(x => x.Hotel_Id)
                .GreaterThan(0)
                .WithMessage("Hotel Id is required");

            RuleFor(x => x.Resta_Name)
                .NotEmpty()
                .WithMessage("Restaurant name is required")
                .MaximumLength(100);

            RuleFor(x => x.Veg_Id)
                .GreaterThan(0)
                .WithMessage("Veg Id is required");

            RuleFor(x => x.Cuisine_Id)
                .GreaterThan(0)
                .WithMessage("Cuisine Id is required");

            RuleFor(x => x.No_of_covers)
                .GreaterThan(0)
                .WithMessage("No of covers must be greater than 0");

            //  FILE VALIDATION
            RuleFor(x => x.ResturantImage)
                .NotNull()
                .WithMessage("At least one file is required");

            RuleForEach(x => x.ResturantImage)
                .Must(file => file.Length > 0)
                .WithMessage("File cannot be empty");

            RuleForEach(x => x.ResturantImage)
                .Must(file =>
                    new[] { ".jpg", ".png", ".pdf" }
                    .Contains(Path.GetExtension(file.FileName).ToLower()))
                .WithMessage("Only jpg, png, pdf allowed");

            RuleForEach(x => x.ResturantImage)
                .Must(file => file.Length <= 5 * 1024 * 1024)
                .WithMessage("File size must be less than 5MB");
        }
    }
}