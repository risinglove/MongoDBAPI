using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace DAL
{
    public class SecuretyDAL
    {
        private const string TableName = "SecuretyTable";
        private static MongoCollection<Securety> mc = null;
         public SecuretyDAL()
        {
            using (DbHelper db = new DbHelper())
            {
                mc = db.DB.GetCollection<Securety>(TableName);
            }
        }
         /// <summary>
         /// 添加一条数据
         /// </summary>
         /// <param name="model"></param>
         /// <returns></returns>
         public bool Add(Securety model)
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
         public List<Securety> GetALL()
         {
             try
             {
                 List<Securety> list = mc.FindAllAs<Securety>().ToList();
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
         public bool Update(Securety model)
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
         public Securety SelectOne(ObjectId objId)
         {
             return mc.FindOne(Query.EQ("_id", objId));
         }


    }
}
