using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Batch_Scripter
{
    internal static class BatchScriptBuilder
    {
        private const string ScriptPrefix = "BatchScripter_";

        public static string CreateTemporaryScript(
            IEnumerable<string> drawingFiles,
            string userScript,
            bool saveDrawings)
        {
            string folder = Path.Combine(Path.GetTempPath(), "BatchScripter");
            Directory.CreateDirectory(folder);

            string path = Path.Combine(folder,
                ScriptPrefix + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" +
                Guid.NewGuid().ToString("N") + ".scr");

            using (StreamWriter writer = new StreamWriter(
                path,
                false,
                new UTF8Encoding(false)))
            {
                foreach (string drawingFile in drawingFiles)
                {
                    writer.WriteLine("_.OPEN \"" + drawingFile + "\"");
                    WriteUserScript(writer, userScript);

                    if (saveDrawings)
                    {
                        writer.WriteLine("_.QSAVE");
                        writer.WriteLine("_.CLOSE");
                    }
                    else
                    {
                        writer.WriteLine("_.CLOSE");
                        writer.WriteLine("_N");
                    }
                }
            }

            return path;
        }

        internal static void WriteUserScript(TextWriter writer, string userScript)
        {
            string normalized = (userScript ?? string.Empty)
                .Replace("\r\n", "\n")
                .Replace('\r', '\n');
            string[] lines = normalized.Split(new[] { '\n' }, StringSplitOptions.None);

            int firstMeaningfulLine = 0;
            while (firstMeaningfulLine < lines.Length &&
                   string.IsNullOrWhiteSpace(lines[firstMeaningfulLine]))
            {
                firstMeaningfulLine++;
            }

            int lastMeaningfulLine = lines.Length - 1;
            while (lastMeaningfulLine >= firstMeaningfulLine &&
                   string.IsNullOrWhiteSpace(lines[lastMeaningfulLine]))
            {
                lastMeaningfulLine--;
            }

            for (int i = firstMeaningfulLine; i <= lastMeaningfulLine; i++)
            {
                string line = lines[i] ?? string.Empty;
                writer.WriteLine(line);

                // This matches the proven Batch Tool behavior. Blank lines after
                // AutoLISP expressions are visual spacing, while blank lines after
                // command responses (for example ALL) are preserved as Enter.
                if (line.TrimStart().StartsWith("(", StringComparison.Ordinal))
                {
                    while (i + 1 <= lastMeaningfulLine &&
                           string.IsNullOrWhiteSpace(lines[i + 1]))
                    {
                        i++;
                    }
                }
            }
        }

        public static void DeleteStaleScripts(TimeSpan maximumAge)
        {
            try
            {
                string folder = Path.Combine(Path.GetTempPath(), "BatchScripter");
                if (!Directory.Exists(folder))
                    return;

                DateTime cutoff = DateTime.UtcNow.Subtract(maximumAge);
                foreach (string file in Directory.GetFiles(folder, ScriptPrefix + "*.scr"))
                {
                    try
                    {
                        if (File.GetLastWriteTimeUtc(file) < cutoff)
                            File.Delete(file);
                    }
                    catch
                    {
                        // A locked or inaccessible stale file can be retried later.
                    }
                }
            }
            catch
            {
                // Cleanup is best-effort and never blocks the application.
            }
        }
    }
}
