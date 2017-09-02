using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AsmEmulator
{
    public abstract class Memory
    {
        protected byte[] _data;
        protected uint _pointer;

        public Memory() { }

        protected void Initialize(int size)
        {
            _data = new byte[size];
        }

        public void SetPointer(uint pos)
        {
            _pointer = pos;
        }

        public uint GetPointer()
        {
            return _pointer;
        }
            
        public byte GetValue()
        {
            return _data[_pointer];
        }
    }

    public class RandomAccessMemory : Memory
    {
        public RandomAccessMemory()
        {
            Initialize(512);
        }

        public void SetValue(byte value)
        {
            if (_pointer + 1 == _data.Length) { return; }
            _data[_pointer] = value;
        }
    }

    public class ReadonlyMemory : Memory
    {
        public ReadonlyMemory(string filepath)
        {
            var lines = File.ReadAllLines(filepath);
            Initialize(lines.Length);
            for (var i = 0; i < _data.Length; i++)
            {
                _data[i] = Convert.ToByte(lines[i], 2);
            }
        }
    }

    public class Accumulator
    {
        private byte _value;
        public Accumulator() { _value = 0; }

        public byte GetValue() { return _value; }
        public void SetValue(byte value) { _value = value; }
    }
}
