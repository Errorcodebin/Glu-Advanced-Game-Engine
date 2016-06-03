using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core
{
    static class SystemDebug
    {
        public static void LogDebug(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[LOG] Engine: " + message);
        }

        public static void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] Engine: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WARNING] Engine: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogFATAL(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[FATAL] Engine: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
