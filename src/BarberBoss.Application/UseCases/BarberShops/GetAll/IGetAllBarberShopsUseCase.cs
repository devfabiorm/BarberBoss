using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.UseCases.BarberShops.GetAll;
public interface IGetAllBarberShopsUseCase
{
    Task<ResponseBarberShopsJson> Execute();
}
