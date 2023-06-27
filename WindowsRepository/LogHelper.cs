/*
 * 所属公司：北京麒宇兄弟科技有限公司
 * 模块功能：输出日志。支持多文件输出。支持控制台输出，支持OutputDebugString API输出。
 * 版    本：V2
 * 创建日期：2018-08-08
 * 修改日期：2023-06-24
 * 创建人：liuzhenhua
 * 电子邮箱：13240137763.com
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace qiyubrother
{
    public class LogHelper
    {
        static ConcurrentQueue<MessagePackage> queue = new ConcurrentQueue<MessagePackage>();
        private static bool _enable = false;
		static bool _consoleOutput = true;
		static bool _debugStringOutput = true;
        /// <summary>
        /// 输出调试信息
        /// </summary>
        /// <param name="message"></param>
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern void OutputDebugString(string message);
		/// <summary>
		/// 启动日志服务
		/// </summary>
		public static void StartService(bool consoleOutput = true, bool debugStringOutput = true)
		{
			_consoleOutput = consoleOutput;
			_debugStringOutput = debugStringOutput;
			_enable = true;
			LogWriter();
		}
		/// <summary>
		/// 停止日志服务
		/// </summary>
		public static void Stop()
        {
            _enable = false;
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="s"></param>
        /// <param name="param"></param>
        public static void Trace(string s, string fileName="")
        {
            if (!_enable)
            {
                return;
            }
            var messagePackage = new MessagePackage
            {
                Message = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fffff")}][ThreadId:{Thread.CurrentThread.ManagedThreadId}]{s}",
                FileName = fileName,
		    };
			queue.Enqueue(messagePackage);
            if (_consoleOutput)
            {
				Console.WriteLine(messagePackage.Message); // 注掉实时回显，提高相应速度
			}
            if (_debugStringOutput)
            {
				OutputDebugString(messagePackage.Message); // 注掉，提高相应速度
			}
        }

        /// <summary>
        /// 写日志到文件
        /// </summary>
        private static void LogWriter()
        {
            new Task(() =>
            {
                while (_enable)
                {
                    if (queue.Count > 0)
                    {
                        if (queue.TryDequeue(out MessagePackage item))
                        {
                            var fn = string.Empty;
                            if (item.FileName != string.Empty)
                            {
                                fn = Path.Combine($@"logs\{item.FileName}");
							}
                            else
                            {
								fn = Path.Combine($@"logs\Trace-{DateTime.Now.Year}{DateTime.Now.Month.ToString().PadLeft(2, '0')}{DateTime.Now.Day.ToString().PadLeft(2, '0')}.log");
							}
                            var cnt = 0;
                            do
                            {
                                try
                                {
                                    var fs = File.OpenWrite(fn);
									fs.Position = fs.Length;
                                    var bytes = Encoding.UTF8.GetBytes(item.Message + Environment.NewLine);
									fs.Write(bytes, 0, bytes.Length);
                                    fs.Close();
                                    fs.Dispose();
                                    break;
                                }
                                catch
                                {
                                    var fi = new FileInfo(fn);
                                    if (!Directory.Exists(fi.DirectoryName))
                                    {
										Directory.CreateDirectory(fi.DirectoryName);
									}
                                    cnt++;
                                    Thread.Sleep(200);
                                }
                                if (cnt > 3)
                                {
                                    // 超过3次写入错误
                                    var efn = $"Error-{DateTime.Now.Year}{DateTime.Now.Month.ToString().PadLeft(2, '0')}{DateTime.Now.Day.ToString().PadLeft(2, '0')}.log";
                                    try
                                    {
                                        File.AppendAllLines(efn, new[] { $"[{DateTime.Now}]日志系统错误。" });
                                    }
                                    catch { }

                                    break;
                                }
                            } while (true);
						}
                        else
                        {
                            Thread.Sleep(50);
                        }
                    }
                    Thread.Sleep(10);
                }
            }).Start();
        }
    }
    /// <summary>
    /// 消息包
    /// </summary>
    internal class MessagePackage
    {
        public string Message { get; set; }
        public string FileName { get; set; }
    }
}
