using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Utility;
using Utility.Data;
using Utility.NVelocity;
using System.IO;
using System.Collections;    
namespace EntityCoder
{
    public partial class Entity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable table = MyDB.GetDataTable("SELECT name AS tname FROM sys.tables WHERE type='U'  ORDER BY name");
                tableList.DataSource = table;
                tableList.DataBind();
            }
            
        }
        /// <summary>
        /// 获取类名
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetClass(string tableName)
        {
            string className = "";
            string[] arr = tableName.Split('_');
            foreach (string str in arr)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    className += str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
                }
            }
            return className;
        }
        /// <summary>
        /// 获取控制器
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetController(string tableName)
        {
            string className = "";
            if (tableName.IndexOf("_") == 1)
            {
                tableName = tableName.Substring(2, tableName.Length - 2);
                string[] arr = tableName.Split('_');
                foreach (string str in arr)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        className += str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
                    }
                }
            }
            else
            { 
                className = tableName.Substring(1, tableName.Length -1);
            }
            return className;
        }
        protected void CreateCode(object sender, EventArgs e)
        {
            string strTables =  MyRequest.GetString("table");    
            string  strDir   = MyRequest.GetString("txtDir")  ;
            string strNameSpace = MyRequest.GetString("txtNameSpace");
            if( !string.IsNullOrEmpty(strTables)){
                string[] arr = strTables.Split(',');
                foreach(string tableName in arr){
                   IDictionary context = new Hashtable();
                   IDictionary context1 = new Hashtable();
                   IDictionary context2 = new Hashtable();
                    string first = tableName.Substring(0, 1);    
                    //string className = first.ToUpper() + tableName.Substring(1,tableName.Length-1).Replace(  "_", "");
                    string className = GetClass(tableName);
                    //context.Add("TableName",tableName) ;
                    //context.Add("ClassName",className);
                    //context1.Add("ClassName", className + "Repository");
                    //context1.Add("ClassNameEntity", className);
                    DataTable dstable = MyDB.GetDataTable("SELECT distinct colorder = C.column_id , ColumnName = C.name , TypeName = T.name , Length = CASE WHEN T.name = 'nchar' THEN C.max_length / 2 WHEN T.name = 'nvarchar' THEN C.max_length / 2 ELSE C.max_length END , IsIdentity = C.is_identity, IsPrimaryKey = ISNULL(IDX.PrimaryKey,0) , IsNull = C.is_nullable , deText = PFD.[value],O.name as tableName  FROM    sys.columns C INNER JOIN sys.objects O ON C.[object_id] = O.[object_id] AND ( O.type = 'U' OR O.type = 'V' ) AND O.is_ms_shipped = 0 INNER JOIN sys.types T ON C.user_type_id = T.user_type_id LEFT JOIN sys.extended_properties PFD ON PFD.class = 1  AND C.[object_id] = PFD.major_id AND C.column_id = PFD.minor_id LEFT JOIN ( SELECT  IDXC.[object_id] , IDXC.column_id , PrimaryKey = IDX.is_primary_key FROM sys.indexes IDX INNER JOIN sys.index_columns IDXC ON IDX.[object_id] = IDXC.[object_id] AND IDX.index_id = IDXC.index_id LEFT JOIN sys.key_constraints KC ON IDX.[object_id] = KC.[parent_object_id]  AND IDX.index_id = KC.unique_index_id  ) IDX ON C.[object_id] = IDX.[object_id]  AND C.column_id = IDX.column_id WHERE   O.name = N'" + tableName + "' ORDER BY O.name , C.column_id ");
                    string controllerName = GetController(tableName);
                    context.Add("TableName", tableName); 
                    context.Add("ControllerName", controllerName);
                    context.Add("nameSpace", strNameSpace);
                    context.Add("ClassName", className);
                    context.Add("ClassNameEntity", className); 

                    context1.Add("ClassName", className + "Repository");
                    context1.Add("ClassNameEntity", className);
                    context1.Add("Name", controllerName);
                    context1.Add("nameSpace", strNameSpace);
                    context1.Add("TableName", tableName);
                    context2.Add("ClassName", className + "Service");
                    context2.Add("ClassNameEntity", className);
                    context2.Add("nameSpace", strNameSpace);
                    context2.Add("ClassRepository", className + "Repository");
                    context2.Add("Name", controllerName);

                    List<TableCol> collist = new List<TableCol>();
                    List<TableCol> collistView = new List<TableCol>();
                    int i = 0;
                    int j = 0;
                    string privateKey = "";
                    foreach (DataRow row in dstable.Rows)
                    {
                        TableCol t = new TableCol(row);
                        t.Index = i % 2;

                        if (i == dstable.Rows.Count - 1)
                        {
                            t.IsEnd = true;
                        }
                        else
                        {
                            t.IsEnd = false;
                        }
                        i++;
                        if (t.IsKey)
                        {
                            privateKey = t.Name;
                        }
                        if (t.Name != "CreatedOn" && t.Name != "CreatedByID" && t.Name != "ModifyOn" && t.Name != "ModifyByID" && t.Name != "IsDeleted" )
                        {
                            collistView.Add(t);
                            j++;
                        }
                        collist.Add(t);
                    }
                    j = 0;
                    foreach (TableCol tc in collistView)
                    {
                        tc.Index = j % 2;
                        if (j == collistView.Count - 1)
                        {
                            tc.IsEnd = true;
                        }
                        j++;
                    }
                    context.Add("Propertys", collist);
                    context.Add("PropertysLength", collist.Count);
                    context.Add("ViewPropertys", collistView);
                    context.Add("PrivatyKey", privateKey);

                    context1.Add("PrivatyKey", privateKey);
                    context2.Add("PrivatyKey", privateKey);
                    context1.Add("Propertys", collist);
                    context2.Add("Propertys", collist);

                    string templateDir = HttpContext.Current.Server.MapPath("/");
                    INVelocityEngine fileEngine = NVelocityEngineFactory.CreateFileEngine(templateDir, true);
                    if (!Directory.Exists(strDir))
                    {
                        Directory.CreateDirectory(strDir);
                        Directory.CreateDirectory(strDir + "/Entity");
                        //Directory.CreateDirectory(strDir + "/Repository");
                        Directory.CreateDirectory(strDir + "/Services");
                        //Directory.CreateDirectory(strDir + "/js");
                        //Directory.CreateDirectory(strDir + "/Views");
                        Directory.CreateDirectory(strDir + "/Models");
                        Directory.CreateDirectory(strDir + "/IServices");
                        Directory.CreateDirectory(strDir + "/Controllers");
                    }
                    if (!Directory.Exists(strDir + "/Entity"))
                    {
                        Directory.CreateDirectory(strDir + "/Entity");
                    }
                    if (!Directory.Exists(strDir + "/Models"))
                    {
                        Directory.CreateDirectory(strDir + "/Models");
                    }
                    if (!Directory.Exists(strDir + "/Controllers"))
                    {
                        Directory.CreateDirectory(strDir + "/Controllers");
                    }
                    //if (!Directory.Exists(strDir + "/Repository"))
                    //{
                    //    Directory.CreateDirectory(strDir + "/Repository");
                    //}
                    if (!Directory.Exists(strDir + "/Services"))
                    {
                        Directory.CreateDirectory(strDir + "/Services");
                    }
                    if (!Directory.Exists(strDir + "/IServices"))
                    {
                        Directory.CreateDirectory(strDir + "/IServices");
                    }
                    //if (!Directory.Exists(strDir + "/js"))
                    //{
                    //    Directory.CreateDirectory(strDir + "/js");
                    //}
                    //if (!Directory.Exists(strDir + "/Views"))
                    //{
                    //    Directory.CreateDirectory(strDir + "/Views");
                    //}

                    //if (!Directory.Exists(strDir + "/js/" + controllerName))
                    //{
                    //    Directory.CreateDirectory(strDir + "/js/" + controllerName);
                    //}
                    //if (!Directory.Exists(strDir + "/Views/" + controllerName))
                    //{
                    //    Directory.CreateDirectory(strDir + "/Views/" + controllerName);
                    //}
                    //生成实体文件
                    MyIO.CreateFile(fileEngine.Process(context, "Entity.vm"), string.Format("{0}/Entity/{1}.cs", strDir, className));
                    //生成仓储文件
                    //MyIO.CreateFile(fileEngine.Process(context1, "Repository.vm"), string.Format("{0}/Repository/{1}.cs", strDir, className + "Repository"));
                    //生成服务文件
                    MyIO.CreateFile(fileEngine.Process(context2, "Services.vm"), string.Format("{0}/Services/{1}.cs", strDir, className + "Service"));
                    MyIO.CreateFile(fileEngine.Process(context2, "Interface.vm"), string.Format("{0}/IServices/{1}.cs", strDir,"I"+ className + "Service"));
                 
                    //生成控制器
                    MyIO.CreateFile(fileEngine.Process(context, "Controller.vm"), string.Format("{0}/Controllers/{1}.cs", strDir, controllerName + "Controller"));
                    //生成模型
                    MyIO.CreateFile(fileEngine.Process(context, "Models.vm"), string.Format("{0}/Models/{1}.cs", strDir, className + "Form"));
                    //生成视图
                    //MyIO.CreateFile(fileEngine.Process(context, "ViewIndex.vm"), string.Format("{0}/Views/" + controllerName + "/index.cshtml", strDir));
                    //MyIO.CreateFile(fileEngine.Process(context, "ViewEdit.vm"), string.Format("{0}/Views/" + controllerName + "/add.cshtml", strDir));
                    ////生成Js文件
                    //MyIO.CreateFile(fileEngine.Process(context, "jsIndex.vm"), string.Format("{0}/js/" + controllerName + "/index.js", strDir));
                    //MyIO.CreateFile(fileEngine.Process(context, "jsEdit.vm"), string.Format("{0}/js/" + controllerName + "/add.js", strDir));
                }
            }    
        }
    }



    public class TableCol
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public int Index
        {
            get;
            set;
        }
        public bool IsEnd
        {
            get;
            set;
        }
        private string _Type;
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private Boolean _IsKey;
        public Boolean IsKey
        {
            get { return _IsKey; }
            set { _IsKey = value; }
        }
        public Boolean _IsNull;
        public Boolean IsNull
        {
            get { return _IsNull; }
            set { _IsNull = value; }
        }
        private string _Remark;
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        private string _Remark2;
        public string Remark2
        {
            get { return _Remark2; }
            set { _Remark2 = value; }
        }
        private bool _ModelCheckNull;
        public bool ModelCheckNull
        {
            get { return _ModelCheckNull; }
            set { _ModelCheckNull = value; }
        }

        private int _maxLength;
        public int MaxLength
        {
            get { return _maxLength; }
            set { _maxLength = value; }
        }
        public TableCol(DataRow row)
        {
            _Name = row["ColumnName"].ToString();
            _Type = CovertDBTypeToVBType(row["TypeName"].ToString());
            _IsKey = (Boolean)row["IsPrimaryKey"];
            if (_Type == "string")
            {
                _IsNull = false;
            }
            else { _IsNull = (Boolean)row["IsNull"]; }
            
            _Remark = row["deText"].ToString().Replace("\r\n", ";");
            _ModelCheckNull = false;
            if (_IsKey)
            {
                _ModelCheckNull = false;
            }
            else if ((Boolean)row["IsNull"]) { _ModelCheckNull = true; }
            _Remark2 = _Remark.Split('：')[0];
            _maxLength = 0;
            if (_Type == "string" && (!string.IsNullOrEmpty(row["Length"].ToString())))
            {
                _maxLength = Convert.ToInt32(row["Length"]);
            }
        }


        public string CovertDBTypeToVBType(string strDbTypeName)
        {
            string returnStr = "string";
            switch (strDbTypeName.ToUpper())
            {
                case "LONG":
                case "INT":
                case "TINYINT":
                    returnStr = "int";
                    break;
                case "BIT":
                case "BOOL":
                    returnStr = "bool";
                    break;
                case "NCHAR":
                case "NTEXT":
                case "NVARCHAR":
                case "CHAR":
                case "TEXT":
                case "VARCHAR":
                case "STRING":
                    returnStr = "string";
                    break;
                case "DATETIME":
                    returnStr = "DateTime";
                    break;
                case "DECIMAL":
                case "DOUBLE":
                case "FLOAT":
                case "MONEY":
                    returnStr = "double";
                    break;
                case "UNIQUEIDENTIFIER":
                    returnStr = "Guid";
                    break;
                default:
                    returnStr = "string";
                    break;
            }
            return returnStr;
        }

    }
   
}