using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ztp_game
{
    static class RankingFile
    {
        private const string Path = "Ranking";
        private static List<SplitData> placements;

        public static void WriteToFile()
        {
            List<SplitData> sorted = placements.OrderByDescending(o => o.score).ToList();
            sorted = sorted.Take(10).ToList();
            StreamWriter writer = new StreamWriter(Path);
            foreach (var line in sorted)
            {
                writer.WriteLine(line.name + ' ' + line.score);
            }
            placements = sorted;
            writer.Close();
        }
        public static void ReadFromFile()
        {
            placements = new List<SplitData>();
            if (File.Exists(Path))
            {
                using (StreamReader reader = new StreamReader(Path))
                {
                    while (!reader.EndOfStream)
                    {
                        String line = reader.ReadLine();
                        if (line != "")
                        {
                            string[] temp = line.Split(' ');
                            placements.Add(new SplitData(temp[0], Convert.ToInt32(temp[1])));
                        }
                    }
                }
            }
            else
            {
                StreamWriter writer = new StreamWriter(Path);
                writer.Close();
            }
        }
        public static List<SplitData> getPlacements()
        {
            List<SplitData> sorted = placements.OrderByDescending(o => o.score).ToList();
            sorted = sorted.Take(10).ToList();
            return sorted;
        }
        public static void AddToList(string name, int score)
        {
            placements.Add(new SplitData(name, score));
            WriteToFile();
        }

        public static string ReadThreeCharacters()
        {
            StringBuilder sb = new StringBuilder();
            bool loop = true;
            while (loop)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // won't show up in console
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        if (sb.Length == 3) loop = false;
                        break;
                    case ConsoleKey.Backspace:
                        {
                            if (sb.Length > 0)
                            {
                                sb.Remove(sb.Length - 1, 1);
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                Console.Write(' ');
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            }
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        break;
                    case ConsoleKey.Tab:
                        break;
                    case ConsoleKey.UpArrow:
                        break;
                    case ConsoleKey.DownArrow:
                        break;
                    case ConsoleKey.RightArrow:
                        break;
                    case ConsoleKey.LeftArrow:
                        break;

                    default:
                        {
                            if (sb.Length < 3)
                            {
                                sb.Append(keyInfo.KeyChar);
                                Console.Write(keyInfo.KeyChar);
                            }
                            break;
                        }
                }
            }
            while (sb.Length < 3)
            {
                sb.Append(' ');
            }

            return sb.ToString();
        }
    }

    public class SplitData
    {
        public string name;
        public int score;
        public SplitData(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
}
