﻿using BarberBoss.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace BarberBoss.Infrastructure.Security.Cryptography;
internal class PasswordEncrypter : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        var hashedPassword = BC.HashPassword(password);

        return hashedPassword;
    }

    public bool Verify(string hashedPassword, string rawTextPassword)
    {
        var result = BC.Verify(rawTextPassword, hashedPassword);

        return result;
    }
}
