﻿using Microsoft.Xna.Framework.Content;
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
    abstract class AbstractLevelGenerator
    {

        public int pick { get; set;}
        public char[,] level_array { get; set; }

        protected IBoardBuilder board_builder { get; set; }
        public ContentManager content;
        public SpriteCollection sprite_collection;

        protected List<BlocksList> blockList = new List<BlocksList>();

        public SpriteCollection GetLevel() { return new SpriteCollection(); }

        public void setPick(int num)
        {
            this.pick = num;
        }

        public abstract void BuildLevel(int height, int width);

        public void CreateLevelLogic(int height, int width)
        {
            this.level_array = new char[height, width];
            FillArray();
            this.level_array = CreateBorder(height, width);
            this.level_array = CreateBlocks(height, width);
            this.level_array = CreateThorns(height, width);
            this.level_array = CreateGaps(height, width);
            this.level_array = CreateCoins(height, width);
            BuildLevel(height, width);
            for(int i = 0; i<height; i++)
            {
                for(int j=0; j<width; j++)
                {
                    Console.Write(this.level_array[i,j]);
                }
                Console.WriteLine();
            }
            Champion.GetInstance().SetCollection(sprite_collection);
            

        }
        protected abstract char[,] CreateBlocks(int height, int width);
        protected abstract char[,] CreateThorns(int height, int width);
        protected abstract char[,] CreateGaps(int height, int width);


        protected abstract char[,] CreateCoins(int height, int width);
        protected abstract char[,] CreateBorder(int height, int width);




        private void FillArray()
        {
            for (int i = 0; i < Screen.getHeight(); i++)
            {
                for (int j = 0; j < Screen.getWidth(); j++)
                {
                    this.level_array[i, j] = ' ';
                }
            }
        }

        public void ResetBlocksList()
        {
            this.blockList = new List<BlocksList>();
        }
    }
}
