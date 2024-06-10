using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Video_Effects
{
    public class Disco_Shots
    {
        public Disco_Shots (string inputVideo, string outputVideo, double duration, int fps, int height, int width)
        {
            Console.WriteLine("Total duration: " + duration);
            fps = 25;
            double firstDurationStart = 0;
            double firstDurationEnd = duration/4;
            double secondDurationEnd = firstDurationEnd + 4;
            double thirdDurationEnd = secondDurationEnd + 6;

            string command =
                // Animated Overlay Start
                $"[0:v] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, fps=fps={fps} [overlay1_base];" +

                $"[0:v] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, scale={width / 1.5}:{height / 2}," +
                $"rotate=5*PI/180:c=none:ow=rotw({width / 1.5}):oh=roth({height / 2.25}), fps=fps={fps} [overlay_rightSide];" +

                $"[0:v] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, scale={width / 2}:{height / 3}," +
                $"rotate=-5*PI/180:c=none:ow=rotw({width / 2}):oh=roth({height / 3}), fps=fps={fps} [overlay_leftSide];" +

                $"[overlay1_base][overlay_rightSide] overlay=shortest=1:x={25}:y={height / 3.5}:enable='between(t,0.2,{firstDurationEnd})', setpts=PTS-STARTPTS [overlayed1];" +
                $"[overlayed1][overlay_leftSide] overlay=shortest=1:x={-25}:y={height / 4.5}:enable='between(t,0.4,{firstDurationEnd})', setpts=PTS-STARTPTS [animatedOverlay_1];" +
                // Animated Overlay End
                

                // 
                $"[0:v] trim=start={firstDurationEnd}:end={secondDurationEnd}, setpts=PTS-STARTPTS, scale={width/2}:{height/ 2 - 90}, fps=fps={fps} [upper_square];" +
                $"[0:v] trim=start={firstDurationEnd}:end={secondDurationEnd}, setpts=PTS-STARTPTS, scale={width/ 2}:{height/ 2 - 90}, fps=fps={fps} [lower_square];" +
                $"color=black:s={width}x{height} [canvas];" +
                $"[canvas][upper_square] overlay=shortest=1:x={width/3 - 60}:y=30 [tmp1];" +
                $"[tmp1][lower_square] overlay=shortest=1:x={width/3 - 60}:y=930[squaredOverlay];" +
                //

                //
                $"[0:v] trim=start={secondDurationEnd}:end={thirdDurationEnd}, setpts=PTS-STARTPTS, fps=fps={fps} [overlay2_base];" +

                $"[0:v] trim=start={secondDurationEnd}:end={thirdDurationEnd}, setpts=PTS-STARTPTS, scale={width / 1.5}:{height / 2}," +
                $"rotate=5*PI/180:c=none:ow=rotw({width / 1.5}):oh=roth({height / 2.25}), fps=fps={fps} [overlay2_rightSide];" +

                $"[0:v] trim=start={secondDurationEnd}:end={thirdDurationEnd}, setpts=PTS-STARTPTS, scale={width / 2}:{height / 3}," +
                $"rotate=-5*PI/180:c=none:ow=rotw({width / 2}):oh=roth({height / 3}), fps=fps={fps} [overlay2_leftSide];" +

                $"[0:v] trim=start={secondDurationEnd}:end={thirdDurationEnd}, setpts=PTS-STARTPTS, scale={width / 1.5}:{height / 2}," +
                $"rotate=15*PI/180:c=none:ow=rotw({width / 1.5}):oh=roth({height / 2}), fps=fps={fps} [overlay2_middle];" +

                $"[overlay2_base][overlay2_rightSide] overlay=shortest=1:x={25}:y={height / 3.5}:enable='between(t,{secondDurationEnd + 0.2},{thirdDurationEnd})', setpts=PTS-STARTPTS [overlayed2_1];" +
                $"[overlayed2_1][overlay2_leftSide] overlay=shortest=1:x={-25}:y={height / 4.5}:enable='between(t,{secondDurationEnd + 0.4},{thirdDurationEnd})', setpts=PTS-STARTPTS [overlayed2_2];" +
                $"[overlayed2_2][overlay2_middle] overlay=shortest=1:x={0}:y={0}:enable='between(t,{secondDurationEnd + 0.6},{thirdDurationEnd})', setpts=PTS-STARTPTS [animatedOverlay_2];" +
                //

                //
                $"" +
                $"" +


                $"[animatedOverlay_1][squaredOverlay] xfade=transition=fadewhite:duration=0.5:offset={firstDurationEnd} [sample1];" +
                $"[sample1][animatedOverlay_2] xfade=transition=fadewhite:duration=0.5:offset={secondDurationEnd} [out];";

            string input = $"-i {inputVideo} -filter_complex \"{command}\" -map \"[out]\" -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
