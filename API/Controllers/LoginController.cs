using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;
using System.ComponentModel;
using Model;

namespace API.Controllers
{
    /// <summary>
    /// 用户登录相关的信息
    /// </summary>
    public class LoginController : ApiController
    {

        /// <summary>
        /// 获取Login的全部数据
        /// </summary>
        /// <returns>返回Json格式的数据</returns>
        public List<Login> GetAllLoginList()
        {
            BLL.LoginBLL bll = new BLL.LoginBLL();
            List<Login> list = bll.GetAll();
            return list;
        }

    }
}
