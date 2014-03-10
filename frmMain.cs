using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepadder
{
    using Extensions;
    public partial class frmMain : Form
    {
        private OpenFileDialog openDialog = new OpenFileDialog();
        private SaveFileDialog saveDialog = new SaveFileDialog();

        private frmFind findDialog = null;
        private frmReplace replaceDialog = null;

        protected string FileName = String.Empty;
        public frmMain()
        {
            InitializeComponent();

            tbxContent.CaretChanged += tbxContent_CaretChanged;
            tbxContent.TextSelected += tbxContent_TextSelected;
            //UpdateStatuLabel();

            appTimer.Start();

            LoadOption();

            string filter = "Text Files|*.txt|All Files|*.*";
            openDialog.Filter = filter;
            saveDialog.Filter = filter;
        }

        void tbxContent_TextSelected(object sender, EventArgs e)
        {
            var isSelected = tbxContent.SelectionLength > 0;
            cutToolStripMenuItem.Enabled = isSelected;
            copyToolStripMenuItem.Enabled = isSelected;
            deleteToolStripMenuItem.Enabled = isSelected;
        }

        void tbxContent_CaretChanged(object sender, CarentChangedEventArgs e)
        {
            Point position = tbxContent.GetCaretPosition();
            string pos = String.Format("Ln {0}, Col {1}", position.Y, position.X);
            toolStripStatusLabelPosition.Text = pos;
        }

        //private void UpdateStatuLabel(object sender = null, EventArgs e = null)
        //{

        //}

        private void LoadOption()
        {
            bool wordwrap = SettingServices.WordWrap;
            wordWrapToolStripMenuItem.Checked = wordwrap;
            tbxContent.WordWrap = wordwrap;

            bool showStatusBar = SettingServices.ShowStatusBar;
            statusBarToolStripMenuItem.Checked = SettingServices.ShowStatusBar;
            statusStrip.Visible = showStatusBar;

            tbxContent.Font = SettingServices.Font;

            toolStripStatusLabelPosition.Text = String.Empty;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tbxContent.IsDirty)
            {
                var result = PromptMaySaveOrNot();

                if (result == DialogResult.No)
                {
                    // do nothing, go next step
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            tbxContent.Text = String.Empty;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tbxContent.IsDirty)
            {
                var result = PromptMaySaveOrNot();

                if (result == DialogResult.No)
                {
                    // do nothing, go next step
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }

            var dlgResult = openDialog.ShowDialog();
            if (dlgResult == System.Windows.Forms.DialogResult.OK)
            {
                var fileName = openDialog.FileName;

                var content = FileServices.ReadContent(fileName);

                // TODO: why this not work????
                //tbxContent = new DirtyCheckingTextbox(content);
                tbxContent.Text = content;
                FileName = fileName;

                this.Text = FileServices.GetFileName(fileName) + " - Notepadder";
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            // if has not opened a file
            if (String.IsNullOrEmpty(FileName))
            {
                SaveAs();
            }
            else
            {
                FileServices.SaveContent(FileName, tbxContent.Text);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void SaveAs()
        {
            var result = saveDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string path = saveDialog.FileName;
                string content = tbxContent.Text;

                FileServices.SaveContent(path, content);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tbxContent.IsDirty)
            {
                var result = PromptMaySaveOrNot();

                if (result == DialogResult.No)
                {
                    // do nothing, go next step
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }

        }

        private DialogResult PromptMaySaveOrNot()
        {
            var result = MessageBox.Show("Do you want to save yoru document?",
                                            "Save Document", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Save();
            }

            return result;
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var isChecked = wordWrapToolStripMenuItem.Checked;
            tbxContent.WordWrap = isChecked;
            SettingServices.WordWrap = isChecked;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            dlg.ShowEffects = false;
            dlg.Font = SettingServices.Font;

            var result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                tbxContent.Font = dlg.Font;
                SettingServices.Font = dlg.Font;
            }

        }

        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var isChecked = statusBarToolStripMenuItem.Checked;
            statusStrip.Visible = isChecked;
            SettingServices.ShowStatusBar = isChecked;
        }

        private void aboutNotepadderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmAbout about = new frmAbout())
            {
                about.ShowDialog();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbxContent.SelectAll();
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO :Check the format
            tbxContent.SelectedText = DateTime.Now.ToString();
        }

        private void tbxContent_TextChanged(object sender, EventArgs e)
        {
            if (tbxContent.CanUndo)
            {
                undoToolStripMenuItem.Enabled = true;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbxContent.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyText();
            DeleteText();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyText();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbxContent.SelectedText = Clipboard.GetText();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbxContent.SelectedText = "";
        }


        private void CopyText(string value)
        {
            Clipboard.SetText(value);
        }

        private void CopyText()
        {
            if (tbxContent.SelectionLength <= 0)
            {
                return;
            }

            Clipboard.SetText(tbxContent.SelectedText);
        }

        // TODO : CHecking this at episode 11
        private void DeleteText()
        {
            if (tbxContent.SelectionLength <= 0)
            {
                return;
            }

            var selectionStart = tbxContent.SelectionStart;
            var text = tbxContent.Text;

            tbxContent.Text = text.Remove(selectionStart, tbxContent.SelectionLength);
        }

        private void DeleteText(int startIndex, int length)
        {
            var text = tbxContent.Text;
            tbxContent.Text = text.Remove(startIndex, tbxContent.SelectionLength);
        }

        private void appTimer_Tick(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                pasteToolStripMenuItem.Enabled = true;
            }

            var hasText = tbxContent.Text.Length > 0;
            findToolStripMenuItem.Enabled = hasText;
            findNextToolStripMenuItem.Enabled = hasText;
            goToToolStripMenuItem.Enabled = hasText;

        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (findDialog == null)
            {
                findDialog = new frmFind(tbxContent);
            }

            findDialog.Show(this);
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (findDialog == null)
            {
                findDialog = new frmFind(tbxContent);
                findDialog.Show(this);
                return;
            }

            findDialog.Search();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (replaceDialog == null)
            {
                replaceDialog = new frmReplace(tbxContent);
            }

            replaceDialog.Show(this);
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            var pos = tbxContent.GetCaretPosition();
            using (frmGoTo dlg = new frmGoTo(pos.Y))
            {
                var result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var lineNumber = dlg.LineNumber;

                    if (lineNumber < 0)
                    {
                        MessageBox.Show(
                            "Line number must be greater than 0",
                            "Line Number Too Small",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                    // verify the linenumber is valid
                    string[] lines = tbxContent.Text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    int totalLines = lines.Length;
                    if (lineNumber > totalLines)
                    {
                        MessageBox.Show(
                            "Line number must be greater than " + totalLines.ToString(),
                            "Line Number Too Small",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        return;
                    }

                    var index = tbxContent.GetFirstCharIndexFromLine(lineNumber - 1);
                    tbxContent.SelectionStart = index;
                }
            }
        }

    }
}
