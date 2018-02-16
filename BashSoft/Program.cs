using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    class Launcher
    {
        static void Main()
        {
            while (true)
            {
                IOManager.TraverseDirectory(1);

                string userInput = Console.ReadLine();

                IOManager.ChangeCurrentDirectoryRelative(userInput);
            }

        }
    }
}
