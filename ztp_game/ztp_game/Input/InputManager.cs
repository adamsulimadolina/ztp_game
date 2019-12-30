using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ztp_game.Input
{
    public class InputManager
    {
        public KeyboardState CurrentState;
        public KeyboardState PreviousState;
        public Dictionary<string, Keys> KeyBindings { get; private set; }

        public InputManager()
        {
            KeyBindings = new Dictionary<string, Keys>
            {
                {"MoveUp", Keys.Up },
                {"MoveRight", Keys.Right },
                {"MoveLeft", Keys.Left },
                {"MoveDown", Keys.Down },
                {"Accept", Keys.Enter },
                {"Back", Keys.Escape }
            };
        }

        public void Update(GameTime gameTime)
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
        }

        public bool ActionWasJustPressed(string actionName)
        {
            if (KeyBindings.ContainsKey(actionName))
            {
                if (CurrentState.IsKeyDown(KeyBindings[actionName]))
                {
                    if (PreviousState.IsKeyUp(KeyBindings[actionName]))
                        return true;
                }
            }
            else
                throw new ArgumentException("This action is not definded in Keybindings dictionary");

            return false;

        }

        public bool ActionIsPressed(string actionName)
        {
            if (KeyBindings.ContainsKey(actionName))
            {
                if (CurrentState.IsKeyDown(KeyBindings[actionName]))
                    return true;
            }
            else
                throw new ArgumentException("This action is not definded in Keybindings dictionary");

            return false;
        }

    }
}
