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

            double baseRotation = Math.PI / 40;
            double rotationIncrement = Math.PI / 40;

            double baseDelay = 0.05;
            double delayIncrement = 0.05;


            double firstDurationStart = 0;
            double firstDurationEnd = duration * 0.25;

            double secondDurationEnd = firstDurationEnd + 2;

            double thirdDurationEnd = secondDurationEnd + 3;

            double left_shift = 3.0;

            string command = 
                $"[0:v] split=20 [1][2][3][4][5][6][7][8][9][10][11][12][13][14][15][16][17][18][19][20];" +
                //
                $"[1] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, rgbashift=bh=-200:gh=-200, hue=s=.5, fps=fps={fps} [red_shift];" +
                
                
                $"[2] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, reverse, fps=fps={fps} [reverse_base];" +
                $"[3] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, format=argb,colorchannelmixer=aa=0.7, fps=fps={fps} [transparent_part];" +
                $"[reverse_base][transparent_part] overlay=shortest=1 [reversed_overlayed];" +


                // Rotation part
                $"[4] trim=start={firstDurationEnd}:end={secondDurationEnd}, setpts=(PTS-STARTPTS), fps=fps={fps}, scale=iw:ih [collage_2_base];" +
                $"[5] trim=start={firstDurationEnd + baseDelay}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay}/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate={baseRotation + rotationIncrement * 0}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 0}):oh=roth({baseRotation + rotationIncrement * 0}) [rotated_0];" +
                $"[6] trim=start={firstDurationEnd + baseDelay + delayIncrement}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement}/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate={baseRotation + rotationIncrement * 1}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 1}):oh=roth({baseRotation + rotationIncrement * 1}) [rotated_1];" +
                $"[7] trim=start={firstDurationEnd + baseDelay + delayIncrement * 2}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 2}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 2}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 2}):oh=roth({baseRotation + rotationIncrement * 2}) [rotated_2];" +
                $"[8] trim=start={firstDurationEnd + baseDelay + delayIncrement * 3}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 3}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 3}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 3}):oh=roth({baseRotation + rotationIncrement * 3}) [rotated_3];" +
                $"[9] trim=start={firstDurationEnd + baseDelay + delayIncrement * 4}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 4}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 4}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 4}):oh=roth({baseRotation + rotationIncrement * 4}) [rotated_4];" +
                $"[10] trim=start={firstDurationEnd + baseDelay + delayIncrement * 5}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 5}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 5}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 5}):oh=roth({baseRotation + rotationIncrement * 5}) [rotated_5];" +
                $"[11] trim=start={firstDurationEnd + baseDelay + delayIncrement * 6}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 6}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 6}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 6}):oh=roth({baseRotation + rotationIncrement * 6}) [rotated_6];" +
                $"[12] trim=start={firstDurationEnd + baseDelay + delayIncrement * 7}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 7}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 7}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 7}):oh=roth({baseRotation + rotationIncrement * 7}) [rotated_7];" +
                $"[13] trim=start={firstDurationEnd + baseDelay + delayIncrement * 8}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 8}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 8}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 8}):oh=roth({baseRotation + rotationIncrement * 8}) [rotated_8];" +
                $"[14] trim=start={firstDurationEnd + baseDelay + delayIncrement * 9}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 9}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 9}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 9}):oh=roth({baseRotation + rotationIncrement * 9}) [rotated_9];" +
                $"[15] trim=start={firstDurationEnd + baseDelay + delayIncrement * 10}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 10}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 10}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 10}):oh=roth({baseRotation + rotationIncrement * 10}) [rotated_10];" +
                $"[16] trim=start={firstDurationEnd + baseDelay + delayIncrement * 11}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 11}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 11}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 11}):oh=roth({baseRotation + rotationIncrement * 11}) [rotated_11];" +
                $"[17] trim=start={firstDurationEnd + baseDelay + delayIncrement * 12}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 12}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 12}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 12}):oh=roth({baseRotation + rotationIncrement * 12}) [rotated_12];"+


                // Overlaying part
                $"[collage_2_base][rotated_0] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay},3)' [v0overlayed];" +
                $"[v0overlayed][rotated_1] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement},3)' [v1overlayed];" +
                $"[v1overlayed][rotated_2] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 2},3)' [v2overlayed];" +
                $"[v2overlayed][rotated_3] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 3},3)' [v3overlayed];" +
                $"[v3overlayed][rotated_4] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 4},3)' [v4overlayed];" +
                $"[v4overlayed][rotated_5] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 5},3)' [v5overlayed];" +
                $"[v5overlayed][rotated_6] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 6},3)' [v6overlayed];" +
                $"[v6overlayed][rotated_7] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 7},3)' [v7overlayed];" +
                $"[v7overlayed][rotated_8] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 8},3)' [v8overlayed];" +
                $"[v8overlayed][rotated_9] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 9},3)' [v9overlayed];" +
                $"[v9overlayed][rotated_10] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 10},3)' [v10overlayed];" +
                $"[v10overlayed][rotated_11] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 11},3)' [v11overlayed];" +
                $"[v11overlayed][rotated_12] overlay=x=(main_w-overlay_w)/2:y=(main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 12},3)' [rot_overlayed];" +


                $"[18] trim=start={secondDurationEnd}:end={thirdDurationEnd}, setpts=PTS-STARTPTS, fps=fps={fps}, format=argb, colorchannelmixer=aa=0.7 [purple_shift_base];" +
                $"[19] trim=start={secondDurationEnd - 0.05}:end={thirdDurationEnd}, setpts=PTS-STARTPTS, lutrgb=g=0, rgbashift=gv=30, format=argb,colorchannelmixer=aa=0.9, fps=fps={fps} [purple_shift_transparent_1];" +
                $"[20] trim=start={secondDurationEnd - 0.1}:end={thirdDurationEnd}, setpts=PTS-STARTPTS, lutrgb=g=0, rgbashift=gv=30, format=argb,colorchannelmixer=aa=0.9, fps=fps={fps} [purple_shift_transparent_2];" +
                
                $"[purple_shift_transparent_1][purple_shift_transparent_2] overlay=shortest=1 [purple_shift_overlayed];" +
                $"[purple_shift_overlayed][purple_shift_base] overlay=shortest=1 [purple_shift];" +

                $"[red_shift][reversed_overlayed] xfade=transition=fadewhite:duration=0.3:offset={firstDurationEnd - 0.3} [part1];" +
                $"[part1][rot_overlayed] xfade=transition=fadewhite:duration=0.3:offset={secondDurationEnd - 0.3} [part2];" +
                $"[part2][purple_shift] concat=n=2:v=1:a=0 [out];";

            string input = $"-i {inputVideo} -filter_complex \"{command}\" -map \"[out]\" -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
