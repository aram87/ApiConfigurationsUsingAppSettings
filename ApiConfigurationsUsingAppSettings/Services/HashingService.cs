using ApiConfigurationsUsingAppSettings.Interfaces;
using ApiConfigurationsUsingAppSettings.Models;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace ApiConfigurationsUsingAppSettings.Services
{
    public class HashingService : IHashingService
    {
        private readonly IOptions<ApiSettingsModel> options;

        public HashingService(IOptions<ApiSettingsModel> options)
        {
            this.options = options;
        }
        public string HashUsingPbkdf2(byte[] password)
        {
            string saltString = "ThisIsAStringToBeUsedAsSalt!@#$%";
            byte[] salt = System.Text.Encoding.ASCII.GetBytes(saltString);
            
            using var bytes = new Rfc2898DeriveBytes(password, salt, options.Value.PbKDF2IterationsCount, HashAlgorithmName.SHA256);
            var derivedRandomKey = bytes.GetBytes(32);
            var hash = Convert.ToBase64String(derivedRandomKey);

            return hash;
        }

        public string ComputeHmacUsingSha256(byte[] data)
        {
            string keyString = "ThisIsAnotherStringToBeUsedAsKey*&^%$#";
            byte[] key = System.Text.Encoding.ASCII.GetBytes(keyString);

            byte[] hmacBytes = HMACSHA256.HashData(key, data);

            return Convert.ToBase64String(hmacBytes);
            
        }
    }
}