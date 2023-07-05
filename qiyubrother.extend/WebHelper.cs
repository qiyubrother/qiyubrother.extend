/*
 * 公司名称：北京麒宇兄弟科技有限公司
 * 模块功能：
 * 创建日期：2023-07-05
 * 修改日期：2023-07-05
 * 作    者：刘振华
 * 电子邮箱：13240137763@163.com
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace qiyubrother.extend
{
    public class WebHelper
    {
        /// <summary>
        /// Web请求
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public static string HttpPost(string Url, string postDataStr)
        {
            // 使用方法：
            // HttpPost($"http://localhost:5000/api/Values", string.Empty);

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                Encoding encoding = Encoding.UTF8;
                byte[] postData = encoding.GetBytes(postDataStr);
                request.ContentLength = postData.Length;
                Stream myRequestStream = request.GetRequestStream();
                myRequestStream.Write(postData, 0, postData.Length);
                myRequestStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, encoding);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (System.Net.WebException we)
            {
                //qiyubrother.LogHelper.Trace($"WebException::{we.Message}");
                return string.Empty;
            }
            catch (System.IO.IOException ioe)
            {
                //qiyubrother.LogHelper.Trace($"IOException::{ioe.Message}, {ioe.InnerException}");
                return string.Empty;
            }
            catch (Exception ex)
            {
                //qiyubrother.LogHelper.Trace($"Exception::{ex.Message}, Type::{ex.GetType()}");
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <param name="timeout">最长等待响应时间（秒）</param>
        /// <returns>返回服务器响应</returns>
        public static string PostData(string url, Dictionary<string, string> param, int timeout = 5)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(timeout);// 设置最长等待时间（秒）
                using (var httpContent = new FormUrlEncodedContent(param))
                {
                    try
                    {
                        var response = httpClient.PostAsync(url, httpContent).Result;
                        response.EnsureSuccessStatusCode();
                        return response.Content.ReadAsStringAsync().Result;
                    }
                    catch (AggregateException ae)
                    {
                        // 响应超时
                        return string.Empty;
                    }
                    catch (Exception ex)
                    {
                        return string.Empty;
                    }
                }
            }
        }
    }
}
