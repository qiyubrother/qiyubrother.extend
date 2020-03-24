using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsRepository
{
    public class Win32APIHelper
    {
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        //使得窗体可以拖动
        [DllImport("user32.dll")]  //需添加using System.Runtime.InteropServices
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", EntryPoint = "ShowCursor", CharSet = CharSet.Auto)]
        public extern static void ShowCursor(int status);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetCursor(IntPtr hCursor);
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        /// <summary>
        /// 设置目标窗体大小，位置
        /// </summary>
        /// <param name="hWnd">目标句柄</param>
        /// <param name="x">目标窗体新位置X轴坐标</param>
        /// <param name="y">目标窗体新位置Y轴坐标</param>
        /// <param name="nWidth">目标窗体新宽度</param>
        /// <param name="nHeight">目标窗体新高度</param>
        /// <param name="BRePaint">是否刷新窗体</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);

        /// <summary>
        /// 设置父窗口
        /// </summary>
        /// <param name="hWndChild">A handle to the child window.</param>
        /// <param name="hWndNewParent">A handle to the new parent window. If this parameter is NULL, the desktop window becomes the new parent window. </param>
        /// <returns>If the function succeeds, the return value is a handle to the previous parent window. If the function fails, the return value is NULL.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
        [DllImport("gdi32.dll")]
        public static extern uint SetBkColor(IntPtr hdc, int crColor);
        [DllImport("user32.dll")]
        public static extern IntPtr GetCursor();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string message);

        [DllImport("user32.dll")]
        // SW_HIDE = 0;
        // SW_NORMAL = 1;
        // SW_MAXIMIZE = 3;
        // SW_SHOWNOACTIVATE = 4;
        // SW_SHOW = 5;
        // SW_MINIMIZE = 6;
        // SW_RESTORE = 9;
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow); // 返回值：如果窗口原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。

        [DllImport("kernel32.dll")]
        public static extern uint SetThreadExecutionState(ExecutionFlag flags);

        public const int MOUSEEVENTF_MOVE = 0x0001;      // 移动鼠标
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;  // 模拟鼠标左键按下
        public const int MOUSEEVENTF_LEFTUP = 0x0004;    // 模拟鼠标左键抬起
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008; // 模拟鼠标右键按下
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;   // 模拟鼠标右键抬起
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;// 模拟鼠标中键按下
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;  // 模拟鼠标中键抬起
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;  // 标示是否采用绝对坐标

        [DllImport("user32", EntryPoint = "mouse_event")]
        public static extern int mouse_event(
            int dwFlags,// 下表中标志之一或它们的组合
            int dx,
            int dy, //指定x，y方向的绝对位置或相对位置
            int cButtons,//没有使用
            int dwExtraInfo//没有使用
        );

        [Flags]
        public enum ExecutionFlag : uint
        {
            System = 0x00000001,
            Display = 0x00000002,
            Continus = 0x80000000,
        }
    }
}
