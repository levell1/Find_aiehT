using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class AESManager : MonoBehaviour
{
    public readonly string key = "01234567890123456789012345678901";
    public readonly string iv = "0123456789012345";

    //AES 암호화
    public string AESEncrypt(string input)
    {
        try
        {
            RijndaelManaged aes = new RijndaelManaged();
            //aes.KeySize = 256; //AES256으로 사용시 
            aes.KeySize = 128; //AES128로 사용시 
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);
            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] buf = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(input);
                    cs.Write(xXml, 0, xXml.Length);
                }
                buf = ms.ToArray();
            }
            string Output = Convert.ToBase64String(buf);
            return Output;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    //AES 복호화
    public string AESDecrypt(string input)
    {
        try
        {
            RijndaelManaged aes = new RijndaelManaged();
            //aes.KeySize = 256; //AES256으로 사용시 
            aes.KeySize = 128; //AES128로 사용시 
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);
            var decrypt = aes.CreateDecryptor();
            byte[] buf = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(input);
                    cs.Write(xXml, 0, xXml.Length);
                }
                buf = ms.ToArray();
            }
            string Output = Encoding.UTF8.GetString(buf);
            return Output;
        }
        catch (Exception ex)
        {
            return input;
        }
    }
}
