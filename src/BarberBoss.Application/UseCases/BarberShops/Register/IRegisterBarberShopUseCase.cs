using BarberBoss.Communication.Requests;

namespace BarberBoss.Application.UseCases.BarberShops.Register;
public interface IRegisterBarberShopUseCase
{
    Task Execute(RequestRegisterBarberShopJson request);
}
