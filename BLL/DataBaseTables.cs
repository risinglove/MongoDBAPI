/**  版本信息模板在安装目录下，可自行修改。
* DataBaseTables.cs
*
* 功 能： N/A
* 类 名： DataBaseTables
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2016/10/11 16:09:12   N/A    初版
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
namespace BLL
{
    /// <summary>
    /// DataBaseTables
    /// </summary>
    public partial class DataBaseTables
    {
        private readonly DAL.DataBaseTables dal = new DAL.DataBaseTables();
        public DataBaseTables()
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
        public bool Exists(int dbId)
        {
            return dal.Exists(dbId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.DataBaseTables model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.DataBaseTables model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int dbId)
        {

            return dal.Delete(dbId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string dbIdlist)
        {
            return dal.DeleteList(dbIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.DataBaseTables GetModel(int dbId)
        {

            return dal.GetModel(dbId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Model.DataBaseTables GetModelByCache(int dbId)
        {

            string CacheKey = "DataBaseTablesModel-" + dbId;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(dbId);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Model.DataBaseTables)objModel;
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
        public List<Model.DataBaseTables> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.DataBaseTables> DataTableToList(DataTable dt)
        {
            List<Model.DataBaseTables> modelList = new List<Model.DataBaseTables>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.DataBaseTables model;
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

        public List<Model.DataBaseTables> Select(int id)
        {
            return GetModelList(" uId=" + id);
        }

        #endregion  ExtensionMethod
    }
}

