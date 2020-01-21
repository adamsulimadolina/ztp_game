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
using ztp_game.Input;
using ztp_game.Logic;
using ztp_game.Memento;
using ztp_game.ObserverTemplate;
using ztp_game.Sprites;
using ztp_game.Strategy;
using ztp_game.TemplateMethod;

namespace ztp_game.States
{
    class GameState : State
    {
        private SpriteFont _font;
        private Champion _champ;
        private static AbstractLevelGenerator level_generator;
        private InputManager inputManager;
        private bool newGame;
        private static int pick = 1;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content, bool newGame) : base(game, graphicsDevice, content)
        {

            this.newGame = newGame;
            _font = content.Load<SpriteFont>("Components/Font");
            _champ = Champion.GetInstance();
            _champ.SetSoundManagerContent(content);
            _champ.Attach(game);


            if (game.getEasyLevel() == true)
                level_generator = new EasyLevelGenerator(content);
            else
                level_generator = new HardLevelGenerator(content);
            inputManager = InputManager.GetInstance();
            Champion.SetContent(content);
            if (newGame)
            {
                _champ.ResetValues();
                setLevel();
            }
            if (_champ.level == 1)
                _champ.SetValuesToStandard();
            _game.PlaySong("gameplay");
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
            if (inputManager.ActionWasJustPressed("Back"))
            {
                _game.ChangeState(new ConfirmExitState(_game, _graphicsDevice, _content));
                return;
            }
            if (Champion.GetInstance().health <= 0)
            {
                _champ.level = 1;

                _champ.NotifyObservers();
                _game.ChangeState(new NewRecordState(_game, _graphicsDevice, _content));


                return;
            }
            
            Champion.GetInstance().Update();
            
        }

        public static void setLevel()
        {
            if (pick == 1) pick = 2;
            else if (pick == 2) pick = 3;
            else pick = 1;
            level_generator.setPick(pick);
            level_generator.ResetBlocksList();
            level_generator.CreateLevelLogic(Screen.getHeight(), Screen.getWidth());
        }



        public static void RemoveCoinFromArray(int height, int width)
        {
            level_generator.level_array[height, width] = ' ';
        }


        public void ReadSave(SaveMemento save)
        {
            if (save != null && !newGame)
            {
                Console.WriteLine("PICK MEMENTO: " + save.GetPick());
                level_generator.level_array = save.GetLevelArray();
                _champ.level = save.GetLevel();
                _champ.ResetValues();
                _champ.points = save.GetPoints();
                _champ.health = save.GetHealth();
                _champ.Velocity = save.GetVelocity();
                _champ.Speed = save.GetSpeed();
                _champ.Position = save.GetPosition();
                _champ.ChangeDirection(save.GetDirection());
                pick = save.GetPick();
                level_generator.setPick(pick);
                level_generator.BuildLevel(Screen.getHeight(), Screen.getWidth());
            }
        }


        public SaveMemento Save()
        {
            return new SaveMemento(level_generator.level_array, _champ.points, _champ.level, _champ.health, _champ.Velocity, _champ.Position, _champ.GetDirection(), pick, _champ.Speed);

        }
    }
}
