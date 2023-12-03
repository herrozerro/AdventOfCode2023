using System.Text.RegularExpressions;
using AdventOfCode2023.Utilities;

namespace AdventOfCode2023.Days;

public static class Day01
{
    
    public static void SolvePart1(bool isTest = false)
    {
        var filename = $"Data/Day01{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        
        var total = 0;
        foreach (var line in input)
        {
            string numberOnly = Regex.Replace(line, "[^0-9.]", "");
            total += int.Parse(numberOnly[0] + numberOnly[numberOnly.Length-1].ToString());
        }
        
        Console.WriteLine(total);
    }

    public static void SolvePart2(bool isTest = false)
    {
        var filename = $"Data/Day01{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        var total = 0;
        
        Dictionary<string, int> dictionary = new Dictionary<string, int>()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };
        
            
        foreach (var line in input)
        {
            var newLine = line;

            // for every key in the dictionary add the first letter of the match to the beginning of the match
            foreach (var (key, value) in dictionary)
            {
                var match = Regex.Matches(newLine, key);
                foreach (Match m in match.Reverse())
                {
                    newLine = newLine.Insert(m.Index, key[0].ToString());
                }
            }
            
            foreach (var (key, value) in dictionary)
            {
                var match = Regex.Matches(newLine, key);
                foreach (Match m in match.Reverse())
                {
                    newLine = newLine.Insert(m.Index, value.ToString());
                }
            }
            

            string numberOnly = Regex.Replace(newLine, "[^0-9.]", "");
            var parseNum = numberOnly[0] + numberOnly[^1].ToString();
            total += int.Parse(parseNum);
            Console.WriteLine($"{line} {newLine} {numberOnly} {parseNum} {total}");
        }
        
        Console.WriteLine(total);
        
    }
}