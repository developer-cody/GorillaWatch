using BepInEx.Logging;

namespace TheGorillaWatch.Utilities
{
    public static class Logger
    {
        public static ManualLogSource log;

        public static void Initialize() => BepInEx.Logging.Logger.CreateLogSource($"{Constants.NAME}, {Constants.VERS}");

        public static void Info(string msg, params object[] args) => Log(LogLevel.Info, msg, args);
        public static void Warn(string msg, params object[] args) => Log(LogLevel.Warning, msg, args);
        public static void Error(string msg, params object[] args) => Log(LogLevel.Error, msg, args);
        public static void Fatal(string msg, params object[] args) => Log(LogLevel.Fatal, msg, args);

        public static void Debug(string msg, params object[] args)
        {
#if DEBUG
            Log(LogLevel.Debug, msg, args);
#endif
        }

        private static void Log(LogLevel level, string msg, params object[] args)
        {
            if (log == null) throw new System.InvalidOperationException("Logger not initialized.");
            if (string.IsNullOrEmpty(msg)) { log.Log(level, "Empty log"); return; }

            try { log.Log(level, args.Length > 0 ? string.Format(msg, args) : msg); }
            catch (System.FormatException) { log.LogError("Bad format"); log.Log(level, msg); }
        }

        public static void LogEx(System.Exception ex, string ctx = null)
        {
            if (log == null) throw new System.InvalidOperationException("Logger not initialized.");
            if (ex == null) return;
            log.LogError(ctx != null ? $"Error in {ctx}: {ex.Message}\n{ex.StackTrace}" : $"{ex.Message}\n{ex.StackTrace}");
        }

        public static void Dispose() => log?.Dispose();
    }
}