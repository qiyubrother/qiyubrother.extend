/*
 * 公司名称：北京麒宇兄弟科技有限公司
 * 模块功能：
 * 创建日期：2023-07-05
 * 修改日期：2023-07-05
 * 作    者：刘振华
 * 电子邮箱：13240137763@163.com
 */
using System;
using System.Text;

namespace qiyubrother.extend
{
    public class Base64
    {
        public static string EncodeBytes(byte[] data) => Convert.ToBase64String(data);

        public static string EncodeString(string data, Encoding encoding) => Convert.ToBase64String(encoding.GetBytes(data));

        public static string EncodeString(string data) => Convert.ToBase64String(Encoding.Default.GetBytes(data));
        public static byte[] DecodeBytes(string data) => Convert.FromBase64String(data);
        public static string DecodeString(string data, Encoding encoding) => encoding.GetString(Convert.FromBase64String(data));
        public static string DecodeString(string data) => Encoding.Default.GetString(Convert.FromBase64String(data));
    }
}
