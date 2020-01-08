using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Sprites;

namespace ztp_game.Logic
{
    class Screen
    {
        private static int width = 100;

        private static int height = 40;
        private static char[,] screen = new char[height, width];
        public static int titleBeg = height / 4, menuBeg = height / 2, middle = width / 2 - 8;
        private static int finishX = width - 4;
        private static int finishY = height - 4;
        private static bool change = false;
        private static int level = 1;

        private static SoundPlayer music = new SoundPlayer("music.wav");

        public static void setLevel(int lvl)
        {
            level = lvl;
        }

        public static int getLevel()
        {
            return level;
        }

        public static int getFinishX()
        {
            return finishX;
        }

        public static int getFinishY()
        {
            return finishY;
        }

        public static int getWidth()
        {
            return width;
        }

        public static int getHeight()
        {
            return height;
        }

        public static char getChar(int x, int y)
        {
            return screen[y, x];
        }

        public static void setChar(int x, int y, char c)
        {
            screen[y, x] = c;
        }

        public static void ChangeMap(bool state)
        {
            change = state;
        }

        public static bool getChange()
        {
            return change;
        }

        public static char[,] getScreen()
        {
            return screen;
        }



        public static ConsoleColor pickColor() //losowy wybor kolorów
        {
            Random rnd = new Random();
            var color = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)color.GetValue(rnd.Next(color.Length));
        }

