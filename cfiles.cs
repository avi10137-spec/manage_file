using System;
using System.IO;
namespace program;

class files_system
{

    static void Main()
    {
        string path = @"..\..\..\reports.txt";
        if (!File.Exists(path))
        {
            Console.WriteLine("file not exist");
        }
    }
}
