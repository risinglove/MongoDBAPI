﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User
    {

        /// <summary>
        /// ID(用户的唯一标识)
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; } = DateTime.Now.ToString();

        public string appID { get; set; }

        public string appsecret { get; set; }
    }

    public class Info
    {
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 所在省份编号
        /// </summary>
        public int Province { get; set; }

        /// <summary>
        /// 所在市编号
        /// </summary>
        public int City { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailAddress { get; set; }
    }

}
