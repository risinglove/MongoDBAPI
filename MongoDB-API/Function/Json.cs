//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Script.Serialization;

//namespace API.Controllers
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public class JsonData : ActionResult
//    {
//        /// <summary>
//        /// 消息
//        /// </summary>
//        private string _msg;
//        public string msg
//        {
//            get { return _msg == null ? "" : _msg; }
//            set { _msg = value; }
//        }
//        /// <summary>
//        /// 数据量
//        /// </summary>
//        private int _count;
//        public int count
//        {
//            get { return _count; }
//            set { _count = value; }
//        }

//        /// <summary>
//        /// 状态
//        /// </summary>
//        private string _status;
//        public string status
//        {
//            get { return _status == null ? "" : _status; }
//            set { _status = value; }
//        }

//        /// <summary>
//        /// 服务器时间
//        /// </summary>
//        public string datetime
//        {
//            get { return DateTime.Now.ToString(); }
//        }

//        /// <summary>
//        /// 数据
//        /// </summary>
//        private object _data;
//        public object data
//        {
//            get { return _data == null ? "" : _data; }
//            set { _data = value; }
//        }

//        /// <summary>
//        /// 跳转时的URL
//        /// </summary>
//        private string _url;
//        public string url
//        {
//            get { return _url; }
//            set { _url = value; }
//        }


//        /// <summary>
//        /// 对操作方法结果的处理
//        /// </summary>
//        /// <param name="context"></param>
//        public override void ExecuteResult(ControllerContext context)
//        {
//            if (context == null)
//            {
//                throw new ArgumentNullException("当前请求上下文出错");
//            }
//            var response = context.HttpContext.Response;
//            response.ContentType = "application/json";
//            response.ContentEncoding = System.Text.Encoding.UTF8;
//            if (data != null)
//            {
//                String buffer;
//                buffer = Newtonsoft.Json.JsonConvert.SerializeObject(data);
//                response.Write(buffer); //Newtonsoft.Json.JsonConvert.SerializeObject(Data));
//            }
//        }


//        /// <summary>
//        /// 复写渲染视图方法
//        /// </summary>
//        /// <param name="context"></param>
//        //public override void ExecuteResult(ControllerContext context)
//        //{
//        //    if (context == null)
//        //    {
//        //        throw new ArgumentNullException("当前请求上下文出错");
//        //    }
//        //    //if ((JsonRequestBehavior == JsonRequestBehavior.DenyGet)
//        //    //    && String.Equals(context.HttpContext.Request.HttpMethod, "GET"))
//        //    //{
//        //    //    throw new InvalidOperationException("Jsonp只能是ＧＥＴ请求");
//        //    //}
//        //    var response = context.HttpContext.Response;
//        //    if (!String.IsNullOrEmpty(ContentType))
//        //    {
//        //        response.ContentType = ContentType;
//        //    }
//        //    else
//        //    {
//        //        response.ContentType = "application/json";
//        //    }
//        //    if (ContentEncoding != null)
//        //    {
//        //        response.ContentEncoding = this.ContentEncoding;
//        //    }
//        //    if (Data != null)
//        //    {
//        //        String buffer;
//        //        var request = context.HttpContext.Request;
//        //        var serializer = new JavaScriptSerializer();
//        //        //if (request[CALLBACKNAME] != null)
//        //        //    buffer = String.Format("{0}({1})", request[CALLBACKNAME], serializer.Serialize(Data));
//        //        //else
//        //        buffer = serializer.Serialize(Data);
//        //        response.Write(buffer); //Newtonsoft.Json.JsonConvert.SerializeObject(Data));
//        //    }
//        //}
//    }
//}