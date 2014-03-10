using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Notepadder
{
    public partial class frmReplace : Notepadder.frmFind
    {
        public frmReplace()
        {
            InitializeComponent();  
        }
        public frmReplace(TextBox textbox) : this()
        {
            TextBoxToSearch = textbox;
        }

        private void tbxSearchTerm_TextChanged(object sender, EventArgs e)
        {
            var hasText = tbxSearchTerm.Text.Length > 0;

            btnReplace.Enabled = hasText;
            btnReplaceAll.Enabled = hasText;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (TextBoxToSearch.SelectedText == String.Empty)
            {
                base.Search();
                return;
            }
        
            TextBoxToSearch.SelectedText = tbxReplace.Text;

            base.Search();
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            int startIndex = 0;
            bool canReplace = true;

            while (canReplace)
            {
                var doc = TextBoxToSearch.Text;

                var indexOf = doc.IndexOf(tbxSearchTerm.Text, startIndex);

                if (indexOf > -1)
                {
                    TextBoxToSearch.Select(indexOf, tbxSearchTerm.Text.Length);
                    TextBoxToSearch.SelectedText = tbxReplace.Text;
                    startIndex += TextBoxToSearch.SelectionLength;
                }
                else
                {
                    canReplace = false;
                }
            }
        }
    }
}
