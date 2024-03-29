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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevKitUI
{
    public static class UiHelper
    {
		public static void FmtGridView(DataGridView dgv)
		{
			dgv.AllowUserToAddRows = false;
			dgv.AllowUserToDeleteRows = false; 
			dgv.AllowUserToResizeRows = false;
			dgv.ReadOnly = true;
			dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgv.DefaultCellStyle.Font = new Font("微软雅黑", 16);
			dgv.RowTemplate.Height = 50;
			dgv.DefaultCellStyle.ForeColor = Color.DarkGray; // Color.FromArgb(240, 240, 240);
			dgv.DefaultCellStyle.BackColor = Color.White;
			dgv.ColumnHeadersDefaultCellStyle.Font = new Font("微软雅黑", 16);
			dgv.ForeColor = Color.DarkGray;
			dgv.EnableHeadersVisualStyles = false;
			dgv.Columns[dgv.Columns.Count - 2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
			dgv.ColumnHeadersHeight = 50;
			dgv.BackgroundColor = Color.White;
			dgv.RowHeadersVisible = false;

			dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
			//dgv.RowHeadersBorderStyle =
			//	DataGridViewHeaderBorderStyle.Single;
			dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

			//dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(251, 212, 78);
			//dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(251, 110, 28);
			//dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
			//dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(1, 39, 39);
			//dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(2, 83, 83);
			//dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(251, 212, 78); ;
			//dgv.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(251, 212, 78);
			// 选择行
			//dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(253, 253, 253); ;
			//dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
			dgv.DefaultCellStyle.SelectionForeColor = Color.DarkGray; // 选择行样式与普通行样式一样
			dgv.DefaultCellStyle.SelectionBackColor = Color.White;    // 选择行样式与普通行样式一样

			// 列头
			dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
			dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
			dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 250, 250);
			// 行头
			dgv.RowHeadersDefaultCellStyle.ForeColor = Color.Black;
			dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
			dgv.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(250, 250, 250);

			foreach (DataGridViewColumn dgvc in dgv.Columns)
			{
				dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			}
		}
		public static void GuiSleep(int millisecond)
		{
			bool isOK = false;
			var task = Task.Run(() => {
				var t = DateTime.Now.Ticks;
				long v;
				do
				{
					Application.DoEvents();
					v = (DateTime.Now.Ticks - t) / 10000;
				} while (v < millisecond);
				isOK = true;
			});
			while (!isOK) Application.DoEvents();
		}
		/// <summary>
		/// 异步方法中等待（用于防止主界面卡顿）
		/// </summary>
		/// <param name="millisecond"></param>
		/// <returns></returns>
		public static async Task AsyncSleep(int millisecond) => await (new Func<Task>(async () => { await Task.Delay(millisecond); }))();
	}
}
