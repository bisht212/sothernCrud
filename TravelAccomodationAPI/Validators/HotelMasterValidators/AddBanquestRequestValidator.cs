using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Linq;
using TravelAccomodationAPI.ModelClass.RequestModel;

public class AddBanquestRequestValidator : AbstractValidator<AddBanquestRequest>
{
    public AddBanquestRequestValidator()
    {
        RuleFor(x => x.Hotel_Id)
            .GreaterThan(0)
            .WithMessage("Hotel_Id must be a valid value.");

        RuleFor(x => x.Banquet_Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Banquet name is required and must not exceed 100 characters.");

        RuleFor(x => x.No_of_covers)
            .GreaterThan(0)
            .WithMessage("No_of_covers must be greater than zero.");

        RuleFor(x => x.Per_plate_cost_veg)
            .GreaterThan(0)
            .WithMessage("Per plate veg cost must be greater than zero.");

        RuleFor(x => x.Per_plate_cost_non_veg)
            .GreaterThan(0)
            .WithMessage("Per plate non‑veg cost must be greater than zero.");

        RuleFor(x => x.Sq_feet_area)
            .GreaterThan(0)
            .WithMessage("Square feet area must be greater than zero.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.BanquetImages)
            .NotNull()
            .Must(x => x.Any())
            .WithMessage("At least one banquet image is required.");

        RuleForEach(x => x.BanquetImages)
            .Must(BeValidImage)
            .WithMessage("Only image files (jpg, jpeg, png) up to 5MB are allowed.");
    }

    private bool BeValidImage(IFormFile file)
    {
        if (file == null) return false;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = System.IO.Path.GetExtension(file.FileName).ToLower();

        return allowedExtensions.Contains(extension)
               && file.Length > 0
               && file.Length <= 5 * 1024 * 1024; // 5 MB
    }
}
