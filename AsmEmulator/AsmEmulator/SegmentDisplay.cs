using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsmEmulator
{
    class SegmentDisplay
    {
        private Display _display;

        private int _currentOffsetX;
        private int _currentOffsetY;

        private int _currentValue;
        private SpriteFont _font;

        public SegmentDisplay(GraphicsDeviceManager graphics)
        {
            _display = new Display(graphics, 9, 7);
        }

        public void Input(byte value)
        {
            _display.Clear();
            _currentValue = value;
            var input = new BitArray(new byte[] { value });
            var first = (input[0] ? 1 : 0) + (input[1] ? 2 : 0) + (input[2] ? 4 : 0) + (input[3] ? 8 : 0);
            var second = (input[4] ? 1 : 0) + (input[5] ? 2 : 0) + (input[6] ? 4 : 0) + (input[7] ? 8 : 0);
            EnableHexadecimal(first, 5, 1);
            EnableHexadecimal(second, 1, 1);
            _display.Refresh();
        }

        private void EnableHexadecimal(int value, int offsetX, int offsetY)
        {
            _currentOffsetX = offsetX;
            _currentOffsetY = offsetY;

            switch (value)
            {
                case 0:
                    EnableTop();
                    EnableTopLeft();
                    EnableTopRight();
                    EnableBottomLeft();
                    EnableBottomRight();
                    EnableBottom();
                    break;

                case 1:
                    EnableTopRight();
                    EnableBottomRight();
                    break;

                case 2:
                    EnableTop();
                    EnableTopRight();
                    EnableMiddle();
                    EnableBottomLeft();
                    EnableBottom();
                    break;

                case 3:
                    EnableTop();
                    EnableTopRight();
                    EnableMiddle();
                    EnableBottomRight();
                    EnableBottom();
                    break;

                case 4:
                    EnableTopLeft();
                    EnableTopRight();
                    EnableMiddle();
                    EnableBottomRight();
                    break;

                case 5:
                    EnableTop();
                    EnableTopLeft();
                    EnableMiddle();
                    EnableBottomRight();
                    EnableBottom();
                    break;

                case 6:
                    EnableTop();
                    EnableTopLeft();
                    EnableMiddle();
                    EnableBottomLeft();
                    EnableBottomRight();
                    EnableBottom();
                    break;

                case 7:
                    EnableTop();
                    EnableTopRight();
                    EnableBottomRight();
                    break;

                case 8:
                    EnableTop();
                    EnableTopLeft();
                    EnableTopRight();
                    EnableMiddle();
                    EnableBottomLeft();
                    EnableBottomRight();
                    EnableBottom();
                    break;

                case 9:
                    EnableTop();
                    EnableTopLeft();
                    EnableTopRight();
                    EnableMiddle();
                    EnableBottomRight();
                    EnableBottom();
                    break;

                case 10:
                    EnableTop();
                    EnableTopLeft();
                    EnableTopRight();
                    EnableMiddle();
                    EnableBottomLeft();
                    EnableBottomRight();
                    break;

                case 11:
                    EnableTop();
                    EnableTopLeft();
                    EnableTopRight();
                    EnableMiddle();
                    EnableBottomLeft();
                    EnableBottomRight();
                    EnableBottom();
                    TurnOffForSegment(2, 0);
                    TurnOffForSegment(2, 2);
                    TurnOffForSegment(2, 4);
                    break;

                case 12:
                    EnableTop();
                    EnableTopLeft();
                    EnableBottomLeft();
                    EnableBottom();
                    break;

                case 13:
                    EnableTop();
                    EnableTopLeft();
                    EnableTopRight();
                    EnableBottomLeft();
                    EnableBottomRight();
                    EnableBottom();
                    TurnOffForSegment(2, 0);
                    TurnOffForSegment(2, 4);
                    break;

                case 14:
                    EnableTop();
                    EnableTopLeft();
                    EnableMiddle();
                    EnableBottomLeft();
                    EnableBottom();
                    break;

                case 15:
                    EnableTop();
                    EnableTopLeft();
                    EnableMiddle();
                    EnableBottomLeft();
                    break;
            }
        }

        private void EnableTop()
        {
            TurnOnForSegment(0, 0);
            TurnOnForSegment(1, 0);
            TurnOnForSegment(2, 0);
        }

        private void EnableTopLeft()
        {
            TurnOnForSegment(0, 0);
            TurnOnForSegment(0, 1);
            TurnOnForSegment(0, 2);
        }

        private void EnableTopRight()
        {
            TurnOnForSegment(2, 0);
            TurnOnForSegment(2, 1);
            TurnOnForSegment(2, 2);
        }

        private void EnableMiddle()
        {
            TurnOnForSegment(0, 2);
            TurnOnForSegment(1, 2);
            TurnOnForSegment(2, 2);
        }

        private void EnableBottomLeft()
        {
            TurnOnForSegment(0, 2);
            TurnOnForSegment(0, 3);
            TurnOnForSegment(0, 4);
        }

        private void EnableBottomRight()
        {
            TurnOnForSegment(2, 2);
            TurnOnForSegment(2, 3);
            TurnOnForSegment(2, 4);
        }

        private void EnableBottom()
        {
            TurnOnForSegment(0, 4);
            TurnOnForSegment(1, 4);
            TurnOnForSegment(2, 4);
        }

        private void TurnOnForSegment(int x, int y)
        {
            _display.TurnOnAt(_currentOffsetX + x, _currentOffsetY + y);
        }

        private void TurnOffForSegment(int x, int y)
        {
            _display.TurnOffAt(_currentOffsetX + x, _currentOffsetY + y);
        }

        public void SetTextures(Texture2D onTexture, Texture2D offTexture)
        {
            _display.SetOnTexture(onTexture);
            _display.SetOffTexture(offTexture);
        }

        public void SetFont(SpriteFont font)
        {
            _font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _display.Draw(spriteBatch);
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, string.Format("{0} / {1}", _currentValue, _currentValue.ToString("X2")), new Vector2(2, _display.Height * 16), Color.White);
            spriteBatch.End();
        }

        public void Refresh()
        {
            _display.Refresh();
        }
    }
}
