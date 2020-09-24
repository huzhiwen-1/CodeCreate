using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;     
using System.Xml;
using NUnit.Framework;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Utility
{
    public class MyString
    {

       

        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;

            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);

                if (bsSrcString.Length > p_Length)
                {
                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = 0; i < p_Length; i++)
                    {

                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_Length - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                    {
                        nRealLength = p_Length + 1;
                    }

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, bsResult, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);

                    myResult = myResult + p_TailString;
                }

            }

            return myResult;
        }

        /// <summary>
        /// 将string的集合用一个字符拼接成
        /// </summary>
        /// <param name="list"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string Join(List<string> list, char c)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in list)
            {
                sb.AppendFormat("{0}{1}", str, c.ToString());
            }
            return sb.ToString().TrimEnd(c);
        }

        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetLen(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }
        /// <summary>
        /// 判断是否存在特殊字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean Checkforbiddenchars(string str)
        {
            string strforbiddenchars = @"/\\!@%#?.|\-=+^$*~×÷＋§※±,;'\&<>" + "\"";
            return Checkforbiddenchars(str, strforbiddenchars);
        }
        /// <summary>
        /// 判断是否存在特殊字符
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="strforbiddenchars">特殊字符串</param>
        /// <returns></returns>
        public static Boolean Checkforbiddenchars(string source, string strforbiddenchars)
        {
            //屏蔽影响正则表达式的特殊字符
            var regChar = new string[] { "^", "[", "]" };
            for (int i = 0; i < regChar.Length; i++)
            {
                string str = regChar[i];
                if (strforbiddenchars.IndexOf(str) > -1)
                {
                    if (source.IndexOf(str) > -1)
                    {
                        return true;
                    }
                    else
                    {
                        strforbiddenchars = strforbiddenchars.Replace(str, "");
                    }
                }
            }

            Regex regExp = new Regex("[" + strforbiddenchars + "]");
            return regExp.IsMatch(source);
        }


        /// <summary>
        /// 判断是否是中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsCnChar(string str)
        {
            return Regex.IsMatch(str, "^[\u4e00-\u9fa5]+$");
        }

        /// <summary>
        /// 是否是数字、英文字母和下划线组成的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Boolean IsEnChar(string str)
        {
            return Regex.IsMatch(str, "^[a-zA-Z0-9_-]+$");
        }

        /// <summary>
        /// 是否是正整数组成的字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsPositiveInteger(string str)
        {
            return Regex.IsMatch(str, @"^[0-9]*$");
        }
        /// <summary>
        /// 是否是数字组成的字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            return Regex.IsMatch(str, @"^(-?\d+)(\.?\d+)?$");
        }

        /// <summary>
        /// 将XML中转义过的字符串还原为原义
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReplaceXmlToChar(string str)
        {
            str = str.Replace("&lt;", "<");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&apos;", "'");
            str = str.Replace("&quot;", "\"");
            str = str.Replace("&amp;", "&");
            return str;
        }
        /// <summary>
        /// 对特殊字符进行编码
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        public static string ReplaceCharToXml(string strXml)
        {
            if (String.IsNullOrEmpty(strXml))
            {
                return "";
            }
            strXml = strXml.Replace("&", "&amp;");
            strXml = strXml.Replace("<", "&lt;");
            strXml = strXml.Replace(">", "&gt;");
            strXml = strXml.Replace("'", "&apos;");
            strXml = strXml.Replace("\"", "&quot;");

            return strXml;
        }
    }
}
                    