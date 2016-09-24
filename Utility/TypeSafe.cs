using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class TypeSafe
    {
        public static object SafeObject(object model, Dictionary<string, object> data)
        {
            PropertyInfo[] modelPro = model.GetType().GetProperties();
            if (modelPro.Length > 0 && data.Count() > 0)
            {
                for (int i = 0; i < modelPro.Length; i++)
                {
                    if (data.ContainsKey(modelPro[i].Name))
                    {
                        modelPro[i].SetValue(model, Safe(modelPro[i].PropertyType.Name, data[modelPro[i].Name]), null);
                    }
                    else if(modelPro[i].Name== "CreateDate")
                    {
                        modelPro[i].SetValue(model, DateTime.Now, null);
                    }
                }
            }
            return model;
        }


        private static object Safe(string name, object obj)
        {
            object model = null;
            switch (name)
            {
                case "Int32":
                    model = SafeInt(obj, 0);
                    break;
                case "Decimal":
                    model = SafeDecimal(obj, 0);
                    break;
                case "Boolean":
                    model = SafeBool(obj, true);
                    break;
                case "String":
                    model = SafeString(obj, "");
                    break;
                case "DateTime":
                    model = SafeDateTime(obj, DateTime.Now);
                    break;
                default:
                    model = null;
                    break;
            }
            return model;
        }

        #region SafeParse
        public static bool SafeBool(object target, bool defaultValue)
        {
            if (target == null) return defaultValue;
            var tmp = target.ToString(); if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeBool(tmp, defaultValue);
        }
        public static bool SafeBool(string text, bool defaultValue)
        {
            bool flag;
            if (bool.TryParse(text, out flag))
            {
                defaultValue = flag;
            }
            return defaultValue;
        }

        public static DateTime SafeDateTime(object target, DateTime defaultValue)
        {
            if (target == null) return defaultValue;
            var tmp = target.ToString(); if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeDateTime(tmp, defaultValue);
        }
        public static DateTime SafeDateTime(string text, DateTime defaultValue)
        {
            DateTime time;
            if (DateTime.TryParse(text, out time))
            {
                defaultValue = time;
            }
            return defaultValue;
        }

        public static decimal SafeDecimal(object target, decimal defaultValue)
        {
            if (target == null) return defaultValue;
            var tmp = target.ToString(); if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeDecimal(tmp, defaultValue);
        }
        public static decimal SafeDecimal(string text, decimal defaultValue)
        {
            decimal num;
            if (decimal.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static short SafeShort(object target, short defaultValue)
        {
            if (target == null) return defaultValue;
            var tmp = target.ToString(); if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeShort(tmp, defaultValue);
        }
        public static short SafeShort(string text, short defaultValue)
        {
            short num;
            if (short.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static int SafeInt(object target, int defaultValue)
        {
            if (target == null) return defaultValue;
            var tmp = target.ToString(); if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeInt(tmp, defaultValue);
        }
        public static int SafeInt(string text, int defaultValue)
        {
            int num;
            if (int.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }
        public static int[] SafeInt(string[] text, int defaultValue)
        {
            if (text == null || text.Length < 1) return new[] { defaultValue };

            var nums = new int[text.Length];
            int tmp;
            for (var i = 0; i < text.Length; i++)
            {
                if (int.TryParse(text[i], out tmp)) nums[i] = tmp;
                else nums[i] = defaultValue;
            }
            return nums;
        }
        public static string SafeIntFilter(string text, int defaultValue, char split = ',')
        {
            if (string.IsNullOrWhiteSpace(text)) return defaultValue.ToString(CultureInfo.InvariantCulture);
            var tmpSplit = text.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries);
            if (tmpSplit.Length < 1) return defaultValue.ToString(CultureInfo.InvariantCulture);

            int tmp;
            for (var i = 0; i < tmpSplit.Length; i++)
            {
                if (int.TryParse(tmpSplit[i], out tmp))
                    tmpSplit[i] = tmp.ToString(CultureInfo.InvariantCulture);
                else
                    tmpSplit[i] = defaultValue.ToString(CultureInfo.InvariantCulture);
            }
            return string.Join(split.ToString(CultureInfo.InvariantCulture), tmpSplit);
        }

        public static long SafeLong(object target, long defaultValue)
        {
            if (target == null) return defaultValue;
            var tmp = target.ToString(); if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeLong(tmp, defaultValue);
        }
        public static long SafeLong(string text, long defaultValue)
        {
            long num;
            if (long.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static long[] SafeLong(string[] text, long defaultValue)
        {
            if (text == null || text.Length < 1) return new[] { defaultValue };

            var nums = new long[text.Length];
            long tmp;
            for (var i = 0; i < text.Length; i++)
            {
                if (long.TryParse(text[i], out tmp)) nums[i] = tmp;
                else nums[i] = defaultValue;
            }
            return nums;
        }

        public static string SafeLongFilter(string text, long defaultValue, char split = ',')
        {
            if (string.IsNullOrWhiteSpace(text)) return defaultValue.ToString(CultureInfo.InvariantCulture);
            var tmpSplit = text.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries);
            if (tmpSplit.Length < 1) return defaultValue.ToString(CultureInfo.InvariantCulture);

            long tmp;
            for (var i = 0; i < tmpSplit.Length; i++)
            {
                if (long.TryParse(tmpSplit[i], out tmp))
                    tmpSplit[i] = tmp.ToString(CultureInfo.InvariantCulture);
                else
                    tmpSplit[i] = defaultValue.ToString(CultureInfo.InvariantCulture);
            }
            return string.Join(split.ToString(CultureInfo.InvariantCulture), tmpSplit);
        }

        public static string SafeString(object target, string defaultValue)
        {
            if (null != target && "" != target.ToString())
            {
                return target.ToString();
            }
            return defaultValue;
        }
        #endregion
        #region SafeNullParse
        public static bool? SafeBool(object target, bool? defaultValue)
        {
            if (target == null) return defaultValue;
            var tmp = target.ToString();
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeBool(tmp, defaultValue);
        }
        public static bool? SafeBool(string text, bool? defaultValue)
        {
            bool flag;
            if (bool.TryParse(text, out flag))
            {
                defaultValue = flag;
            }
            return defaultValue;
        }

        public static DateTime? SafeDateTime(object target, DateTime? defaultValue)
        {
            if (target == null) return defaultValue;
            var tmp = target.ToString();
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeDateTime(tmp, defaultValue);
        }
        public static DateTime? SafeDateTime(string text, DateTime? defaultValue)
        {
            DateTime time;
            if (DateTime.TryParse(text, out time))
            {
                defaultValue = time;
            }
            return defaultValue;
        }

        public static decimal? SafeDecimal(object target, decimal? defaultValue)
        {
            if (target == null) return defaultValue;
            var tmp = target.ToString();
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeDecimal(tmp, defaultValue);
        }
        public static decimal? SafeDecimal(string text, decimal? defaultValue)
        {
            decimal num;
            if (decimal.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static short? SafeShort(object target, short? defaultValue)
        {
            if (target == null) return defaultValue;
            var tmp = target.ToString();
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeShort(tmp, defaultValue);
        }
        public static short? SafeShort(string text, short? defaultValue)
        {
            short num;
            if (short.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static int? SafeInt(object target, int? defaultValue)
        {
            if (target == null) return defaultValue;
            var tmp = target.ToString();
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeInt(tmp, defaultValue);
        }
        public static int? SafeInt(string text, int? defaultValue)
        {
            int num;
            if (int.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        public static long? SafeLong(object target, long? defaultValue)
        {
            if (target == null) return defaultValue;
            var tmp = target.ToString();
            if (string.IsNullOrWhiteSpace(tmp)) return defaultValue;
            return SafeLong(tmp, defaultValue);
        }
        public static long? SafeLong(string text, long? defaultValue)
        {
            long num;
            if (long.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }
        #endregion

        #region SafeEnum
        /// <summary>
        /// 将枚举数值or枚举名称 安全转换为枚举对象
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">数值or名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <remarks>转换区分大小写</remarks>
        /// <returns></returns>
        public static T SafeEnum<T>(string value, T defaultValue) where T : struct
        {
            return SafeEnum<T>(value, defaultValue, false);
        }

        /// <summary>
        /// 将枚举数值or枚举名称 安全转换为枚举对象
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">数值or名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="ignoreCase">是否忽略大小写 true 不区分大小写 | false 区分大小写</param>
        /// <returns></returns>
        public static T SafeEnum<T>(string value, T defaultValue, bool ignoreCase) where T : struct
        {
            T result;
            if (Enum.TryParse<T>(value, ignoreCase, out result))
            {
                if (Enum.IsDefined(typeof(T), result))
                {
                    defaultValue = result;
                }
            }
            return defaultValue;
        }
        #endregion


    }
}
