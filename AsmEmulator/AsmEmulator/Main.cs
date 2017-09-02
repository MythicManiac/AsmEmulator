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
using AsmEmulator.InstructionSets;
using AsmEmulator.Configurations;

namespace AsmEmulator
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Emulator _emulator;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            var configuration = SetupFibonacci();
            _emulator = new Emulator(configuration, 4);

            base.Initialize();
        }

        private Configuration SetupFibonacci()
        {
            var compileInstructionSet = new Dictionary<string, string>()
            {
                    {"set", "0001"}, // Set
                    {"sto", "0010"}, // Store
                    {"add", "0011"}, // Add
                    {"ada", "0100"}, // Add from address
                    {"out", "0101"}, // Output
                    {"jmp", "0110"}, // Jump
                    {"lda", "0111"}, // Load
                    {"hlt", "1111"}  // Halt
            };
            AssemblyCompiler.CompileAssembly(compileInstructionSet, "fibonacci.txt", "bytecode.txt");
            var configuration = new ConfigurationFibonacci(
                new ReadonlyMemory("bytecode.txt"),
                new RandomAccessMemory(),
                new SegmentDisplay(graphics),
                new Accumulator()
            );
            var instructionSet = new InstructionSetFibonacci(configuration);
            configuration.AttachInstructionSet(instructionSet);
            return configuration;
        }

        private Configuration SetupDefault()
        {
            //AssemblyCompiler.CompileAssembly("assembly.txt", "bytecode.txt");
            var configuration = new ConfigurationDefault(
                new ReadonlyMemory("bytecode.txt"),
                new RandomAccessMemory(),
                new GraphicsProcessor(graphics),
                new Accumulator(),
                new ArrowkeyInput(),
                new Random()
            );
            var instructionSet = new InstructionSetDefault(configuration);
            configuration.AttachInstructionSet(instructionSet);
            return configuration;
        }

        private Texture2D GetSubtexture(Texture2D sourceTexture, Rectangle sourceRectangle)
        {
            var result = new Texture2D(GraphicsDevice, sourceRectangle.Width, sourceRectangle.Height);
            var data = new Color[sourceRectangle.Width * sourceRectangle.Height];
            sourceTexture.GetData<Color>(0, sourceRectangle, data, 0, data.Length);
            result.SetData<Color>(data);
            return result;
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var texture = Content.Load<Texture2D>("Tiles_445");
            var onTexture = GetSubtexture(texture, new Rectangle(18, 0, 16, 16));
            var offTexture = GetSubtexture(texture, new Rectangle(0, 0, 16, 16));
            var font = Content.Load<SpriteFont>("Consolas");
            _emulator.Configuration.SetTextures(onTexture, offTexture);
            _emulator.Configuration.SetFont(font);
        }

        protected override void Update(GameTime gameTime)
        {
            _emulator.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _emulator.Draw(spriteBatch);
        }
    }
}
