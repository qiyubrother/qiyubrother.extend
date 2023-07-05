/*
 * 公司名称：北京麒宇兄弟科技有限公司
 * 模块功能：
 * 创建日期：2023-07-05
 * 修改日期：2023-07-05
 * 作    者：刘振华2023-07-05
 * 电子邮箱：13240137763@163.com2023-07-05
 */
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;

namespace Tester
{
    public partial class AutocompleteSample : Form
    {
        FastColoredTextBoxNS.AutocompleteMenu popupMenu;

        public AutocompleteSample()
        {
            InitializeComponent();

            //create autocomplete popup menu
            popupMenu = new FastColoredTextBoxNS.AutocompleteMenu(fctb);
            popupMenu.MinFragmentLength = 2;

            //generate 456976 words
            var randomWords = new List<string>();
            int codeA = Convert.ToInt32('a');
            for (int i = 0; i < 26; i++)
            for (int j = 0; j < 26; j++)
            for (int k = 0; k < 26; k++)
            for (int l = 0; l < 26; l++)
                randomWords.Add(
                    new string(new char[]{Convert.ToChar(i + codeA), Convert.ToChar(j + codeA), Convert.ToChar(k + codeA), Convert.ToChar(l + codeA)}));

            //set words as autocomplete source
            popupMenu.Items.SetAutocompleteItems(randomWords);
            //size of popupmenu
            popupMenu.Items.MaximumSize = new System.Drawing.Size(200, 300);
            popupMenu.Items.Width = 200;
        }

        private void fctb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.K | Keys.Control))
            {
                //forced show (MinFragmentLength will be ignored)
                popupMenu.Show(true);
                e.Handled = true;
            }
        }
    }
}
