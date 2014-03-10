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
    public partial class frmFind : Form
    {
        protected TextBox TextBoxToSearch;

        public frmFind()
        {
            InitializeComponent();
        }

        public frmFind(TextBox textbox) : this()
        {
            TextBoxToSearch = textbox;
        }

        private void tbxSearchTerm_TextChanged(object sender, EventArgs e)
        {
            var hasText = tbxSearchTerm.Text.Length > 0;

            btnFind.Enabled = hasText;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Search();
        }

        public void Search()
        {
            string term = tbxSearchTerm.Text,
                    doc = TextBoxToSearch.Text;

            var startIndex = TextBoxToSearch.SelectionStart;

            StringComparison matchCase = chkMatchCase.Checked ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            var index = -1;

            if (radioButtonDown.Checked)
            {
                startIndex += TextBoxToSearch.SelectionLength;
                index = doc.IndexOf(term, startIndex);
            }
            else if (radioButtonUp.Checked)
            {
                if (startIndex != 0)
                {
                    startIndex -= TextBoxToSearch.SelectionLength;
                    index = doc.LastIndexOf(term, startIndex);
                }
            }

            if (index > -1)
            {
                TextBoxToSearch.Focus();
                TextBoxToSearch.Select(index, term.Length);
            }
            else
            {
                MessageBox.Show(String.Format(@"Cannot find ""{0}""", term));
            }
        }
    }
}
