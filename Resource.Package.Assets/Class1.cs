using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Resource.Package.Assets.Secure;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;

namespace Resource.Package.Assets
{
    public static class Class1
    {




        public static void Test()
        {
            // KeyGenerater.Generate(Environment.CurrentDirectory);






            var data = new Byte[100000];

            Random rnd = new Random();
            rnd.NextBytes(data);


            //var publicKey = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "assets.key"));
            //var privateKey = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "package.key"));

            //RSA rsa1 = RSA.Create();
            //rsa1.ImportRSAPrivateKey(privateKey, out _);

            var key = new byte[] { 40, 155 };


            var output = AES.Encrypt(data, key);


            var result = AES.Decrypt(output, key);








            Console.WriteLine(result);

        }



    }
}