using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Video_Effects
{
    public class Spiral
    {
        public Spiral(string inputVideo, string outputVideo, double duration, int fps, int height, int width)
        {
            Console.WriteLine("Total duration: " + duration);
            double firstDurationStart = 2;
            double firstDurationEnd = 3;

            double secondDurationEnd = 4;

            double thirdDurationEnd = 5;

            string command =
                //$"[0:v] split=6 [1][2][3][4][5][6];" +


                $"[0:v] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, fps=25 [out];";

            string input = $"-i {inputVideo} -filter_complex \"{command}\" -map \"[out]\" -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
