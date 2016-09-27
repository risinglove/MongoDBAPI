using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using ViewModel;

namespace BLL
{
    public class CurrencyBLL
    {
        private DAL.CurrencyDAL<object> dal = null;
        private string tName = "";


        public CurrencyBLL(string tableName)
        {
            tName = tableName;
            dal = new DAL.CurrencyDAL<object>(tableName);
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(object model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 查询出所有数据
        /// </summary>
        /// <returns></returns>
        public List<object> GetALL()
        {
            try
            {
                return dal.GetALL();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(object model, String Id)
        {
            try
            {
                return dal.Update(model, Id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据ObjectID 删除
        /// </summary>
        /// <param name="objId"></param>
        public bool Delete(string objId)
        {
            try
            {
                return dal.Delete(ObjectId.Parse(objId)); ;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据ObjectID 查询
        /// </summary>
        public object SelectOne(string objId)
        {
            return dal.SelectOne(ObjectId.Parse(objId));
        }


        public bool Update(string userId, string path, DataBaseModel model)
        {
            try
            {
                var json = new Dictionary<string, object>();
                string id = ObjectId.GenerateNewId().ToString();
                json.Add("Id", id);
                foreach (Column c in model.list)
                {
                    json.Add(c.name, null);
                }
                var m = ModelHelper.GetModel(className: tName, UserId: userId, path: path);
                m = TypeSafe.SafeObject(m, json);
                dal.Add(m);
                return dal.Delete(ObjectId.Parse(id));

                //return dal.Add(m);
            }
            catch (Exception e)
            {
            }
            return false;
        }

    }
}
