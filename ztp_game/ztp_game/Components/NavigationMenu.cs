using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Input;
using ztp_game.States;

namespace ztp_game.Components
{
    class NavigationMenu: Component
    {
        private List<Button> buttons;
        private InputManager inputManager;
        protected int currentlySelectedButton = 0;

        public NavigationMenu(List<Button> buttons)
        {
            this.buttons = buttons;
            inputManager = InputManager.GetInstance();
        }

        protected void Navigate()
        {
            if (inputManager.ActionWasJustPressed("MoveUp"))
            {
                currentlySelectedButton = (currentlySelectedButton - 1) % buttons.Count;
                //there is no button higher than this button -> set it to the index of last button
                if (currentlySelectedButton == -1)
                    currentlySelectedButton = buttons.Count - 1;
            }
            else if (inputManager.ActionWasJustPressed("MoveDown"))
            {
                currentlySelectedButton = (currentlySelectedButton + 1) % buttons.Count;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var button in buttons)
                button.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            ChangeStateOfButtons();
            UpdateButtons(gameTime);
            Navigate();
        }

        private void UpdateButtons(GameTime gameTime)
        {
            foreach (var button in buttons)
                button.Update(gameTime);
        }

        private void ChangeStateOfButtons()
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Selected = false;
            buttons[currentlySelectedButton].Selected = true;
        }
    }
}
