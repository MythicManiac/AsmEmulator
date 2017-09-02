using System;

namespace AsmEmulator
{
#if WINDOWS || XBOX
    static class Program
    {
        static void Main(string[] args)
        {
            try { Console.Title = "CPU Emulator"; }
            catch { }
            using (Main game = new Main())
            {
                game.Run();
            }
        }
    }
#endif
}

