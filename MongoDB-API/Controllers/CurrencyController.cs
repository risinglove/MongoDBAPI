using BLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Utility;

namespace API.Controllers
{
    public class CurrencyController : ApiController
    {

        public async Task<HttpResponseMessage> Insurance([FromBody] dynamic dyn)
        {
            var ctx = HttpContext.Current;

            List<string> list = new List<string>() { "name", "age", "sex" };
            
            ModelHelper.CreateClass("Student", list, HttpContext.Current.Server.MapPath("~/MyDLL/"));


            string _json = dyn.ToString();
            return await Task.Run(() =>
            {
                HttpContext.Current = ctx;
                return new HttpResponseMessage
                {
                    Content =
                        new StringContent(new Insurace(_json).Ins(), Encoding.UTF8, "application/json")
                };
            });
        }


    }
}
