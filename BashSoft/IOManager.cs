using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BashSoft
{
    public static class IOManager
    {
        public static void TraverseDirectory(string path)
        {
            OutputWriter.WriteEmptyLine();
            int initialIdentation = path.Split('\\').Length;
            Queue<string> subFolders = new Queue<string>();
            subFolders.Enqueue(path);

            while (subFolders.Count != 0)
            {
                string dequeuedPath = subFolders.Dequeue();
                int identication = dequeuedPath.Split('\\').Length - initialIdentation;

                OutputWriter.WriteMessageOnNewLine(string.Format("{0}{1}", new string('-', identication), dequeuedPath));

                foreach (string directoryPath in Directory.GetDirectories(dequeuedPath))
                {
                    subFolders.Enqueue(directoryPath);
                }
            }

        }

    }
}
