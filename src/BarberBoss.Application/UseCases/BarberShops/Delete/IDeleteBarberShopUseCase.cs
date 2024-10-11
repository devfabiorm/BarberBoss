namespace BarberBoss.Application.UseCases.BarberShops.Delete;
public interface IDeleteBarberShopUseCase
{
    Task Execute(long id);
}
