namespace BarberBoss.Domain.Security.Cryptography;
public interface IPasswordEncrypter
{
    string Encrypt(string password);
    bool Verify(string hashedPassword, string rawTextPassword);
}
