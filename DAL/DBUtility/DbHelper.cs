using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using System.Configuration;
using MongoDB.Bson;

namespace DAL
{
    public class DbHelper : IDisposable
    {
        /*
            下面的代码帮助你从Mongo数据库中查找一条现有记录。
            _collection = _database.GetCollection<users1 />("users");
            IMongoQuery query = Query.EQ("Name", "Anoop");
            Users1 _user = _collection.FindAs<users1 />(query).FirstOrDefault();
            MessageBox.Show(_user.Age.ToString());

            下面的代码帮助你更新Mongo数据库中的一条现有记录。
            _collection = _database.GetCollection<users1 />("users");
            IMongoQuery query = Query.EQ("Name", "Anoop");
            Users1 _user = _collection.FindAs<users1 />(query).FirstOrDefault();
            MessageBox.Show("Age before update :" + _user.Age.ToString());
            //更新年龄的值
            _user.Age = 30;

            保存更改            
            _collection.Save(_user);
            MessageBox.Show("Age after update :" + _user.Age.ToString());
         */

        //private static MongoServer server;
        //private static MongoDatabase db;
        private string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private string DataBaseName = ConfigurationManager.AppSettings["MongoDBDataBaseName"];
        MongoClient client = null;
        public MongoServer Servar
        {
            get
            {
                if (client == null)
                    client = new MongoClient(connectionString);
                return client.GetServer();
            }
        }

        public MongoDatabase DB
        {
            get
            {
                return Servar.GetDatabase(DataBaseName);
            }
        }

        public void Dispose()
        {
            if (client != null)
            {
                client = null;
            }
        }
    }
}
