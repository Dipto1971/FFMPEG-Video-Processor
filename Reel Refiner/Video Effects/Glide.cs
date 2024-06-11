using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Video_Effects
{
    public class Glide
    {
        public Glide(string inputVideo, string outputVideo, double duration, int fps, int height, int width)
        {
            Console.WriteLine("Total duration: " + duration);
            double firstDurationStart = 0;
            double firstDurationEnd = 3;


            string command =
                $"[0:v] split=5 [1][2][3][4][5];" +

                $"[1] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, fps=fps={fps} [startingpart];" +

                $"[2] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, fps=fps={fps} [reverse_base];" +
                $"[3] trim=start={firstDurationStart + 1}:end={firstDurationEnd}, setpts=PTS-STARTPTS, format=argb,colorchannelmixer=aa=0.7, fps=fps={fps} [transparent_part_1];" +
                $"[4] trim=start={firstDurationStart + 1.5}:end={firstDurationEnd}, setpts=PTS-STARTPTS, format=argb,colorchannelmixer=aa=0.7, fps=fps={fps} [transparent_part_2];" +
                $"[4] trim=start={firstDurationStart + 2}:end={firstDurationEnd}, setpts=PTS-STARTPTS, reverse, format=argb,colorchannelmixer=aa=0.7, fps=fps={fps} [transparent_part_3];" +

                $"[reverse_base][transparent_part_1] overlay=shortest=1 [overlay1];" +
                $"[overlay1][transparent_part_2] overlay=shortest=1 [overlay2];" +
                $"[overlay2][transparent_part_3] overlay=shortest=1 [overlay3];" +

                $"";


            string input = $"-i {inputVideo} -filter_complex \"{command}\" -map \"[out]\" -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
