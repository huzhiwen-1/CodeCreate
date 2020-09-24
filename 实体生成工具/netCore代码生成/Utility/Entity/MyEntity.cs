using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.ComponentModel;       
using System;

namespace Utility
{
    public abstract class MyEntity
    {
        public MyEntity()
        {
        }

        public MyEntity(string dataxml)
        {
            Type type = this.GetType();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(dataxml);

            string tableName = type.Name;
            object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            if (tableAttributes.Length > 0)
            {
                TableAttribute attr = (TableAttribute)tableAttributes[0];
                tableName = attr.TableName;
            }

            XmlNode rootNode = xmlDoc.SelectSingleNode(tableName);

            PropertyInfo[] Propertys = type.GetProperties();

            XmlNode node;
            for (Int16 i = 0; i < Propertys.Length; i++)
            {

                String PropertyName = Propertys[i].Name;
                object[] columnAttributes = Propertys[i].GetCustomAttributes(typeof(ColumnAttribute), true);
                if (columnAttributes.Length > 0)
                {
                    ColumnAttribute column = (ColumnAttribute)columnAttributes[0];
                    if (column.IsPrimaryKey)
                    {
                        if (rootNode.Attributes["keyvalue"] != null)
                        {
                            string keyvalue = rootNode.Attributes["keyvalue"].Value;
                            Propertys[i].SetValue(this, Utils.GetObjectOfType(Propertys[i].PropertyType, keyvalue), null);
                        }
                    }
                    else
                    {
                        node = rootNode.SelectSingleNode(PropertyName);
                        if (node != null)
                        {
                            var nodeValue = Utils.GetObjectOfType(Propertys[i].PropertyType, node.InnerText);
                            if(nodeValue!=null){
                                Propertys[i].SetValue(this, nodeValue, null);
                            }
                            
                        }
                    }
                }
            }
        }

        public string ToDataXml()
        {
            StringBuilder buffer = new StringBuilder();
            Type type = this.GetType();
            XmlSerializer serializer = new XmlSerializer(type);
            using (TextWriter writer = new StringWriter(buffer))
            {
                serializer.Serialize(writer, this);
                writer.Close();
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(buffer.ToString());
            string tableName = type.Name;
            object[] tableAttributes = type.GetCustomAttributes(typeof(TableAttribute), true);
            if (tableAttributes.Length > 0)
            {
                TableAttribute attr = (TableAttribute)tableAttributes[0];
                tableName = attr.TableName;
            }

            PropertyInfo[] Propertys = type.GetProperties();
            string PrimaryKey = "";

            XmlNode rootNode = xmlDoc.SelectSingleNode(type.Name);
            XmlNode PropertyNode;

            for (Int16 i = 0; i < Propertys.Length; i++)
            {
                object[] columnAttributes = Propertys[i].GetCustomAttributes(typeof(ColumnAttribute), true);


                if (columnAttributes.Length > 0)
                {
                    ColumnAttribute column = (ColumnAttribute)columnAttributes[0];
                    if (column.IsPrimaryKey)
                    {
                        PrimaryKey = Propertys[i].Name;
                    }
                }
                else
                {
                    //将没有增加特性的节点从原xml中移除
                    PropertyNode = rootNode.SelectSingleNode(Propertys[i].Name);
                    if (PropertyNode != null)
                    {
                        rootNode.RemoveChild(PropertyNode);
                    }

                }
            }


            PropertyNode = rootNode.SelectSingleNode(PrimaryKey);
            String PrimaryValue = "";
            if (PropertyNode != null)
            {
                PrimaryValue = PropertyNode.InnerText;
                rootNode.RemoveChild(PropertyNode);
            }


            buffer = new StringBuilder();
            buffer.AppendFormat("<{0} keyname='{1}' keyvalue='{2}'>", tableName, PrimaryKey, PrimaryValue);
            buffer.Append(rootNode.InnerXml);
            buffer.AppendFormat("</{0}>", tableName);
            return buffer.ToString();
        }
    }
}


