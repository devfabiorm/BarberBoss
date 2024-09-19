using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.BarberShops.Register;
public interface IRegisterBarberShopUseCase
{
    Task<ResponseBarberShopJson> Execute(RequestRegisterBarberShopJson request);
}
