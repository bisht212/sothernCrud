using FluentValidation;
using TravelAccomodationAPI.ModelClass.RequestModel;

public class AddRoomFacilityRequestValidator
    : AbstractValidator<AddRoomFacilityRequest>
{
    public AddRoomFacilityRequestValidator()
    {
        RuleFor(x => x.RoomId)
            .GreaterThan(0).WithMessage("RoomId must be greater than 0");

        RuleFor(x => x.RoomFacilityId)
            .GreaterThan(0).WithMessage("RoomFacilityId must be greater than 0");
    }
}