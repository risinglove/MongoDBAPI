using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Model
{

    public class Login
    {
        /// <summary>
        /// ID
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string LoginName { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public string RegTime { get; set; }

    }

}
