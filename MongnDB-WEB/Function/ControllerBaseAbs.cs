﻿using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongnDB_WEB.Function
{
    public class ControllerBaseAbs : Controller
    {
        public UsersTable userBll
        {
            get
            {
                return new UsersTable();
            }
        }


        public DataBaseTables databaseBLL
        {
            get { return new DataBaseTables(); }
        }
        


        private Model.UsersTable currentUser;
        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        public Model.UsersTable CurrentUser { get { return currentUser; } }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session[Globals.SESSIONKEY_USER] == null)
            {
                string name = HttpContext.User.Identity.Name;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    currentUser = userBll.Login(name);
                    Session[Globals.SESSIONKEY_USER] = currentUser;
                }
            }
            else
            {
                currentUser = Session[Globals.SESSIONKEY_USER] as Model.UsersTable;
            }
            ViewBag.Name = currentUser != null ? (string.IsNullOrWhiteSpace(currentUser.NickName) ? currentUser.UserName : currentUser.NickName) : "";
            ViewBag.IsLogin = currentUser != null;
        }

        //protected virtual bool InitializeComponent(ActionExecutingContext filterContext)
        //{
        //    if (Session[Globals.SESSIONKEY_USER] == null)
        //    {
        //        string name = HttpContext.User.Identity.Name;
        //        currentUser = userBll.Login(name);
        //        Session[Globals.SESSIONKEY_USER] = currentUser;
        //    }
        //    else
        //    {
        //        currentUser = Session[Globals.SESSIONKEY_USER] as Model.User;
        //    }
        //    ViewBag.IsLogin = currentUser != null;
        //    return true;
        //}

    }
}