using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notepadder
{
    public class FileServices
    {
        public static string ReadContent(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                // TODO: do something meaningful
                throw new NotImplementedException();
            }

            using (StreamReader reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }

        public static void SaveContent(string path, string content)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(content);
            }
        }

        internal static string GetFileName(string path)
        {
            int startIndex = path.LastIndexOf('\\');
            int endIndex = path.LastIndexOf('.');
            return path.Substring(startIndex + 1, endIndex - startIndex - 1);
        }
    }
}
