using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AsmEmulator
{
    public class Display
    {
        private bool[,] _matrix;
        private bool[,] _displayMatrix;
        private Texture2D _onTexture;
        private Texture2D _offTexture;
        private GraphicsDeviceManager _graphics;
        
        public int Width { get { return _matrix.GetLength(0); } }
        public int Height { get { return _matrix.GetLength(1); } }

        public Display(GraphicsDeviceManager graphics, int width, int height)
        {
            _graphics = graphics;
            _matrix = new bool[width, height];
            _displayMatrix = new bool[width, height];
            _graphics.PreferredBackBufferWidth = Width * 16;
            _graphics.PreferredBackBufferHeight = Height * 16 + 16;
            _graphics.ApplyChanges();
        }

        public void ToggleAt(int x, int y)
        {
            _matrix[x, y] = !_matrix[x, y];
        }

        public void TurnOnAt(int x, int y)
        {
            _matrix[x, y] = true;
        }

        public void TurnOffAt(int x, int y)
        {
            _matrix[x, y] = false;
        }

        public void SetOnTexture(Texture2D texture)
        {
            _onTexture = texture;
        }

        public void SetOffTexture(Texture2D texture)
        {
            _offTexture = texture;
        }

        public void Refresh()
        {
            Array.Copy(_matrix, 0, _displayMatrix, 0, _matrix.Length);
        }

        public void Clear()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _matrix[x, y] = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var texture = _offTexture;
                    if (_displayMatrix[x, y]) { texture = _onTexture; }
                    spriteBatch.Draw(texture, new Vector2(x * texture.Width, y * texture.Height), Color.White);
                }
            }
            spriteBatch.End();
        }
    }
}
