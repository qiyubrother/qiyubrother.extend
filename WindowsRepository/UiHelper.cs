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

		/// <summary>
		/// 当前线程挂起指定的毫秒数（用于防止主界面卡顿）
		/// </summary>
		/// <param name="millisecond"></param>
		public static void GuiSleep(int millisecond)
		{
			var task = Task.Run(() => {
				var t = DateTime.Now.Ticks;
				do { Application.DoEvents(); } while (((DateTime.Now.Ticks - t) / 10000) < millisecond);
			});
			while (task.Status == TaskStatus.Running || task.Status == TaskStatus.WaitingForActivation) Application.DoEvents();
		}
		/// <summary>
		/// 异步方法中等待（用于防止主界面卡顿）
		/// </summary>
		/// <param name="millisecond"></param>
		/// <returns></returns>
		public static async Task AsyncSleep(int millisecond) => await (new Func<Task>(async () => { await Task.Delay(millisecond); }))();
	}
}
