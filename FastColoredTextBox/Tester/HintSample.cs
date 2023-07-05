/*
 * 公司名称：北京麒宇兄弟科技有限公司
 * 模块功能：
 * 创建日期：2023-07-05
 * 修改日期：2023-07-05
 * 作    者：刘振华2023-07-05
 * 电子邮箱：13240137763@163.com2023-07-05
 */
using System;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace Tester
{
    public partial class HintSample : Form
    {
        public HintSample()
        {
            InitializeComponent();   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fctb.Hints.Clear();
            foreach (var r in fctb.GetRanges(tbFind.Text))
            {
                Hint hint;
                if(cbSimple.Checked)
                    hint = new Hint(r, "This is hint " + fctb.Hints.Count, cbInline.Checked, cbDock.Checked);
                else
                    hint = new Hint(r, new CustomHint(), cbInline.Checked, cbDock.Checked);

                fctb.Hints.Add(hint);
            }
        }

        private void fctb_HintClick(object sender, HintClickEventArgs e)
        {
            MessageBox.Show("You click on the hint");
        }
    }
}
