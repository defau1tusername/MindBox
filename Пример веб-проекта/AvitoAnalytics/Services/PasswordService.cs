
using System.Text;
using System;
using System.Security.Cryptography;

public class PasswordService
{
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hash = BitConverter.ToString(hashedBytes);

            return hash;
        }
    }
}

