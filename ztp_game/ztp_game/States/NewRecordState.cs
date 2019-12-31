using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ztp_game.Components;

namespace ztp_game.States
{
    class NewRecordState : State
    {
        private List<Component> _components;
        private SpriteFont _font;
        private Champion champ;
        private string name;
        private bool isNewRekord;
        public static KeyboardState CurrentState;
        public static KeyboardState PreviousState;


        public NewRecordState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {


            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            _font = _content.Load<SpriteFont>("Fonts/Font");

            var submitButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(550, 570),
                Text = "Submit your score"
            };

            var backButton = new Button(buttonTexture, _font)
            {
                Position = new Vector2(550, 570),
                Text = "Go to main menu"
            };

            backButton.Click += backButton_Click;
            submitButton.Click += submitButton_Click;

            //wczytanie wyników list=RankingFile.getPlacements();

            if (list.Count < 10 || list[list.Count - 1].score < champ.Points)
            {
                _components = new List<Component>()
                {
                    submitButton
                };
                isNewRekord = true;
            }
            else
            {
                _components = new List<Component>()
                {
                    backButton
                };
                isNewRekord = false;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            string points = "Great job! You got " + champ.Points + " points!";
            string signature = "Please enter your three letter signature: ";

            Vector2 vector1 = new Vector2(625, 270);
            Vector2 vector2 = new Vector2(525, 320);
            Vector2 vector3 = new Vector2(780, 370);

            spriteBatch.Begin();

            spriteBatch.DrawString(_font, points, vector1, Color.White);
            if (isNewRekord)
            {
                spriteBatch.DrawString(_font, signature, vector2, Color.White);
                name = ReadCharacter(name);
                spriteBatch.DrawString(_font, name, vector3, Color.White);
            }

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            if (!HasThreeCharacters(name))
            {
                _game.ChangeState(new NewRekordState(_game, _graphicsDevice, _content, champ, name));
            }
            else
            {
                RankingFile.AddToList(name, champ.Points);
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
            }
        }
        private static bool HasThreeCharacters(string name)
        {
            if (name == null) return false;
            if (name.Length == 3) return true;
            else return false;
        }
        private static string ReadCharacter(string name)
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();

            StringBuilder sb = new StringBuilder();
            sb.Append(name);
            if (ActionWasJustPressed(Keys.Back))
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
            if (!HasThreeCharacters(sb.ToString()))
            {
                if (ActionWasJustPressed(Keys.Q))
                    sb.Append('Q');
                else if (ActionWasJustPressed(Keys.W))
                    sb.Append('W');
                else if (ActionWasJustPressed(Keys.E))
                    sb.Append('E');
                else if (ActionWasJustPressed(Keys.R))
                    sb.Append('R');
                else if (ActionWasJustPressed(Keys.T))
                    sb.Append('T');
                else if (ActionWasJustPressed(Keys.Y))
                    sb.Append('Y');
                else if (ActionWasJustPressed(Keys.U))
                    sb.Append('U');
                else if (ActionWasJustPressed(Keys.I))
                    sb.Append('I');
                else if (ActionWasJustPressed(Keys.O))
                    sb.Append('O');
                else if (ActionWasJustPressed(Keys.P))
                    sb.Append('P');
                else if (ActionWasJustPressed(Keys.A))
                    sb.Append('A');
                else if (ActionWasJustPressed(Keys.S))
                    sb.Append('S');
                else if (ActionWasJustPressed(Keys.D))
                    sb.Append('D');
                else if (ActionWasJustPressed(Keys.F))
                    sb.Append('F');
                else if (ActionWasJustPressed(Keys.G))
                    sb.Append('G');
                else if (ActionWasJustPressed(Keys.H))
                    sb.Append('H');
                else if (ActionWasJustPressed(Keys.J))
                    sb.Append('J');
                else if (ActionWasJustPressed(Keys.K))
                    sb.Append('K');
                else if (ActionWasJustPressed(Keys.L))
                    sb.Append('L');
                else if (ActionWasJustPressed(Keys.Z))
                    sb.Append('Z');
                else if (ActionWasJustPressed(Keys.X))
                    sb.Append('X');
                else if (ActionWasJustPressed(Keys.C))
                    sb.Append('C');
                else if (ActionWasJustPressed(Keys.V))
                    sb.Append('V');
                else if (ActionWasJustPressed(Keys.B))
                    sb.Append('B');
                else if (ActionWasJustPressed(Keys.N))
                    sb.Append('N');
                else if (ActionWasJustPressed(Keys.M))
                    sb.Append('M');
            }
            return sb.ToString();
        }
        public static bool ActionWasJustPressed(Keys key)
        {
            if (ActionIsPressed(key) && PreviousState.IsKeyUp(key))
                return true;
            else
                return false;

        }

        private static bool ActionIsPressed(Keys key)
        {
            if (CurrentState.IsKeyDown(key))
                return true;
            else
                return false;
        }
    }
}
