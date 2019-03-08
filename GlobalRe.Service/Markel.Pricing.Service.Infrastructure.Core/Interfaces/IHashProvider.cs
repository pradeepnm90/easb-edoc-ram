using System;
using System.Linq;

namespace Markel.Pricing.Service.Infrastructure.Interfaces
{
    /// <summary>
    /// Provides basic hashing functions
    /// </summary>
    public interface IHashProvider
    {
        /// <summary>
        /// Creates the hash byte array.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        string CreateHash(byte[] data);

        /// <summary>
        /// Creates the hash from an object.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        string CreateHash(object data);

        /// <summary>
        /// Returns a hashcode representing the contents of a file.
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="hashName">SHA1 (default), MD5, SHA256, SHA384, SHA512</param>
        /// <returns></returns>
        string CreateFileHash(string path, string hashName = "SHA1");

        /// <summary>
        /// Compares the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        bool CompareHash(byte[] data, string hash);
    }
}