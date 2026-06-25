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
            Console.WriteLine($"invalid record");
            return false;
        }
        if (priority < 1 || priority > 5)
        {
            Console.WriteLine($"invalid record out of range");
            return false;
        }
        double score;
        if (!double.TryParse(myline[3], out score))
        {
            Console.WriteLine($"invalid record");
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

    static int ProcessReports(List<string> myLines, string[] UnitName, ReportType[] types, int[] Priority, double[] Score, ReportStatus[] Status,  int numberValidLines, ref int numbeInalidLines)

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
        for (int i = 0; i < numberValid; i++)
        {
            sumi += score[i];
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
    static int CountByStatus(ReportStatus[] statuses, string status, int number)
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
    static int CountByType(ReportType[] types, string type,int numValid)
    {
        int sumType = 0;
        for (int i = 0; i < numValid ; i++)
        {
            if (types[i].ToString().ToLower() == type.ToLower())
            {
                sumType += 1;
            }
      
           
        }
        return sumType;
    }
    static void DisplayBasicStatistcs(double[] score, int numberValid)
    {
        Console.WriteLine("=== Report Statistics ===");
        Console.WriteLine($"number of valid {numberValid}\n");
        double avg = CalculateAverege(score, numberValid);
        Console.WriteLine($"avg is {avg.ToString("F2")}\n");
        double maxi = findMaxScore(score, numberValid);
        Console.WriteLine($"high score  is {maxi}\n");
        double mini = findMinScore(score, numberValid);
        Console.WriteLine($"min score is {mini}\n");
    }
    static void DisplayStatusCounts(ReportStatus[] statuses, int numberVaklid)
    {
        Console.WriteLine("=== Reports by Status ===");
        int counterStatus = CountByStatus(statuses, "Rejected", numberVaklid);
        Console.WriteLine($"Rejected {counterStatus}");
        int counterStatus1 = CountByStatus(statuses, "Approved", numberVaklid);
        Console.WriteLine($"Approved: {counterStatus1}");
        int counterStatus2 = CountByStatus(statuses, "Pending", numberVaklid);
        Console.WriteLine($"Pending {counterStatus2}/n");
        Console.WriteLine();
    }
    static void displayTypeCountReportType(ReportType[] types, int numberValid)
    {
        Console.WriteLine("=== Reports by Type ===");
        int counterType = CountByType(types, "Collect",numberValid);
        Console.WriteLine($"Collect: {counterType}");
        int counterType1 = CountByType(types, "Analyze",numberValid);
        Console.WriteLine($"Analyze: {counterType1}");
        int counterType2 = CountByType(types, "Recon",numberValid);
        Console.WriteLine($"Recon: {counterType2}");
        int counterType3 = CountByType(types, "Intel",numberValid);
        Console.WriteLine($"Intel: {counterType3}\n");
    }
    static void DisplayHighestPriorityApproved(string[] UnitName, ReportType[] types, int[] Priority, double[] Score, ReportStatus[] Status, int numList)
    {
        int maxi = -1;
        int index = -1;

        for (int i = 0; i < numList; i++)
        {
            if (Status[i] == ReportStatus.Approved && Priority[i] > maxi)
            {
                index = i;
                maxi = Priority[i];
            }
        }
        if(index != -1) { 
        Console.WriteLine("=== Highest Priority Approved Report ===");
        Console.WriteLine($"unit : {UnitName[index]}");
        Console.WriteLine($"type : {types[index]}");
        Console.WriteLine($"priority: {Priority[index]}");
        Console.WriteLine($"Score :{Score[index]}\n ");
    }
    }
    static void DisplayAverageByPriority(int[] Priority, double[] Score, int numValid)
    {
        int[] counter = new int[6];
        double[] sumi = new double[6];
        for (int i = 0; i < numValid; i++)
        {
            counter[Priority[i]] += 1;
            sumi[Priority[i]] += Score[i];

        }
        Console.WriteLine("=== Average Score by Priority ===");
        for (int i = 1; i < counter.Length; i++)
        {
            Console.WriteLine($" priority {i} : {(counter[i] != 0 ? (sumi[i] / counter[i]).ToString("F2") : "no reports")}");

        }
        Console.WriteLine();
    }
    static void Main()
    {
      
            string path = @"..\..\..\reports.txt";
            List<string> allFiles = loadFile(path);
            int counter_files = allFiles.Count;
            if (counter_files > 0)
            {

                int numberValidLines = 0;
                int numberInvalidLines = 0;
                string[] UnitName = new string[100];
                ReportType[] ReportType = new ReportType[100];
                int[] Priority = new int[100];
                double[] Score = new double[100];
                ReportStatus[] Status = new ReportStatus[100];
                numberValidLines = ProcessReports(allFiles, UnitName, ReportType, Priority, Score, Status,  numberValidLines, ref numberInvalidLines);
                Console.WriteLine($"file loaded :{counter_files} lines found ");
                Console.WriteLine("Processing complete ");
                Console.WriteLine($"valid records  {numberValidLines}");
                Console.WriteLine($"invalid records  {numberInvalidLines}");
                Console.WriteLine($"Stored {numberValidLines} valid records for analysis\n");

                DisplayBasicStatistcs(Score, numberValidLines);
                DisplayStatusCounts(Status, numberValidLines);
                displayTypeCountReportType(ReportType, numberValidLines);

                DisplayHighestPriorityApproved(UnitName, ReportType, Priority, Score, Status, numberValidLines);
                DisplayAverageByPriority(Priority, Score, numberValidLines);



            }





        }

    }

