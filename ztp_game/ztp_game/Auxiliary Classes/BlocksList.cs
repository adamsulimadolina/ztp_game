using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Logic;

namespace ztp_game.Auxiliary_Classes
{
    class BlocksList
    {
        private int start_x;
        private int finish_x;
        private string direction;
        private int height;
        public static string direction_up = "up";
        public static string direction_full = "full";
        public static string direction_down = "down";


        public BlocksList(int start_x, int finish_x, int direction, int height)
        {
            this.start_x = start_x;
            this.finish_x = finish_x;
            this.height = height;
            if (direction == 0) this.direction = direction_down;
            else if (direction == 1) this.direction = direction_up;
            else if (direction == 2)
            {
                this.direction = direction_full;
                this.height = Screen.getHeight() - 4; //wysokosc
            }
        }

        public int getWidth()
        {
            return finish_x - start_x;
        }

        public int getStartX()
        {
            return start_x;
        }

        public int getFinishX()
        {
            return finish_x;
        }

        public int getHeight()
        {
            return height;
        }

        public string getDirection()
        {
            return direction;
        }

    }
}
