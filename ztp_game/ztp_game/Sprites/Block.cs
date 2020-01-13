﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ztp_game.Sprites
{
    public class Block: Sprite
    {
        public enum LevelType
        {
            Magma,
            Ice,
            Normal
        }

        public LevelType level_type;

        public void SetType(LevelType type)
        {
            this.level_type = type;
        }



    }
}
