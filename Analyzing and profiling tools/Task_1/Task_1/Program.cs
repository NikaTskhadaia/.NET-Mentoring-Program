using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

object obj = new();

GeneratePasswordUsingKeyDerivation("nikoloz", new byte[16]);

Stopwatch stopwatch = new();
stopwatch.Start();
var hashedKeyDerivation = GeneratePasswordUsingKeyDerivation("nikoloz", new byte[16]);
stopwatch.Stop();
Console.WriteLine($"- {stopwatch.ElapsedTicks} {hashedKeyDerivation} - using Microsoft.AspNetCore.Cryptography.KeyDerivation");
stopwatch.Reset();
stopwatch.Start();
var hashedPassword = GeneratePasswordHashUsingSalt("nikoloz", new byte[16]);
stopwatch.Stop();
Console.WriteLine($"- {stopwatch.ElapsedTicks} {hashedPassword} - using System.Security.Cryptography.Rfc2898DeriveBytes");

Console.ReadLine();

string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
{
    var iterate = 10000;
    var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
    byte[] hash = pbkdf2.GetBytes(20);
    byte[] hashBytes = new byte[36];
    Array.Copy(salt, 0, hashBytes, 0, 16);
    Array.Copy(hash, 0, hashBytes, 16, 20);

    var passwordHash = Convert.ToBase64String(hashBytes);

    return passwordHash;
}

string GeneratePasswordUsingKeyDerivation(string passwordText, byte[] salt)
{
    var hash = KeyDerivation.Pbkdf2(passwordText, salt, KeyDerivationPrf.HMACSHA1, 10000, 20);

    byte[] hashBytes = new byte[36];
    Array.Copy(salt, 0, hashBytes, 0, 16);
    Array.Copy(hash, 0, hashBytes, 16, 20);

    var passwordHash = Convert.ToBase64String(hashBytes);

    return passwordHash;
}