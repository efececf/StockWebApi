using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using StockWebApi.Interfaces;

namespace StockWebApi.Models
{
    public class PasswordHasher: IPasswordHasher
    {
        private const int SaltSize=128/8;
        private const int KeySize=256/8;
        private const int Iterations=10000;
        private static readonly HashAlgorithmName _hashAlgoName=HashAlgorithmName.SHA256;
        private const char Delimeter=';';
        
        public string Hash(string password){
            var salt=RandomNumberGenerator.GetBytes(SaltSize);
            var hash=Rfc289DeriveBytes.Pbkdf2(password, salt, Iterations,_hashAlgoName,KeySize);
            return string.Join(Delimeter,Convert.ToBase64String(salt),Convert.ToBase64String(hash));
        }
        public bool VerifyPassword(string passwordHash,string inputPassword){
            var elements=passwordHash.Split(Delimeter);
            var salt=Convert.FromBase64String(elements[0]);
            var hash=Convert.FromBase64String(elements[1]);
            var inputHash=Rfc289DeriveBytes.Pbkdf2(inputPassword, salt, Iterations,_hashAlgoName,KeySize);
            return CryptographicOperations.FixedTimeEquals(hash, inputHash);

        }
    }
}