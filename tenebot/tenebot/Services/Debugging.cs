using System;
using Discord;

namespace tenebot.Services
{
    public static class Debugging
    {
        private static ConsoleColor GetColor(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Critical:
                    return ConsoleColor.Magenta;
                case LogSeverity.Error:
                    return ConsoleColor.Red;
                case LogSeverity.Warning:
                    return ConsoleColor.Yellow;
                case LogSeverity.Info:
                    return ConsoleColor.White;
                case LogSeverity.Verbose:
                    return ConsoleColor.DarkYellow;
                case LogSeverity.Debug:
                    return ConsoleColor.DarkGray;
                default:
                    return ConsoleColor.White;
            }
        }

        private static void PrintMessage(LogMessage message)
        {
            Console.ForegroundColor = GetColor(message.Severity);
            Console.Write($"{DateTime.Now,-19} [{message.Severity,8}]");
            Console.ResetColor();
            Console.Write($" {message.Source}: {message.Message}; {message.Exception}\n");
        }

        private static void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void Log(LogMessage message)
        {
            PrintMessage(message);
        }

        public static void TestLog()
        {
            LogMessage message = new LogMessage(LogSeverity.Critical, "Log testing", $"A random message");
            Log(message);

            message = new LogMessage(LogSeverity.Debug, "Log testing", $"A random message");
            Log(message);

            message = new LogMessage(LogSeverity.Error, "Log testing", $"A random message");
            Log(message);

            message = new LogMessage(LogSeverity.Info, "Log testing", $"A random message");
            Log(message);

            message = new LogMessage(LogSeverity.Verbose, "Log testing", $"A random message");
            Log(message);

            message = new LogMessage(LogSeverity.Warning, "Log testing", $"A random message");
            Log(message);
        }

        public static void DivideByZero()
        {
            int zero = 0;
            int x = 1 / zero;
        }
    }
}
