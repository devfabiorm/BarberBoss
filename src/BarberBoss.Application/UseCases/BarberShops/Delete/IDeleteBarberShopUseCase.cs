namespace BarberBoss.Application.UseCases.BarberShops.Delete;
public interface IDeleteBarberShopUseCase
{
    Task Delete(long id);
}
