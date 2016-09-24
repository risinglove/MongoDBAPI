using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace DAL
{
    public class DataBaseDAL
    {
        private const string TableName = "DataBaseTable";
        private static MongoCollection<DataBaseModel> mc = null;
        public DataBaseDAL()
        {
            using (DbHelper db = new DbHelper())
            {
                mc = db.DB.GetCollection<DataBaseModel>(TableName);
            }
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
                WriteConcernResult wcr = mc.Insert(model);
                return !wcr.HasLastErrorMessage;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 查询出所有数据
        /// </summary>
        /// <returns></returns>
        public List<DataBaseModel> GetALL()
        {
            try
            {
                List<DataBaseModel> list = mc.FindAllAs<DataBaseModel>().ToList();
                return list;
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
        public bool Update(DataBaseModel model)
        {
            try
            {
                var query = Query.EQ("_id", model.Id);
                BsonDocument bd = BsonExtensionMethods.ToBsonDocument(model);
                WriteConcernResult wcr = mc.Update(query, new UpdateDocument(bd));
                return !wcr.HasLastErrorMessage;
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
        public bool Delete(ObjectId objId)
        {
            try
            {
                IMongoQuery query = Query.EQ("_id", objId);
                WriteConcernResult wcr = mc.Remove(query);
                return !wcr.HasLastErrorMessage;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据ObjectID 查询
        /// </summary>
        public DataBaseModel SelectOne(ObjectId objId)
        {
            return mc.FindOne(Query.EQ("_id", objId));
        }

        /// <summary>
        /// 根据用户ID查询
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<DataBaseModel> Select(string UserId)
        {
            var list = GetALL();
            if (list != null)
            {
                list = list.Where(d => d.UserID == UserId).ToList();
            }
            return list;
        }
    }
}
