var input = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "/input.txt").ToList();

static int GetSafeReportsCount(IEnumerable<string> input, int unsafeLevelsTolerance)
{
    var safeCounter = 0;

    foreach (var line in input)
    {
        var numbers = line.Split(" ", StringSplitOptions.TrimEntries).Select(int.Parse).ToList();

        var isIncreasing = numbers[1] > numbers[0];

        if (IsReportSafe(numbers, isIncreasing, unsafeLevelsTolerance))
        {
            safeCounter++;
        }
    }

    return safeCounter;
}

static bool IsReportSafe(List<int> report, bool isIncreasing, int unsafeLevelsTolerance)
{
    var unsafeLevelsCount = 0;

    for (var i = 0; i <= report.Count - 2; i++)
    {
        var difference = report[i + 1] - report[i];

        if (Math.Abs(difference) is 0 or > 3)
        {
            unsafeLevelsCount++;
        }

        if ((isIncreasing && difference < 0) || (!isIncreasing && difference > 0))
        {
            unsafeLevelsCount++;
        }

        if (unsafeLevelsCount > unsafeLevelsTolerance)
        {
            return false;
        }
    }

    return true;
}

Console.WriteLine("Part one result: " + GetSafeReportsCount(input, 0));
Console.WriteLine("Part one result: " + GetSafeReportsCount(input, 1));