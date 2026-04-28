using System.Diagnostics;
using System.Text;
using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib;

public abstract class TimeGrid
{
    public virtual int GridHeight => 10;
    public virtual int GridWidth => 11;

    private char[][]? _charGrid;

    protected abstract string RawGrid { get; }

    private static readonly Dictionary<LanguagePreset.Language, TimeGrid> Instances =
        new()
        {
            { LanguagePreset.Language.English, new TimeGridEnglish() },
            { LanguagePreset.Language.EnglishPrecise, new TimeGridEnglishPrecise() },
            { LanguagePreset.Language.Dutch, new TimeGridDutch() },
            { LanguagePreset.Language.DutchPrecise, new TimeGridDutchPrecise() },
            { LanguagePreset.Language.French, new TimeGridFrench() },
            { LanguagePreset.Language.FrenchPrecise, new TimeGridFrenchPrecise() },
            { LanguagePreset.Language.Spanish, new TimeGridSpanish() },
            { LanguagePreset.Language.SpanishPrecise, new TimeGridSpanishPrecise() },
            { LanguagePreset.Language.German, new TimeGridGerman() },
            { LanguagePreset.Language.GermanPrecise, new TimeGridGermanPrecise() },
        };

    public static TimeGrid Get(LanguagePreset.Language lang)
    {
        if (Instances.TryGetValue(lang, out var timeGrid))
        {
            return timeGrid;
        }

        throw new ArgumentOutOfRangeException(nameof(lang), lang, "Language not implemented");
    }

    public char[][] CharGrid => _charGrid ?? BuildCharGrid();

    private char[][] BuildCharGrid()
    {
        _charGrid = new char[GridHeight][];

        for (var i = 0; i < GridHeight; i++)
        {
            _charGrid[i] = new char[GridWidth];
        }

        var x = 0;
        var y = 0;

        foreach (var line in RawGrid.Split('\n'))
        {
            foreach (var c in line)
            {
                _charGrid[y][x] = c;
                x++;
            }
            y++;
            x = 0;
        }

        Debug.WriteLine("Built character grid");

        return _charGrid;
    }

    public Bitmask GetBitMask(string input, bool strict)
    {
        var output = new bool[GridHeight][];
        for (var i = 0; i < GridHeight; i++)
        {
            output[i] = new bool[GridWidth];
        }

        return strict ? GetBitmaskStrict(input, output) : GetBitmaskNonStrict(input, output);
    }

    public string ToString(Bitmask bitmask)
    {
        var b = new StringBuilder();

        var x = 0;
        var y = 0;

        foreach (var line in CharGrid)
        {
            foreach (var c in line)
            {
                if (bitmask.Mask[y][x])
                {
                    b.Append(c);
                }
                else
                {
                    b.Append('.');
                }

                x++;
            }
            y++;
            x = 0;
            b.Append('\n');
        }

        return b.ToString();
    }

    public override string ToString() => RawGrid;

    private Bitmask GetBitmaskNonStrict(string input, bool[][] output)
    {
        // remove spaces
        input = input.Replace(" ", "");

        var wordIndex = 0;
        var gridX = 0;
        var gridY = 0;

        foreach (var line in CharGrid)
        {
            foreach (var cell in line)
            {
                if (wordIndex >= input.Length)
                {
                    break; // we're done :)
                }

                if (input[wordIndex] == cell)
                {
                    output[gridY][gridX] = true;
                    wordIndex++;
                }
                gridX++;
            }
            gridY++;
            gridX = 0;
        }

        return new Bitmask(output);
    }

    private Bitmask GetBitmaskStrict(string input, bool[][] output)
    {
        var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var rows = CharGrid.Select(row => new string(row)).ToArray();

        var cursorRow = 0;
        var cursorCol = 0;

        foreach (var word in words)
        {
            var (row, col) = FindWordFrom(rows, word, cursorRow, cursorCol);
            if (row < 0)
            {
                break;
            }

            for (var c = col; c < col + word.Length; c++)
            {
                output[row][c] = true;
            }

            cursorRow = row;
            cursorCol = col + word.Length;
        }

        return new Bitmask(output);
    }

    private static (int row, int col) FindWordFrom(string[] rows, string word, int startRow, int startCol)
    {
        for (var row = startRow; row < rows.Length; row++)
        {
            var from = row == startRow ? startCol : 0;
            if (from >= rows[row].Length)
            {
                continue;
            }

            var found = rows[row].IndexOf(word, from, StringComparison.Ordinal);
            if (found >= 0)
            {
                return (row, found);
            }
        }
        return (-1, -1);
    }
}
