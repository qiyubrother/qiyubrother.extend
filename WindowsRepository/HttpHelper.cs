using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsRepository
{
	public static class HttpHelper
	{
		/// <summary>
		/// Post 数据到服务端
		/// </summary>
		/// <param name="url"></param>
		/// <param name="json"></param>
		/// <returns></returns>
		public static string Post(string url, string json)
		{
			try
			{
				var request = (HttpWebRequest)WebRequest.Create(url);
				request.Method = "POST";
				request.ContentType = "application/json;charset=UTF-8";//ContentType
				byte[] byteData = Encoding.UTF8.GetBytes(json);
				int length = byteData.Length;
				request.ContentLength = length;
				Stream writer = request.GetRequestStream();
				writer.Write(byteData, 0, length);
				writer.Close();
				var response = (HttpWebResponse)request.GetResponse();
				var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
				return responseString.ToString();
			}
			catch(Exception ex)
			{
				var jo = new JObject();
				jo["ErrorCode"] = -1;
				jo["ErrorMessage"] = ex.Message;
				return jo.ToString();
			}
		}
	}
}
