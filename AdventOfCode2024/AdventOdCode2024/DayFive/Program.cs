using System.Text.RegularExpressions;

var input = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "/input.txt").ToList();

static (List<Tuple<int, int>>, List<List<int>>) ProcessInput(IEnumerable<string> input)
{
    var pageOrderingRules = new List<Tuple<int, int>>();
    var pageNumbers = new List<List<int>>();

    foreach (var line in input)
    {
        if (line == "\n")
        {
            continue;
        }

        if (line.Contains('|'))
        {
            var order = line.Split('|');
            pageOrderingRules.Add(new Tuple<int, int>(Int16.Parse(order[0]), Int32.Parse(order[1])));
        }

        if (line.Contains(','))
        {
            var pattern = @"\d+";
            var matches = Regex.Matches(line, pattern);
            pageNumbers.Add(matches.Select(match => Int32.Parse(match.Value)).ToList());
        }
    }

    return (pageOrderingRules, pageNumbers);
}

static int SolvePartOne(List<string> input)
{
    var sum = 0;

    var (pageOrderingRules, pageNumbers) = ProcessInput(input);

    foreach (var pageNumbersLine in pageNumbers)
    {
        if (IsInCorrectOrder(pageNumbersLine, pageOrderingRules))
        {
            sum += pageNumbersLine[pageNumbersLine.Count / 2];
        }
    }

    return sum;
}

static int SolvePartTwo(List<string> input)
{
    var sum = 0;

    var (pageOrderingRules, pageNumbers) = ProcessInput(input);

    foreach (var pageNumbersLine in pageNumbers)
    {
        if (!IsInCorrectOrder(pageNumbersLine, pageOrderingRules))
        {
            var newLine = ReorderLine(pageNumbersLine, pageOrderingRules);
            sum += newLine[pageNumbersLine.Count / 2];
        }
    }

    return sum;
}

static bool IsInCorrectOrder(List<int> pageNumberLines, List<Tuple<int, int>> pageOrderingRules)
{
    for (var i = 0; i < pageNumberLines.Count; i++)
    {
        for (var j = 0; j < i; j++)
        {
            if (pageOrderingRules.Any(orderRule =>
                    orderRule.Item1 == pageNumberLines[i] && orderRule.Item2 == pageNumberLines[j]))
            {
                return false;
            }
        }
    }

    return true;
}

static List<int> ReorderLine(List<int> pageNumbersLine, List<Tuple<int, int>> pageOrderingRules)
{
    while (!IsInCorrectOrder(pageNumbersLine, pageOrderingRules))
    {
        for (var i = 0; i < pageNumbersLine.Count; i++)
        {
            for (var j = 0; j < i; j++)
            {
                if (pageOrderingRules.Any(orderRule =>
                        orderRule.Item1 == pageNumbersLine[i] && orderRule.Item2 == pageNumbersLine[j]))
                {
                    (pageNumbersLine[i], pageNumbersLine[j]) = (pageNumbersLine[j], pageNumbersLine[i]);
                }
            }
        }
    }

    return pageNumbersLine;
}

Console.WriteLine("Part one result: {0}", SolvePartOne(input));
Console.WriteLine("Part two result: {0}", SolvePartTwo(input));