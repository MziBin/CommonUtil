using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CommonUtil
{
    /// <summary>
    /// 加密解密工具类，提供常用的加密解密方法
    /// </summary>
    public static class CryptoHelper
    {
        #region MD5加密

        /// <summary>
        /// 计算字符串的MD5值
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>MD5值（32位小写）</returns>
        public static string Md5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件的MD5值（32位小写）</returns>
        public static string Md5File(string filePath)
        {
            using (MD5 md5 = MD5.Create())
            using (FileStream stream = File.OpenRead(filePath))
            {
                string str = "";
                byte[] buffer = new byte[8192];
                while (stream.Read(buffer, 0, buffer.Length) != 0)
                {
                    str += Encoding.UTF8.GetString(buffer);
                }
                byte[] inputBytes = Encoding.UTF8.GetBytes(str);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        #endregion

        #region SHA加密

        /// <summary>
        /// 计算字符串的SHA1值
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>SHA1值（40位小写）</returns>
        public static string Sha1(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 计算字符串的SHA256值
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>SHA256值（64位小写）</returns>
        public static string Sha256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// 计算字符串的SHA512值
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>SHA512值（128位小写）</returns>
        public static string Sha512(string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha512.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        #endregion

        #region AES加密解密

        /// <summary>
        /// AES加密字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="key">密钥（必须为16、24或32字节）</param>
        /// <param name="iv">初始化向量（必须为16字节）</param>
        /// <returns>加密后的Base64字符串</returns>
        public static string AesEncrypt(string input, string key, string iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(iv);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(input);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// AES解密字符串
        /// </summary>
        /// <param name="encryptedText">加密后的Base64字符串</param>
        /// <param name="key">密钥（必须为16、24或32字节）</param>
        /// <param name="iv">初始化向量（必须为16字节）</param>
        /// <returns>解密后的字符串</returns>
        public static string AesDecrypt(string encryptedText, string key, string iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(iv);
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        #endregion

        #region Base64编码解码

        /// <summary>
        /// Base64编码字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>Base64编码后的字符串</returns>
        public static string Base64Encode(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解码字符串
        /// </summary>
        /// <param name="base64String">Base64编码的字符串</param>
        /// <returns>解码后的字符串</returns>
        public static string Base64Decode(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Base64编码文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>Base64编码后的字符串</returns>
        public static string Base64EncodeFile(string filePath)
        {
            byte[] bytes = Encoding.UTF8.GetBytes( File.ReadAllText(filePath, Encoding.UTF8) );
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解码到文件
        /// </summary>
        /// <param name="base64String">Base64编码的字符串</param>
        /// <param name="filePath">输出文件路径</param>
        public static void Base64DecodeToFile(string base64String, string filePath)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            File.WriteAllBytes(filePath, bytes);
        }

        #endregion
    }
}