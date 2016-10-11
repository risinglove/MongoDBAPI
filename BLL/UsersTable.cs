/**  版本信息模板在安装目录下，可自行修改。
* UsersTable.cs
*
* 功 能： N/A
* 类 名： UsersTable
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2016/10/11 16:09:14   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Model;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// UsersTable
    /// </summary>
    public partial class UsersTable
    {
        private readonly DAL.UsersTable dal = new DAL.UsersTable();
        public UsersTable()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int uId)
        {
            return dal.Exists(uId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.UsersTable model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.UsersTable model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int uId)
        {

            return dal.Delete(uId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string uIdlist)
        {
            return dal.DeleteList(uIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.UsersTable GetModel(int uId)
        {

            return dal.GetModel(uId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.UsersTable GetModelByCache(int uId)
        {

            string CacheKey = "UsersTableModel-" + uId;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(uId);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.UsersTable)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.UsersTable> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.UsersTable> DataTableToList(DataTable dt)
        {
            List<Model.UsersTable> modelList = new List<Model.UsersTable>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.UsersTable model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod


        #region  ExtensionMethod

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        public bool Register(Model.UsersTable model)
        {
            return dal.Add(model) > 0;
        }

        /// <summary>
        /// 检验点子邮件是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Check(string name)
        {
            try
            {
                var ds = dal.GetList(" UserName=" + name);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Model.UsersTable Login(string name)
        {
            try
            {
                var list = GetModelList(" UserName=" + name);
                if (list != null && list.Count == 1)
                {
                    return list[0];
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public Model.UsersTable apiLogin(string appID, string appsecret)
        {
            try
            {
                var list = GetModelList(" AppID=" + appID + " AND appsecret=" + appsecret);
                if (list != null && list.Count == 1)
                {
                    return list[0];
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public Model.UsersTable Login(string name, string paw)
        {
            try
            {
                var list = GetModelList(" UserName=" + name + " AND Password=" + paw);
                if (list != null && list.Count == 1)
                {
                    return list[0];
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public Model.UsersTable IdLogin(int Id)
        {
            try
            {
                var model = dal.GetModel(Id);
                return model;
            }
            catch (Exception)
            {
            }
            return null;
        }

        #endregion  ExtensionMethod
    }
}

