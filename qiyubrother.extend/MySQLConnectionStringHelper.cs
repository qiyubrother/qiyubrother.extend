using System;
using System.Collections.Generic;
using System.Text;

namespace qiyubrother.extend
{
	/// <summary>
	/// mySQL数据库连接串修改工具。主要用于连接串的数据库密码被加密后，动态解密
	/// </summary>
	public class MySQLConnectionStringHelper
	{
		#region 用户名
		public static string GetUserId(string s) => GetValue(s, "user id");
		public static string SetUserId(string s, string newUserId) => SetValue(s, "user id", newUserId);
		#endregion
		#region 密码
		/// <summary>
		/// 修改数据库连接串密码
		/// </summary>
		/// <param name="s"></param>
		/// <param name="newPwd"></param>
		/// <returns></returns>
		public static string SetPassword(string s, string newPwd)=> SetValue(s, "password", newPwd);
		/// <summary>
		/// 获取数据库连接串中的密码
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string GetPassword(string s) => GetValue(s, "password");
		#endregion
		#region 服务器地址
		public static string GetServer(string s) => GetValue(s, "server");
		public static string SetServer(string s, string newServer) => SetValue(s, "server", newServer);
		#endregion
		#region 端口
		public static string GetPort(string s) => GetValue(s, "port");
		public static string SetPort(string s, string newPort) => SetValue(s, "port", newPort);
		#endregion
		#region 数据库
		public static string GetDatabase(string s) => GetValue(s, "database");
		public static string SetDatabase(string s, string newDatabase) => SetValue(s, "database", newDatabase);
		#endregion
		#region 通用方法
		public static string GetValue(string s, string key)
		{
			var sb = new StringBuilder();
			var startPos = s.IndexOf(key, 0, StringComparison.OrdinalIgnoreCase);
			if (startPos >= 0)
			{
				startPos += key.Length;
			}
			sb.Append(s.Substring(0, startPos));
			var i = startPos;
			while (i < s.Length && s[i] != '=')
				i++;
			i++;
			while (i < s.Length && s[i] == ' ')
				i++;
			startPos = i;
			while (i < s.Length && s[i] != ';')
				i++;
			var endPos = i;

			var len = endPos - startPos;
			return s.Substring(startPos, len);
		}
		public static string SetValue(string s, string key, string value)
		{
			var sb = new StringBuilder();
			var startPos = s.IndexOf(key, 0, StringComparison.OrdinalIgnoreCase);
			if (startPos >= 0)
			{
				startPos += key.Length;
			}
			sb.Append(s.Substring(0, startPos));
			var i = startPos;
			while (i < s.Length && s[i] != '=')
				i++;
			i++;
			while (i < s.Length && s[i] == ' ')
				i++;
			startPos = i;
			while (i < s.Length && s[i] != ';')
				i++;
			var endPos = i;

			var len = endPos - startPos;
			var pwd = s.Substring(startPos, len);
			sb.Append($"={value}{s.Substring(endPos)}");

			return sb.ToString();
		}
		#endregion
	}
}
