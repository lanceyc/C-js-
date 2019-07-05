using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1
{
    public static class ControllerExtension
    {

        public static JsonpResult Jsonp(this Controller controller, object data)
        {
            JsonpResult jsonpResult = new JsonpResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            return jsonpResult;
        }
    }
}