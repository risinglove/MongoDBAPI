using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.IO;

/// <summary>
/// internal 同一命名空间下可以访问
/// </summary>
namespace Utility
{
    /// <summary>
    /// 该Class用来存放各种加密方法
    /// </summary>
    public static class EncyryptionUtil
    {
        private static string MAIBAO_DES_KEY => "xGQ7DaR^%mY7fska";
        private static string MAIBAO_AES_KEY => "T%t!iAkFCnwc2wUU";

        /// <summary>
        /// 该方法需要自动在密码字符串后加上WebConfig内约定的字符串，然后再进行二次MD5
        /// </summary>
        /// <param name="pi_password"></param>
        /// <returns></returns>
        public static string genUserPassMD5(string pi_password)
        {
            var passAtt = ConfigurationManager.AppSettings["UserPassWordMd5Att"];
            var readyPass = pi_password + passAtt;
            var afterMD5 = getMD5(readyPass);
            afterMD5 = getMD5(afterMD5);//进行两次MD5k
            return afterMD5;
        }

        /// <summary>
        /// 专门用于脉保各个产品间数据交互加密时产生Key的方法
        /// </summary>
        /// <param name="pi_receiveToken"></param>
        /// <returns></returns>
        public static string getGJBToken(string pi_receiveToken)
        {
            var gjbPortSalt = ConfigurationManager.AppSettings["GJBPortSalt"];
            var md5Token = getMD5(pi_receiveToken + gjbPortSalt);
            //LogUtil.DoLog("MD5", gjbPortSalt + " md5Token:" + md5Token);
            return md5Token;
        }


