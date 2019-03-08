using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    public static class CipherExtension
    {
        private const string DEFAULT_SALT = "Markel Enterprise Pricing - Copyright © 2018";

        public static string EncryptJson<ALGORITHM>(this object data, string password, string salt = DEFAULT_SALT) where ALGORITHM : SymmetricAlgorithm, new()
        {
            string jsonString = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });
            if (string.IsNullOrEmpty(jsonString)) return jsonString;

            return Encrypt<ALGORITHM>(jsonString, password, salt);
        }

        public static T DecryptJson<ALGORITHM, T>(this string value, string password, string salt = DEFAULT_SALT) where ALGORITHM : SymmetricAlgorithm, new()
        {
            string decryptedJson = Decrypt<ALGORITHM>(value, password, salt);
            if (string.IsNullOrEmpty(decryptedJson)) return default(T);

            object decryptedObject = JsonConvert.DeserializeObject(decryptedJson, typeof(T), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple
            });

            return (T)decryptedObject;
        }

        public static string Encrypt<ALGORITHM>(this string value, string password, string salt = DEFAULT_SALT) where ALGORITHM : SymmetricAlgorithm, new()
        {
            DeriveBytes rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

            SymmetricAlgorithm algorithm = new ALGORITHM();

            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            ICryptoTransform transform = algorithm.CreateEncryptor(rgbKey, rgbIV);

            using (MemoryStream buffer = new MemoryStream())
            {
                using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(value);
                    }
                }

                return Convert.ToBase64String(buffer.ToArray());
            }
        }

        public static string Decrypt<ALGORITHM>(this string text, string password, string salt = DEFAULT_SALT) where ALGORITHM : SymmetricAlgorithm, new()
        {
            if (string.IsNullOrEmpty(salt) || salt.Length < 8) return string.Empty;

            DeriveBytes rgb = new Rfc2898DeriveBytes(password, Encoding.Unicode.GetBytes(salt));

            SymmetricAlgorithm algorithm = new ALGORITHM();

            byte[] rgbKey = rgb.GetBytes(algorithm.KeySize >> 3);
            byte[] rgbIV = rgb.GetBytes(algorithm.BlockSize >> 3);

            ICryptoTransform transform = algorithm.CreateDecryptor(rgbKey, rgbIV);

            try
            {
                using (MemoryStream buffer = new MemoryStream(Convert.FromBase64String(text)))
                {
                    using (CryptoStream stream = new CryptoStream(buffer, transform, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (CryptographicException)
            {
                return string.Empty;
            }
        }
    }
}
