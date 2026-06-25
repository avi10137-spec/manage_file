using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
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

    static int ProcessReports(List<string> myLines, string[] UnitName, ReportType[] types, int[] Priority, double[] Score, ReportStatus[] Status, ref int numberValidLines, ref int numbeInalidLines)

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
                ReportType.TryParse(myLine[1], true, out ReportType Repot);
                types[numberValidLines] = Repot;
                Priority[numberValidLines] = priority;
                Score[numberValidLines] = score;
                ReportStatus.TryParse(myLine[4], true, out ReportStatus ReporS);
                Status[numberValidLines] = ReporS;
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
    static int CountByStatus(ReportStatus[] statuses,string status,int number)
    {
        int sumStatus = 0;
        for (int i = 0; i < number; i++)
        {
            if (statuses[i].ToString().ToLower() == status.ToLower())
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
        Console.WriteLine($"avg is {avg.ToString("F2")}");
        double maxi = findMaxScore(score, numberValid);
        Console.WriteLine($" high score  is {maxi}");
        double mini = findMinScore(score, numberValid);
        Console.WriteLine($" min score is {mini}");
    }
    static void DisplayStatusCounts(ReportStatus[] statuses,int numberVaklid)
    {
        int counterStatus = CountByStatus(statuses, "Rejected",numberVaklid);
        Console.WriteLine($"count of status is {counterStatus}");
        int counterStatus1 = CountByStatus(statuses, "Approved",numberVaklid);
        Console.WriteLine($"count of status is {counterStatus1}");
        int counterStatus2 = CountByStatus(statuses, "Pending",numberVaklid);
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
    static void DisplayHighestPriorityApproved(string[] UnitName, ReportType[] types, int[] Priority, double[] Score, ReportStatus[] Status,int numList)
    {
        int maxi = Priority[0];
        int index = 0;

        for (int i = 0; i < numList; i++)
        {
         if (Status[i] == ReportStatus.Approved && Priority[i] > maxi)
            {
                index = i;
                maxi = Priority[i];
            }
        }
        Console.WriteLine("=== Highest Priority Approved Report ===");
        Console.WriteLine($"unit : {UnitName[index]}");
        Console.WriteLine($"type : {types[index]}");
        Console.WriteLine($"priority: {Priority[index]}");
        Console.WriteLine($"Score :{Score[index]} ");
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
                ReportType[] ReportType = new ReportType[100];
                int[] Priority = new int[100];
                double[] Score = new double[100];
                ReportStatus [] Status = new ReportStatus[100];
                numberValidLines = ProcessReports(allFiles, UnitName, ReportType, Priority, Score, Status, ref numberValidLines, ref numberInvalidLines);
                Console.WriteLine($"number  of valid records for analisis {numberValidLines}");
                Console.WriteLine($"number  of invalid records for analisis {numberInvalidLines}");
               
               
                
                DisplayStatusCounts(Status, numberValidLines);
            DisplayHighestPriorityApproved(UnitName, ReportType, Priority, Score, Status, numberValidLines);




        }





        }
    
}
