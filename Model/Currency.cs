using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using MongoDB.Bson;
using Utility;
using System.Reflection;

namespace Model
{
    /// <summary>
    /// 前台提交数据时使用的Model
    /// </summary>
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

        public object Data
        {
            get
            {
                object model = null;
                if (data != null && data.Count > 0)
                {
                    try
                    {
                        model = ModelHelper.GetModel(TableName, UserId: "57e3932ea95dd03f4432d232");
                        model = TypeSafe.SafeObject(model, data);
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
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
