using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml;
using AdventOfCode2023.Utilities;

namespace AdventOfCode2023.Days;

public static class Day02
{
    
    public static void SolvePart1(bool isTest = false)
    {
        var filename = $"Data/Day02{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);

        var games = ParseGame(input);

        var validGames = games.Where(x => x.Pulls.TrueForAll(y => y.ReadIsValid())).Sum(z => z.Id);
        
        Console.WriteLine(validGames);
    }

    private static List<Game> ParseGame(List<string> gameStrings)
    {
        var games = new List<Game>();
        //parse into games "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
        foreach (var game in gameStrings)
        {
            var gameNumber = Regex.Match(game, @"Game (\d+):");
            var pulls = game.Split(':')[1].Split(';');
            var gamePulls = new List<Pull>();
            foreach (var pull in pulls)
            {
                var p = new Pull();
                var balls = pull.Split(',');
                foreach (var ball in balls)
                {
                    var ballSplit = ball.Trim().Split(' ');
                    p.Balls.Add(ballSplit[1], int.Parse(ballSplit[0]));
                }
                gamePulls.Add(p);
            }
            games.Add(new Game(){Id = int.Parse(gameNumber.Groups[1].Value), Pulls = gamePulls});
        }

        return games;
    }
    
    public static void SolvePart2(bool isTest = false)
    {
        var filename = $"Data/Day02{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        
        var games = ParseGame(input);

        Console.WriteLine(games.Sum(x=>x.ReadPower()));
    }
}

public class Game
{
    public int Id { get; set; }
    
    private int MinRed { get; set; }
    private int MinBlue { get; set; }
    private int MinGreen { get; set; }

    public int ReadPower()
    {
        MinRed = Pulls.SelectMany(x=>x.Balls).Max(y=>y.Key == "red" ? y.Value : 0);
        MinBlue = Pulls.SelectMany(x=>x.Balls).Max(y=>y.Key == "blue" ? y.Value : 0);
        MinGreen = Pulls.SelectMany(x=>x.Balls).Max(y=>y.Key == "green" ? y.Value : 0);
        return MinBlue * MinGreen * MinRed;
    }
    
    public List<Pull> Pulls { get; set; } = new();
}

public class Pull
{
    public bool ReadIsValid()
    {
        bool valid = true;
        
        Balls.TryGetValue("red", out var red);
        Balls.TryGetValue("blue", out var blue);
        Balls.TryGetValue("green", out var green);
        valid = red <= 12 && green <= 13 && blue <= 14;
        
        return valid;
    }
    public Dictionary<string, int> Balls { get; set; } = new();
}
