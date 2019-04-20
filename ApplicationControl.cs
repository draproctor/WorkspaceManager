using System;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace WorkspaceManager
{
    public static class ApplicationControl
    {
        private static bool _pathExists(string path)
        {
            // Both of these methods return false if the spaces in the path are escaped.
            // Replacing backslashes with nothing allows true testing of the paths.
            return File.Exists(path.Replace("\\", "")) ||
                   Directory.Exists(path.Replace("\\", ""));
        }

        private static bool _processContainsPath(Process p, string path)
        {
            // Sometimes MainModule is null and sometimes FileName in MainModule is null.
            return p?.MainModule?.FileName != null && p.MainModule.FileName.Contains(path);
        }

        public static void CreateAndRun(string path)
        {
            if (!_pathExists(path))
            {
                Console.WriteLine($"File does not exist: {path}");
                Console.WriteLine("Skipping to next item.");
                return;
            }

            // Set default ProcessStartInfo properties.
            var startInfo = new ProcessStartInfo()
            {
                FileName = path,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            using (var proc = new Process() { StartInfo = startInfo })
            {
                Process[] relatedProcesses = Process.GetProcesses()
                    .Where(p => _processContainsPath(p, path))
                    .ToArray();

                // No need to start a new process if it is already running.
                if (relatedProcesses.Length > 0)
                {
                    Console.WriteLine($"Already running: {path}");
                }
                else
                {
                    Console.WriteLine(
                        $"Starting process: {proc.StartInfo.FileName} {proc.StartInfo.Arguments}");
                    proc.Start();
                }
            }
        }
    }
}
