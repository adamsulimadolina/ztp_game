using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Auxiliary_Classes;
using ztp_game.Builder;
using ztp_game.Collection;
using ztp_game.Logic;
using ztp_game.Sprites;

namespace ztp_game.TemplateMethod
{
    class EasyLevelGenerator : AbstractLevelGenerator
    {
        
        public EasyLevelGenerator(ContentManager content)
        {
            this.content = content;
            this.blockList = new List<BlocksList>();
        }
        protected override char[,] CreateBlocks(int height, int width)
        {
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
                    if (double_block % 2 == 0 && Champion.GetInstance().level > 5) //tutaj musi byc level z Championa
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
                if (block_width > 0) this.blockList.Add(new BlocksList(last_block_width, last_block_width + block_width, direction, block_height));
                switch (direction)
                {
                    case 0:
                        for (int i = height - 2; i > height - block_height; i--)
                        {
                            for (int j = last_block_width; j < last_block_width + block_width; j++)
                            {
                                this.level_array[i, j] = '\u2588';
                            }
                        }
                        last_block_width = last_block_width + block_width + blocks_gap;
                        break;

                    case 1:
                        for (int i = 1; i < block_height; i++)
                        {
                            for (int j = last_block_width; j < last_block_width + block_width; j++)
                            {
                                this.level_array[i, j] = '\u2588';
                            }
                        }
                        last_block_width = last_block_width + blocks_gap + block_width;
                        break;

                    case 2:
                        for (int i = 1; i < height - 1; i++)
                        {
                            for (int j = last_block_width; j < last_block_width + block_width; j++)
                            {
                                this.level_array[i, j] = '\u2588';
                            }
                        }

                        for (int i = tmp_helper; i < tmp_helper + 4; i++)

                        {
                            for (int j = last_block_width; j < last_block_width + block_width; j++)
                            {
                                this.level_array[i, j] = ' ';
                            }
                        }
                        last_block_width = last_block_width + blocks_gap + block_width;
                        break;
                }
            }

            return this.level_array;
        }


        protected override char[,] CreateCoins(int height, int width)
        {
            Random rnd = new Random();

            int numOfPoints = rnd.Next(5, 10);
            int rndWidth = 0;
            int rndHeight = 0;
            for (int i = 0; i < numOfPoints; i++)
            {
                while (this.level_array[rndHeight, rndWidth] != ' ')
                {
                    rndHeight = rnd.Next(2, height - 2);
                    rndWidth = rnd.Next(4, width - 5);
                }
                this.level_array[rndHeight, rndWidth] = '$';
            }

            return this.level_array;
        }


        protected override char[,] CreateGaps(int height, int width)
        {
            Random rnd = new Random();
            int length = 0;
            int isGapRoll = 0;
            int j = 1;
            bool isGap = false;
            bool wasGap = false;
            this.level_array = this.level_array;

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
                                if (j < b.getStartX()) this.level_array[0, j] = ' ';
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
            j = 5;


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
                                if (j < b.getStartX()) this.level_array[height - 1, j] = ' ';
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
            return this.level_array;
        }


        protected override char[,] CreateThorns(int height, int width)
        {
            return this.level_array;
        }

        protected override char[,] CreateBorder(int height, int width)
        {
            for(int i = 0; i< height; i++)
            {
                this.level_array[i, 0] = 'b';
                this.level_array[i, width - 1] = 'b';
            }
            for(int i = 0; i < width; i++)
            {
                this.level_array[0, i] = 'b';
                this.level_array[height - 1, i] = 'b';
            }
            return this.level_array;
        }

        public override void BuildLevel(int height, int width)
        {
            char sign = ' ';
            board_builder = new MagmaLevelBuilder(this.content);
            if (pick == 1) board_builder = new IceLevelBuilder(this.content);
            if (pick == 2) board_builder = new JungleLevelBuilder(this.content);

            board_builder.GenerateBackground();
            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    sign = this.level_array[i, j];
                    if (sign == 'b') board_builder.GenerateBorder();
                    else if (sign == '\u2588') board_builder.GenerateBlock();
                    else if (sign == '$') board_builder.GenerateCoin();
                    board_builder.GenerateDoors(height, width);
                    board_builder.x += 16;
                }
                board_builder.x = 0;
                board_builder.y += 16;
            }
            sprite_collection = board_builder.GetLevel();

            Champion.GetInstance().SetCollection(sprite_collection);

        }


    }

    
}
