using FluentValidation;
using Microsoft.AspNetCore.Http;
using TravelAccomodationAPI.ModelClass.RequestModel;

public class AddHotelRoomMediaRequestValidator
    : AbstractValidator<AddHotelRoomMediaRequest>
{
    public AddHotelRoomMediaRequestValidator()
    {
        RuleFor(x => x.RoomId)
            .GreaterThan(0).WithMessage("RoomId must be greater than 0");

        RuleFor(x => x.ImageName)
            .NotEmpty().WithMessage("Image name is required")
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .MaximumLength(300);

        RuleFor(x => x.ImagePath)
            .NotNull().WithMessage("Room image is required")
            .Must(BeValidImage).WithMessage("Only JPG, JPEG, PNG images are allowed")
            .Must(BeValidSize).WithMessage("Image size must not exceed 5 MB");
    }

    private bool BeValidImage(IFormFile file)
    {
        if (file == null) return false;

        var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
        return allowedTypes.Contains(file.ContentType);
    }

    private bool BeValidSize(IFormFile file)
    {
        if (file == null) return false;

        return file.Length <= 10 * 1024 * 1024; // 10 MB
    }
}