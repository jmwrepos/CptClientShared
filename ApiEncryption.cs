using System;
using System.IO;
using System.Security.Cryptography;

namespace CptClientShared
{
    public static class ApiEncryption
    {
        public static byte[] NewKey()
        {
            Aes aes = Aes.Create();
            aes.GenerateKey();
            return aes.Key;
        }
        public static byte[] NewIV()
        {
            Aes aes = Aes.Create();
            aes.GenerateIV();
            return aes.IV;
        }
        public static byte[] Encrypt(byte[] key, byte[] iv, string plainText)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0 || plainText == String.Empty)
                throw new Exception("plainText");
            if (key == null || key.Length <= 0)
                throw new Exception("Key");
            if (iv == null || iv.Length <= 0)
                throw new Exception("IV");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                // Create the streams used for encryption.
                #pragma warning disable IDE0063 // Use simple 'using' statement
                using (MemoryStream msEncrypt = new())
                #pragma warning restore IDE0063 // Use simple 'using' statement
                {
                    #pragma warning disable IDE0063 // Use simple 'using' statement
                    using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                    #pragma warning restore IDE0063 // Use simple 'using' statement
                    {
                        using (StreamWriter swEncrypt = new(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }
        public static string Decrypt(byte[] key, byte[] iv, byte[] cipherText)
        {
            // Declare the string used to hold
            // the decrypted text.
            string plaintext = string.Empty;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes rijAlg = Aes.Create())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;
                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                // Create the streams used for decryption.
                #pragma warning disable IDE0063 // Use simple 'using' statement
                using (MemoryStream msDecrypt = new(cipherText))
                #pragma warning restore IDE0063 // Use simple 'using' statement
                {
                    #pragma warning disable IDE0063 // Use simple 'using' statement
                    using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read))
                    #pragma warning restore IDE0063 // Use simple 'using' statement
                    {
                        #pragma warning disable IDE0063 // Use simple 'using' statement
                        using (StreamReader srDecrypt = new(csDecrypt))
                        #pragma warning restore IDE0063 // Use simple 'using' statement
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext += srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext; 
        }
    }
}
