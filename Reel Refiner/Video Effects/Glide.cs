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
            $"[0:v] split=9 [v1][v2][v3][v3tr][v4][v5][v6][v7][v8];" +
            $"[1:v] scale={width}:{height} [thunder];" +
            
            $"[v1] trim = start = 0:end = 1, setpts = PTS - STARTPTS, fps=fps={fps} [v1a];" +
            $"[v2] trim = start = 1:end = 2, setpts = PTS - STARTPTS, fps=fps={fps} [v2a];" +
            $"[v3] trim = start = 1:end = 2, reverse, setpts = PTS - STARTPTS, geq=g='150*sin(T/2)':b='150*cos(T/2)', fps=fps={fps} [v3a];" + // controlling color with geq
            
            $"[v3tr] trim = start = 1:end = 2, reverse, setpts = PTS - STARTPTS, fps=fps={fps} [v3atr];" + // this is for transition purpose
            $"[v3atr][thunder] overlay=shortest=1 [v3atrtrans];" + 

            $"[v4] trim = start = 2:end = 4, setpts = PTS - STARTPTS, fps=fps={fps} [v4a];" + // here camera rotates 360

            $"[v5] trim = start = 1:end = 2, setpts = PTS - STARTPTS, fps=fps={fps} [v5a];" +
            $"[v6] trim = start = 1:end = 2, reverse, setpts = PTS - STARTPTS, fps=fps={fps} [v6a];" +
            
            $"[v7] trim = start = 4:end = 5, reverse, setpts = PTS - STARTPTS, fps=fps={fps} [v7a];" + // here camera rotates 360 for the second time
            $"[v8] trim = start = 2:end = 4, setpts = 2*(PTS - STARTPTS), geq=g='150*sin(T/7)':b='150*cos(T/7)', fps=fps={fps} [v8a];" + // controlling color with geq


            $"[v3atrtrans][v4a] xfade=transition=pixelize:duration=0.2:offset=0 [v34];" +

            $"[v1a][v2a][v3a][v34][v5a][v6a][v7a][v8a] concat=n=8:v=1:a=0 [out];";

            string input = $"-i {inputVideo} -i input/Thunder_Transparent.gif -filter_complex \"{command}\" -map \"[out]\" -y {outputVideo}";


            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
