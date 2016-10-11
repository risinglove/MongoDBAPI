using BLL;
using MongnDB_WEB.Function;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Utility;
using ViewModel;

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
            var list = databaseBLL.Select(CurrentUser.uId);
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
            //currencyBLL = new BLL.CurrencyBLL(tableName);
            //var list = currencyBLL.GetALL();
            return Json("");
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
                        model = ModelHelper.GetModel(className: tableName, UserId: CurrentUser.uId.ToString(), path: Request.MapPath("/MyDLL"));
                        model = TypeSafe.SafeObject(model, json);

                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                }
                //currencyBLL = new BLL.CurrencyBLL(tableName);
                //if (currencyBLL.Add(model))
                //{
                //    return Json("ok");
                //}
                //else
                //{
                //    return Json("no");
                //}
            }
            catch (Exception e)
            {
            }
            return Json("no");
        }
        #endregion


        #region 添加表
        /// <summary>
        /// 添加表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public JsonResult AddTable(string tableName)
        {
            //DataBaseBLL dbBll = new DataBaseBLL();
            //DataBaseModel model = new DataBaseModel() { TableName = tableName, UserID = CurrentUser.Id };
            //if (dbBll.Add(model))
            //{
            //    dbBll.GenerateUserDLL(CurrentUser.Id, Request.MapPath("/MyDLL"));
            //    return Json("ok");
            //}
            //else { return Json("no"); }
            return null;
        }
        #endregion

        #region 添加列
        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult AddColumn(string tableId, string name, string type)
        {
            //DataBaseBLL dbBll = new DataBaseBLL();
            //DataBaseModel model = dbBll.SelectOne(tableId);
            //if (model.list == null)
            //{
            //    model.list = new List<Column>() { new Column() { name = name, type = type } };
            //}
            //else
            //{
            //    model.list.Add(new Column() { name = name, type = type });
            //}
            //if (dbBll.Update(model))
            //{
            //    dbBll.GenerateUserDLL(CurrentUser.Id, Request.MapPath("/MyDLL"));
            //    return Json("ok");
            //}
            //else { return Json("no"); }
            return null;
        }
        #endregion

    }
}