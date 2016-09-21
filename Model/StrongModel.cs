using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StrongModel : DynamicObject
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; } = DateTime.Now.ToString();

        /// <summary>
        /// 数据状态（1 有效、 0 无效）
        /// </summary>
        public string Status { get; set; } = "1";

        Dictionary<string, object> Properties = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!Properties.Keys.Contains(binder.Name))
            {
                //在此可以做一些小动作
                //if (binder.Name == "Col")
                //　　Properties.Add(binder.Name + (Properties.Count), value.ToString());
                //else
                //　　Properties.Add(binder.Name, value.ToString());

                Properties.Add(binder.Name, value.ToString());
            }
            return true;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return Properties.TryGetValue(binder.Name, out result);
        }

    }
}
