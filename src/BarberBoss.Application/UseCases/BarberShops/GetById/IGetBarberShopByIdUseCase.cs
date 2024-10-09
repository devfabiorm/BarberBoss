using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.BarberShops.GetById;
public interface IGetBarberShopByIdUseCase
{
    Task<ResponseBarberShopJson> Execute(long id);
}
