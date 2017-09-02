using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsmEmulator
{
    public class GraphicsProcessor
    {
        private Display _display;

        public GraphicsProcessor(GraphicsDeviceManager graphics)
        {
            _display = new Display(graphics, 8, 8);
        }

        public void Input(byte value)
        {
            var input = new BitArray(new byte[] { value });
            var x = (input[0] ? 1 : 0) + (input[1] ? 2 : 0) + (input[2] ? 4 : 0);
            var y = (input[4] ? 1 : 0) + (input[5] ? 2 : 0) + (input[6] ? 4 : 0);
            _display.ToggleAt(x, y);
        }

        public void SetTextures(Texture2D onTexture, Texture2D offTexture)
        {
            _display.SetOnTexture(onTexture);
            _display.SetOffTexture(offTexture);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _display.Draw(spriteBatch);
        }

        public void Refresh()
        {
            _display.Refresh();
        }
    }
}
