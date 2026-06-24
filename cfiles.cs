using System;
using System.IO;
namespace program;

class files_system
{
    static List<string> loadFile (string path)
    {
        if (!File.Exists(path))
        {
            string[] nemeFile = path.Split('\\');
            
            Console.WriteLine($"Eror: File {nemeFile[nemeFile.Length-1]}  not found");
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

    static string[] processLine(string line)
    {
        string[] myLine = line.Split(',', StringSplitOptions.TrimEntries);
        {        
            return myLine;
        }
        return [];

    }

    static bool countLine(string[] myline)
    {
        if (myline.Length == 5)
        {
            return true;
        }
        return false;
    }
    static bool validConvert(string[] myline)
    {
        int priority;
        if (!int.TryParse(myline[2], out priority) || (priority < 1 & priority > 5))
        {
            Console.WriteLine($" {myline[2]} invalid record");//todo
            return false;
        }
        double score;
        if (!double.TryParse(myline[3], out score) || (score < 0.0 & priority > 100.0))
        {
            Console.WriteLine($" {myline[3]} invalid record");//todo
            return false;
        }
        Console.WriteLine("valid record process");
        return true;
    }

    //static int ProcessReports(List<string>)
    //{

    //}
    static void Main()
    {
        string path = @"..\..\..\reports.txt";
        List<string> allFiles = loadFile(path);
        int counter_files = allFiles.Count;
        if (counter_files > 0)
        {
            Console.WriteLine($"file loaded :{counter_files} lines found ");

                string[] myLine1 = processLine("Alpha,  Collect,3,87.5,Approved");











        }
        

     


    }
}
