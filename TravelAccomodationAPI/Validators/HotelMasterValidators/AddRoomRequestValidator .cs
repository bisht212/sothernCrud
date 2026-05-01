using FluentValidation;
using TravelAccomodationAPI.ModelClass.RequestModel;

public class AddRoomRequestValidator : AbstractValidator<AddRoomRequest>
{
    public AddRoomRequestValidator()
    {
        RuleFor(x => x.HotelId)
            .GreaterThan(0).WithMessage("HotelId must be greater than 0");

        RuleFor(x => x.RoomCategoryName)
            .NotEmpty().WithMessage("Room category name is required")
            .MaximumLength(100);

        RuleFor(x => x.RoomDescription)
            .NotEmpty().WithMessage("Room description is required")
            .MaximumLength(500);

        RuleFor(x => x.NoOfBeds)
            .GreaterThan(0).WithMessage("Number of beds must be at least 1");

        RuleFor(x => x.NoOfRooms)
            .GreaterThan(0).WithMessage("Number of rooms must be at least 1");
    }
}