using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib;

public abstract class TimeGrid
{
    public const int GridHeight = 10;
    public const int GridWidth = 11;

    private char[][] _charGrid;

    protected abstract string RawGrid { get; }

    private static readonly Dictionary<LanguagePreset.Language, TimeGrid> Instances =
        new()
        {
            { LanguagePreset.Language.English, new TimeGridEnglish() },
            { LanguagePreset.Language.Dutch, new TimeGridDutch() },
        };

    public static TimeGrid Get(LanguagePreset.Language lang)
    {
        if (Instances.TryGetValue(lang, out var timeGrid))
        {
            return timeGrid;
        }

        throw new ArgumentOutOfRangeException(nameof(lang), lang, "Language not implemented");
    }

    public char[][] CharGrid
    {
        get
        {
            if (_charGrid == null)
            {
                BuildCharGrid();
            }

            return _charGrid;
        }
    }

    private void BuildCharGrid()
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
        /*
         * Strict mode finds whole words in the grid, respecting word boundaries.
         * Example: "IT IS FIVE" will match letters that form complete words.
         *
         * The duplicate character handling (lines 160-163) handles cases where
         * a word like "ELEVEN" appears in the grid but we're looking for "SEVEN".
         * When we see "EL" but need "SE", we keep the 'L' as it might be the
         * start of the next word.
         */

        var wordIndex = 0;
        var gridX = 0;
        var gridY = 0;
        var words = input.Split(' ');
        var currentMatch = "";

        foreach (var line in CharGrid)
        {
            foreach (var cell in line)
            {
                if (wordIndex >= words.Length)
                {
                    break; // All words found
                }

                currentMatch += cell;
                gridX++;

                if (IsExactWordMatch(words[wordIndex], currentMatch))
                {
                    MarkWordAsActive(output, gridY, gridX, words[wordIndex].Length);
                    currentMatch = "";
                    wordIndex++;
                }
                else if (IsPartialWordMatch(words[wordIndex], currentMatch))
                {
                    // Continue building the current word
                    continue;
                }
                else
                {
                    // Mismatch - reset but handle duplicate characters
                    currentMatch = HandleDuplicateCharacters(currentMatch);
                }
            }
            gridY++;
            gridX = 0;
        }

        return new Bitmask(output);
    }

    private static bool IsExactWordMatch(string targetWord, string currentMatch) => targetWord == currentMatch;

    private static bool IsPartialWordMatch(string targetWord, string currentMatch) =>
        targetWord.StartsWith(currentMatch, StringComparison.InvariantCulture);

    private static void MarkWordAsActive(bool[][] output, int y, int x, int wordLength)
    {
        var startX = x - wordLength;
        var endX = x;

        for (var xx = startX; xx < endX; xx++)
        {
            output[y][xx] = true;
        }
    }

    private static string HandleDuplicateCharacters(string currentMatch)
    {
        // If we have 2 identical chars (like "EE"), keep the last one
        // as it might be the start of the next word
        // Example: Looking for "SEVEN" but found "EL" - keep the "L"
        if (currentMatch.Length == 2 && currentMatch[0] == currentMatch[1])
        {
            return currentMatch[1].ToString();
        }

        return "";
    }
}
