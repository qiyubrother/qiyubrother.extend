/*
 * 公司名称：北京麒宇兄弟科技有限公司
 * 模块功能：
 * 创建日期：2023-07-05
 * 修改日期：2023-07-05
 * 作    者：刘振华
 * 电子邮箱：13240137763@163.com
 */
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace Tester
{
    public partial class SimplestSyntaxHighlightingSample : Form
    {
        //Create style for highlighting
        TextStyle brownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular);

        public SimplestSyntaxHighlightingSample()
        {
            InitializeComponent();
        }

        private void fctb_TextChanged(object sender, TextChangedEventArgs e)
        {
            //clear previous highlighting
            e.ChangedRange.ClearStyle(brownStyle);
            //highlight tags
            e.ChangedRange.SetStyle(brownStyle, "<[^>]+>");
        }
    }
}
