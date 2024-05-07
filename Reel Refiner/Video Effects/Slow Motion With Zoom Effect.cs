using OpenCvSharp;
using System.Diagnostics;

namespace ReelRefiner.Video_Effects
{
    public class Slow_Motion_With_Zoom_Effect
    {
        public Slow_Motion_With_Zoom_Effect(string videoPath, string outputPath)
        {
            //string videoPath = "input/glam.mov";
            string tempDir = "temp/"; 
            //string outputPath = "output/concatenated_video.mp4";

            // Check and delete the output file if it already exists
            if (File.Exists(outputPath))
            {
                Console.WriteLine("Output file already exists. Deleting...");
                File.Delete(outputPath);
            }

            // Ensure the temp directory is clean before starting
            if (Directory.Exists(tempDir))
            {
                Console.WriteLine("Cleaning existing temporary directory...");
                Directory.Delete(tempDir, true);
            }
            Console.WriteLine("Creating directories...");
            Directory.CreateDirectory(tempDir);

            try
            {
                var part1Path = Path.Combine(tempDir, "part1.mp4");
                var part2Path = Path.Combine(tempDir, "part2.mp4");
                var part2SlowZoomPath = Path.Combine(tempDir, "part2_slow_zoom.mp4");

                using (var capture = new VideoCapture(videoPath))
                {
                    if (!capture.IsOpened())
                    {
                        Console.WriteLine("Error: Unable to open video file");
                        return;
                    }

                    Console.WriteLine("Processing video...");
                    int width = capture.FrameWidth;
                    int height = capture.FrameHeight;
                    double fps = capture.Fps;
                    int frameCount = capture.FrameCount;

                    double duration = frameCount/fps;
                    Console.WriteLine($"Duration of the video: {duration}");

                    CaptureFrames(capture, part1Path, 0, (int)(0.75 * fps), width, height, fps);
                    CaptureFrames(capture, part2Path, (int)(0.75 * fps), (int)(duration * fps), width, height, fps);

                    ApplySlowMotionAndZoom(part2Path, part2SlowZoomPath, width, height, fps);
                }

                ConcatenateVideos(new[] { part1Path, part2SlowZoomPath }, outputPath);
            }
            finally
            {
                Console.WriteLine("Cleaning up temporary files...");
                Directory.Delete(tempDir, true);
                Console.WriteLine("Cleanup complete. All temporary files removed.");
            }

            Console.WriteLine("Concatenated video generated successfully!");
            Environment.Exit(0);
        }

        static void ApplySlowMotionAndZoom(string inputPath, string outputPath, int width, int height, double fps)
        {
            using var cap = new VideoCapture(inputPath);
            using var writer = new VideoWriter(outputPath, FourCC.MP4V, fps / 4, new Size(width, height));
            int totalFrames = (int)cap.FrameCount;
            int startFrame = totalFrames / 2;
            double slowMotionFps = fps / 4;
            // Calculate the number of frames that represent 0.5 seconds in the slow motion video
            int zoomFrames = (int)(0.5 * slowMotionFps);

            Mat frame = new Mat();
            for (int i = 0; i < totalFrames; i++)
            {
                if (!cap.Read(frame)) break;

                for (int j = 0; j < 2; j++) // Writing each frame 2 times for slow motion
                {
                    if (i >= startFrame)
                    {
                        // Adjust zoom increment to complete within 0.5 seconds of slow motion video
                        double zoomFactor = 1 + (Math.Min((i - startFrame) * 4 + j, zoomFrames) / (double)zoomFrames) * 0.3;
                        zoomFactor = Math.Min(zoomFactor, 1.3); // Ensure zoom does not exceed 1.3x

                        int newWidth = (int)(width * zoomFactor);
                        int newHeight = (int)(height * zoomFactor);
                        int topLeftX = (newWidth - width) / 2;
                        int topLeftY = (newHeight - height) / 2;

                        using (var resizedFrame = new Mat())
                        {
                            Cv2.Resize(frame, resizedFrame, new Size(newWidth, newHeight));
                            var roi = new Rect(topLeftX, topLeftY, width, height);
                            using (var zoomedFrame = new Mat(resizedFrame, roi))
                            {
                                writer.Write(zoomedFrame);
                            }
                        }
                    }
                    else
                    {
                        writer.Write(frame);
                    }
                }
            }
        }

        static void CaptureFrames(VideoCapture capture, string outputPath, int startFrame, int endFrame, int width, int height, double fps)
        {
            using var writer = new VideoWriter(outputPath, FourCC.MP4V, fps, new Size(width, height));
            capture.Set(VideoCaptureProperties.PosFrames, startFrame);
            Mat frame = new Mat();

            while (capture.PosFrames < endFrame && capture.Read(frame))
            {
                writer.Write(frame);
            }
        }

        static void ConcatenateVideos(string[] inputFiles, string outputFile)
        {
            var fileArgs = string.Join(" ", inputFiles.Select(f => $"-i {f}"));
            var filterComplex = string.Join("", inputFiles.Select((f, index) => $"[{index}:v]"));
            var filterArgs = $"{filterComplex} concat=n={inputFiles.Length}:v=1 [v]";
            var arguments = $"{fileArgs} -filter_complex \"{filterArgs}\" -map \"[v]\" {outputFile}";

            Utils.Utils.ExecuteFFmpegCommand(arguments);
        }
    }
}
