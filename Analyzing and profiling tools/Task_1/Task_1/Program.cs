using System.Diagnostics;
using System.Security.Cryptography;

Stopwatch stopwatch = new();
stopwatch.Start();
var improvedHash = GeneratePasswordMultipleGetBytes("nikoloz", new byte[16]);
stopwatch.Stop();
Console.WriteLine($"{stopwatch.ElapsedTicks} - {improvedHash} - improved");
stopwatch.Reset();
stopwatch.Start();
var hashedPassword = GeneratePasswordHashUsingSalt("nikoloz", new byte[16]);
stopwatch.Stop();
Console.WriteLine($"{stopwatch.ElapsedTicks} - {improvedHash}");

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

string GeneratePasswordMultipleGetBytes(string passwordText, byte[] salt)
{
    var iterate = 10000;
    var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
    List<byte> hash = new(20);
    for (int i = 0; i < 20; i++)
    {
        hash.Add(pbkdf2.GetBytes(1)[0]);
    }
    byte[] hashBytes = new byte[36];
    Array.Copy(salt, 0, hashBytes, 0, 16);
    Array.Copy(hash.ToArray(), 0, hashBytes, 16, 20);

    var passwordHash = Convert.ToBase64String(hashBytes);

    return passwordHash;
}