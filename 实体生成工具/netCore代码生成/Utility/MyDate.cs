using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;    
using NUnit.Framework;
using Utility.Data;

namespace  Utility
{
    public class MyDate
    {

        public static string GetSysdate()
        {
            return GetSysdate("yyyy-MM-dd");
        }
        //功能：获取数据库服务器的系统日期
        //参数：所获取日期的格式
        //返回：当前日期的字符串
        public static string GetSysdate(string strDateFormat)
        {
            return DateTime.Parse(MyDB.GetDataItemString("SELECT GetDate()")).ToString(strDateFormat);

        }

        public static string Format(string strDate ,string strDateFormat)
        {
            if (string.IsNullOrEmpty(strDate)) {
                return "";
            }
               
            return Convert.ToDateTime(strDate).ToString(strDateFormat);
        }

        public static string Format(string strDate)
        {
            if (string.IsNullOrEmpty(strDate))
            {
                return "";
            }
            return Convert.ToDateTime(strDate).ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 获取两个日期之间的天数
        /// </summary>
        /// <param name="strBgnDate"></param>
        /// <param name="strEndDate"></param>
        /// <returns></returns>
        public static int Diff(string strBgnDate, string strEndDate)
        {
            DateTime dtBgnDate = Convert.ToDateTime(strBgnDate);
            DateTime dtEndDate = Convert.ToDateTime(strEndDate);

            return new TimeSpan(dtEndDate.Ticks - dtBgnDate.Ticks).Days;
        }

    }
}
