using BarberBoss.Communication.Requests;
using BarberBoss.Exception.Messages;
using FluentValidation;

namespace BarberBoss.Application.UseCases.BarberShops.Register;
public class RegisterBaberShopValidator : AbstractValidator<RequestRegisterBarberShopJson>
{
    public RegisterBaberShopValidator()
    {
        RuleFor(barber => barber.Name).NotEmpty().WithMessage(ResourceErrorMessages.INVALID_SHOP_NAME);
        RuleFor(barber => barber.Address).NotEmpty().WithMessage(ResourceErrorMessages.INVALID_SHOP_ADDRESS);
    }
}
