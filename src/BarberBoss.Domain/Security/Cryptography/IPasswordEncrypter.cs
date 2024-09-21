namespace BarberBoss.Domain.Security.Cryptography;
public interface IPasswordEncrypter
{
    string Encrypt(string password);
}
