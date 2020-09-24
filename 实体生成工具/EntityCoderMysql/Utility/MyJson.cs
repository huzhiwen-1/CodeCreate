using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;   
using System.Xml;
using NUnit.Framework;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace  Utility
{
    public class MyJson
    {
        /// <summary>
        /// 将对象序列化为字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);

        }

        /// <summary>
        /// 将Dtatable序列化为json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeDatatable(DataTable dt)
        {
            return JsonConvert.SerializeObject(dt, new ConvertDataTableSetting());
        }

        /// <summary>
        /// 将DataRow数组序列化为json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeDataRows(DataRow[] rows)
        {
            return JsonConvert.SerializeObject(rows, new ConvertDataRowsSetting());
        }


        /// <summary>
        ///DataTable解析JSON
        /// </summary>
        private class ConvertDataTableSetting : JsonConverter
        {
            public ConvertDataTableSetting()
            {
                //
                //TODO: 在此处添加构造函数逻辑
                //
            }
            public override bool CanConvert(Type objectType)
            {
                return typeof(DataTable).IsAssignableFrom(objectType);
            }



            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                DataTable dt = (DataTable)value;
                writer.WriteStartArray();
                foreach (DataRow dr in dt.Rows)
                {
                    writer.WriteStartObject();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        writer.WritePropertyName(dc.ColumnName);
                        writer.WriteValue(dr[dc].ToString());
                        
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
        }

        /// <summary>
        ///DataRow数组解析JSON
        /// </summary>
        private class ConvertDataRowsSetting : JsonConverter
        {
            public ConvertDataRowsSetting()
            {
                //
                //TODO: 在此处添加构造函数逻辑
                //
            }
            public override bool CanConvert(Type objectType)
            {
                return typeof(DataRow[]).IsAssignableFrom(objectType);
            }



            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                DataRow[] rows = (DataRow[])value;
                writer.WriteStartArray();
                foreach (DataRow dr in rows)
                {
                    writer.WriteStartObject();
                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        writer.WritePropertyName(dc.ColumnName);
                        writer.WriteValue(dr[dc].ToString());
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }
        }
    }

    
}
