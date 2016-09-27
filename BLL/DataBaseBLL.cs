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
    public class DataBaseBLL
    {
        private DAL.DataBaseDAL dal = null;
        public DataBaseBLL()
        {
            dal = new DAL.DataBaseDAL();
        }

        public void GenerateUserDLL(string UserId, string path)
        {
            var list = dal.Select(UserId);
            ModelHelper.CreateUserDll(UserId, list, path: path);
            CurrencyBLL cBll = null;
            foreach (var item in list)
            {
                cBll = new CurrencyBLL(item.TableName);
                cBll.Update(UserId, path,item);
            }
        }


        /// <summary>
        /// 根据用户ID查询
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<DataBaseModel> Select(string UserId)
        {
            var list = dal.Select(UserId);
            return list;
        }

        /// <summary>
        /// 根据ObjectID 查询
        /// </summary>
        public DataBaseModel SelectOne(string objId)
        {
            return dal.SelectOne(ObjectId.Parse(objId));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(DataBaseModel model)
        {
            dal.Delete(ObjectId.Parse(model.Id));
            return dal.Add(model);
        }

        /// <summary>
        /// 根据ObjectID 删除
        /// </summary>
        /// <param name="objId"></param>
        public bool Delete(string objId)
        {
            return dal.Delete(ObjectId.Parse(objId));
        }
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(DataBaseModel model)
        {
            try
            {
                return dal.Add(model);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
