using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Model
{
    public class SysDic
    {
        /// <summary>
        /// ID
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Dic_Type { get; set; }

        /// <summary>
        /// 类型名
        /// </summary>
        public string Dic_TypeName { get; set; }

        /// <summary>
        /// 类型下编号
        /// </summary>
        public int Dic_ItemCode { get; set; }

        /// <summary>
        /// 编号名称
        /// </summary>
        public string Dic_ItemName { get; set; }

    }
}
