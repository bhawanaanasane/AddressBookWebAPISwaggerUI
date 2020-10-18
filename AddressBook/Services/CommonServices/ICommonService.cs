using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Services.CommonServices
{
  public interface ICommonService
    {
        /// <summary>
        /// Encrypt text
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Encrypted text</returns>
        string EncryptText(string plainText);

        /// <summary>
        /// Decrypt text
        /// </summary>
        /// <param name="cipherText">Text to decrypt</param>       
        /// <returns>Decrypted text</returns>
        string DecryptText(string cipherText);
    }
}
