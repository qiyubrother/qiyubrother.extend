/*
 * 公司名称：北京麒宇兄弟科技有限公司
 * 模块功能：
 * 创建日期：2023-07-05
 * 修改日期：2023-07-05
 * 作    者：刘振华
 * 电子邮箱：13240137763@163.com
 */
using System.Reflection;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Drawing;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tester
{
    public partial class AutocompleteSample3 : Form
    {
        AutocompleteMenu popupMenu;

        public AutocompleteSample3()
        {
            InitializeComponent();

            //create autocomplete popup menu
            popupMenu = new AutocompleteMenu(fctb);
            popupMenu.ForeColor = Color.White;
            popupMenu.BackColor = Color.Gray;
            popupMenu.SelectedColor = Color.Purple;
            popupMenu.SearchPattern = @"[\w\.]";
            popupMenu.AllowTabKey = true;
            popupMenu.AlwaysShowTooltip = true;
            //assign DynamicCollection as items source
            popupMenu.Items.SetAutocompleteItems(new DynamicCollection(popupMenu, fctb));
        }
    }

    /// <summary>
    /// Builds list of methods and properties for current class name was typed in the textbox
    /// </summary>
    internal class DynamicCollection : IEnumerable<AutocompleteItem>
    {
        private AutocompleteMenu menu;
        private FastColoredTextBox tb;

        public DynamicCollection(AutocompleteMenu menu, FastColoredTextBox tb)
        {
            this.menu = menu;
            this.tb = tb;
        }

        public IEnumerator<AutocompleteItem> GetEnumerator()
        {
            //get current fragment of the text
            var text = menu.Fragment.Text;

            //extract class name (part before dot)
            var parts = text.Split('.');
            if (parts.Length < 2)
                yield break;
            var className = parts[parts.Length - 2];

            //find type for given className
            var type = FindTypeByName(className);

            if (type == null)
                yield break;

            //return static methods of the class
            foreach (var methodName in type.GetMethods().AsEnumerable().Select(mi => mi.Name).Distinct())
                yield return new MethodAutocompleteItem(methodName + "()")
                {
                    ToolTipTitle = methodName,
                    ToolTipText = "Description of method " + methodName + " goes here.",
                };

            //return static properties of the class
            foreach (var pi in type.GetProperties())
                yield return new MethodAutocompleteItem(pi.Name)
                {
                    ToolTipTitle = pi.Name,
                    ToolTipText = "Description of property " + pi.Name + " goes here.",
                };
        }

        Type FindTypeByName(string name)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Type type = null;
            foreach (var a in assemblies)
            {
                foreach (var t in a.GetTypes())
                    if (t.Name == name)
                    {
                        return t;
                    }
            }

            return null;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
