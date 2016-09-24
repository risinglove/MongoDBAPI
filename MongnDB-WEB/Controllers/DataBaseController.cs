using MongnDB_WEB.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;

namespace MongnDB_WEB.Controllers
{
    public class DataBaseController : ControllerBaseAbs
    {



        // GET: DataBase
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

       

    }
}