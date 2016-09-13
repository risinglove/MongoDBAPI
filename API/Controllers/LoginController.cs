using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Model;
using MongoDB.Bson;

namespace API.Controllers
{
    /// <summary>
    /// 用户登录相关的信息
    /// </summary>
    public class LoginController : ApiController
    {
        private BLL.LoginBLL _bll;
        //private BLL.UserBLL _userBLL;
        /// <summary>
        /// 
        /// </summary>
        public LoginController()
        {
            _bll = new BLL.LoginBLL();
            //_userBLL = new BLL.UserBLL();
        }
        private BLL.LoginBLL bll
        {
            get
            {
                if (_bll == null)
                {
                    _bll = new BLL.LoginBLL();
                }
                return _bll;
            }
        }

        #region 获取Login的全部数据
        /// <summary>
        /// 获取Login的全部数据
        /// </summary>
        /// <returns>返回Json格式的数据</returns>
        public List<Login> GetAllLoginList()
        {
            List<Login> list = bll.GetAll();
            return list;
        }
        #endregion

        #region 用户注册
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="returnUrl">跳转地址</param>
        /// <returns>结果</returns>
        [System.Web.Mvc.HttpPost]
        public JsonData Register(string LoginName,string Password)//ViewModel.Register model, string returnUrl)
        {
            //if (string.IsNullOrEmpty(returnUrl))
            //{
            //    returnUrl = "";
            //}
            //string code = HttpContext.Current.Session["VerifyCode"].ToString();
            //if (string.IsNullOrEmpty(code))
            //{
            //    return new JsonData() { status = "no", msg = "验证码错误" };
            //}
            //if (model.VerifyCode != null && code == model.VerifyCode)
            //{
            Model.Login login = new Login()
            {
                LoginName = LoginName,
                Password = Password,
                RegTime = DateTime.Now.ToString(),
                Status = 1
            };
            if (bll.Add(login))
            {
                return new JsonData() { status = "ok", msg = "注册成功", url = "" };
            }
            //}
            //else
            //{
            //    return new JsonData() { status = "no", msg = "验证码错误" };
            //}
            return new JsonData() { status = "no", msg = "注册失败" };
        }
        #endregion

        #region 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">数据模型</param>
        /// <param name="returnUrl">跳转地址</param>
        /// <returns>结果</returns>
        public JsonData Login(ViewModel.SignIn model, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "";
            }
            JsonData json = new JsonData();
            string code = HttpContext.Current.Session["VerifyCode"].ToString();
            if (string.IsNullOrEmpty(code))
            {
                return new JsonData() { status = "no", msg = "验证码错误" };
            }
            if (model.VerifyCode != null && code == model.VerifyCode)
            {
                List<Login> list = GetAllLoginList();
                list = list.Where(l => l.Status == 1 && l.LoginName == model.Password && l.LoginName == model.LoginName).ToList();
                if (list != null)
                {
                    if (model.RememberMe)
                    {
                        //记住密码
                        HttpCookie hc = new HttpCookie("LoginName", model.LoginName);
                        HttpContext.Current.Request.Cookies.Add(hc);
                    }
                    return new JsonData() { status = "ok", msg = "登录成功", url = returnUrl };
                }
                else
                {
                    return new JsonData() { status = "no", msg = "用户名或密码错误" };
                }
            }
            else
            {
                return new JsonData() { status = "no", msg = "验证码错误" };
            }
        }
        #endregion

        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model">数据模型</param>
        /// <param name="returnUrl">跳转地址</param>
        /// <returns></returns>
        public JsonData ModifyPassword(ViewModel.ModifyPaw model, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "";
            }
            Login login = bll.SelectOne(new ObjectId(model.Id));
            login.Password = model.NewPassword;
            if (bll.Update(login))
            {
                return new JsonData() { status = "ok", msg = "修改成功", url = returnUrl };
            }
            return new JsonData() { status = "no", msg = "修改失败" };
        }
        #endregion

    }
}
