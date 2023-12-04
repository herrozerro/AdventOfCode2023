using System.Text.RegularExpressions;
using AdventOfCode2023.Utilities;

namespace AdventOfCode2023.Days;

public static class Day04
{
    
    public static void SolvePart1(bool isTest = false)
    {
        var filename = $"Data/Day04{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        
        var tickets = Ticket.ParseTickets(input);

        int total = 0;
        
        // for each ticket, check if each number is in the winning numbers, each match doubles the point value
        foreach (var ticket in tickets)
        {
            var ticketTotal = 0;
            foreach (var number in ticket.Numbers)
            {
                if (ticket.WinningNumbers.Contains(number))
                {
                    ticketTotal = ticketTotal == 0 ? 1 : ticketTotal * 2;
                }
            }
            total += ticketTotal;
        }
        
        Console.WriteLine(total);
    }
    
    public static void SolvePart2(bool isTest = false)
    {
        var filename = $"Data/Day04{(isTest ? ".Test" : "")}.txt";
        var input = FileUtility.ReadLinesFromFile(filename);
        
        var tickets = Ticket.ParseTickets(input);
        Dictionary<string, int> ticketCount = new Dictionary<string, int>();

        foreach (var ticket in tickets)
        {
            ticketCount.Add(ticket.Name, 1);
        }

        foreach (var ticket in tickets)
        {
            var ticketNumber = int.Parse(ticket.Name.Substring(4).Trim());
            for (int i = 1; i <= ticket.WinningNumberCount(); i++)
            {
                ticketCount[$"Card {(ticketNumber + i).ToString(),3}"] += ticketCount[ticket.Name];
            }
        }
        
        Console.WriteLine(ticketCount.Sum(x=>x.Value));
    }
}

public class Ticket
{
    public string Name { get; set; }
    public int[] Numbers { get; set; }
    public int[] WinningNumbers { get; set; }

    public int WinningNumberCount()
    {
        //get number of matches between numbers and winning numbers
        return Numbers.Count(x => WinningNumbers.Contains(x));
    }
    
    public static List<Ticket> ParseTickets(List<string> input)
    {
        var tickets = new List<Ticket>();
        foreach (var s in input)
        {
            tickets.Add(ParseTicket(s));
        }
        return tickets;
    }
    
    // parse a ticket from a string "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53"
    private static Ticket ParseTicket(string input)
    {
        var ticket = new Ticket();
        ticket.Name = input.Split(':')[0];
        ticket.Numbers = input.Split(':')[1].Split('|')[0].Trim().Split(' ').Where(x=>x!="").Select(int.Parse).ToArray();
        ticket.WinningNumbers = input.Split(':')[1].Split('|')[1].Trim().Split(' ').Where(x=>x!="").Select(int.Parse).ToArray();
        return ticket;
    }
}