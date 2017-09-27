using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace JayZeeTool
{
    public static class CommonHelpr
    {
        public const string SUserAgent =
            "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        public const string SContentType =
            "application/x-www-form-urlencoded";
        /// <summary>
        /// Post data到url
        /// </summary>
        /// <param name="data">要post的数据</param>
        /// <param name="url">目标url</param>
        /// <returns>服务器响应</returns>
        public static string PostDataToUrl(byte[] data, string url, string contentType = "")
        {
            #region 创建httpWebRequest对象
            WebRequest webRequest = WebRequest.Create(url);
            HttpWebRequest httpRequest = webRequest as HttpWebRequest;
            if (httpRequest == null)
            {
                throw new ApplicationException(
                    string.Format("Invalid url string: {0}", url)
                    );
            }
            #endregion

            #region 填充httpWebRequest的基本信息

            //增加下面两个属性即可  

            httpRequest.UserAgent = SUserAgent;
            httpRequest.ContentType = contentType == "" ? SContentType : contentType;
            httpRequest.Timeout = 300000;
            httpRequest.Method = "POST";

            // httpRequest.KeepAlive = false;
            // httpRequest.ProtocolVersion = HttpVersion.Version10;

            #endregion


            #region 发送post请求到服务器并读取服务器返回信息
            Stream responseStream;
            try
            {
                #region 填充要post的内容
                httpRequest.ContentLength = data.Length;
                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                #endregion

                responseStream = httpRequest.GetResponse().GetResponseStream();
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    responseStream = ex.Response.GetResponseStream();
                }
                else
                    responseStream = null;
            }
            #endregion

            #region 读取服务器返回信息
            string stringResponse = null;
            if (responseStream != null)
            {
                using (StreamReader responseReader =
                    new StreamReader(responseStream, Encoding.UTF8))
                {
                    stringResponse = responseReader.ReadToEnd();
                }
                responseStream.Close();
            }


            #endregion
            return stringResponse;
        }
    }
}