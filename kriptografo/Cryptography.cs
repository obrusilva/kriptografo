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
        private readonly string _privateKey = configuration["rsa-private-key.pem"];

        public string Encrypt(string text)
        {
            _rsa.ImportFromPem(_privateKey);
            var encrypt = _rsa.Encrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.Pkcs1);
            string encryptText =  BitConverter.ToString(encrypt);
            return encryptText;       
         }

        public string Decrypt(string text)
        {
            string decrypt = text;
            try
            {
                var decryptByte = _rsa.Decrypt(Encoding.UTF8.GetBytes(text), RSAEncryptionPadding.Pkcs1);
                decrypt =  Encoding.UTF8.GetString(decryptByte);
                return decrypt;
            }
            catch
            {
                return decrypt;
            }
        }
    }
}
