using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsmEmulator.Configurations;

namespace AsmEmulator.InstructionSets
{
    public class InstructionSetDefault : InstructionSet
    {
        public ConfigurationDefault Configuration { get { return (ConfigurationDefault)_configuration; } }
        public InstructionSetDefault(ConfigurationDefault configuration) : base(configuration) { }

        protected override void Initialize()
        {
            AddInstruction(new Instruction("00000001", "add", Add));
            AddInstruction(new Instruction("00000010", "sub", Substract));
            AddInstruction(new Instruction("00000011", "jmp", Jump));
            AddInstruction(new Instruction("00000100", "jie", JumpIfEqual));
            AddInstruction(new Instruction("00000101", "jgt", JumpIfGreaterThan));
            AddInstruction(new Instruction("00000110", "jlt", JumpIfLessThan));
            AddInstruction(new Instruction("00000111", "lda", Load));
            AddInstruction(new Instruction("00001000", "sto", Store));
            AddInstruction(new Instruction("00001001", "out", Output));
            AddInstruction(new Instruction("00001010", "ota", OutputFromAddress));
            AddInstruction(new Instruction("00001011", "inp", Input));
            AddInstruction(new Instruction("00001100", "hlt", Halt));
            AddInstruction(new Instruction("00001101", "and", And));
            AddInstruction(new Instruction("00001110", "set", Set));
            AddInstruction(new Instruction("00001111", "rnd", Rand));
            AddInstruction(new Instruction("00010000", "drw", Draw));
        }

        private void MicroinstructionJump()
        {
            var pointer = Configuration.Memory.GetValue();
            Configuration.Memory.SetPointer(pointer);
        }

        private void Add()
        {
            var value1 = Configuration.Accumulator.GetValue();
            var value2 = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            var result = value1 + value2;
            if (result > 255) result = 255;
            Configuration.Accumulator.SetValue((byte)result);
        }

        private void Substract()
        {
            var value1 = Configuration.Accumulator.GetValue();
            var value2 = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            var result = value1 - value2;
            if (result < 0) result = 0;
            Configuration.Accumulator.SetValue((byte)result);
        }

        private void Jump()
        {
            MicroinstructionJump();
        }

        private void JumpIfEqual()
        {
            var value1 = Configuration.Accumulator.GetValue();
            var value2 = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            if (value1 == value2)
            {
                MicroinstructionJump();
                return;
            }
            MicroinstructionAdvanceMainMemory();
        }

        private void JumpIfGreaterThan()
        {
            var value1 = Configuration.Accumulator.GetValue();
            var value2 = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            if (value1 > value2)
            {
                MicroinstructionJump();
                return;
            }
            MicroinstructionAdvanceMainMemory();
        }

        private void JumpIfLessThan()
        {
            var value1 = Configuration.Accumulator.GetValue();
            var value2 = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            if (value1 < value2)
            {
                MicroinstructionJump();
                return;
            }
            MicroinstructionAdvanceMainMemory();
        }

        private void And()
        {
            var value1 = Configuration.Accumulator.GetValue();
            var value2 = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            var value3 = (byte)(value1 & value2);
            Configuration.Accumulator.SetValue(value3);
        }

        private void Or()
        {
            var value1 = Configuration.Accumulator.GetValue();
            var value2 = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            var value3 = (byte)(value1 | value2);
            Configuration.Accumulator.SetValue(value3);
        }

        private void Set()
        {
            var value = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            Configuration.Accumulator.SetValue(value);
        }

        private void Rand()
        {
            var value = (byte)Configuration.Random.Next(256);
            Configuration.Accumulator.SetValue(value);
        }

        private void Load()
        {
            var pointer = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            Configuration.RAM.SetPointer(pointer);
            var value = Configuration.RAM.GetValue();
            Configuration.Accumulator.SetValue(value);
        }

        private void Store()
        {
            var pointer = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            Configuration.RAM.SetPointer(pointer);
            var value = Configuration.Accumulator.GetValue();
            Configuration.RAM.SetValue(value);
        }

        private void Output()
        {
            var value = Configuration.Accumulator.GetValue();
            Configuration.GraphicsProcessor.Input(value);
        }

        private void OutputFromAddress()
        {
            var pointer = Configuration.Memory.GetValue();
            MicroinstructionAdvanceMainMemory();
            Configuration.RAM.SetPointer(pointer);
            var value = Configuration.RAM.GetValue();
            Configuration.GraphicsProcessor.Input(value);
        }

        private void Input()
        {
            var value = Configuration.InputDevice.GetState();
            Configuration.Accumulator.SetValue(value);
        }

        private void Halt()
        {
        }

        private void Draw()
        {
            Configuration.GraphicsProcessor.Refresh();
        }
    }
}
