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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace RobotLinkElevator
{
    public class DebugHelper
    {
        public static void PrintTxMessage(byte[] buffer)
        {
            var sb = new StringBuilder();
            foreach (var b in buffer)
            {
                sb.Append($"{Convert.ToString(b, 16).PadLeft(2, '0').ToUpper()} ");
            }
            var s = $"[{DateTime.Now}]Tx::{sb}";

            Parallel.Invoke(()=> {
                var fc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(s);
                Console.ForegroundColor = fc;
            }, ()=> {
                Log.Information(s);
            } );
        }

        public static void PrintRxMessage(byte[] buffer)
        {
            var sb = new StringBuilder();
            foreach (var b in buffer)
            {
                sb.Append($"{Convert.ToString(b, 16).PadLeft(2, '0').ToUpper()} ");
            }

            var fc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{DateTime.Now}]Rx::{sb}");
            Console.ForegroundColor = fc;
        }

        public static void PrintDebugMessage(byte[] buffer)
        {
            var sb = new StringBuilder();
            foreach (var b in buffer)
            {
                sb.Append($"{Convert.ToString(b, 16).PadLeft(2, '0').ToUpper()} ");
            }
            var s = $"[{DateTime.Now}]{sb}";

            Parallel.Invoke(() => {
                var fc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(s);
                Console.ForegroundColor = fc;
            }, () => {
                Log.Information(s);
            });
        }

        public static void PrintDebugMessage(byte[] buffer, int length)
        {
            var sb = new StringBuilder();
            var len = Math.Min(buffer.Length, length);
            for (var i = 0; i < len; i++)
            {
                var b = buffer[i];
                sb.Append($"{Convert.ToString(b, 16).PadLeft(2, '0').ToUpper()} ");
            }
            var s = $"[{DateTime.Now}]{sb}";

            Parallel.Invoke(() => {
                var fc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(s);
                Console.ForegroundColor = fc;
            }, () => {
                Log.Information(s);
            });
        }

        public static void PrintTraceMessage(string s)
        {
            Parallel.Invoke(() => {
                var fc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{DateTime.Now}][Trace]::{s}");
                Console.ForegroundColor = fc;
            }, () => {
                Log.Information(s);
            });
        }

        public static void PrintErrorMessage(string s)
        {
            Parallel.Invoke(() => {
                var fc = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[{DateTime.Now}][Error]::{s}");
                Console.ForegroundColor = fc;
            }, () => {
                Log.Information(s);
            });
        }
    }
}
