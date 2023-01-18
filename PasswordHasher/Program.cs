// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using System.Text;

const int keySize = 64;
const int iterations = 350000;
HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

//  C:...\michieloppenheimer_io\PasswordHasher\bin\Debug\net7.0
//  Make sure to clean directory after running and do not check in to 
// github!!
string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "passwordhash.txt");

var hash = HashPasword("some_password_here", out var salt, out var password);


//  We will use the Rfc2898DeriveBytes.Pbkdf2() static method. The method
//  takes several parameters including a salt. To generate a proper random
//  salt, let’s use the RandomNumberGenerator.GetBytes() static method:
string HashPasword(string _password, out byte[] salt, out string password)
{
    salt = RandomNumberGenerator.GetBytes(keySize);
    password = _password;

    var hash = Rfc2898DeriveBytes.Pbkdf2(
        Encoding.UTF8.GetBytes(_password),
        salt,
        iterations,
        hashAlgorithm,
        keySize);

    return Convert.ToHexString(hash);
}


void WritePassword()
{
    var fs = new System.IO.FileStream(path, System.IO.FileMode.Append, System.IO.FileAccess.Write);
    var sw = new System.IO.StreamWriter(fs);
    sw.WriteLine($"Password: {password}" + Environment.NewLine);
    sw.WriteLine($"Salt {Convert.ToHexString(salt)}" + Environment.NewLine);
    sw.WriteLine($"Hash: {hash}");
    sw.Flush();
    sw.Close();
    fs.Close();

}

WritePassword();
Console.WriteLine($"Password hash: {hash}");
Console.WriteLine($"Generated salt: {Convert.ToHexString(salt)}");

bool VerifyPassword(string password, string hash, byte[] salt)
{
    var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

    return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
}

Console.WriteLine("...Checking password...");
bool ispasswordVerified = VerifyPassword("some_password_here", hash, salt);
Console.WriteLine($"Password verified: {ispasswordVerified}");
Console.ReadKey();

