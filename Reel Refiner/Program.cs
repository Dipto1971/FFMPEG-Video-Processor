using System.IO;
using ReelRefiner.Video_Effects;

namespace Testing
{
    internal class Program
    {
        public static string FileLocation = "";
        public static string OutputLocation = "";
        public static int Effect;

        static void Main(string[] args)
        {
            // Check if any arguments are provided
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments provided.");
                return;
            }

            // Parsing arguments
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-f":
                    case "--file":
                    case "--filelocation":
                        if (i + 1 < args.Length)
                            FileLocation = args[i + 1];
                        break;

                    case "-o":
                    case "-output":
                    case "-outputlocation":
                        if (i+1 < args.Length)
                            OutputLocation = args[i+1];
                        break;

                    case "-e":
                    case "--effect":
                        if (i + 1 < args.Length)
                        {
                            int.TryParse(args[i + 1], out int effect);
                            Effect = effect;
                        }
                        break;
                }
            }

            if(Effect == (int)VideoEffects.SlowMotionWithZoomEffect)
            {
                Console.WriteLine($"Slow Motion with zoom effect");

               new Slow_Motion_With_Zoom_Effect(FileLocation, OutputLocation);
            }
            else if (Effect == (int)VideoEffects.ShakeEffect)
            {
                Console.WriteLine($"Shake effect");

                TimeSpan duration = Utils.Utils.GetVideoDuration("./input/input-2.mp4");
                new Shake_Effect(FileLocation, OutputLocation, duration.TotalSeconds);
            }
        }
        public enum VideoEffects
        {
            SlowMotionWithZoomEffect,
            ShakeEffect,
        }
    }
}
