using System.Diagnostics;

namespace RellRefiner.Utils
{
    public static class Utils
    {
        private static string _FFMPEG = @"./ffmpeg/ffmpeg.exe";

        public static TimeSpan GetVideoDuration(string videoPath)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = _FFMPEG;
                    process.StartInfo.Arguments = $"-i \"{videoPath}\" -hide_banner";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();

                    string output = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    var durationLine = output.Substring(output.IndexOf("Duration: "));
                    var durationStr = durationLine.Split(',')[0].Replace("Duration: ", "").Trim();
                    var duration = TimeSpan.Parse(durationStr);

                    return duration;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return TimeSpan.Zero; // Return zero if there is an error
            }
        }

        public static void ExecuteFFmpegCommand(string input)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = _FFMPEG;
                process.StartInfo.Arguments = input;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                // Event handler for asynchronous output reading
                process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                process.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);

                process.Start();

                // Begin asynchronous reading of the output
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    Console.WriteLine(process.StandardError.ReadToEnd());
                }
            }
        }
    }
}
