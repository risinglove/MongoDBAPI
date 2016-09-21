using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using MongoDB.Bson;

namespace Model
{
    public class Currency
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 操作单条数据时当前数据的ID
        /// </summary>
        public string _id { get; set; }

        /// <summary>
        /// 数据（单条用）
        /// </summary>
        public Dictionary<string, object> data { get; set; }

        ///// <summary>
        ///// 数据（多条用）
        ///// </summary>
        //public string list { get; set; }

        public dynamic Data
        {
            get
            {
                dynamic model = new StrongModel();
                try
                {
                    foreach (string current in data.Keys)
                    {
                        model[current] = data[current];
                    }
                }
                catch (Exception e)
                {
                }
                return model;
            }
        }

        //public List<JObject> List
        //{
        //    get
        //    {
        //        var o2 = JsonConvert.DeserializeObject(list) as JObject;
        //        return null;
        //    }
        //}

        /// <summary>
        /// 操作类型
        /// （ 查 001、增 002、删 003、改 004） 默认为查询
        /// </summary>
        public string OperationType { get; set; } = "001";
    }
}
