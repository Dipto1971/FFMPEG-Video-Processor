using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Video_Effects
{
    public class Sway
    {
        public Sway(string inputVideo, string outputVideo, double duration, int fps, int height, int width)
        {
            Console.WriteLine("Total duration: " + duration);

            fps = 25;

            double firstDurationStart = 0;
            double firstDurationEnd = 1;

            double secondDurationStart = 1;
            double secondDurationEnd = 2;

            double thirdDurationStart = 2;
            double thirdDurationEnd = 3;

            double baseRotation = Math.PI / 50;
            double rotationIncrement = Math.PI / 50;

            double baseDelay = 0.05;
            double delayIncrement = 0.05;


            string command = 
                $"[0:v] split=18 [1][2][3][4][5][6][7][8][9][10][11][12][13][14][15][16][17][18];" +

                // Rotation part
                $"[1] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=(PTS-STARTPTS), fps=fps={fps}, scale=iw:ih [collage_2_base];" +
                $"[2] trim=start={firstDurationStart + baseDelay}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay}/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate={baseRotation + rotationIncrement * 0}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 0}):oh=roth({baseRotation + rotationIncrement * 0}) [rotated_1];" +
                $"[3] trim=start={firstDurationStart + baseDelay + delayIncrement}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement}/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate={baseRotation + rotationIncrement * 1}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 1}):oh=roth({baseRotation + rotationIncrement * 1}) [rotated_2];" +
                $"[4] trim=start={firstDurationStart + baseDelay + delayIncrement * 2}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 2}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 2}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 2}):oh=roth({baseRotation + rotationIncrement * 2}) [rotated_3];" +
                $"[5] trim=start={firstDurationStart + baseDelay + delayIncrement * 3}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 3}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 3}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 3}):oh=roth({baseRotation + rotationIncrement * 3}) [rotated_4];" +
                $"[6] trim=start={firstDurationStart + baseDelay + delayIncrement * 4}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 4}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 4}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 4}):oh=roth({baseRotation + rotationIncrement * 4}) [rotated_5];" +
                $"[7] trim=start={firstDurationStart + baseDelay + delayIncrement * 5}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 5}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 5}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 5}):oh=roth({baseRotation + rotationIncrement * 5}) [rotated_6];" +
                $"[8] trim=start={firstDurationStart + baseDelay + delayIncrement * 6}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 6}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 6}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 6}):oh=roth({baseRotation + rotationIncrement * 6}) [rotated_7];" +
                $"[9] trim=start={firstDurationStart + baseDelay + delayIncrement * 7}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 7}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 7}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 7}):oh=roth({baseRotation + rotationIncrement * 7}) [rotated_8];" +
                $"[10] trim=start={firstDurationStart + baseDelay + delayIncrement * 8}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 8}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 8}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 8}):oh=roth({baseRotation + rotationIncrement * 8}) [rotated_9];" +
                $"[11] trim=start={firstDurationStart + baseDelay + delayIncrement * 9}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 9}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 9}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 9}):oh=roth({baseRotation + rotationIncrement * 9}) [rotated_10];" +
                $"[12] trim=start={firstDurationStart + baseDelay + delayIncrement * 10}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 10}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 10}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 10}):oh=roth({baseRotation + rotationIncrement * 10}) [rotated_11];" +
                $"[13] trim=start={firstDurationStart + baseDelay + delayIncrement * 11}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 11}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 11}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 11}):oh=roth({baseRotation + rotationIncrement * 11}) [rotated_12];" +
                $"[14] trim=start={firstDurationStart + baseDelay + delayIncrement * 12}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 12}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 12}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 12}):oh=roth({baseRotation + rotationIncrement * 12}) [rotated_13];" +
                $"[15] trim=start={firstDurationStart + baseDelay + delayIncrement * 13}:end={firstDurationEnd}, setpts=(PTS-STARTPTS + {baseDelay + delayIncrement * 13}/TB), fps=fps={fps}, scale=iw-1:ih-1, rotate={baseRotation + rotationIncrement * 13}:fillcolor=none:ow=rotw({baseRotation + rotationIncrement * 13}):oh=roth({baseRotation + rotationIncrement * 13}) [rotated_14];" +


                // Overlaying part
                $"[collage_2_base][rotated_1] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay},3)' [v0overlayed];" +
                $"[v0overlayed][rotated_2] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement},3)' [v1overlayed];" +
                $"[v1overlayed][rotated_3] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 2},3)' [v2overlayed];" +
                $"[v2overlayed][rotated_4] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 3},3)' [v3overlayed];" +
                $"[v3overlayed][rotated_5] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 4},3)' [v4overlayed];" +
                $"[v4overlayed][rotated_6] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 5},3)' [v5overlayed];" +
                $"[v5overlayed][rotated_7] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 6},3)' [v6overlayed];" +
                $"[v6overlayed][rotated_8] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 7},3)' [v7overlayed];" +
                $"[v7overlayed][rotated_9] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 8},3)' [v8overlayed];" +
                $"[v8overlayed][rotated_10] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 9},3)' [v9overlayed];" +
                $"[v9overlayed][rotated_11] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 10},3)' [v10overlayed];" +
                $"[v10overlayed][rotated_12] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 11},3)' [v11overlayed];" +
                $"[v11overlayed][rotated_13] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 12},3)' [v12overlayed];" +
                $"[v12overlayed][rotated_14] overlay=x= (main_w-overlay_w)/2:y= (main_h-overlay_h)/2 :enable='between(t,{baseDelay + delayIncrement * 13},3)' [rotation_part];" +

                
                $"[16] trim=start={secondDurationStart}:end={secondDurationEnd}, setpts=2*(PTS-STARTPTS), fps=fps={fps} [slowmotion];" +
                $"[17] trim=start={secondDurationStart}:end={secondDurationEnd}, setpts=2*(PTS-STARTPTS), reverse, fps=fps={fps} [slowmotion_reversed];" +
                
                $"[18] trim=start={thirdDurationStart}:end={thirdDurationEnd}, setpts=2*(PTS-STARTPTS), fps=fps={fps} [slowmotion_chumma];" +
                
                
                $"[rotation_part][slowmotion] xfade=transition=fadewhite:duration=0.2:offset={firstDurationEnd - 0.2} [part1];" +
                $"[part1][slowmotion_reversed] xfade=transition=fadewhite:duration=0.2:offset={secondDurationEnd - 0.2} [part2];" +
                $"[part2][slowmotion_chumma] xfade=transition=fadewhite:duration=0.2:offset={thirdDurationEnd - 0.2} [out];";

            string input = $"-i {inputVideo} -filter_complex \"{command}\" -map \"[out]\" -y {outputVideo}";
            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
