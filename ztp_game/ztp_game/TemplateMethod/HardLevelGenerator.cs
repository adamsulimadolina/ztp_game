using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Builder;
using ztp_game.Collection;
using ztp_game.Auxiliary_Classes;

namespace ztp_game.TemplateMethod
{
    class HardLevelGenerator : AbstractLevelGenerator
    {
        public char[,] level_array { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IBoardBuilder board_builder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override char[,] CreateBlocks(int height, int width)
        {
            char[,] buffer = new char[height, width];
            Random rnd = new Random();
            List<bool> blockDirection = new List<bool> { true, false };
            int tmp_number = rnd.Next(6, width / 4);
            int last_block_width = 10;
            int blocks_gap = 6;
            int tmp_helper = 0;
            int double_block, direction, block_width, block_height;
            for (int k = 0; k < tmp_number; k++)
            {
                direction = rnd.Next(blockDirection.Count);
                block_width = rnd.Next(2, width / 4);
                block_height = rnd.Next(height / 3, height - 5);

                if (k == 0) direction = 0;
                else
                {
                    double_block = rnd.Next(0, 100);
                    if (double_block % 2 == 0 && Screen.getLevel() > 5) //tutaj musi byc level z Championa
                    {
                        direction = 2;
                        tmp_helper = rnd.Next(4, height - 4);
                        block_height = tmp_helper;
                    }

                }
                if (last_block_width + block_width >= width - 6)
                {
                    block_width = width - last_block_width - 5;
                    direction = 0;
                }
                if (block_width > 0) this.blockList.Add(new BlocksList(last_block_width, last_block_width + block_width, direction, block_height)); //lista spritów
                switch (direction)
                {
                    case 0:
                        for (int i = height - 2; i > height - block_height; i--)
                        {
                            for (int j = last_block_width; j < last_block_width + block_width; j++)
                            {
                                buffer[i, j] = '\u2588';
                            }
                        }
                        last_block_width = last_block_width + block_width + blocks_gap;
                        break;

                    case 1:
                        for (int i = 1; i < block_height; i++)
                        {
                            for (int j = last_block_width; j < last_block_width + block_width; j++)
                            {
                                buffer[i, j] = '\u2588';
                            }
                        }
                        last_block_width = last_block_width + blocks_gap + block_width;
                        break;

                    case 2:
                        for (int i = 1; i < height - 1; i++)
                        {
                            for (int j = last_block_width; j < last_block_width + block_width; j++)
                            {
                                buffer[i, j] = '\u2588';
                            }
                        }

                        for (int i = tmp_helper; i < tmp_helper + 4; i++)

                        {
                            for (int j = last_block_width; j < last_block_width + block_width; j++)
                            {
                                buffer[i, j] = ' ';
                            }
                        }
                        last_block_width = last_block_width + blocks_gap + block_width;
                        break;
                }
            }

            return buffer;
        }


        public override char[,] CreateCoins(int height, int width)
        {
            char[,] buffer = this.level_array;
            Random rnd = new Random();

            int numOfPoints = rnd.Next(5, 10);
            int rndWidth = 0;
            int rndHeight = 0;
            for (int i = 0; i < numOfPoints; i++)
            {
                while (buffer[rndHeight, rndWidth] != ' ')
                {
                    rndHeight = rnd.Next(2, height - 2);
                    rndWidth = rnd.Next(4, width - 5);
                }
                buffer[rndHeight, rndWidth] = '$';
            }

            return buffer;
        }


        public override char[,] CreateExit(int height, int width)
        {
            throw new NotImplementedException();
        }

        public override char[,] CreateGaps(int height, int width)
        {
            Random rnd = new Random();
            int length = 0;
            int isGapRoll = 0;
            int j = 1;
            bool isGap = false;
            bool wasGap = false;
            char[,] buffer = this.level_array;

            foreach (BlocksList b in this.blockList)
            {

                while (j < b.getStartX() - 1)
                {
                    isGapRoll = rnd.Next(1, 100);

                    length = rnd.Next(2, 5);

                    if (wasGap == true)
                    {
                        j += length;
                        wasGap = false;
                        if (b.getStartX() - j < 3) break;
                    }
                    else
                    {
                        if (isGapRoll < 40) isGap = true;


                        if (isGap == true)
                        {
                            for (int k = 0; k < length; k++)
                            {
                                if (j < b.getStartX()) buffer[0, j] = ' ';
                                j++;
                            }
                            wasGap = true;
                        }
                        else
                        {
                            j += (length - 1);
                            wasGap = false;
                        }
                    }

                }
                j = b.getFinishX() + 1;
            }

            isGap = false;
            wasGap = false;
            isGapRoll = 0;
            j = 5;
            length = 0;


            foreach (BlocksList b in this.blockList) //sprite lista
            {

                while (j < b.getStartX() - 1)
                {
                    isGapRoll = rnd.Next(1, 100);

                    length = rnd.Next(2, 5);

                    if (wasGap == true)
                    {
                        j += length;
                        wasGap = false;
                        if (b.getStartX() - j < 3) break;
                    }
                    else
                    {
                        if (isGapRoll < 40) isGap = true;

                        if (isGap == true)
                        {
                            for (int k = 0; k < length; k++)
                            {
                                if (j < b.getStartX()) buffer[height - 1, j] = ' ';
                                j++;
                            }
                            wasGap = true;
                        }
                        else
                        {
                            j += (length - 1);
                            wasGap = false;
                        }
                    }

                }
                j = b.getFinishX() + 1;
            }
            return buffer;
        }

        public override char[,] CreateThorns(int height, int width)
        {
            return this.level_array;
        }


    }
}
