using SHA3.Net;
using System;
using System.Text;
using Watchster.Application.Interfaces;

namespace Watchster.Application.Utils.Cryptography
{
    public class CryptographyService : ICryptographyService
    {
        public string GetPasswordSHA3Hash(string password)
        {
            using (var shaAlg = Sha3.Sha3256())
            {
                var hasedPasswordBytes = shaAlg.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hasedPassword = Convert.ToBase64String(hasedPasswordBytes);
                return hasedPassword;
            }
        }
    }
}
