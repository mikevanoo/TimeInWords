using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TextToTimeGridLib.Grids;
using TimeToTextLib;

namespace TextToTimeGridLib
{
    public abstract class TimeGrid
    {
        public static TimeGrid Get(LanguagePreset.Language lang)
        {
            switch (lang)
            {
                case LanguagePreset.Language.English:
                    return new TimeGridEnglish();
                    
                case LanguagePreset.Language.Dutch:
                    return new TimeGridDutch();

                default:
                    throw new ArgumentOutOfRangeException(nameof(lang), lang, "Language not implemented");
            }
        }

        public abstract string RawGrid { get; }

        private char[][] _charGrid;
        
        public const int GridWidth = 11;
        public const int GridHeight = 10;

        public char[][] CharGrid
        {
            get
            {
                if (_charGrid == null)
                    BuildCharGrid();

                return _charGrid;
            }
        }

        private void BuildCharGrid()
        {
            _charGrid = new char[GridHeight][];

            for(int i = 0; i < GridHeight; i++)
                _charGrid[i] = new char[GridWidth];

            int x = 0;
            int y = 0;

            foreach (string line in RawGrid.Split('\n'))
            {
                foreach (char c in line)
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
            for (int i = 0; i < GridHeight; i++)
                output[i] = new bool[GridWidth];

            if (!strict)
            {
                //remove spaces
                input = input.Replace(" ", "");

                int index = 0;
                int x = 0;
                int y = 0;

                foreach (char[] line in CharGrid)
                {
                    foreach (char cell in line)
                    {
                        if (index >= input.Length)
                            break; //we're done :)

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
            }
            else
            {
                int index = 0;
                int x = 0;
                int y = 0;
                string[] words = input.Split(' ');
                string current = "";

                foreach (char[] line in CharGrid)
                {
                    foreach (char cell in line)
                    {
                        if (index >= words.Length)
                            break; //we're done :)

                        current += cell;
                        x++;


                        if (words[index] == current)
                        {
                            //this word is complete
                            int from = x - words[index].Length;
                            int to = x;

                            for (int xx = from; xx < to; xx++)
                            {
                                output[y][xx] = true; //turn the required pixels 'on'
                            }

                            current = "";
                            index++;

                        } else if (words[index].StartsWith(current))
                        {
                            //this letter matches the current word, go to next
                            continue;
                        }
                        else
                        {
                            //this word is wrong, if we had a duplicate start again with the new character so we dont skip anything, else start fresh

                            if (current.Length == 2 && current[0] == current[1])
                                current = current[0].ToString();
                            else
                                current = "";
                        }
                        
                        
                    }
                    y++;
                    x = 0;
                }
            }



            return new Bitmask(output);
        }

        public string ToString(Bitmask bitmask)
        {
            StringBuilder b = new StringBuilder();

            int x = 0;
            int y = 0;

            foreach (char[] line in CharGrid)
            {
                foreach (char c in line)
                {

                    if (bitmask.Mask[y][x])
                        b.Append(c);
                    else
                        b.Append(".");
                    

                    x++;
                }
                y++;
                x = 0;
                b.AppendLine();
            }

            return b.ToString();

        }

        public override string ToString()
        {
            return RawGrid;
        }
    }
}
