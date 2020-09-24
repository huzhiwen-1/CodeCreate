﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;    
using System.Security.Cryptography;

namespace  Utility
{
    /// <summary>
    /// 加密解密算法
    /// </summary>
    public class MyEncrypt
    {
        private static byte[] Keys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };

        /// <summary>
        /// ASC加密算法
        /// </summary>
        /// <param name="encryptString">需要加密的字符串</param>
        /// <param name="encryptKey">加密密钥</param>
        /// <returns></returns>
        public static string AesEncode(string encryptString, string encryptKey)
        {
            encryptKey = MyString.GetSubString(encryptKey, 32, "");
            encryptKey = encryptKey.PadRight(32, ' ');

            RijndaelManaged rijndaelProvider = new RijndaelManaged();
            rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
            rijndaelProvider.IV = Keys;
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

            byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

            return Convert.ToBase64String(encryptedData);
        }
        public static string AesEncode(string encryptString)
        {
            return AesEncode(encryptString, "MYSOFT_CRE");
        }


        /// <summary>
        /// ASC解密算法
        /// </summary>
        /// <param name="decryptString">需要解密的字符串</param>
        /// <param name="decryptKey">解密密钥</param>
        /// <returns></returns>
        public static string AesDecode(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = MyString.GetSubString(decryptKey, 32, "");
                decryptKey = decryptKey.PadRight(32, ' ');

                RijndaelManaged rijndaelProvider = new RijndaelManaged();
                rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
                rijndaelProvider.IV = Keys;
                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                return Encoding.UTF8.GetString(decryptedData);
            }
            catch
            {
                return "";
            }

        }
        public static string AesDecode(string decryptString)
        {
            return AesDecode(decryptString, "MYSOFT_CRE");
        }
    }
}
