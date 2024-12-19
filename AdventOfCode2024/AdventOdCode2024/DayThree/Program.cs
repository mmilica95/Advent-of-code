using System.Text.RegularExpressions;
using static System.Int32;

var input = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/input.txt");

static int SolvePartOne(string input)
{
    var pattern = @"mul\((\d+),(\d+)\)";

    var matches = Regex.Matches(input, pattern);

    var result = 0;

    foreach (Match match in matches)
    {
        var number1 = Parse(match.Groups[1].Value);
        var number2 = Parse(match.Groups[2].Value);

        result += number1 * number2;
    }

    return result;
}

static int SolvePartTwo(string input)
{
    var mulPattern = @"mul\((\d+),(\d+)\)";
    var controlPattern = @"do\(\)|don't\(\)";
    
    var tokens = Regex.Split(input, $@"({mulPattern}|{controlPattern})");

    var result = 0;

    var enabled = true;
    foreach (var token in tokens)
    {
        if (token == "do()")
        {
            enabled = true;
        }
        else if (token == "don't()")
        {
            enabled = false;
        }
        else if (Regex.IsMatch(token, mulPattern))
        {
            if (enabled)
            {
                var match = Regex.Match(token, mulPattern);
                var num1 = Parse(match.Groups[1].Value);
                var num2 = Parse(match.Groups[2].Value);
                result += num1 * num2;
            }
        }
    }

    return result;
}


Console.WriteLine("Part one result: {0}", SolvePartOne(input));
Console.WriteLine("Part two result: {0}", SolvePartTwo(input));