using Archipelago.Core.Util;
using System;
using System.IO;

namespace THAWAPClient.Helpers
{
    public class DebugWriter
    {
        private static readonly object _loglocationLock = new object();
        private static readonly object _logmissionLock = new object();
        private static readonly object _logdeathlinkLock = new object();
        private static readonly object _logplayerLock = new object();
        public static void LogMissionDebug(string message)
        {
            string logFolder = Path.Combine(AppContext.BaseDirectory, "Logs");

            Directory.CreateDirectory(logFolder);

            string logFile = Path.Combine(logFolder, "missiondebug.txt");

            lock (_logmissionLock)
            {
                File.AppendAllText(
                    logFile,
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}"
                );
            }
        }

        public static void LogLocationDebug(string message)
        {
            string logFolder = Path.Combine(AppContext.BaseDirectory, "Logs");

            Directory.CreateDirectory(logFolder);

            string logFile = Path.Combine(logFolder, "locationdebug.txt");

            lock (_loglocationLock)
            {
                File.AppendAllText(
                    logFile,
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}"
                );
            }
        }

        public static void LogDeathlinkDebug(string message)
        {
            string logFolder = Path.Combine(AppContext.BaseDirectory, "Logs");

            Directory.CreateDirectory(logFolder);

            string logFile = Path.Combine(logFolder, "deathlinkdebug.txt");

            lock (_logdeathlinkLock)
            {
                File.AppendAllText(
                    logFile,
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}"
                );
            }
        }

        public static void LogPlayerDebug(string message)
        {
            string logFolder = Path.Combine(AppContext.BaseDirectory, "Logs");

            Directory.CreateDirectory(logFolder);

            string logFile = Path.Combine(logFolder, "playerdebug.txt");

            lock (_logplayerLock)
            {
                File.AppendAllText(
                    logFile,
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}"
                );
            }
        }    
    }
}