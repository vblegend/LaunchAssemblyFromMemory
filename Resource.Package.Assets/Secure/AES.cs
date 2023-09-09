using System.Security.Cryptography;
using System.Text;


namespace Resource.Package.Assets.Secure
{
    internal static class AES
    {

        private static Byte[] defaultKey = Encoding.UTF8.GetBytes("0123456789!@#$%^&*();:'\",<.>\\/ab");

        public static Byte[] Encrypt(Byte[] entData, Byte[] inputKey)
        {
            var key = inputKey.Concat(defaultKey).Take(32).ToArray();
            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        msEncrypt.Write(aesAlg.IV);
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(entData);
                        }
                        return msEncrypt.ToArray();
                    }
                }
            }
        }


        public static Byte[] Decrypt(Byte[] fullCipher, Byte[] inputKey)
        {
            var key = inputKey.Concat(defaultKey).Take(32).ToArray();
            var worldSpan = fullCipher.AsSpan();
            var iv = worldSpan.Slice(start: 0, length: 16);
            var cipher = worldSpan.Slice(start: 16);
            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv.ToArray()))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        using (var aesStream = new CryptoStream(resultStream, decryptor, CryptoStreamMode.Write))
                        {
                            aesStream.Write(cipher);
                        }
                        return resultStream.ToArray();
                    }
                }
            }
        }

    }
}
