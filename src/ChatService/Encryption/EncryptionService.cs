using System.Security.Cryptography;
using System.Text;

namespace ChatService.Encryption;

public static class EncryptionService
{
    private static readonly byte[] initializationVector = new byte[16];
    
    public static string Encrypt(string plainText, byte[] key)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = initializationVector;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();
                }
                encryptedBytes = memoryStream.ToArray();
            }

            return Convert.ToBase64String(encryptedBytes);
        }
    }
    
    public static string Decrypt(string encryptedText, byte[] key)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = initializationVector;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] decryptedBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    cryptoStream.FlushFinalBlock();
                }
                decryptedBytes = memoryStream.ToArray();
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}