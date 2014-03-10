using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepadder
{
    using Extensions;

    public delegate void CaretChangedHandler(object sender, CarentChangedEventArgs e);
    public delegate void TextSelectedHandler(object sender, EventArgs e);
    public class DirtyCheckingTextbox : TextBox
    {
        public event CaretChangedHandler CaretChanged;
        public event TextSelectedHandler TextSelected;
        public string CleanText { get; private set; }

        public bool IsDirty
        {
            get
            {
                return CleanText != this.Text;
            }
        }

        public DirtyCheckingTextbox() : this(String.Empty)
        {
        }

        public DirtyCheckingTextbox(string initialText)
        {
            this.Text = initialText;
            this.CleanText = this.Text;

            this.Click += DirtyCheckingTextbox_Click;
            this.KeyUp += DirtyCheckingTextbox_KeyUp;
            this.KeyDown += DirtyCheckingTextbox_KeyDown;
        }

        void DirtyCheckingTextbox_Click(object sender, EventArgs e)
        {
            FireTextSelectedEvent();
            FireCaretChangedEvent();
        }
        // only click, keyup fires textselected(most make sense for text selection)
        void DirtyCheckingTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            FireCaretChangedEvent();
        }

        void DirtyCheckingTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            FireTextSelectedEvent();
            FireCaretChangedEvent();
        }

        public void Clean()
        {
            this.CleanText = this.Text;
        }

        protected void FireCaretChangedEvent()
        {
            if (CaretChanged != null)
            {
                CaretChanged(this, new CarentChangedEventArgs(this.GetCaretPosition()));
            }
        }

        protected void FireTextSelectedEvent()
        {
            if (this.SelectionLength <= 0)
            {
                return;
            }

            if (TextSelected != null)
            {
                TextSelected(this, EventArgs.Empty);
            }

        }
    }
}
