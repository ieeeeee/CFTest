using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OA.Web.Models
{
    //通用Json 结果对象
    public class JsonResultModel<T>
    {
        //是否操作成功
        public bool flag { get; set; }

        //返回的数据
        public T data { get; set; }

        //消息
        public string msg { get; set; }


        public JsonResultModel()
        {
            flag = false;
            msg = string.Empty;
            data = default(T);
        }

        //ctor with params
        public JsonResultModel(bool flagResult,string message, T returnData)
        {
            flag = flagResult;
            msg = message;
            data = returnData;
        }

    }
}