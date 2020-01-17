using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ztp_game.Logic;
using ztp_game.Memento;
using ztp_game.ObserverTemplate;
using ztp_game.Sprites;
using ztp_game.Strategy;
using ztp_game.TemplateMethod;

namespace ztp_game.States
{
    class GameState : State , Observer
    {
        private SpriteFont _font;
        private Champion _champ;
        private static AbstractLevelGenerator level_generator;
        private SoundManager soundManager;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            saveCaretaker.RemoveSave();
            _font = content.Load<SpriteFont>("Components/Font");
            _champ = Champion.GetInstance();
            _champ.ResetValues();
            level_generator = new EasyLevelGenerator(content);
            Champion.SetContent(content);
            setLevel();
            
        }

        
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, SaveMemento save) : base(game, graphicsDevice, content)
        {
            _font = content.Load<SpriteFont>("Components/Font");
            _champ = Champion.GetInstance();
            level_generator = new EasyLevelGenerator(content);
            Champion.SetContent(content);
            level_generator.level_array = save.GetLevelArray();
            _champ.points = save.GetPoints();
            _champ.level = save.GetLevel();
            _champ.health = save.GetHealth();
            _champ.Velocity = save.GetVelocity();
            _champ.Position = save.GetPosition();
            if (_champ.Velocity.Y < 0)
                _champ.ChangeDirection(Direction.Up);
            else
                _champ.ChangeDirection(Direction.Down);
            level_generator.BuildLevel(Screen.getHeight(), Screen.getWidth());

            soundManager = new SoundManager(content);
            soundManager.LoadFiles();
            soundManager.PlaySong("gameplay");
        }


    

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Begin();
            
            //_champ.Draw(spriteBatch);
            //_board.Draw(spriteBatch);
            spriteBatch.DrawString(_font, "Score: " + _champ.points + "  ", new Vector2(0, Screen.getHeight() * 16), Color.White);
            spriteBatch.DrawString(_font, "  Level: " + _champ.level + "  ", new Vector2((Screen.getWidth() / 3) * 16, Screen.getHeight() * 16), Color.White);
            spriteBatch.DrawString(_font, "  Health: " + _champ.health + "  ", new Vector2((Screen.getWidth() * 2 / 3) * 16, Screen.getHeight() * 16), Color.White);

            foreach (var sprite in level_generator.sprite_collection.GetList())
            {
                var sprite_draw = (Sprite)sprite;
                sprite_draw.Draw(spriteBatch);
            }
            _champ.Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                saveCaretaker.AddMemento(new SaveMemento(level_generator.level_array, _champ.points, _champ.level, _champ.health, _champ.Velocity, _champ.Position));
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
                return;
            }
            if (Champion.GetInstance().health <= 0)
            {
                _champ.level = 1;


                saveCaretaker.RemoveSave();

                _champ.notifyObservers();
                _game.ChangeState(new NewRecordState(_game, _graphicsDevice, _content));

                return;
            }
            //_champ.Update(_sprites);
            if (Screen.getChange())
            {
                Screen.ChangeMap(false);
                _champ.points += 15;
                Screen.setLevel(Screen.getLevel() + 1);
                _champ.health = 3;

                _game.ChangeState(new GameState(_game, _graphicsDevice, _content));

            }
            Champion.GetInstance().Update();
            
        }

        public static void setLevel()
        {
            level_generator.ResetBlocksList();
            level_generator.CreateLevelLogic(Screen.getHeight(), Screen.getWidth());
        }



        public static void RemoveCoinFromArray(int height, int width)
        {
            level_generator.level_array[height, width] = ' ';
        }

        public void update()
        {
            //usunięcie zapisu z memento

        }
    }
}
