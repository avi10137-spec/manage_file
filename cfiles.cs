using System;
using System.IO;
namespace program;

class files_system
{

    static void Main()
    {
        string path = @"C:\Users\User\csfiles\manage_file\reports.txt";
        if (!File.Exists(path))
        {
            Console.WriteLine("file not exist");
        }
    }
}
