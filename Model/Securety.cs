using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class Securety
    {
        /// <summary>
        /// ID
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// 登录ID
        /// </summary>
        public int LoginID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 第一个问题
        /// </summary>
        public string Us_Q1 { get; set; }

        /// <summary>
        /// 第一个问题答案
        /// </summary>
        public string Us_A1 { get; set; }

        /// <summary>
        /// 第二个问题
        /// </summary>
        public string Us_Q2 { get; set; }

        /// <summary>
        /// 第二个问题答案
        /// </summary>
        public string Us_A2 { get; set; }
    }
}
