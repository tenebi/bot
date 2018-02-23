using System;
using Discord;
using System.IO;

namespace tenebot.Services
{
    public static class Debugging
    {
        private static string path = Path.Combine(Directory.GetCurrentDirectory(), "logs");
        private static string fullPath = null;

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

        private static void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void PrintMessage(LogMessage message)
        {
            Console.ForegroundColor = GetColor(message.Severity);
            Console.Write($"{DateTime.Now,-19} [{message.Severity,8}]");
            Console.ResetColor();
            Console.Write($" {message.Source}: {message.Message}; {message.Exception}\n");
        }

        private static void SetLogPath()
        {
            string newPath = Path.Combine(path, DateTime.Now.ToString() + ".tenelog").Replace(":", ".");
            newPath = (newPath.Substring(0, 3)).Replace(".", ":") + newPath.Substring(3, newPath.Length - 3);

            try
            {
                if (!Directory.Exists(path))
                {
                    LogNoLog("Debugging.Log", $"No log directory found, creating at '{path}'", LogSeverity.Debug);
                    Directory.CreateDirectory(path);
                }

                fullPath = newPath;
                File.Create(fullPath).Dispose();
                LogNoLog("Debugging.Log", $"Created new log file for session at '{fullPath}'", LogSeverity.Debug);
            }
            catch (Exception e)
            {
                LogNoLog(new LogMessage(LogSeverity.Error, "SetLogPath", "Failed to create directory", e));
            }
        }

        private static void LogMessage(LogMessage message)
        {
            PrintMessage(message);
            LogToFile(message);
        }

        private static void LogToFile(LogMessage message)
        {
            if (fullPath == null)
                SetLogPath();
            else
                File.AppendAllText(fullPath, message.ToString() + "\n");
        }

        /// <summary>
        /// Prints a message on the debug console and writes to output log.
        /// </summary>
        /// <param name="message">Log message for the message.</param>
        public static void Log(LogMessage message)
        {
            LogMessage(message);
        }

        /// <summary>
        /// Prints a message on the debug console and writes to output log.
        /// </summary>
        /// <param name="source">Source where the message is coming from (parent class of function or similar).</param>
        /// <param name="message">Message to print to console.</param>
        public static void Log(string source, string message)
        {
            LogMessage(new LogMessage(LogSeverity.Info, source, message));
        }

        /// <summary>
        /// Prints a message on the debug console and writes to output log.
        /// </summary>
        /// <param name="source">Source where the message is coming from (parent class of function or similar).</param>
        /// <param name="message">Message to print to console.</param>
        /// <param name="severity">Message severity</param>
        public static void Log(string source, string message, LogSeverity severity = LogSeverity.Info)
        {
            LogMessage(new LogMessage(severity, source, message));
        }

        /// <summary>
        /// Prints a message on the debug console without writing it to output log.
        /// </summary>
        /// <param name="message">Log message for the message.</param>
        public static void LogNoLog(LogMessage message)
        {
            PrintMessage(message);
        }

        /// <summary>
        /// Prints a message on the debug console and writes to output log.
        /// </summary>
        /// <param name="source">Source where the message is coming from (parent class of function or similar).</param>
        /// <param name="message">Message to print to console.</param>
        public static void LogNoLog(string source, string message)
        {
            PrintMessage(new LogMessage(LogSeverity.Info, source, message));
        }

        /// <summary>
        /// Prints a message on the debug console and writes to output log.
        /// </summary>
        /// <param name="source">Source where the message is coming from (parent class of function or similar).</param>
        /// <param name="message">Message to print to console.</param>
        /// <param name="severity">Message severity</param>
        public static void LogNoLog(string source, string message, LogSeverity severity = LogSeverity.Info)
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
