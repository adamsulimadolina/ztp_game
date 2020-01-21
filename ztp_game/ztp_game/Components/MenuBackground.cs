using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ztp_game.Components
{
    class MenuBackground : Component
    {
        
        public MenuBackground(Texture2D texture)
        {
            _texture = texture;

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
           
        }
    }
}
