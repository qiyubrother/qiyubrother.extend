/*
 * 公司名称：北京麒宇兄弟科技有限公司
 * 模块功能：
 * 创建日期：2023-07-05
 * 修改日期：2023-07-05
 * 作    者：刘振华2023-07-05
 * 电子邮箱：13240137763@163.com2023-07-05
 */
using System.Drawing;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;

namespace Tester
{
    public partial class CustomStyleSample : Form
    {
        //create my custom style
        EllipseStyle ellipseStyle = new EllipseStyle();

        public CustomStyleSample()
        {
            InitializeComponent();
        }

        private void fctb_TextChanged(object sender, TextChangedEventArgs e)
        {
            //clear old styles of chars
            e.ChangedRange.ClearStyle(ellipseStyle);
            //append style for word 'Babylon'
            e.ChangedRange.SetStyle(ellipseStyle, @"\bBabylon\b", RegexOptions.IgnoreCase);
        }
    }

    /// <summary>
    /// This style will drawing ellipse around of the word
    /// </summary>
    class EllipseStyle : Style
    {
        public override void Draw(Graphics gr, Point position, Range range)
        {
            //get size of rectangle
            Size size = GetSizeOfRange(range);
            //create rectangle
            Rectangle rect = new Rectangle(position, size);
            //inflate it
            rect.Inflate(2, 2);
            //get rounded rectangle
            var path = GetRoundedRectangle(rect, 7);
            //draw rounded rectangle
            gr.DrawPath(Pens.Red, path);
        }
    }
}
