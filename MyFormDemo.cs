using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepadder
{
    public class MyFormDemo : Form
    {
        public MyFormDemo()
        {
            this.Text = "Hello world";

            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem fileitem = new ToolStripMenuItem("&File");
            ToolStripMenuItem newitem = new ToolStripMenuItem("&New");
            ToolStripMenuItem openitem = new ToolStripMenuItem("&Open");

            openitem.Click += openitem_Click;

            this.Controls.Add(menu);

            menu.Items.Add(fileitem);

            fileitem.DropDownItems.Add(newitem);
            fileitem.DropDown.Items.Add(openitem);
        }

        void openitem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");    
        }
    }

}