        /// <summary>
        /// 输入字符串获取MD5（32位）的方法，该方法适用于我们自己的系统
        /// </summary>
        /// <param name="pi_string"></param>
        /// <returns></returns>
        public static string getMD5(string pi_string)
        {
            // Use input string to calculate MD5 hash
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(pi_string);//注意使用Encoding.ASCII
            var hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();
            for (var i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// UTF8的MD5获取方法，这个一般通用到所有合作伙伴
        /// </summary>
        /// <param name="pi_string"></param>
        /// <returns></returns>
        public static string GetMd5Utf8(string pi_string)
        {
            return huizeGetMD5(pi_string);
        }


        /// <summary>
        /// 输入字符串获取MD5（32位）的方法，其中字符串读取方式用UTF8
        /// </summary>
        /// <param name="pi_string"></param>
        /// <returns></returns>
        public static string huizeGetMD5(string pi_string)
        {
            // Use input string to calculate MD5 hash
            var md5 = MD5.Create();
            var inputBytes = Encoding.UTF8.GetBytes(pi_string);//注意使用Encoding.UTF8
            var hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            var sb = new StringBuilder();
            for (var i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 用来生成随机密码
        /// </summary>
        /// <param name="passwordLen"></param>
        /// <returns></returns>
        public static string GetRandomPassword(int passwordLen)
        {
            var randomChars = "BCDFGHJKMPQRTVWXY2346789";
            var password = string.Empty;
            int randomNum;
            var random = new Random();
            for (var i = 0; i < passwordLen; i++)
            {
                randomNum = random.Next(randomChars.Length);
                password += randomChars[randomNum];
            }
            return password;
        }

        /// <summary>
        /// AES加密，默认为256位的Key，没有向量
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key">如果没有默认值，那么就取WebConfig中的（方便前后台传递信息加密）</param>
        /// <returns></returns>
        public static string AESEncrypt(string plainText, string key = "")
        {
            string AESKey;
            if (string.IsNullOrWhiteSpace(key))
            {
                AESKey = MAIBAO_AES_KEY;
            }
            else
            {
                AESKey = key;
            }
            return new AES().AESEncrypt(plainText, AESKey, AES.AesKeyLength.Long256);
        }

        /// <summary>
        /// AES解密，默认为256位的Key，没有向量
        /// </summary>
        /// <param name="showText"></param>
        /// <param name="key">如果没有默认值，那么就取WebConfig中的（方便前后台传递信息加密）</param>
        /// <returns></returns>
        public static string AESDecrypt(string showText, string key = "")
        {
            string AESKey;
            if (string.IsNullOrWhiteSpace(key))
            {
                AESKey = MAIBAO_AES_KEY;
            }
            else
            {
                AESKey = key;
            }
            return new AES().AESDecrypt(showText, AESKey, AES.AesKeyLength.Long256);
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string DESEncrypt(string plainText, string key)
        {
            return new DESUtil().DESEncrypt(plainText, key);
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="showText"></param>
        /// <returns></returns>
        public static string DESDecrypt(string showText, string key)
        {
            return new DESUtil().DESDecrypt(showText, key);
        }

    }
    
    internal class AES
    {
        /// <summary>
        /// AESKey的长度，分别为64位，128位，256位
        /// </summary>
        public enum AesKeyLength : int
        {
            Short64 = 8, Middle128 = 16, Long256 = 32
        }

        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="Key">密钥</param>
        /// <returns>将加密后的密文转换为Base64编码，以便显示</returns>
        public string AESEncrypt(string plainText, string Key, AesKeyLength keyLengh)
        {
            int keyL = 32;
            switch (keyLengh)
            {
                case AesKeyLength.Long256:
                    keyL = 32;
                    break;
                case AesKeyLength.Middle128:
                    keyL = 16;
                    break;
                case AesKeyLength.Short64:
                    keyL = 8;
                    break;
            }

            MemoryStream mStream = new MemoryStream();
            RijndaelManaged aes = new RijndaelManaged();

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            Byte[] bKey = new Byte[keyL];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);

            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            //aes.Key = _key;
            aes.Key = bKey;
            //aes.IV = _iV;
            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            try
            {
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                aes.Clear();
            }
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">密文字符串</param>
        /// <param name="Key">密钥</param>
        /// <returns>返回解密后的明文字符串</returns>
        public string AESDecrypt(string showText, string Key, AesKeyLength keyLengh)
        {
            int keyL = 32;
            switch (keyLengh)
            {
                case AesKeyLength.Long256:
                    keyL = 32;
                    break;
                case AesKeyLength.Middle128:
                    keyL = 16;
                    break;
                case AesKeyLength.Short64:
                    keyL = 8;
                    break;
            }

            Byte[] encryptedBytes = Convert.FromBase64String(showText);
            Byte[] bKey = new Byte[keyL];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);

            MemoryStream mStream = new MemoryStream(encryptedBytes);
            //mStream.Write( encryptedBytes, 0, encryptedBytes.Length );
            //mStream.Seek( 0, SeekOrigin.Begin );
            RijndaelManaged aes = new RijndaelManaged();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.Key = bKey;
            //aes.IV = _iV;
            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            try
            {
                byte[] tmp = new byte[encryptedBytes.Length + 32];
                int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length + 32);
                byte[] ret = new byte[len];
                Array.Copy(tmp, 0, ret, 0, len);
                return Encoding.UTF8.GetString(ret);
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                aes.Clear();
            }
        }


    }
    
    /// <summary>
    /// 
    /// </summary>
    internal class DESUtil
    {
        //private string _DESKey = "MAIBAKEY";
        //public string DESKey
        //{
        //    set
        //    {
        //        _DESKey = value;
        //    }
        //}

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <returns></returns>
        public string DESEncrypt(string toEncrypt, string _DESKey)
        {
            //定义DES加密服务提供类
            var des = new DESCryptoServiceProvider();
            //加密字符串转换为byte数组
            var inputByte = System.Text.ASCIIEncoding.UTF8.GetBytes(toEncrypt);
            //加密密匙转化为byte数组
            var key = Encoding.ASCII.GetBytes(_DESKey); //DES密钥(必须8字节)
            des.Key = key;
            des.IV = key;
            //创建其支持存储区为内存的流
            var ms = new MemoryStream();
            //定义将数据流链接到加密转换的流
            var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByte, 0, inputByte.Length);
            cs.FlushFinalBlock();
            var ret = new StringBuilder();
            foreach (var b in ms.ToArray())
            {
                //向可变字符串追加转换成十六进制数字符串的加密后byte数组。
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();

        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        public string DESDecrypt(string toDecrypt, string _DESKey)
        {
            //定义DES加密解密服务提供类
            var des = new DESCryptoServiceProvider();
            //加密密匙转化为byte数组
            var key = Encoding.ASCII.GetBytes(_DESKey);
            des.Key = key;
            des.IV = key;
            //将被解密的字符串每两个字符以十六进制解析为byte类型，组成byte数组
            var length = (toDecrypt.Length / 2);
            var inputByte = new byte[length];
            for (var index = 0; index < length; index++)
            {
                var substring = toDecrypt.Substring(index * 2, 2);
                inputByte[index] = Convert.ToByte(substring, 16);
            }
            //创建其支持存储区为内存的流
            var ms = new MemoryStream();
            //定义将数据流链接到加密转换的流
            var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByte, 0, inputByte.Length);
            cs.FlushFinalBlock();

            return ASCIIEncoding.UTF8.GetString((ms.ToArray()));
        }
    }

}