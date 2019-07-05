using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1
{
    public class JsonpResult : JsonResult
    {
        public static readonly string JsonpCallbackName = "callback";

        public static readonly string CallbackApplicationType = "application/json";


        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new AccessViolationException("error : context is null");
            }

            //如果不允许来自客户端的Get请求,并且请求类型是Get
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)) 
            {
                throw new AccessViolationException();
          }

            var response = context.HttpContext.Response;

            //这个ContentType是获取或设置内容的类型。它是JsonResult类中的一个属性
            if (!string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = this.ContentType;  //设置响应内容的类型
            }
            else
            {
                response.ContentType = CallbackApplicationType; //设置响应内容的类型
            }

            if (ContentEncoding!=null )
            {
                response.ContentEncoding = this.ContentEncoding;    //设置响应内容的编码
            }

            if (Data!=null)
            {
                string buffer = string.Empty;
                var request = context.HttpContext.Request;

                //获取回调函数名称（即：获取这个 < script src = "http://localhost:58382/home/IndexTest?callback=showMessage" type = "text/javascript" ></script>
                if (request[JsonpCallbackName]!=null)
                {
                    buffer = string.Format("{0}({1})", request[JsonpCallbackName], JsonConvert.SerializeObject(Data));
                }
                else
                {
                    buffer = JsonConvert.SerializeObject(Data);
                }
                response.Write(buffer);

            }
        }


    }
}