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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryTraceSample
{
	public class StackTrackHelper
	{
		/// <summary>
		/// 输出文件名
		/// </summary>
		public static string LogFileName = "stackTrace.log";
		/// <summary>
		/// 输出函数调用栈信息
		/// </summary>
		/// <param name="stackTrace"></param>
		/// <param name="logFileName"></param>
		/// <param name="outputParameters"></param>
		public static void StackTrace(StackTrace stackTrace, string logFileName= "stackTrace.log")
		{
			LogFileName = logFileName;
			var sb = new StringBuilder();
			var now = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fffff")}";
			#region 收集调用栈信息
			for (var i = 0; i < stackTrace.FrameCount; i++)
			{
				StackFrame stackFrame = stackTrace.GetFrame(i); // 获取调用ExampleMethod的方法栈帧，0表示当前方法，1表示调用者
				string fileName = stackFrame.GetFileName(); // 获取文件名
				string className = stackFrame.GetMethod().DeclaringType.Name; // 获取类名
				string methodName = stackFrame.GetMethod().Name; // 获取方法名
				int lineNumber = stackFrame.GetFileLineNumber(); // 获取行号

				if (string.IsNullOrEmpty(fileName) && lineNumber == 0)
				{
					// 没有文件名和行号信息
					sb.AppendLine($"[{now}]Frame:{(i.ToString() + ',').PadRight(6, ' ')} Class:{(stackFrame.GetMethod().DeclaringType.Name + ',').PadRight(20, ' ')} Method:{stackFrame.GetMethod().Name}");
				}
				else
				{
					sb.AppendLine($"[{now}]Frame:{(i.ToString() + ',').PadRight(6, ' ')} Class:{(stackFrame.GetMethod().DeclaringType.Name + ',').PadRight(20, ' ')} Method:{(stackFrame.GetMethod().Name + ',').PadRight(20, ' ')} LineNumber:{(stackFrame.GetFileLineNumber().ToString() + ',').PadRight(7, ' ')} FileName:{fileName}");
				}
			}
			#endregion
			#region 将收集结果输出到文件
			while (true)
			{
				try
				{
					using (var writer = new StreamWriter(LogFileName, true))
					{
						writer.WriteLine(sb);
					}
					break;
				}
				catch (IOException)
				{
					// 如果无法访问日志文件，则等待10毫秒后重试
					Thread.Sleep(10);
				}
			}
			#endregion
		}
	}
}
/*
 *  调用方法：
 *  StackTrackHelper.StackTrace(new StackTrace(true));
 * 
 */

