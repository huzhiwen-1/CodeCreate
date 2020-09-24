using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace  Utility
{
    public class Utils
    {
        /// <summary>
        /// 将object转换为指定类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object GetObjectOfType(Type type, object obj)
        {
            object tempValue;
            var tempType = type.ToString();
            if (tempType == "System.Double" || tempType == "System.Decimal")
            {
                if (string.IsNullOrEmpty(obj.ToString()))
                {
                    tempValue = null;
                }
                else {
                    tempValue = Convert.ToDouble(obj);
                }
                
            }
            else if (tempType == "System.DateTime")
            {
                if (string.IsNullOrEmpty(obj.ToString()))
                {
                    tempValue = null;
                }
                else
                {
                    tempValue = Convert.ToDateTime(obj);
                }
            }
            else if (tempType == "System.Int16" || tempType == "System.Int32" || tempType == "System.Int64")
            {
                if (string.IsNullOrEmpty(obj.ToString()))
                {
                    tempValue = null;
                }
                else
                {
                    tempValue = Convert.ToInt32(obj);
                }
            }
            else
            {
                tempValue = obj.ToString();
            }

            return tempValue;
        }

        /// <summary>
        /// 将列转化为字符串，避免DBNull时直接ToString报错
        /// </summary>
        /// <param name="column">需要转化的列</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static object IsDBNull(Object column, object defaultValue)
        {
            if (DBNull.Value.Equals(column))
            {
                return defaultValue;
            }
            else {
                return column.ToString();
            }
        }

        /// <summary>
        /// 将列转化为字符串，避免DBNull时直接ToString报错
        /// </summary>
        /// <param name="column">需要转化的列</param>
        /// <returns></returns>
        public static object IsDBNull(Object column)
        {
            return IsDBNull(column, "");
        }

        
    }
}
