using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Video_Effects
{
    public class Hover
    {
        public Hover(string inputVideo, string outputVideo, double duration, int fps, int height, int width)
        {
            Console.WriteLine("Total duration: " + duration);

            double firstDurationStart = 0;
            double firstDurationEnd = 1;

            double secondDurationStart = 1;
            double secondDurationEnd = 3;

            double thirdDurationStart = 3;
            double thirdDurationEnd = 4;

            string command =
                $"[0:v] split=6 [1][2][3][4][5][6];" +

                $"[1] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, fps=fps={fps} [normal_part];" +
                $"[2] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, reverse, fps=fps={fps} [reversed_part];" +

                $"[3] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=1.5*(PTS-STARTPTS), fps=fps={fps} [slowmotion_part];" +
                
                $"[4] trim=start={secondDurationStart}:end={secondDurationEnd} , setpts=PTS-STARTPTS [360view];" +

                $"[5] trim=start={thirdDurationStart}:end={thirdDurationEnd}, setpts=PTS-STARTPTS, fps=fps={fps} [normal_part_2];" +
                $"[6] trim=start={thirdDurationStart}:end={thirdDurationEnd}, setpts=1.5*(PTS-STARTPTS), reverse, fps=fps={fps} [reversed_part_2];" +
                
                $"[normal_part][reversed_part][slowmotion_part][360view][normal_part_2][reversed_part_2] concat=n=6:v=1:a=0 [out];";


            string input = $"-i {inputVideo} -filter_complex \"{command}\" -map \"[out]\" -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
