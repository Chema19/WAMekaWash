using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WAMekaWash.Logics
{
    public class CipherLogic
    {
        private static string Salt => string.Join("", ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), true)[0]).Value.ToUpper().Split('-'));
        public static string Cipher(CipherAction action, CipherType type, string data)
        {
            try
            {
                byte[] iv;
                byte[] key;

                switch (type)
                {
                    case CipherType.UserPassword:
                        iv = Encoding.UTF8.GetBytes("B?E(H+MbQeThWmZq");
                        key = Encoding.UTF8.GetBytes("(H+MbQeThWmZq4t7w!z%C*F-J@NcRfUj");
                        break;
                    case CipherType.Token:
                        iv = Encoding.UTF8.GetBytes("aNdRgUkXp2s5v8y/");
                        key = Encoding.UTF8.GetBytes(")H@McQfTjWnZr4u7x!A%D*G-JaNdRgUk");
                        break;
                    default: return null;
                }

                switch (action)
                {
                    case CipherAction.Encrypt: return Encrypt(iv, key, data);
                    case CipherAction.Decrypt: return Decrypt(iv, key, data);
                    default: return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private static string Encrypt(byte[] iv, byte[] key, string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        private static string Decrypt(byte[] iv, byte[] key, string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }

    public enum CipherAction
    {
        Encrypt,
        Decrypt
    }

    public enum CipherType
    {
        UserPassword,
        Token
    }
}