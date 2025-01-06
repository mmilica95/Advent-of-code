var input = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "/input.txt").ToList();

static int SolvePartOne(List<string> input)
{
    var xmasCount = 0;
    for (var row = 0; row < input.Count; row++)
    {
        for (var column = 0; column < input[row].Length; column++)
        {
            if (input[row][column] == 'X')
            {
                var results = new List<bool>
                {
                    findXMAS(row, column, input, 0, 1),
                    findXMAS(row, column, input, 0, -1),
                    findXMAS(row, column, input, 1, 0),
                    findXMAS(row, column, input, -1, 0),
                    findXMAS(row, column, input, 1, 1),
                    findXMAS(row, column, input, -1, 1),
                    findXMAS(row, column, input, 1, -1),
                    findXMAS(row, column, input, -1, -1)
                };

                xmasCount += results.Count(r => r);
            }
        }
    }

    return xmasCount;
}

static bool findXMAS(int xRow, int xColumn, List<string> input, int row, int column)
{
    var wordToMatch = "MAS";

    for (var i = 0; i < wordToMatch.Length; i++)
    {
        if (xRow + row * (i + 1) < 0 || xRow + row * (i + 1) >= input.Count())
        {
            return false;
        }

        if (xColumn + column * (i + 1) < 0 || xColumn + column * (i + 1) >= input[xRow].Length)
        {
            return false;
        }

        if (input[xRow + row * (i + 1)][xColumn + column * (i + 1)] != wordToMatch[i])
        {
            return false;
        }
    }

    return true;
}

static int SolvePartTwo(List<string> input)
{
    var xmasCount = 0;
    for (var row = 0; row < input.Count; row++)
    {
        for (var column = 0; column < input[row].Length; column++)
        {
            if (input[row][column] == 'A')
            {
                xmasCount += isXmas(row, column, input) ? 1 : 0;
            }
        }
    }

    return xmasCount;
}

static bool isXmas(int aRow, int aColumn, List<string> input)
{
    if (aRow + 1 >= input.Count || aRow - 1 < 0)
    {
        return false;
    }

    if (aColumn + 1 >= input[aRow].Length || aColumn - 1 < 0)
    {
        return false;
    }

    return (input[aRow + 1][aColumn - 1] == 'M' && input[aRow - 1][aColumn + 1] == 'S' ||
            input[aRow + 1][aColumn - 1] == 'S' && input[aRow - 1][aColumn + 1] == 'M') &&
           (input[aRow + 1][aColumn + 1] == 'M' && input[aRow - 1][aColumn - 1] == 'S' ||
            input[aRow + 1][aColumn + 1] == 'S' && input[aRow - 1][aColumn - 1] == 'M');
}

Console.WriteLine("Part one: {0}", SolvePartOne(input));
Console.WriteLine("Part two: {0}", SolvePartTwo(input));