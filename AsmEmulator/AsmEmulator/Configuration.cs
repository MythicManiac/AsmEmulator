using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace AsmEmulator
{
    public class Configuration
    {
        public ReadonlyMemory Memory { get; private set; }
        public InstructionSet InstructionSet { get; private set; }

        public Configuration(ReadonlyMemory memory) { Memory = memory; }

        public virtual void SetTextures(Texture2D onTexture, Texture2D offTexture) { }

        public virtual void SetFont(SpriteFont font) { }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public virtual void Update() { }

        public virtual byte GetWorkingMemory() { throw new NotImplementedException(); }

        public virtual void AttachInstructionSet(InstructionSet set)
        {
            if (InstructionSet != null) throw new Exception("InstructionSet already attached");
            InstructionSet = set;
        }
    }
}
