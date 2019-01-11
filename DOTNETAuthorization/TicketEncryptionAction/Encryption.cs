using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DOTNETAuthorization.TicketEncryptionAction
{
    /// <summary>
    /// 加密
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// DES加密字
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        /// <returns></returns>
        public static String DesEncrypt(String encryptString, String encryptKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(encryptString);
            des.Key = ASCIIEncoding.ASCII.GetBytes(GetMD5(encryptKey).Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(GetMD5(encryptKey).Substring(0, 8));
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            StringBuilder ret = new StringBuilder();
            foreach (byte b in memoryStream.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }

            return ret.ToString();
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static String GetMD5(String txt)
        {
            MD5 mD5 = new MD5CryptoServiceProvider();
            Byte[] data = mD5.ComputeHash(Encoding.UTF8.GetBytes(txt));
            mD5.Clear();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("X").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static String DesDecrypt(string decryptString, string decryptKey)
        {
            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
            Int32 len;
            len = decryptString.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(decryptString.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }

            desc.Key = ASCIIEncoding.ASCII.GetBytes(GetMD5(decryptKey).Substring(0, 8));
            desc.IV = ASCIIEncoding.ASCII.GetBytes(GetMD5(decryptKey).Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, desc.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}