        public static char[,] Fill() //wypelnianie bufora (postac, ramka, przeszkody)
        {
            char[,] buffer = new char[height, width];
            Level l = new Level();


            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    buffer[i, j] = ' ';
                }
            }


            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || j == 0 || i == height - 1 || j == width - 1)
                    {
                        buffer[i, j] = '\u2593';
                    }
                }
            }

            buffer = l.BlocksGenerator(buffer, height, width);
            buffer = l.PointsGenerator(buffer, height, width);
            return buffer;
        }

        public static void DisplayMenu(int position)
        {
            music.StopMusic();
            //Console.Clear();
            Console.CursorVisible = false;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 0);
            string title1 = "sample text game", title2 = "ǝɯɐƃ ʇxǝʇ ǝldɯɐs", menu1 = "New Game", menu2 = "Ranking", menu3 = "Credits", menu4 = "Exit";

            for (int i = 0; i < height; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (i == titleBeg)
                {
                    Console.SetCursorPosition(middle, titleBeg);
                    Console.Write(title1);
                }
                if (i == titleBeg + 2)
                {
                    Console.SetCursorPosition(middle, titleBeg + 2);
                    Console.Write(title2);
                }
                if (i == menuBeg)
                {
                    Console.SetCursorPosition(middle, menuBeg);
                    if (position == 1) Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(menu1);
                }
                if (i == menuBeg + 2)
                {
                    Console.SetCursorPosition(middle, menuBeg + 2);
                    if (position == 2) Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(menu2);
                }
                if (i == menuBeg + 4)
                {
                    Console.SetCursorPosition(middle, menuBeg + 4);
                    if (position == 3) Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(menu3);
                }
                if (i == menuBeg + 6)
                {
                    Console.SetCursorPosition(middle, menuBeg + 6);
                    if (position == 4) Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(menu4);
                }

                Console.WriteLine();
                //System.Threading.Thread.Sleep(100);
            }

        }

        public static void DisplayRanking()
        {
            int counter = 1;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            //Console.CursorVisible = false;
            List<SplitData> placements = RankingFile.getPlacements();

            Console.SetCursorPosition(middle + 4, 2);

            foreach (var placement in placements)
            {
                Console.WriteLine(counter + ". " + placement.name + ' ' + placement.score);
                Console.SetCursorPosition(middle + 4, Console.CursorTop + 2);
                counter++;

            }
            while (counter <= 10)
            {
                Console.WriteLine(counter + ". ");
                Console.SetCursorPosition(middle + 4, Console.CursorTop + 2);
                counter++;
            }
            Console.SetCursorPosition(0, 0);
            //RankingFile.ReadFromFile();

        }

        public class ScreenColor
        {

            public void ScreenBackground(int roll)
            {
                switch (roll)
                {
                    case 1:
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        break;
                    case 2:
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        break;
                    case 3:
                        Console.BackgroundColor = ConsoleColor.Gray;
                        break;
                    case 4:
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        break;
                }
            }
            public void ScreenForeground(int roll)
            {
                switch (roll)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                }
            }
            public void ScreenBorder(int roll)
            {
                switch (roll)
                {
                    case 1:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }
            }
            public void ScreenGoal(int roll)
            {
                switch (roll)
                {
                    case 1:
                        Console.BackgroundColor = ConsoleColor.Red;
                        break;
                    case 2:
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        break;
                    case 3:
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        break;
                    case 4:
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        break;
                }
            }
        }





        public static void DisplayGame(Champion champ) //wyswietlenie tego co w buforze
        {

            music.StopMusic();
            ScreenColor color = new ScreenColor();
            Random rnd = new Random();
            int roll = rnd.Next(1, 4);
            //int roll = 4;
            int rollReggae = rnd.Next(1, 17);
            if (rollReggae == 16) roll = 4;

            screen = Fill();
            Console.SetCursorPosition(0, 0);
            //Console.BackgroundColor = ConsoleColor.DarkGray;
            color.ScreenBackground(roll);
            if (roll == 4) music.PlayMusic();
            Console.CursorVisible = false;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || j == 0 || i == height - 1 || j == width - 1)
                    {
                        color.ScreenBorder(roll);
                    }
                    else
                    {
                        color.ScreenForeground(roll);
                    }

                    if (i >= height - 4 && i < height - 1 && j >= width - 4 && j < width - 1)
                    {
                        color.ScreenGoal(roll);
                    }
                    else
                    {
                        color.ScreenBackground(roll);
                    }
                    Console.Write(screen[i, j]);

                }
                Console.WriteLine();
            }
            //Console.WriteLine();

            Console.WriteLine("Health: {0} Points: {1} Level: {2}", champ.getHealth(), champ.getPoints(), level);

        }


        public static void DisplayCredits()
        {
            string name1 = "Adam Sulima Dolina", name2 = "Michał Szorc", name3 = "Piotr Awramiuk";
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            //Console.CursorVisible = false;
            for (int i = 0; i < height; i++)
            {
                if (i == menuBeg)
                {
                    Console.SetCursorPosition(middle, titleBeg + 4);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(name1);
                }
                if (i == menuBeg + 2)
                {
                    Console.SetCursorPosition(middle, titleBeg + 6);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(name2);
                }
                if (i == menuBeg + 4)
                {
                    Console.SetCursorPosition(middle, titleBeg + 8);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(name3);
                }

                Console.WriteLine();
            }
        }

        public static void AddPlacement(Champion champ)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            var list = RankingFile.getPlacements();
            Console.CursorVisible = true;
            string points = "Great job! You got " + champ.getPoints() + " points!";
            string signature = "Please enter your three letter signature: ";
            Console.SetCursorPosition(width / 2 - points.Length / 2, height / 2 - 4);
            Console.WriteLine(points);
            if (list.Count < 10)
            {
                Console.SetCursorPosition(width / 2 - signature.Length / 2, height / 2 - 2);
                Console.WriteLine(signature);
                Console.SetCursorPosition(width / 2 - 1, height / 2);
                string name = RankingFile.ReadThreeCharacters();
                RankingFile.AddToList(name, champ.getPoints());
                Console.Clear();
                return;
            }
            if (list[list.Count - 1].score < champ.getPoints())
            {
                Console.SetCursorPosition(width / 2 - signature.Length / 2, height / 2 - 2);
                Console.WriteLine(signature);
                Console.SetCursorPosition(width / 2 - 1, height / 2);
                string name = RankingFile.ReadThreeCharacters();
                RankingFile.AddToList(name, champ.getPoints());
            }
            else
            {
                Console.CursorVisible = false;
                Console.ReadKey();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }
    }
}
