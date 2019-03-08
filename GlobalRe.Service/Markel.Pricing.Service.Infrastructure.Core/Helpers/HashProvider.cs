using Markel.Pricing.Service.Infrastructure.Interfaces;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Markel.Pricing.Service.Infrastructure.Helpers
{
    /// <summary>
    /// Provides standard hashing functions using byte to byte serialization
    /// </summary>
    public class HashProvider : IHashProvider
    {
        #region IHashProvider

        /// <summary>
        /// Creates the hash byte array.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public string CreateHash(byte[] data)
        {
            MD5 md5Hash = MD5.Create();
            byte[] hashBytes = md5Hash.ComputeHash(data);

            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sBuilder.Append(hashBytes[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// Creates the hash from an object.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public string CreateHash(object data)
        {
            if (data == null)
            {
                return null;
            }

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, data);

                MD5 md5Hash = MD5.Create();
                md5Hash.ComputeHash(memoryStream.ToArray());
                return Convert.ToBase64String(md5Hash.Hash);
            }
        }

        public string CreateFileHash(string path, string hashName = "SHA1")
        {
            HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashName);
            FileStream fileStream = null;
            try
            {
                fileStream = File.OpenRead(path);
                var computedHash = hashAlgorithm.ComputeHash(fileStream);
                return BitConverter.ToString(computedHash);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                fileStream.Close();
            }
        }

        /// <summary>
        /// Compares the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public bool CompareHash(byte[] data, string hash)
        {
            // Hash the input.
            string hashOfInput = CreateHash(data);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
