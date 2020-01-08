using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Auxiliary_Classes;
using ztp_game.Builder;
using ztp_game.Collection;

namespace ztp_game.TemplateMethod
{
    abstract class AbstractLevelGenerator
    {
        protected char[,] level_array { get; set; }
        protected IBoardBuilder board_builder { get; set; }

        protected List<BlocksList> blockList = new List<BlocksList>();

        public SpriteCollection GetLevel() { return new SpriteCollection(); }

        public void BuildLevel() { }

        public void CreateLevelLogic(int height, int width)
        {
            this.level_array = new char[height, width];
            this.level_array = CreateBlocks(height, width);
            this.level_array = CreateThorns(height, width);
            this.level_array = CreateGaps(height, width);
            this.level_array = CreateExit(height, width);
            this.level_array = CreateCoins(height, width);

        }
        public abstract char[,] CreateBlocks(int height, int width);
        public abstract char[,] CreateThorns(int height, int width);
        public abstract char[,] CreateGaps(int height, int width);
        public abstract char[,] CreateExit(int height, int width);
        public abstract char[,] CreateCoins(int height, int width);
    }
}
