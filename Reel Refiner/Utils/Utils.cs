using OpenCvSharp;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Testing.Utils
{
    public static class Utils
    {
        private static string _FFMPEG = @"./ffmpeg/ffmpeg.exe";
        private static string _FFPROBE = @"./ffmpeg/ffprobe.exe";

        public struct VideoResolution
        {
            public int Width { get; set; }
            public int Height { get; set; }
        }

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
        public static VideoResolution GetVideoResolution(string videoPath)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo.FileName = _FFPROBE;
                    process.StartInfo.Arguments = $"-v error -select_streams v -show_entries stream=width,height -of csv=p=0:s=x \"{videoPath}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.WaitForExit();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    var resolutionStr = output.Split('x');
                    var widthStr = resolutionStr[0];
                    var heightStr = resolutionStr[1].Substring(0, resolutionStr[1].Length - 2);

                    var resolution = new VideoResolution
                    {
                        Width = int.Parse(widthStr),
                        Height = int.Parse(heightStr)
                    };

                    return resolution;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return new VideoResolution { Width = 0, Height = 0 }; // Return zero width and height if there is an error
            }
        }


        public static void ApplySlowMotion(string inputPath, string outputPath, double startSeconds, double endSeconds)
        {
            outputPath = outputPath + $"Zoomed";

            using (var cap = new VideoCapture(inputPath))
            {
                if (!cap.IsOpened())
                {
                    Console.WriteLine("Error: Unable to open video file");
                    return;
                }
                int width = (int)cap.FrameWidth;
                int height = (int)cap.FrameHeight;
                double fps = cap.Fps;

                using (var writer = new VideoWriter(outputPath, FourCC.MP4V, fps / 4, new Size(width, height)))
                {
                    int totalFrames = (int)cap.FrameCount;
                    double startFrame = startSeconds * fps;
                    double endFrame = endSeconds * fps;

                    Mat frame = new Mat();
                    for (int i = 0; i < totalFrames; i++)
                    {
                        if (!cap.Read(frame)) break;

                        // Check if current frame is within the slow-motion interval
                        if (i >= startFrame && i < endFrame)
                        {
                            for (int j = 0; j < 2; j++) // Writing each frame 2 times for slow motion
                            {
                                writer.Write(frame);
                            }
                        }
                        else
                        {
                            writer.Write(frame); // Write frame once without slow-motion effect
                        }
                    }
                }
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

                process.Start();
                process.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);

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
