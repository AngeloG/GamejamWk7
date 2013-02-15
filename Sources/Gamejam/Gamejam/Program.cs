using System;

namespace Gamejam
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Gamejam game = new Gamejam())
            {
                game.Run();
            }
        }
    }
#endif
}

