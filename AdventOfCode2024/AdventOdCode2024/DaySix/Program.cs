var input = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "/input.txt").ToList();

var guard = '^';
var startingRow = input.FindIndex(row => row.Any(r => r.Equals(guard)));
var startingColumn = input[startingRow].ToList().FindIndex(col => col.Equals(guard));

var rowDirection = -1; // 0, +1
var columnDirection = 0; // +1, 
var rowMax = 0;
var columnMax = 0;

for (var row = startingRow; row <= rowMax; row = row + rowDirection)
{
    for (var column = startingColumn; column <= columnMax; column = column + columnDirection)
    {
        if (input[row][column] == '.')
        {
            continue;
        }
        
        if (input[row][column] == '#')
        {
            rowDirection = rowDirection + 1;  // 0
            columnDirection = columnDirection + 1;  // +1
            // change direction to the right
        }
    }
}

int GetDirection(int currentDirection)
{
    switch (currentDirection)
    {
        case 0:
        {
            return 1; //row (
        }
        case 1:
        {
            return 0;
        }
        case -1:
        {
            return 0;
        }
    }

    return 0;
}