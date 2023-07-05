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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace Tester
{
    public partial class DynamicSyntaxHighlighting : Form
    {
        Style KeywordsStyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        Style FunctionNameStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);

        public DynamicSyntaxHighlighting()
        {
            InitializeComponent();
        }

        private void fctb_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            //clear styles
            fctb.Range.ClearStyle(KeywordsStyle, FunctionNameStyle);
            //highlight keywords of LISP
            fctb.Range.SetStyle(KeywordsStyle, @"\b(and|eval|else|if|lambda|or|set|defun)\b", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //find function declarations, highlight all of their entry into the code
            foreach (Range found in fctb.GetRanges(@"\b(defun|DEFUN)\s+(?<range>\w+)\b"))
                fctb.Range.SetStyle(FunctionNameStyle, @"\b" + found.Text + @"\b");
        }
    }
}
