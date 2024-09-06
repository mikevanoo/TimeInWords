using System;
using TextToTimeGridLib.Grids;

var grid = new TimeGridEnglish();
while (true)
{
    Console.Write("input: ");
    var input = Console.ReadLine();

    var mask = grid.GetBitMask(input, true);

    var sGrid = grid.ToString().Split('\n');
    var sMask = mask.ToString().Split('\n');
    var result = grid.ToString(mask).Split('\n');

    Console.WriteLine();
    Console.WriteLine("Clock grid\tBitmask\t\tResult");
    Console.WriteLine();

    for (var i = 0; i < sGrid.Length; i++)
    {
        var line = $"{sGrid[i].Trim()}\t{sMask[i].Trim()}\t{result[i].Trim()}";
        Console.WriteLine(line);
    }

    Console.WriteLine();
}
