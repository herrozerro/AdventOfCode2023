using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode2023.Utilities;

namespace AdventOfCode2023.Days;

public static class Day03
{
    
    public static void SolvePart1(bool isTest = false)
    {
        var filename = $"Data/Day03{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        
        var entities = Entities.ParseEntities(input);
        var nums = entities.Where(x => x.IsSymbol == false);
        var sym = entities.Where(x => x.IsSymbol).SelectMany(y=>y.Positions).ToList();
        int total = 0;
        
        foreach (var e in nums)
        {
            if (e.Positions.Any(x => sym.Any(y => IsAdjecent(x.Key, x.Value, y.Key, y.Value))))
            {
                Console.WriteLine($"Is Part Number {e.Id}");
                total += int.Parse(e.Id);
            }
            else
            {
                Console.WriteLine($"Is Not Part Number {e.Id}");
            }
        }
        
        Console.WriteLine($"Day03 Part1 {(isTest ? "Test" : "")}: {total}");
    }

    public static void SolvePart2(bool isTest = false)
    {
        var filename = $"Data/Day03{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        
        var entities = Entities.ParseEntities(input);
        var nums = entities.Where(x => x.IsSymbol == false);
        var sym = entities.Where(x => x is { IsSymbol: true, Id: "*" }).ToList();
        int total = 0;


        foreach (var s in sym)
        {
            var gears = nums.Where(x=>x.Positions.Any(y=>IsAdjecent(y.Key, y.Value, s.Positions[0].Key, s.Positions[0].Value))).ToList();
            if (gears.Count > 1)
            {
                total += gears.Aggregate(1, (x,y) => x * int.Parse(y.Id));
            }
        }
        
        Console.WriteLine($"Day03 Part2 {(isTest ? "Test" : "")}: {total}");
    }

    public static bool IsAdjecent(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x1 - x2) <= 1 && Math.Abs(y1 - y2) <= 1;
    }
}



public class Entities
{
    public string Id { get; set; }
    public List<KeyValuePair<int,int>> Positions { get; set; }
    public bool IsSymbol { get; set; } = true;
    public static List<Entities> ParseEntities(List<string> entityStrings)
    {
        var entities = new List<Entities>();
        for (int l = 0; l < entityStrings.Count; l++)
        {
            StringBuilder num = new StringBuilder();
            List<KeyValuePair<int, int>> pos = new();
            bool parsingNum = false;
            
            for (int i = 0; i < entityStrings[l].Length; i++)
            {
                switch (entityStrings[l][i])
                {
                    case '.':
                        if (parsingNum)
                        {
                            var e = new Entities(){Id = num.ToString(), Positions = new List<KeyValuePair<int, int>>(pos), IsSymbol = false};
                            entities.Add(e);
                            pos.Clear();
                            num.Clear();
                            parsingNum = false;
                        }
                        break;
                    case var n when Char.IsDigit(entityStrings[l][i]):
                        num.Append(n);
                        pos.Add(new KeyValuePair<int, int>(l, i));
                        parsingNum = true;
                        break;
                    default:
                        if (parsingNum)
                        {
                            var e = new Entities(){Id = num.ToString(), Positions = new List<KeyValuePair<int, int>>(pos), IsSymbol = false};
                            entities.Add(e);
                            pos.Clear();
                            num.Clear();
                            parsingNum = false;
                        }

                        entities.Add(new Entities(){Id = entityStrings[l][i].ToString(), Positions = new List<KeyValuePair<int, int>> {new(l,i)}});

                        break;
                }
            }
            if (parsingNum)
            {
                var e = new Entities(){Id = num.ToString(), Positions = new List<KeyValuePair<int, int>>(pos), IsSymbol = false};
                entities.Add(e);
                pos.Clear();
                num.Clear();
                parsingNum = false;
            }
            
        }

        return entities;
    }
}