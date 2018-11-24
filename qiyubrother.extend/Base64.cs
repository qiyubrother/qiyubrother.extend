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
