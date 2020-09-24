using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;            
using System.Threading;
using System.Drawing;

namespace  Utility
{
    public class MyRequest
    {
        #region 判断请求类型（post 或 get）
        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }
        #endregion

        #region 返回上一个页面的地址
        /// <summary>
        /// 返回上一个页面的地址
        /// </summary>
        /// <returns>上一个页面的地址</returns>
        public static string GetUrlReferrer()
        {
            var UrlReferrer = HttpContext.Current.Request.UrlReferrer;
            return UrlReferrer == null ? "" : UrlReferrer.ToString();
        }
        #endregion

        #region 获取页面请求值
        /// <summary>
        /// 获取页面请求的值（字符串）
        /// </summary>
        /// <param name="strName">参数名称</param>
        /// <returns></returns>
        public static string GetString(string strName)
        {
            var QueryString = HttpContext.Current.Request.QueryString[strName];
            if (string.IsNullOrEmpty(QueryString))
            {
                var FormString = HttpContext.Current.Request.Form[strName];
                return string.IsNullOrEmpty(FormString) ? "" : FormString;
            }
            return QueryString;
        }

        /// <summary>
        /// 获取页面请求的值（整数）
        /// </summary>
        /// <param name="strName">参数名称</param>
        /// <returns></returns>
        public static int GetInt(string strName)
        {
            var strValue = GetString(strName);
            return string.IsNullOrEmpty(strValue) ? 0 : Convert.ToInt32(strValue);
        }
        #endregion
          
    }
}