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
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsRepository
{
	public sealed class MacAddressHelper
	{
		///<summary>
		/// 根据截取ipconfig /all命令的输出流获取网卡Mac，支持不同语言编码
		///</summary>
		///<returns></returns>
		public static string GetMacByIpConfig()
		{
			List<string> macs = new List<string>();

			var runCmd = Cmd.RunCmd("chcp 437&&ipconfig/all");

			foreach (var line in runCmd.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()))
			{
				if (!string.IsNullOrEmpty(line))
				{
					if (line.StartsWith("Physical Address"))
					{
						macs.Add(line.Substring(36));
					}
					else if (line.StartsWith("DNS Servers") && line.Length > 36 && line.Substring(36).Contains("::"))
					{
						macs.Clear();
					}
					else if (macs.Count > 0 && line.StartsWith("NetBIOS") && line.Contains("Enabled"))
					{
						return macs.Last();
					}
				}
			}

			return macs.FirstOrDefault();
		}

		///<summary>
		/// 通过WMI读取系统信息里的网卡MAC
		///</summary>
		///<returns></returns>
		public static List<string> GetMacByWmi()
		{
			try
			{
				List<string> macs = new List<string>();
				ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
				ManagementObjectCollection moc = mc.GetInstances();
				foreach (ManagementObject mo in moc)
				{
					if ((bool)mo["IPEnabled"])
					{
						var mac = mo["MacAddress"].ToString();
						macs.Add(mac);
					}
				}
				return macs;
			}
			catch (Exception e)
			{
				return null;
			}
		}

		///<summary>
		/// 通过NetworkInterface读取网卡Mac
		///</summary>
		///<returns></returns>
		public static List<string> GetMacByNetworkInterface()
		{
			List<string> macs = new List<string>();
			NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface ni in interfaces)
			{
				macs.Add(ni.GetPhysicalAddress().ToString());
			}
			return macs;
		}

		///<summary>
		/// 通过SendARP获取网卡Mac
		/// 网络被禁用或未接入网络（如没插网线）时此方法失灵
		///</summary>
		///<param name="remoteIP"></param>
		///<returns></returns>
		public static string GetMacBySendArp(string remoteIP)
		{
			StringBuilder macAddress = new StringBuilder();
			try
			{
				Int32 remote = inet_addr(remoteIP);
				Int64 macInfo = new Int64();
				Int32 length = 6;
				SendARP(remote, 0, ref macInfo, ref length);
				string temp = Convert.ToString(macInfo, 16).PadLeft(12, '0').ToUpper();
				int x = 12;
				for (int i = 0; i < 6; i++)
				{
					if (i == 5)
					{
						macAddress.Append(temp.Substring(x - 2, 2));
					}
					else
					{
						macAddress.Append(temp.Substring(x - 2, 2) + "-");
					}
					x -= 2;
				}
				return macAddress.ToString();
			}
			catch
			{
				return macAddress.ToString();
			}
		}

		[DllImport("Iphlpapi.dll")]
		private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
		[DllImport("Ws2_32.dll")]
		private static extern Int32 inet_addr(string ip);

		class Cmd
		{
			private static string CmdPath = @"C:\Windows\System32\cmd.exe";
			/// <summary>
			/// 执行cmd命令 返回cmd窗口显示的信息
			/// 多命令请使用批处理命令连接符：
			/// <![CDATA[
			/// &:同时执行两个命令
			/// |:将上一个命令的输出,作为下一个命令的输入
			/// &&：当&&前的命令成功时,才执行&&后的命令
			/// ||：当||前的命令失败时,才执行||后的命令]]>
			/// </summary>
			/// <param name="cmd">执行的命令</param>
			public static string RunCmd(string cmd)
			{
				cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
				using (Process p = new Process())
				{
					p.StartInfo.FileName = CmdPath;
					p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
					p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
					p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
					p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
					p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
					p.Start();//启动程序

					//向cmd窗口写入命令
					p.StandardInput.WriteLine(cmd);
					p.StandardInput.AutoFlush = true;

					//获取cmd窗口的输出信息
					string output = p.StandardOutput.ReadToEnd();
					p.WaitForExit();//等待程序执行完退出进程
					p.Close();

					return output;
				}
			}
		}
	}
}
