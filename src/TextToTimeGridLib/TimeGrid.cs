using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib
{
    public abstract class TimeGrid
    {
        public const int GridHeight = 10;
        public const int GridWidth = 11;

        private char[][] _charGrid;

        protected abstract string RawGrid { get; }

        private static readonly Dictionary<LanguagePreset.Language, TimeGrid> Instances = new Dictionary<
            LanguagePreset.Language,
            TimeGrid
        >
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

        private Bitmask GetBitmaskNonStrict(string input, bool[][] output)
        {
            // remove spaces
            input = input.Replace(" ", "");

            var index = 0;
            var x = 0;
            var y = 0;

            foreach (var line in CharGrid)
            {
                foreach (var cell in line)
                {
                    if (index >= input.Length)
                    {
                        break; // we're done :)
                    }

                    if (input[index] == cell)
                    {
                        output[y][x] = true;
                        index++;
                    }
                    x++;
                }
                y++;
                x = 0;
            }

            return new Bitmask(output);
        }

        private Bitmask GetBitmaskStrict(string input, bool[][] output)
        {
            var index = 0;
            var x = 0;
            var y = 0;
            var words = input.Split(' ');
            var current = "";

            foreach (var line in CharGrid)
            {
                foreach (var cell in line)
                {
                    if (index >= words.Length)
                    {
                        break; // we're done :)
                    }

                    current += cell;
                    x++;

                    if (words[index] == current)
                    {
                        // this word is complete
                        var from = x - words[index].Length;
                        var to = x;

                        for (var xx = from; xx < to; xx++)
                        {
                            output[y][xx] = true; // turn the required pixels 'on'
                        }

                        current = "";
                        index++;
                    }
                    else if (words[index].StartsWith(current))
                    {
                        // this letter matches the current word, go to next
                        continue;
                    }
                    else
                    {
                        // this word is wrong, if we had a duplicate start again with the new character so we don't skip anything, else start fresh
                        if (current.Length == 2 && current[0] == current[1])
                        {
                            current = current[0].ToString();
                        }
                        else
                        {
                            current = "";
                        }
                    }
                }
                y++;
                x = 0;
            }

            return new Bitmask(output);
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
                        b.Append(".");
                    }

                    x++;
                }
                y++;
                x = 0;
                b.AppendLine();
            }

            return b.ToString();
        }

        public override string ToString() => RawGrid;
    }
}
