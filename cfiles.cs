using System;
using System.IO;
namespace program;

class files_system
{
    static List<string> loadFile (string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Eror: File reports.txt not found");
            return [];
        }
        
        string [] allLines = File.ReadAllLines(path);
        List<string> resultAllLines = new List<string>();
        foreach(string line in allLines)
        {
            resultAllLines.Add(line);
        }
        if (resultAllLines.Count == 0)
        {
            Console.WriteLine("Eror: File is empty");
        }
        return resultAllLines;
    }
    static void Main()
    {
        string path = @"..\..\..\reports.txt";
        List<string> allFiles = loadFile(path);
        int counter_files = allFiles.Count;
        if (counter_files > 0)
        {
            Console.WriteLine($"file loaded :{counter_files} lines found ");













        }
        

     


    }
}
