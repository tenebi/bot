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

        /// <summary>
        /// Prints a message on the debug console.
        /// </summary>
        /// <param name="message">Log message for the message.</param>
        public static void Log(LogMessage message)
        {
            PrintMessage(message);
        }

        /// <summary>
        /// Prints a message on the debug console
        /// </summary>
        /// <param name="source">Source where the message is coming from (parent class of function or similar).</param>
        /// <param name="message">Message to print to console.</param>
        public static void Log(string source, string message)
        {
            PrintMessage(new LogMessage(LogSeverity.Info, source, message));
        }

        /// <summary>
        /// Prints a message on the debug console
        /// </summary>
        /// <param name="source">Source where the message is coming from (parent class of function or similar).</param>
        /// <param name="message">Message to print to console.</param>
        /// <param name="severity">Message severity</param>
        public static void Log(string source, string message, LogSeverity severity = LogSeverity.Info)
        {
            PrintMessage(new LogMessage(severity, source, message));
        }

        /// <summary>
        /// Outputs test messages in all severity colors.
        /// </summary>
        public static void TestLog()
        {
            LogMessage message = new LogMessage(LogSeverity.Critical, "Log testing", $"Lorem ipsum dolor sit amet");
            Log(message);

            message = new LogMessage(LogSeverity.Debug, "Log testing", $"Consectetur adipiscing elit");
            Log(message);

            message = new LogMessage(LogSeverity.Error, "Log testing", $"Nam in diam maximus");
            Log(message);

            message = new LogMessage(LogSeverity.Info, "Log testing", $"Bibendum nisi eu");
            Log(message);

            message = new LogMessage(LogSeverity.Verbose, "Log testing", $"Pulvinar lorem");
            Log(message);

            message = new LogMessage(LogSeverity.Warning, "Log testing", $"Morbi nulla sapien");
            Log(message);
        }

        /// <summary>
        /// Divides by zero, meant to cause an exception for testing try catch methods. Only to be used in debugging.
        /// </summary>
        public static void DivideByZero()
        {
            int zero = 0;
            int x = 1 / zero;
        }
    }
}
