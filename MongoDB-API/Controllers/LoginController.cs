using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using static System.String;
using ViewModel;
using BLL;

namespace API.Controllers
{
    public class LoginController : ApiController
    {
        private static BLL.LoginBLL _bll;
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
        [HttpGet]
        public List<object> GetAllLoginList()
        {
            List<object> list = bll.All();
            return list;
        }
        #endregion

        #region 根据ID删除
        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        public string PostDel(string id)
        {
            try
            {
                bll.Delete(ObjectId.Parse(id));
                return "ok";
            }
            catch (Exception e)
            {
                return "no";
                throw;
            }
        }
        #endregion

        #region 用户注册
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model">模型</param>
        /// <param name="returnUrl">跳转地址</param>
        /// <returns>结果</returns>
        [HttpPost]
        public BaseResult PostRegister([FromBody]Register model)
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
                LoginName = model.LoginName,
                Password = model.Password,
                RegTime = DateTime.Now.ToString(),
                Status = 1
            };
            if (bll.Add(login))
            {
                return new BaseResult() { status = "ok", msg = "注册成功" };
            }
            //}
            //else
            //{
            //    return new JsonData() { status = "no", msg = "验证码错误" };
            //}
            return new BaseResult() { status = "no", msg = "注册失败" };
        }
        #endregion

        #region 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">数据模型</param>
        /// <param name="returnUrl">跳转地址</param>
        /// <returns>结果</returns>
        public BaseResult Login(ViewModel.SignIn model, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "";
            }
            BaseResult json = new BaseResult();
            string code = "";  //HttpContext.Current.Session["VerifyCode"].ToString();
            if (string.IsNullOrEmpty(code))
            {
                return new BaseResult() { status = "no", msg = "验证码错误" };
            }
            if (model.VerifyCode != null && code == model.VerifyCode)
            {
                List<Login> list = null;// GetAllLoginList();
                list = list.Where(l => l.Status == 1 && l.LoginName == model.Password && l.LoginName == model.LoginName).ToList();
                if (list != null)
                {
                    if (model.RememberMe)
                    {
                        ////记住密码
                        //HttpCookie hc = new HttpCookie("LoginName", model.LoginName);
                        //HttpContext.Current.Request.Cookies.Add(hc);
                    }
                    return new BaseResult() { status = "ok", msg = "登录成功" };
                }
                else
                {
                    return new BaseResult() { status = "no", msg = "用户名或密码错误" };
                }
            }
            else
            {
                return new BaseResult() { status = "no", msg = "验证码错误" };
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
        public BaseResult ModifyPassword(ViewModel.ModifyPaw model)
        {
            Login login = bll.SelectOne(new ObjectId(model.Id));
            login.Password = model.NewPassword;
            if (bll.Update(login))
            {
                return new BaseResult() { status = "ok", msg = "修改成功" };
            }
            return new BaseResult() { status = "no", msg = "修改失败" };
        }
        #endregion

        [HttpPost]
        public void Add()
        {
            DataBaseBLL dbBll = new DataBaseBLL();
            dbBll.GenerateUserDLL("57e3932ea95dd03f4432d232");

            //try
            //{
            //    List<Column> list = new List<Column>();
            //    list.Add(new Column() { name = "Name", type = "string" });
            //    list.Add(new Column() { name = "Number ", type = "int" });
            //    list.Add(new Column() { name = "Bool", type = "bool" });
            //    list.Add(new Column() { name = "Price", type = "decimal" });
            //    list.Add(new Column() { name = "Date", type = "date" });
            //    DataBaseModel model = new DataBaseModel() { TableName = "Classes", UserID = "57e3932ea95dd03f4432d232", list = list };

            //    bll.AddTest(model);
            //}
            //catch (Exception e)
            //{
            //    throw;
            //}

        }


    }
    

}
