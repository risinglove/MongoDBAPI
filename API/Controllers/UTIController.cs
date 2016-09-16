using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace API.Controllers
{
    public class UTIController : Controller
    {
        //
        // GET: /UTI/
        public FileResult Verfy()
        {
            string code;
            byte[] by = Helper.CreateValidateGraphic(out code, 4, 100, 30, 20);
            return File(by, "image/Jpeg");
        }

    }   
}
