using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace kriptografo
{
    //# Gerar chave RSA de 2048 bits
    //    openssl genrsa -out rsa-private-key.pem 2048

    public class Cryptography(IConfiguration configuration)
    {
        private readonly RSA _rsa = RSA.Create();
        private readonly string _privateKey = Encoding.UTF8.GetString(Convert.FromBase64String(configuration["rsa-private-key"]));


        public string Encrypt(string text)
        {
            _rsa.ImportFromPem(_privateKey);
            var biteMsg = Encoding.UTF8.GetBytes(text);
            var encrypt = _rsa.Encrypt(biteMsg, RSAEncryptionPadding.Pkcs1);
            string encryptText = Convert.ToBase64String(encrypt);
            return encryptText;
        }

        public string Decrypt(string text)
        {

            _rsa.ImportFromPem(_privateKey);
            string decrypt = text;
            try
            {
                var textBite = Convert.FromBase64String(text);
                var decryptByte = _rsa.Decrypt(textBite, RSAEncryptionPadding.Pkcs1);
                decrypt = Encoding.UTF8.GetString(decryptByte);
                return decrypt;
            }
            catch
            {
                return decrypt;
            }
        }
    }
}
