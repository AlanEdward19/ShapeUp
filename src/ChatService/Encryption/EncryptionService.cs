using System.Security.Cryptography;
using System.Text;

namespace ChatService.Encryption;

/// <summary>
/// Serviço de criptografia para criptografar e descriptografar textos.
/// </summary>
public static class EncryptionService
{
    /// <summary>
    /// Vetor de inicialização para o algoritmo de criptografia.
    /// </summary>
    private static readonly byte[] InitializationVector = new byte[16];
    
    /// <summary>
    /// Método para criptografar um texto.
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string Encrypt(string plainText, byte[] key)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = InitializationVector;

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
    
    /// <summary>
    /// Método para descriptografar um texto.
    /// </summary>
    /// <param name="encryptedText"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string Decrypt(string encryptedText, byte[] key)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = InitializationVector;

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