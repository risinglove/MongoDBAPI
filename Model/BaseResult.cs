using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    /// <summary>
    /// 返回数据时使用的Model
    /// </summary>
    public class BaseResult
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 状态码
        /// 1成功 0错误
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// data中的数据条数
        /// 当大于一时，data里面是一个列表
        /// </summary>
        public int count { get; set; } = 1;

        /// <summary>
        /// 对应的数据
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 当前时间戳
        /// </summary>
        public int timestamp
        {
            get
            {
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return (int)(DateTime.Now - startTime).TotalSeconds;
            }
        }

    }
}