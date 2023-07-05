/*
 * 公司名称：北京麒宇兄弟科技有限公司
 * 模块功能：
 * 创建日期：2023-07-05
 * 修改日期：2023-07-05
 * 作    者：刘振华2023-07-05
 * 电子邮箱：13240137763@163.com2023-07-05
 */
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
