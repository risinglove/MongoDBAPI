using MongnDB_WEB.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace MongnDB_WEB.Controllers
{
    public class ServiceController : ControllerBaseAbs
    {

        #region 获取DataBase数据列表
        /// <summary>
        /// 获取DataBase数据列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetDataBaseList()
        {
            var list = databaseBLL.Select(CurrentUser.Id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取表名对应的数据
        /// <summary>
        /// 获取表名对应的数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public JsonResult GetTableData(string tableName)
        {
            currencyBLL = new BLL.CurrencyBLL(tableName);
            var list = currencyBLL.GetALL();
            return Json(list);
        }
        #endregion

        #region 向指定表中添加数据
        /// <summary>
        /// 向指定表中添加数据
        /// </summary>
        /// <param name="json"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public JsonResult AddTableRowData(Dictionary<string, object> json, string tableName)
        {
            try
            {
                object model = null;
                if (json != null && json.Count > 0)
                {
                    try
                    {
                        model = ModelHelper.GetModel(className: tableName, UserId: CurrentUser.Id, path: Request.MapPath("/MyDLL"));
                        model = TypeSafe.SafeObject(model, json);

                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                }
                currencyBLL = new BLL.CurrencyBLL(tableName);
                if (currencyBLL.Add(model))
                {
                    return Json("ok");
                }
                else
                {
                    return Json("no");
                }
            }
            catch (Exception e)
            {
            }
            return Json("no");
        }
        #endregion

    }
}