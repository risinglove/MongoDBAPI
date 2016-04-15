using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MongoDB.Bson;

namespace BLL
{
    public class UserBLL
    {
        private DAL.UserDAL dal = null;
        public UserBLL()
        {
            dal = new DAL.UserDAL();
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(User model)
        {
            dal.Add(model);
            return true;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            return dal.GetALL();
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(User model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据ObjectID 删除
        /// </summary>
        /// <param name="objId"></param>
        public bool Delete(ObjectId objId)
        {
            return dal.Delete(objId);
        }

        /// <summary>
        /// 根据ObjectID 查询
        /// </summary>
        public User SelectOne(ObjectId objId)
        {
            return dal.SelectOne(objId);
        }
    }
}
