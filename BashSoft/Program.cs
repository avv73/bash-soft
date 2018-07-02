
using BashSoft.Contracts;
using BashSoft.IO;
using BashSoft.Judge;
using BashSoft.Repository;

namespace BashSoft
{
    class Launcher
    {
        static void Main()
        {
            IContentComparer tester = new Tester();
            IDirectoryManager ioManager = new IOManager();
            IDatabase repo = new StudentsRepository(new RepositorySorter(), new RepositoryFilter());

            IInterpreter currentInterpreter = new CommandInterpreter(tester, repo, ioManager);
            IReader reader = new InputReader(currentInterpreter);

            reader.StartReadingCommands();
        }
    }

}
