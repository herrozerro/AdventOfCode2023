namespace AdventOfCode2023.Utilities;

public class FileUtility
{
    public static string ReadFromFile(string path)
    {
        return File.ReadAllText(path);
    }

    public static List<string> ReadLinesFromFile(string path)
    {
        return File.ReadAllLines(path).ToList();
    }
    
    public static List<int> ReadIntsFromFile(string path)
    {
        return File.ReadAllLines(path).Select(int.Parse).ToList();
    }
    
    public static List<long> ReadLongsFromFile(string path)
    {
        return File.ReadAllLines(path).Select(long.Parse).ToList();
    }
    
    public static List<double> ReadDoublesFromFile(string path)
    {
        return File.ReadAllLines(path).Select(double.Parse).ToList();
    }
    
    public static List<float> ReadFloatsFromFile(string path)
    {
        return File.ReadAllLines(path).Select(float.Parse).ToList();
    }
    
    public static List<decimal> ReadDecimalsFromFile(string path)
    {
        return File.ReadAllLines(path).Select(decimal.Parse).ToList();
    }
    
    public static List<char> ReadCharsFromFile(string path)
    {
        return File.ReadAllText(path).ToList();
    }
    
    public static List<string> ReadDelimitedStringsFromFile(string path, char delimiter)
    {
        return File.ReadAllText(path).Split(delimiter).ToList();
    }
}