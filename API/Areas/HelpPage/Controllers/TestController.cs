using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Areas.HelpPage.Controllers
{
    public class TestController : Controller
    {
        BLL.LoginBLL bll = new BLL.LoginBLL();

        [HttpGet]
        public ActionResult AddLogin()
        {
            return View();
        }

        //public ActionResult AddLogin(ViewModel.AddLogin model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Model.Login login = new Model.Login() { 
        //         LoginName = model.LoginName,
        //          Password = model.Password,
        //           RegTime = DateTime.Now.ToString(),
        //            Status = 1
        //        };
        //        if (bll.Add(login))
        //        {
        //            ModelState.AddModelError("", "添加成功！");
        //            return View();
        //        }
        //        else {
        //            ModelState.AddModelError("", "添加失败！");
        //            return View(model);
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "提供的用户名或密码不正确。");
        //        return View(model);
        //    }
        //}

    }
}
