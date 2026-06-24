using System;
using System.IO;
using System.Net.NetworkInformation;
namespace program;
enum ReportType  {Collect, Analyze, Recon, Intel}
enum ReportStatus { Rejected, Approved, Pending }
class files_system
{
    static List<string> loadFile(string path)
    {
        if (!File.Exists(path))
        {
            string[] nemeFile = path.Split('\\');

            Console.WriteLine($"Eror: File {nemeFile[nemeFile.Length - 1]}  not found");
            return [];
        }

        string[] allLines = File.ReadAllLines(path);
        List<string> resultAllLines = new List<string>();
        foreach (string line in allLines)
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
        if (!int.TryParse(myline[2], out priority))
        {
            Console.WriteLine($"invalid record");//todo
            return false;
        }
        if (priority < 1 || priority > 5)
        {
            Console.WriteLine($"invalid record out of range");//todo
            return false;
        }
        double score;
        if (!double.TryParse(myline[3], out score))
        {
            Console.WriteLine($"invalid record");//todo
            return false;
        }
        if (score < 0.0 || score > 100.0)
        {
            Console.WriteLine($"invalid score out of range");
            return false;
        }

        ReportType repoT;
        if (!ReportType.TryParse(myline[1], true, out repoT))
        {
            Console.WriteLine("reson : its invalid type");
            return false;
        }
        ReportStatus repoS;
        if (!ReportStatus.TryParse(myline[4], true, out repoS))
        {
            Console.WriteLine("invalid record Unkown ");
            return false;
        }
        Console.WriteLine("valid record process");
        return true;
    }

    static int ProcessReports(List<string> myLines, string[] UnitName, string[] ReportType, int[] Priority, double[] Score, string[] Status, ref int numberValidLines, ref int numbeInalidLines)

    {

        foreach (string line in myLines)
        {
            string[] myLine = processLine(line);
            if (countLine(myLine) && validConvert(myLine))
            {
                int priority;
                int.TryParse(myLine[2], out priority);
                double score;
                double.TryParse(myLine[3], out score);
                UnitName[numberValidLines] = myLine[0];
                ReportType[numberValidLines] = myLine[1];
                Priority[numberValidLines] = priority;
                Score[numberValidLines] = score;
                Status[numberValidLines] = myLine[4];
                numberValidLines++;




            }
            else
            {
                numbeInalidLines++;
            }

        }
        return numberValidLines;
    }
    static double CalculateAverege(double[] score, int numberValid)
    {
        double sumi = 0;
        foreach (double numi in score)
        {
            sumi += numi;
        }
        return sumi / numberValid;
    }
    static double findMaxScore(double[] score, int numbervalid)
    {
        double maxi = score[0];
        for (int i = 0; i < numbervalid; i++)
        {
            if (score[i] > maxi)
            {
                maxi = score[i];
            }

        }
        return maxi;
    }
    static double findMinScore(double[] score, int numberValid)
    {
        {
            double mini = score[0];
            for (int i = 0; i < numberValid; i++)
            {
                if (score[i] < mini)
                {
                    mini = score[i];
                }

            }
            return mini;
        }
    }
    static int CountByStatus(string[] statuses,string status)
    {
        int sumStatus = 0;
        foreach(string stri in statuses)
        {
            if (stri.ToLower() == status.ToLower())
            {
                sumStatus += 1;
            }
        }
        return sumStatus;
    }
    static int CountByType(string[] types, string type)
    {
        int sumType = 0;
        foreach (string stri in types)
        {
            if (stri.ToLower() == type.ToLower())
            {
                sumType += 1;
            }
        }
        return sumType;
    }
    static void DisplayBasicStatistcs(double[] score,int numberValid)
    {
        Console.WriteLine("=== Report Statistics ===");
        Console.WriteLine($"number of valid {numberValid}");
        double avg = CalculateAverege(score, numberValid);
        Console.WriteLine($"avg is {avg}");
        double maxi = findMaxScore(score, numberValid);
        Console.WriteLine($" high score  is {maxi}");
        double mini = findMinScore(score, numberValid);
        Console.WriteLine($" min score is {mini}");
    }
    static void DisplayStatusCounts(string[] statuses,int numberVaklid)
    {
        int counterStatus = CountByStatus(statuses, "Rejected");
        Console.WriteLine($"count of status is {counterStatus}");
        int counterStatus1 = CountByStatus(statuses, "Approved");
        Console.WriteLine($"count of status is {counterStatus1}");
        int counterStatus2 = CountByStatus(statuses, "Pending");
        Console.WriteLine($"count of status is {counterStatus2}");
    }
    static void displayTypeCount(string[] types,int numberValid)
    {
        int counterType = CountByType(types, "Collect");
        Console.WriteLine($"count of type is {counterType}");
        int counterType1 = CountByType(types, "Analyze");
        Console.WriteLine($"count of type is {counterType1}");
        int counterType2 = CountByType(types, "Recon");
        Console.WriteLine($"count of type is {counterType2}");
        int counterType3 = CountByType(types, "Intel");
        Console.WriteLine($"count of type is {counterType3}");
    }
    static void Main()
        {
            string path = @"..\..\..\reports.txt";
            List<string> allFiles = loadFile(path);
            int counter_files = allFiles.Count;
            if (counter_files > 0)
            {
                Console.WriteLine($"file loaded :{counter_files} lines found ");
                int numberValidLines = 0;
                int numberInvalidLines = 0;
                string[] myLine1 = processLine("Alpha,  Collect,3,87.5,Approved");
                string[] UnitName = new string[100];
                string[] ReportType = new string[100];
                int[] Priority = new int[100];
                double[] Score = new double[100];
                string[] Status = new string[100];
                numberValidLines = ProcessReports(allFiles, UnitName, ReportType, Priority, Score, Status, ref numberValidLines, ref numberInvalidLines);
                Console.WriteLine($"number  of valid records for analisis {numberValidLines}");
                Console.WriteLine($"number  of invalid records for analisis {numberInvalidLines}");
               
                int counterStatus = CountByStatus(Status, "Rejected");
                Console.WriteLine($"count of status is {counterStatus}");
               





        }





        }
    
}
