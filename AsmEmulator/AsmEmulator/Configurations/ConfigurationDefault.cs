using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace AsmEmulator.Configurations
{
    public class ConfigurationDefault : Configuration
    {
        public RandomAccessMemory RAM { get; private set; }
        public GraphicsProcessor GraphicsProcessor { get; private set; }
        public Accumulator Accumulator { get; private set; }
        public InputDevice InputDevice { get; private set; }
        public Random Random { get; private set; }

        public ConfigurationDefault(ReadonlyMemory memory, RandomAccessMemory ram, GraphicsProcessor graphicsProcessor, Accumulator accumulator, InputDevice inputDevice, Random random) : base(memory)
        {
            RAM = ram;
            GraphicsProcessor = graphicsProcessor;
            Accumulator = accumulator;
            InputDevice = inputDevice;
            Random = random;
        }

        public override void Update()
        {
            InputDevice.UpdateState();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GraphicsProcessor.Draw(spriteBatch);
        }

        public override void SetTextures(Texture2D onTexture, Texture2D offTexture)
        {
            GraphicsProcessor.SetTextures(onTexture, offTexture);
        }

        public override byte GetWorkingMemory()
        {
            return Accumulator.GetValue();
        }
    }
}
