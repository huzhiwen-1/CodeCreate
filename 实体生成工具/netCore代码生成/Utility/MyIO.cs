using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Web;
using  Utility.NVelocity;

namespace  Utility
{
    public class MyIO
    {
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>创建是否成功</returns>
        [DllImport("dbgHelp", SetLastError = true)]
        private static extern bool MakeSureDirectoryPathExists(string name);
        public static bool CreateDir(string path)
        {
            //return Utils.MakeSureDirectoryPathExists(name);
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取临时目录
        /// </summary>
        /// <returns></returns>
        public static string GetTempDir()
        {
            string tempDir = HttpContext.Current.Server.MapPath("/TempFiles/");

            if (CreateDir(tempDir))
            {
                return tempDir;
            }
            else
            {
                throw new Exception("临时目录创建失败");
            }
        }

        /// <summary>
        /// 判断文件是否只读
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsReadOnly(string filePath)
        {
            FileInfo fInfo = new FileInfo(filePath);
            return fInfo.IsReadOnly;
        }

        /// <summary>
        /// 生成文件
        /// </summary>
        /// <param name="context">文件内容</param>
        /// <param name="path">绝对路径</param>
        /// <returns></returns>
        public static bool CreateFile(string context, string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
                    {
                        sw.Write(context);
                        sw.Close();
                        fs.Close();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DeleteFile(string path)
        {
            try
            {
                if (Directory.Exists(path))
                    Directory.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("bin",""), strPath.Replace("~/","").Replace("/","\\"));
            }
        }
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="strPath">原始文件路径</param>
        /// <param name="strNewPath">拷贝的路径</param>
        /// <returns></returns>
        public static bool CopyFile(string strPath, string strNewPath)
        {
            try
            {   
                if (string.IsNullOrEmpty(strPath) || string.IsNullOrEmpty(strNewPath))
                {
                    return true;
                }
               strPath= GetMapPath(strPath );
               strNewPath = GetMapPath(strNewPath);
                if (File.Exists(strNewPath))
                {
                       return false ;
                }
                if (File.Exists(strPath))
                {
                    //using (FileStream fs = File.Create(strPath)) {                      
                    //}
                    File.Delete(strNewPath);
                    File.Copy(strPath, strNewPath);
                }
                return true;
            }

            catch  
            {
                return false;
            }
            
        }

    }
}
