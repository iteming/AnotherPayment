using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Scraper_Page
{
    /// <summary>
    /// 下载需要的页面源码
    /// </summary>
    public interface IDownloadPageSource
    {
        /// <summary>
        /// 需要的页面URL
        /// </summary>
        string PageUrl { get; set; }
        /// <summary>
        /// 页面解析的编码名称
        /// </summary>
        string EncodingName { get; set; }
        /// <summary>
        /// 给出指定页面源码
        /// </summary>
        /// <returns>页面源码</returns>
        string GetPageSource();
    }

    public abstract class DownloadPageSource : IDownloadPageSource
    {
        Uri _PageUri;
        string _PageUrl = "";
        string _Referer = "";

        public string Referer
        {
            get
            {
                return _Referer;
            }
            set
            {
                _Referer = value;
            }
        }

        public string PageUrl
        {
            get
            {
                return _PageUrl;
            }
            set
            {
                _PageUrl = value;
                _PageUri = new Uri(_PageUrl);
            }
        }

        string _EncodingName = "GB2312";
        public string EncodingName
        {
            get
            {
                return _EncodingName;
            }
            set
            {
                _EncodingName = value;
            }
        }

        public string GetPageSource()
        {
            WebResponse response = null;
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_PageUri);

                if (!string.IsNullOrEmpty(_Referer))
                {
                    //request.Headers.Add(HttpRequestHeader.Referer, _Referer);
                    request.Referer = _Referer; 
                }

                response = GetResponse(request);

                stream = response.GetResponseStream();

                if (!response.ContentType.ToLower().StartsWith("text/"))
                {
                    return null;
                }

                string buffer = "", line;

                reader = new StreamReader(stream, System.Text.Encoding.GetEncoding(_EncodingName));

                while ((line = reader.ReadLine()) != null)
                {
                    buffer += line + "\r\n";
                }

                return buffer;
            }
            catch (WebException e)
            {

                return "web 下载失败，错误：" + e;
            }
            catch (IOException e)
            {
                return "IO 下载失败，错误：" + e;
            }
            finally
            {
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (response != null) response.Close();
            }
        }

        /// <summary>
        /// 不同的请求方法
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        protected abstract WebResponse GetResponse(WebRequest Request);
    }

    public class PageSource_GetMethod : DownloadPageSource
    {
        public PageSource_GetMethod(String Url, String strReferer)
        {
            PageUrl = Url;
            Referer = strReferer;
        }
        protected override WebResponse GetResponse(WebRequest Request)
        {
            return Request.GetResponse();
        }
    }
    public class PageSource_PostMethod : DownloadPageSource
    {
        String _Postdata;
        string _PostEncodingName = "UTF-8"; 
        String _Referer;
        
        public PageSource_PostMethod(String Url, String Postdata,String strReferer)
        {
            PageUrl = Url;
            _Postdata = Postdata;
            _Referer = strReferer;
        }
        public PageSource_PostMethod(String Url, String Postdata, String strReferer, String PostEncodingName)
            : this(Url, Postdata, strReferer)
        {
            _PostEncodingName = PostEncodingName;
        }



        protected override WebResponse GetResponse(WebRequest Request)
        {
            Encoding encoding = Encoding.GetEncoding(_PostEncodingName);
            byte[] data = encoding.GetBytes(_Postdata);
            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";
            Request.ContentLength = data.Length;
            if (!string.IsNullOrEmpty(_Referer))
            {
                Request.Headers.Add(HttpRequestHeader.Referer, _Referer);
            }
            Stream myStream = Request.GetRequestStream();
            myStream.Write(data, 0, data.Length);
            myStream.Close();

            return Request.GetResponse();

        }
    }
}
