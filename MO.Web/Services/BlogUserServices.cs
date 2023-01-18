using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MO.Web.Helpers;

namespace MO.Web.Services
{
	public class BlogUserServices : IBlogUserServices
	{
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        private IConfiguration Config { get; }

		public BlogUserServices(IConfiguration config)
		{
			Config = config;
		}


		public bool ValidateUser(string username, string password)
		{
			if ((username == Config[User.UserName]) && (VerifyPassword(password) == true)) {
				return true;
			}
			else
			{
				return false;
			}
        }


        bool VerifyPassword(string password)
		{
			var storedSalt = Convert.FromHexString(Config[User.Salt]);
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, storedSalt, iterations, hashAlgorithm, keySize);
            return hashToCompare.SequenceEqual(Convert.FromHexString(Config[User.HashKey]));
        }
    }
}
