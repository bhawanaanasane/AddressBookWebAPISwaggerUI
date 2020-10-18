using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Services.CommonServices
{
    public class CommonService :ICommonService
    {
        #region Utilities

        private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    var toEncrypt = Encoding.Unicode.GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(cs, Encoding.Unicode))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        #endregion
        /// <summary>
        /// Encrypt text
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>        
        /// <returns>Encrypted text</returns>
        public string EncryptText(string plainText) {
            if (string.IsNullOrEmpty(plainText))
                return plainText;
            
              string encryptionPrivateKey = "273ece6f97dd844d";

            using (var provider = new TripleDESCryptoServiceProvider())
            {
                provider.Key = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0, 16));
                provider.IV = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(8, 8));

                var encryptedBinary = EncryptTextToMemory(plainText, provider.Key, provider.IV);
                return Convert.ToBase64String(encryptedBinary);
            }
        }
        /// <summary>
        /// Decrypt text
        /// </summary>
        /// <param name="cipherText">Text to decrypt</param>
        
        /// <returns>Decrypted text</returns>
        public virtual string DecryptText(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

           
           string   encryptionPrivateKey = "273ece6f97dd844d";

            using (var provider = new TripleDESCryptoServiceProvider())
            {
                provider.Key = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0, 16));
                provider.IV = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(8, 8));

                var buffer = Convert.FromBase64String(cipherText);
                return DecryptTextFromMemory(buffer, provider.Key, provider.IV);
            }
        }
    }
}
