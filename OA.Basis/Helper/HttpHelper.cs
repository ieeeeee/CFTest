
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace OA.Basis.Helper
{
    /// <summary>
    /// Http连接操作帮助类
    /// </summary>
    public class HttpHelper
    {
        #region 预定义方法或者变更
        //默认的编码
        private Encoding encoding = Encoding.Default;

        //HttpWebRequest对象用来发起请求
        private HttpWebRequest request = null;

        //HttpWebResponse 获取影响流的数据对象
        private HttpWebResponse response = null;

        private string GetHttpRequestData(HttpItem objHttpItem)
        {
            try
            {
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    MemoryStream _stream = new MemoryStream();
                    if(response.Cookies!=null)
                    {
                        objHttpItem.CookieCollection = response.Cookies;
                    }
                    if (response.Headers["set-cookie"] != null)
                    {
                        objHttpItem.Cookie = response.Headers["set-cookie"];
                    }
                    if(response.ContentEncoding!=null&&response.ContentEncoding.Equals("gzip",StringComparison.InvariantCultureIgnoreCase))
                    {
                        //开始读取流并设置编码方式
                        _stream = GetMemoryStream(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress));
                    }
                    else
                    {
                        _stream = GetMemoryStream(response.GetResponseStream());
                    }
                    //获取Byte
                    byte[] RawResponse = _stream.ToArray();
                    //是否返回Byte类型数据
                    if(objHttpItem.ResultType==ResultType.Byte)
                    {
                        objHttpItem.ResultByte = RawResponse;
                    }
                    //从这里开始我们要无视编码了
                    if(encoding==null)
                    {
                        string temp = Encoding.Default.GetString(RawResponse, 0, RawResponse.Length);
                        Match meta = Regex.Match(temp, "<meta([^<]*)charset=([^<]*)[\"']", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string charter = (meta.Groups.Count > 2) ? meta.Groups[2].Value : string.Empty;
                        charter = charter.Replace("\"", string.Empty).Replace("'", string.Empty).Replace(";", string.Empty);
                        if(charter.Length>0)
                        {
                            charter = charter.ToLower().Replace("iso-8859-1", "gbk");
                            encoding = Encoding.GetEncoding(charter);
                        }
                        else
                        {
                            if(response.CharacterSet.ToLower().Trim()=="iso-8859-1")
                            {
                                encoding = Encoding.GetEncoding("gbk");
                            }
                            else
                            {
                                if(string.IsNullOrEmpty(response.CharacterSet.Trim()))
                                {
                                    encoding = Encoding.UTF8;
                                }
                                else
                                {
                                    encoding = Encoding.GetEncoding(response.CharacterSet);
                                }
                            }
                        }
                    }
                    //得到返回的Html
                    objHttpItem.Html = encoding.GetString(RawResponse);
                    //最后释放流
                    _stream.Close();
                }
            }
            catch(WebException ex)
            {
                objHttpItem.Html = "String Error";
                response = (HttpWebResponse)ex.Response;
            }
            if(objHttpItem.IsToLower)
            {
                objHttpItem.Html = objHttpItem.Html.ToLower();
            }
            return objHttpItem.Html;
        }
        /// <summary>
        /// 4.0以下版本取数据使用
        /// </summary>
        /// <param name="streamResponse"></param>
        /// <returns></returns>
        private static MemoryStream GetMemoryStream(Stream streamResponse)
        {
            MemoryStream _stream = new MemoryStream();
            int Length = 256;
            Byte[] buffer = new byte[Length];
            int bytesRead = streamResponse.Read(buffer, 0, Length);
            while(bytesRead>0)
            {
                _stream.Write(buffer, 0, bytesRead);
                bytesRead = streamResponse.Read(buffer, 0, Length);
            }
            return _stream;
        }
        /// <summary>
        /// 为请求准备参数
        /// </summary>
        /// <param name="objHttpItem"></param>
        private void SetRequest(HttpItem objHttpItem)
        {
            //验证证书
            SetCer(objHttpItem);
            //设置代理
            SetProxy(objHttpItem);
            //请求方式Get或者Post
            request.Method = objHttpItem.Method;
            request.Timeout = objHttpItem.Timeout;
            request.ReadWriteTimeout = objHttpItem.ReadWriteTimeout;
            //Accept
            request.Accept = objHttpItem.Accept;
            //ContentType返回类型
            request.ContentType = objHttpItem.ContentType;
            //UserAgent客户端的访问类型，包括浏览器版本和操作系统信息
            request.UserAgent = objHttpItem.UserAgent;
            //编码
            SetEncoding(objHttpItem);
            //设置Cookie
            SetCookie(objHttpItem);
            //来源地址
            request.Referer = objHttpItem.Referer;
            //是否执行跳转功能
            request.AllowAutoRedirect = objHttpItem.Allowautoredirect;
            //设置Post数据
            SetPostData(objHttpItem);
            //设置最大连接
            if(objHttpItem.ConnectionLimit>0)
            {
                request.ServicePoint.ConnectionLimit = objHttpItem.ConnectionLimit;
            }
        }
        /// <summary>
        /// 验证证书
        /// </summary>
        /// <param name="objHttpItem"></param>
        private void SetCer(HttpItem objHttpItem)
        {
            if(!string.IsNullOrEmpty(objHttpItem.CerPath))
            {
                //这一句一定要写在创建连接的前面。使用回调的方法进行证书验证。
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
                //初始化对象，并设置请求的URL地址
                request = (HttpWebRequest)WebRequest.Create(GetUrl(objHttpItem.URL));
                //创建证书文件
                X509Certificate objx509 = new X509Certificate(objHttpItem.CerPath);
                //添加到请求里
                request.ClientCertificates.Add(objx509);
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(GetUrl(objHttpItem.URL));
            }
        }
        /// <summary>
        /// 设置编码
        /// </summary>
        /// <param name="objHttpItem"></param>
        private void SetEncoding(HttpItem objHttpItem)
        {
            if(string.IsNullOrEmpty(objHttpItem.Encoding)||objHttpItem.Encoding.ToLower().Trim()=="null")
            {
                //读取数据时的编码方式
                encoding = null;
            }
            else
            {
                //读取数据时的编码方式
                encoding = System.Text.Encoding.GetEncoding(objHttpItem.Encoding);
            }
        }
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="objHttpItem"></param>
        private void SetCookie(HttpItem objHttpItem)
        {
            if(!string.IsNullOrEmpty(objHttpItem.Cookie))
            {
                request.Headers[HttpRequestHeader.Cookie] = objHttpItem.Cookie;
            }
            //设置cookie
            if(objHttpItem.CookieCollection!=null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(objHttpItem.CookieCollection);
            }
        }
        /// <summary>
        /// 设置Post数据
        /// </summary>
        /// <param name="objHttpItem"></param>
        private void SetPostData(HttpItem objHttpItem)
        {
            if(!string.IsNullOrEmpty(objHttpItem.Postdata)&&request.Method.Trim().ToLower().Contains("post"))
            {
                byte[] buffer = Encoding.Default.GetBytes(objHttpItem.Postdata);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
            }
        }
        /// <summary>
        /// 设置代理
        /// </summary>
        /// <param name="objHttpItem"></param>
        private void SetProxy(HttpItem objHttpItem)
        {
            if(string.IsNullOrEmpty(objHttpItem.ProxyUserName)&&string.IsNullOrEmpty(objHttpItem.ProxyPwd)&&string.IsNullOrEmpty(objHttpItem.ProxyIP))
            {
                //不需要设置
            }
            else
            {
                //设置代理服务器
                WebProxy myProxy = new WebProxy(objHttpItem.ProxyIP, false);
                //建立连接
                myProxy.Credentials = new NetworkCredential(objHttpItem.ProxyUserName, objHttpItem.ProxyPwd);
                //给当前请求对象
                request.Proxy = myProxy;
                //设置安全凭证
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
            }
        }
        /// <summary>
        /// 回调验证证书问题
        /// </summary>
        /// <param name="sender">流对象</param>
        /// <param name="certificate">证书</param>
        /// <param name="chain">X509Chain</param>
        /// <param name="errors">SslPolicyErrors</param>
        /// <returns></returns>
        public bool CheckValidationResult(object sender,X509Certificate certificate,X509Chain chain,SslPolicyErrors errors)
        {
            //总是接受
            return true;
        }
        /// <summary>
        /// 传入一个正确或不正确的URL,返回正确的URL
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static string GetUrl(string URL)
        {
            if(!(URL.Contains("http://")||URL.Contains("https://")))
            {
                URL = "http://" + URL;
            }
            return URL;
        }

        #endregion
    }

    /// <summary>
    /// Http 请求参考类
    /// </summary>
    public class HttpItem
    {
        string _URL;
        /// <summary>
        /// 请求URL必须填写
        /// </summary>
        public string URL
        {
            get { return _URL; }
            set { _URL = value; }
        }

        string _Method = "GET";
        /// <summary>
        /// 请求方式默认为Get方式
        /// </summary>
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }
        int _Timeout = 100000;
        /// <summary>
        /// 默认请求超时时间
        /// </summary>
        public int Timeout
        {
            get { return _Timeout; }
            set { _Timeout = value; }
        }
        int _ReadWriteTimeout = 30000;
        /// <summary>
        /// 默认写入Post数据超时间
        /// </summary>
        public int ReadWriteTimeout
        {
            get { return _ReadWriteTimeout; }
            set { _ReadWriteTimeout = value; }
        }

        string _Accept = "text/html,application/xhtml+xml,*/*";
        /// <summary>
        /// 请求标头值 默认为text/html,application/xhtml+xml,*/*
        /// </summary>
        public string Accept
        {
            get { return _Accept; }
            set { _Accept = value; }
        }
        string _ContentType = "text/html";
        /// <summary>
        /// 请求返回类型默认 text/html
        /// </summary>
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }

        string _UserAgent = "Mozilla/5.0(compatible:MSIE 9.0;Windows NT 6.1;Trident/5.0)";
        /// <summary>
        ///  客户端访问信息默认Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)
        /// </summary>
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }
        string _Encoding = string.Empty;
        /// <summary>
        /// 返回数据编码默认为NUll,可以自动识别
        /// </summary>
        public string Encoding
        {
            get { return _Encoding; }
            set { _Encoding = value; }
        }

        string _Postdata;
        /// <summary>
        /// Post请求时要发送的Post数据
        /// </summary>
        public string Postdata
        {
            get { return _Postdata; }
            set { _Postdata = value; }
        }
        string _Cookie = string.Empty;
        /// <summary>
        /// 请求时的Cookie
        /// </summary>
        public string Cookie
        {
            get { return _Cookie; }
            set { _Cookie = value; }
        }
        string _Referer = string.Empty;
        /// <summary>
        /// 来源地址，上次访问地址
        /// </summary>
        public string Referer
        {
            get { return _Referer; }
            set { _Referer = value; }
        }
        string _CerPath = string.Empty;
        /// <summary>
        /// 证书绝对路径
        /// </summary>
        public string CerPath
        {
            get { return _CerPath; }
            set { _CerPath = value; }
        }
        CookieCollection cookieCollection = null;
        /// <summary>
        /// Cookie对象集合
        /// </summary>
        public CookieCollection CookieCollection
        {
            get { return cookieCollection; }
            set { CookieCollection = value; }
        }
        private Boolean isToLower = true;
        /// <summary>
        /// 是否设置为全文小写
        /// </summary>
        public Boolean IsToLower
        {
            get { return isToLower; }
            set { isToLower = value; }
        }
        private Boolean allowautoredirect = true;
        /// <summary>
        /// 支持跳转页面，查询结果将时跳转后的页面
        /// </summary>
        public Boolean Allowautoredirect
        {
            get { return allowautoredirect; }
            set { allowautoredirect = value; }
        }
        private int connectionlimit = 1024;
        /// <summary>
        /// 最大连接数
        /// </summary>
        public int ConnectionLimit
        {
            get { return connectionlimit; }
            set { connectionlimit = value; }
        }
        private string proxyusername = string.Empty;
        /// <summary>
        /// 代理服务器用户名
        /// </summary>
        public string ProxyUserName
        {
            get { return proxyusername; }
            set { proxyusername = value; }
        }
        private string proxypwd = string.Empty;
        /// <summary>
        /// 代理服务器密码
        /// </summary>
        public string ProxyPwd
        {
            get { return proxypwd; }
            set { proxypwd = value; }
        }
        private string proxyip = string.Empty;
        /// <summary>
        /// 代理服务IP
        /// </summary>
        public string ProxyIP
        {
            get { return proxyip; }
            set { proxyip = value; }
        }
        private ResultType resultType = ResultType.String;
        /// <summary>
        /// 设置返回类型String和Byte
        /// </summary>
        public ResultType ResultType
        {
            get { return resultType; }
            set { resultType = value; }
        }
        private string html = string.Empty;
        /// <summary>
        /// 返回的String类型数据
        /// </summary>
        public string Html
        {
            get { return html; }
            set { html = value; }
        }
        private byte[] resultByte = null;
        /// <summary>
        /// 返回的Byte数组
        /// </summary>
        public byte[] ResultByte
        {
            get { return resultByte; }
            set { resultByte = value; }
        }

    }
    public enum ResultType
    {
        /// <summary>
        /// 只返回字符串
        /// </summary>
        String,
        /// <summary>
        /// 返回字符串和字节流
        /// </summary>
        Byte
    }

}