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
using System.Text;
using System.Net.Sockets;
namespace qiyubrother.extend.Net
{
    /// <summary>
    /// Socket操作扩展类
    /// </summary>
    public static class SocketExtend
    {
        /// <summary>
        /// 检测Socket连接是否已经断开。
        /// </summary>
        /// <param name="socket"></param>
        /// <returns>true：表示已经断开；false表示已连接</returns>
        public static bool RuntimeSocketIsConnected(this Socket socket)
        {
            return !socket.Poll(100, SelectMode.SelectRead);
        }
    }
}
