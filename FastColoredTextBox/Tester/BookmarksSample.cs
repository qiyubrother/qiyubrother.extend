/*
 * 公司名称：北京麒宇兄弟科技有限公司
 * 模块功能：
 * 创建日期：2023-07-05
 * 修改日期：2023-07-05
 * 作    者：刘振华
 * 电子邮箱：13240137763@163.com
 */
using System;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace Tester
{
    public partial class BookmarksSample : Form
    {
        public BookmarksSample()
        {
            InitializeComponent();
        }

        private void btAddBookmark_Click(object sender, EventArgs e)
        {
            fctb.Bookmarks.Add(fctb.Selection.Start.iLine);
        }

        private void btRemoveBookmark_Click(object sender, EventArgs e)
        {
            fctb.Bookmarks.Remove(fctb.Selection.Start.iLine);
        }

        private void btGo_DropDownOpening(object sender, EventArgs e)
        {
            btGo.DropDownItems.Clear();
            foreach (var bookmark in fctb.Bookmarks)
            {
                var item = btGo.DropDownItems.Add(bookmark.Name);
                item.Tag = bookmark;
                item.Click += (o, a) => ((Bookmark)(o as ToolStripItem).Tag).DoVisible();
            }
        }

        private void fctb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.X < fctb.LeftIndent)
            {
                var place = fctb.PointToPlace(e.Location);
                if(fctb.Bookmarks.Contains(place.iLine))
                    fctb.Bookmarks.Remove(place.iLine);
                else
                    fctb.Bookmarks.Add(place.iLine);
            }
        }
    }
}
