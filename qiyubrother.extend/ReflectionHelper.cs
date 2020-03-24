using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qiyubrother.extend
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// 读取指定类型的所有静态属性值
        /// </summary>
        /// <param name="t"></param>
        /// <returns>（属性名，属性值）的数据字典</returns>
        public static Dictionary<string, object> GetAllStaticPropertys(Type t)
        {
            Dictionary<string, Object> dict = new Dictionary<string, object>();
            
            foreach (var att in t.GetProperties())
            {
                try
                {
                    dict.Add(att.Name, att.GetValue(att.Name));
                }
                catch (System.Reflection.TargetException te)
                {
                    continue;
                }
            }

            return dict;
        }

    }
}
