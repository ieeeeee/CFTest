using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OA.Web
{
    /// <summary>
    /// upload 的摘要说明
    /// </summary>
    public class upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            var files = context.Request.Files;
            if (files.Count <= 0)
            {
                return;
            }

            HttpPostedFile file = files[0];
            var result=  "{\"errno\":500,\"data\":[]}";
            if (file == null)
            {
                context.Response.Write(result);
                return;
            }
            else
            {
                string path = context.Server.MapPath("~/uploadedFiles/");  //存储图片的文件夹
                string originalFileName = file.FileName;
                string fileExtension = originalFileName.Substring(originalFileName.LastIndexOf('.'), originalFileName.Length - originalFileName.LastIndexOf('.'));
                string currentFileName = (new Random()).Next() + fileExtension;  //文件名中不要带中文，否则会出错
                                                                                 //生成文件路径
                string imagePath = path + currentFileName;
                //保存文件
                file.SaveAs(imagePath);

                //获取图片url地址
                string imgUrl = "http://localhost:55010/uploadedFiles/" + currentFileName;
                result = "{\"errno\":0,\"data\":[\""+ imgUrl + "\"]}";
                //返回图片url地址
                context.Response.Write(result);
                return;
            }
        }

       


         public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}