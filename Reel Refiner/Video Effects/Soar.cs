using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Video_Effects
{
    public class Soar
    {
        public Soar(string inputVideo, string outputVideo, double duration, int fps, int height, int width)
        {
            Console.WriteLine("Total duration: " + duration);
            double firstDurationStart = 0;
            double firstDurationEnd = duration * 0.25;

            double secondDurationEnd = firstDurationEnd + 3;

            string command = 
                $"[0:v] split=3 [1][2][3];" +
                $"[1] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, rgbashift=bh=-200:gh=-200, hue=s=.5, fps=fps={fps} [red_shift];" +
                
                
                $"[2] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, reverse, fps=fps={fps} [reverse_base];" +
                $"[3] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, format=argb,colorchannelmixer=aa=0.7, fps=fps={fps} [transparent_part];" +
                $"[reverse_base][transparent_part] overlay=shortest=1 [reversed_overlayed];" +


                // Rotation part done
                $"[0:v] trim=start={firstDurationEnd}:end={secondDurationEnd}, setpts=(PTS-STARTPTS), fps=fps={fps}, scale={width}:{height} [collage_2_base];" +
                $"[0:v] trim=start={firstDurationEnd + 0.1}:end={secondDurationEnd}, setpts=(PTS-STARTPTS+.1/TB), fps=fps={fps}, rotate=10*PI/180:c=none:ow=rotw(iw):oh=roth(ih), scale={width}:{height} [v0scaled];" +
                $"[0:v] trim=start={firstDurationEnd + 0.2}:end={secondDurationEnd}, setpts=(PTS-STARTPTS+.2/TB), fps=fps={fps}, rotate=20*PI/180:c=none:ow=rotw(iw):oh=roth(ih), scale={width}:{height} [v1scaled];" +
                $"[0:v] trim=start={firstDurationEnd + 0.3}:end={secondDurationEnd}, setpts=(PTS-STARTPTS+.3/TB), fps=fps={fps}, rotate=30*PI/180:c=none:ow=rotw(iw):oh=roth(ih), scale={width}:{height} [v2scaled];" +
                $"[0:v] trim=start={firstDurationEnd + 0.4}:end={secondDurationEnd}, setpts=(PTS-STARTPTS+.4/TB), fps=fps={fps}, rotate=40*PI/180:c=none:ow=rotw(iw):oh=roth(ih), scale={width}:{height} [v3scaled];" +
                $"[0:v] trim=start={firstDurationEnd + 0.5}:end={secondDurationEnd}, setpts=(PTS-STARTPTS+.5/TB), fps=fps={fps}, rotate=50*PI/180:c=none:ow=rotw(iw):oh=roth(ih), scale={width}:{height} [v4scaled];" +
                $"[0:v] trim=start={firstDurationEnd + 0.6}:end={secondDurationEnd}, setpts=(PTS-STARTPTS+.6/TB), fps=fps={fps}, rotate=60*PI/180:c=none:ow=rotw(iw):oh=roth(ih), scale={width}:{height} [v5scaled];" +


                // Overlaying part is not working correctly
                $"[collage_2_base][v0scaled] overlay=shortest=1:x=0:y=0 [v0overlayed];" +
                $"[v0overlayed][v1scaled] overlay=shortest=1:x=0:y=0 [v1overlayed];" +
                $"[v1overlayed][v2scaled] overlay=shortest=1:x=0:y=0 [v2overlayed];" +
                $"[v2overlayed][v3scaled] overlay=shortest=1:x=0:y=0 [v3overlayed];" +
                $"[v3overlayed][v4scaled] overlay=shortest=1:x=0:y=0 [v4overlayed];" +
                $"[v4overlayed][v5scaled] overlay=shortest=1:x=0:y=0 [rot_overlayed];" +


                $"[red_shift][reversed_overlayed] xfade=transition=fadewhite:duration=0.3:offset={firstDurationEnd - 0.3} [part1];" +
                $"[part1][rot_overlayed]  xfade=transition=fadewhite:duration=0.3:offset={secondDurationEnd - 0.3}  [out]";

            string input = $"-i {inputVideo} -filter_complex \"{command}\" -map \"[out]\" -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
