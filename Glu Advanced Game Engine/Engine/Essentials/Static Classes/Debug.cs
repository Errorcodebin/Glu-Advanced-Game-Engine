using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    static class Debug
    {
        public static void LogDebug(string message)
        {
            Console.WriteLine("[DEBUG]: " + message);
        }

        public static void Log(string message)
        {
            Console.WriteLine("[LOG]: " + message);
        }

        public static void LogSucess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[SUCCESS]: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WARNING]: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR]: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
