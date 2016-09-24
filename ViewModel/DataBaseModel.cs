using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    /// <summary>
    /// 用户自定义表对应的Model
    /// </summary>
    public class DataBaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 所属用户ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 对应表中的列名以及列对应的属性
        /// </summary>
        public List<Column> list { get; set; }

    }

    /// <summary>
    /// 表对应的字段Model
    /// </summary>
    public class Column
    {
        public string name { get; set; }

        public string type { get; set; }
    }

}
