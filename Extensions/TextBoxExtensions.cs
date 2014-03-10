using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepadder.Extensions
{
    public static class TextBoxExtensions
    {
        public static Point GetCaretPosition(this TextBox textBox)
        {
            int startIndex = textBox.SelectionStart;
            int line = textBox.GetLineFromCharIndex(startIndex);
            int column = startIndex - textBox.GetFirstCharIndexFromLine(line);
            return new Point(column + 1, line + 1);
        }
    }
}
