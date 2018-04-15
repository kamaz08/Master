using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace L3.Model
{
    public interface IKrypto
    {
        string Decrypt(string ciphertext, KeyIvModel keyiv);
        string Encrypt(string plaintext, KeyIvModel keyiv);
    }

    public interface IKryptoFiles
    {
        void Decrypt(string filename, string outfile, KeyIvModel keyiv);
        void Encrypt(string filename, string outfile, KeyIvModel keyiv);
    }

    public class AesKrypto : IKrypto, IKryptoFiles
    {
        private CipherMode _cipheMode;
        private PaddingMode _paddingMode;
        private int _keySize;
        public AesKrypto() : this(CipherMode.CBC, PaddingMode.PKCS7, 256) { }

        public AesKrypto(CipherMode cipherMode) : this(CipherMode.CBC, PaddingMode.PKCS7, 256) { }

        public AesKrypto(CipherMode cipherMode, PaddingMode paddingMode, int keySize)
        {
            _cipheMode = cipherMode;
            _keySize = keySize;
            _paddingMode = paddingMode;
        }

        private ICryptoTransform GetEncryptorAes(KeyIvModel keyiv)
        {
            ICryptoTransform result;
            using (Aes myAes = Aes.Create())
            {
                myAes.Mode = _cipheMode;
                myAes.KeySize = _keySize;
                myAes.Padding = _paddingMode;
                result = myAes.CreateEncryptor(keyiv.Key, keyiv.IV);
            }
            return result;
        }

        private ICryptoTransform GetDecryptorAes(KeyIvModel keyiv)
        {
            ICryptoTransform result;
            using (Aes myAes = Aes.Create())
            {
                myAes.Mode = _cipheMode;
                myAes.KeySize = _keySize;
                myAes.Padding = _paddingMode;
                result = myAes.CreateDecryptor(keyiv.Key, keyiv.IV);
            }
            return result;
        }

        public string Decrypt(string ciphertext, KeyIvModel keyiv)
        {
            var cipherbyte = Convert.FromBase64String(ciphertext);
            var decryptor = GetDecryptorAes(keyiv);
            string plaintext;
            using (MemoryStream msDecrypt = new MemoryStream(cipherbyte))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }

        public string Encrypt(string plaintext, KeyIvModel keyiv)
        {
            var encryptor = GetEncryptorAes(keyiv);
            byte[] cipherText;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plaintext);
                    }
                    cipherText = msEncrypt.ToArray();
                }
            }
            var result = Convert.ToBase64String(cipherText);
            return result;
        }

        public byte[] DecryptFromByte(byte[] ciphertext, KeyIvModel keyiv)
        {
            var decryptor = GetDecryptorAes(keyiv);
            byte[] plain;
            using (MemoryStream msDecrypt = new MemoryStream(ciphertext))
            {
                using (CryptoStream cryptoStream = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader stream = new StreamReader(cryptoStream))
                    {
                        string sf = stream.ReadToEnd();
                        plain = System.Text.Encoding.Default.GetBytes(sf);
                    }
                }
            }
            return plain;
        }

        public byte[] EncryptToByte(byte[] plaintext, KeyIvModel keyiv)
        {
            var encryptor = GetEncryptorAes(keyiv);
            byte[] cipherText;
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(plaintext, 0, plaintext.Length);
                }
                cipherText = msEncrypt.ToArray();
            }
            return cipherText;
        }

        public void Decrypt(string filename, string outfile, KeyIvModel keyiv)
        {
            var decryptor = GetDecryptorAes(keyiv);

            using (FileStream fsCrypt = new FileStream(filename, FileMode.Open))
            {
                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (FileStream fsOut = new FileStream(outfile, FileMode.Create))
                    {
                        int data;
                        while ((data = cs.ReadByte()) != -1)
                            fsOut.WriteByte((byte)data);
                    }
                }
            }
        }



        public void Encrypt(string filename, string outfile, KeyIvModel keyiv)
        {
            using (FileStream fsCrypt = new FileStream(outfile, FileMode.Create))
            {
                using (CryptoStream cs = new CryptoStream(fsCrypt, GetEncryptorAes(keyiv), CryptoStreamMode.Write))
                {
                    using (FileStream fsIn = new FileStream(filename, FileMode.Open))
                    {
                        byte[] buffer = new byte[1048576];
                        int read;
                        while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            cs.Write(buffer, 0, read);
                        }
                    }
                }
            }
        }

    }


}
