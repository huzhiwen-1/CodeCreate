using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;
using System.Reflection;
using System.Xml;       

namespace  Utility
{
    public class MyCollections<T> where T : new() 
    {
        /// <summary>
        /// 将DataTable转化为list
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="name">名称</param>
        /// <returns>实体集合</returns>
        public static IList<T> GetListFromDataTable(DataTable dt)
        {
            List<T> list = new List<T>();
            Type type = typeof(T); 
            PropertyInfo[] Propertys = type.GetProperties();
            string tempName = "",tempType="";
            foreach (DataRow row in dt.Rows)
            {
                T model = new T();
                foreach (PropertyInfo pi in Propertys)
                {
                    tempName = pi.Name;
                    tempType = pi.PropertyType.ToString();
                    if (dt.Columns.Contains(tempName))
                    {

                        object tempValue = Utils.GetObjectOfType(pi.PropertyType, row[tempName]);
                        try
                        {
                            if (tempValue != DBNull.Value)
                                pi.SetValue(model, tempValue, null); 
                        }
                        catch {
                            throw new Exception("属性赋值失败");
                        }
                    }
                }
                list.Add(model);
            }

            return list;
        }
                     
    }
}
