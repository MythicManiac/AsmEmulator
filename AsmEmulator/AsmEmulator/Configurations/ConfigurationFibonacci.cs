using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace AsmEmulator.Configurations
{
    class ConfigurationFibonacci : Configuration
    {
        public RandomAccessMemory RAM { get; private set; }
        public SegmentDisplay SegmentDisplay { get; private set; }
        public Accumulator Accumulator { get; private set; }

        public ConfigurationFibonacci(ReadonlyMemory memory, RandomAccessMemory ram, SegmentDisplay segmentDisplay, Accumulator accumulator) : base(memory)
        {
            RAM = ram;
            SegmentDisplay = segmentDisplay;
            Accumulator = accumulator;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SegmentDisplay.Draw(spriteBatch);
        }

        public override void SetTextures(Texture2D onTexture, Texture2D offTexture)
        {
            SegmentDisplay.SetTextures(onTexture, offTexture);
        }

        public override void SetFont(SpriteFont font)
        {
            SegmentDisplay.SetFont(font);
        }

        public override byte GetWorkingMemory()
        {
            return Accumulator.GetValue();
        }
    }
}
