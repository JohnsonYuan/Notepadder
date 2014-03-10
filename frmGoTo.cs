using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepadder
{
    public partial class frmGoTo : Form
    {
        public int LineNumber { get; private set; }
        public frmGoTo(int lineNumber)
        {
            InitializeComponent();

            tbxLine.Text = lineNumber.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            int lineNumber = 0;
            if (Int32.TryParse(tbxLine.Text, out lineNumber))
            {
                this.LineNumber = lineNumber;
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("You must enter number!", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }
    }
}
