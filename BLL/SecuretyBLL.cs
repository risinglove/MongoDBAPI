using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace BLL
{
    public class SecuretyBLL
    {
        private DAL.SecuretyDAL dal = null;
        public SecuretyBLL()
        {
            dal = new DAL.SecuretyDAL();
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Securety model)
        {
            dal.Add(model);
            return true;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<Securety> GetAll()
        {
            return dal.GetALL();
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Securety model)
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
        public Securety SelectOne(ObjectId objId)
        {
            return dal.SelectOne(objId);
        }

    }
}
