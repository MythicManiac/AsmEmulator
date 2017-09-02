using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsmEmulator
{
    public class Emulator
    {
        public Configuration Configuration { get; private set; }
        public double TicksPerSecond { get; private set; }

        private InstructionSet _instructionSet;
        private long _elapsedTicks;
        private long _tickCounter;
        private long _tickRate;

        public Emulator(Configuration configuration, double ticksPerSecond)
        {
            Configuration = configuration;
            _instructionSet = configuration.InstructionSet;
            SetTickRate(ticksPerSecond);
        }

        public void Update(GameTime gameTime)
        {
            _elapsedTicks += gameTime.ElapsedGameTime.Ticks;
            Configuration.Update();
            if (_elapsedTicks >= _tickRate)
            {
                _elapsedTicks -= _tickRate;
                UpdateTick();
                _tickCounter += 1;
                //Console.Title = string.Format("Ticks elapsed: {0}", _tickCounter);
            }
        }

        public void SetTickRate(double ticksPerSecond)
        {
            TicksPerSecond = ticksPerSecond;
            _tickRate = (long)Math.Round(TimeSpan.TicksPerSecond * (1 / TicksPerSecond));
        }

        private string PadString(string input, int padding)
        {
            return string.Format("{0}{1}", input, new string(' ', padding - input.Length));
        }

        private void DebugState()
        {
            var pos = Convert.ToString(Configuration.Memory.GetPointer() + 1);
            var instruction = "";
            var memory = Convert.ToString(Configuration.GetWorkingMemory());
            if (_instructionSet.BytecodeInstructions.ContainsKey(Configuration.Memory.GetValue()))
            {
                instruction = _instructionSet.BytecodeInstructions[Configuration.Memory.GetValue()].Identifier;
            }
            else
            {
                instruction = Convert.ToString(Configuration.Memory.GetValue());
            }
            var padding = 5;
            Console.WriteLine("{0} - {1} - {2}",
                PadString(pos, padding),
                PadString(instruction, padding),
                PadString(memory, padding)
            );
        }

        private void UpdateTick()
        {
            DebugState();
            var instruction = Configuration.Memory.GetValue();
            _instructionSet.MicroinstructionAdvanceMainMemory();
            if (_instructionSet.BytecodeInstructions.ContainsKey(instruction))
            {
                _instructionSet.BytecodeInstructions[instruction].Action();
            }
            //if (_tickCounter % 2 == 0) { Console.WriteLine("Tick"); }
            //else { Console.WriteLine("Tock"); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Configuration.Draw(spriteBatch);
        }
    }
}
