using OpenCvSharp;
using System;
using System.Collections;
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

            fps = 25;

            string command =
            $"[0:v] split=11 [v1][v2][v3][v4a][v4b][v4c][v4d][v5][v6][v7][v8];" +
            //$"[1:v] split=1 [thunder];" +
            
            $"[v1] trim = start = 0:end = 1, setpts = PTS - STARTPTS, fps=fps={fps} [v1a];" +
            $"[v2] trim = start = 1:end = 2, setpts = PTS - STARTPTS, fps=fps={fps} [v2a];" +
            $"[v3] trim = start = 1:end = 2, reverse, setpts = PTS - STARTPTS, geq=g='150*sin(T)':b='150*cos(T)', fps=fps={fps} [v3a];" + // controlling color with geq

            $"[v4a]trim=start=2:end=2.5,setpts=PTS-STARTPTS,scale=iw:ih, fps=fps={fps} [v4a1];" +
            $"[v4b]trim=start=3:end=3.5,setpts=PTS-STARTPTS,scale=iw:ih, fps=fps={fps} [v4b1];" +
            $"[v4c]trim=start=4:end=4.5,setpts=PTS-STARTPTS,scale=iw:ih, fps=fps={fps} [v4c1];" +
            $"[v4d]trim=start=5:end=5.5,setpts=PTS-STARTPTS,scale=iw:ih, fps=fps={fps} [v4d1];" +

            $"[v4a1][v4b1] blend=all_mode='overlay':all_opacity=0.5 [ov1];" +
            $"[ov1][v4c1] blend=all_mode='overlay':all_opacity=0.5 [ov2];" +
            $"[ov2][v4d1] blend=all_mode='overlay':all_opacity=0.5 [transition_overlay];" +

            $"[v5] trim = start = 1:end = 2, setpts = PTS - STARTPTS, fps=fps={fps} [v5a];" +
            $"[v6] trim = start = 1:end = 2, reverse, setpts = PTS - STARTPTS, fps=fps={fps} [v6a];" +
            
            $"[v7] trim = start = 4:end = 5, reverse, setpts = PTS - STARTPTS, fps=fps={fps} [v7a];" + // here camera rotates 360 for the second time
            $"[v8] trim = start = 2:end = 4, setpts = 2*(PTS - STARTPTS), geq=g='150*sin(T/5)':b='150*cos(T/5)', fps=fps={fps} [v8a];" + // controlling color with geq


            $"[v1a][v2a][v3a][transition_overlay][v5a][v6a][v7a][v8a] concat=n=8:v=1:a=0 [out];";

            string input = $"-i {inputVideo} -filter_complex \"{command}\" -map \"[out]\" -y {outputVideo}";


            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
