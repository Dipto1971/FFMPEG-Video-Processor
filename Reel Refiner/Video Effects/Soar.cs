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

            double thirdDurationEnd = secondDurationEnd + 3;

            string command = 
                $"[0:v] split=20 [1][2][3][4][5][6][7][8][9][10][11][12][13][14][15][16][17][18][19][20];" +
                //
                $"[1] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, rgbashift=bh=-200:gh=-200, hue=s=.5, fps=fps={fps} [red_shift];" +
                
                
                $"[2] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, reverse, fps=fps={fps} [reverse_base];" +
                $"[3] trim=start={firstDurationStart}:end={firstDurationEnd}, setpts=PTS-STARTPTS, format=argb,colorchannelmixer=aa=0.7, fps=fps={fps} [transparent_part];" +
                $"[reverse_base][transparent_part] overlay=shortest=1 [reversed_overlayed];" +


                // Rotation part done
                $"[4] trim=start={firstDurationEnd}:end={secondDurationEnd}, setpts=(PTS-STARTPTS), fps=fps={fps}, scale=iw:ih [collage_2_base];" +
                $"[5] trim=start={firstDurationEnd + 0.1}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + .1/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/15:fillcolor=none:ow=rotw(PI/15):oh=roth(PI/15) [rotated_0];" +
                $"[6] trim=start={firstDurationEnd + 0.2}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + .2/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/14:fillcolor=none:ow=rotw(PI/14):oh=roth(PI/14) [rotated_1];" +
                $"[7] trim=start={firstDurationEnd + 0.3}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + .3/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/13:fillcolor=none:ow=rotw(PI/13):oh=roth(PI/13) [rotated_2];" +
                $"[8] trim=start={firstDurationEnd + 0.4}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + .4/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/12:fillcolor=none:ow=rotw(PI/12):oh=roth(PI/12) [rotated_3];" +
                $"[9] trim=start={firstDurationEnd + 0.5}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + .5/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/11:fillcolor=none:ow=rotw(PI/11):oh=roth(PI/11) [rotated_4];" +
                $"[10] trim=start={firstDurationEnd + 0.6}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + .6/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/10:fillcolor=none:ow=rotw(PI/10):oh=roth(PI/10) [rotated_5];" +
                $"[11] trim=start={firstDurationEnd + 0.7}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + .7/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/9:fillcolor=none:ow=rotw(PI/9):oh=roth(PI/9) [rotated_6];" +
                $"[12] trim=start={firstDurationEnd + 0.8}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + .8/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/8:fillcolor=none:ow=rotw(PI/8):oh=roth(PI/8) [rotated_7];" +
                $"[13] trim=start={firstDurationEnd + 0.9}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + .9/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/7:fillcolor=none:ow=rotw(PI/7):oh=roth(PI/7) [rotated_8];" +
                $"[14] trim=start={firstDurationEnd + 1.0}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + 1.0/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/6:fillcolor=none:ow=rotw(PI/6):oh=roth(PI/6) [rotated_9];" +
                $"[15] trim=start={firstDurationEnd + 1.1}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + 1.1/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/5:fillcolor=none:ow=rotw(PI/5):oh=roth(PI/5) [rotated_10];" +
                $"[16] trim=start={firstDurationEnd + 1.2}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + 1.2/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/4:fillcolor=none:ow=rotw(PI/4):oh=roth(PI/4) [rotated_11];" +
                $"[17] trim=start={firstDurationEnd + 1.3}:end={secondDurationEnd}, setpts=(PTS-STARTPTS + 1.3/TB), fps=fps={fps}, scale=iw-10:ih-10, rotate=PI/3:fillcolor=none:ow=rotw(PI/3):oh=roth(PI/3) [rotated_12];" +


                // Overlaying part is not working correctly
                $"[collage_2_base][rotated_0] overlay=x={-width / 2.0}:y={0} :enable='between(t,0.1,3)' [v0overlayed];" +
                $"[v0overlayed][rotated_1] overlay=x={-width / 2.1}:y=0 :enable='between(t,0.2,3)' [v1overlayed];" +
                $"[v1overlayed][rotated_2] overlay=x={-width / 2.2}:y=0 :enable='between(t,0.3,3)' [v2overlayed];" +
                $"[v2overlayed][rotated_3] overlay=x={-width / 2.3}:y=0 :enable='between(t,0.4,3)' [v3overlayed];" +
                $"[v3overlayed][rotated_4] overlay=x={-width / 2.4}:y=0 :enable='between(t,0.5,3)' [v4overlayed];" +
                $"[v4overlayed][rotated_5] overlay=x={-width / 2.5}:y=0 :enable='between(t,0.6,3)' [v5overlayed];" +
                $"[v5overlayed][rotated_6] overlay=x={-width / 2.6}:y=0 :enable='between(t,0.7,3)' [v6overlayed];" +
                $"[v6overlayed][rotated_7] overlay=x={-width / 2.7}:y=0 :enable='between(t,0.8,3)' [v7overlayed];" +
                $"[v7overlayed][rotated_8] overlay=x={-width / 2.7}:y=0 :enable='between(t,0.9,3)' [v8overlayed];" +
                $"[v8overlayed][rotated_9] overlay=x={-width / 2.6}:y=0 :enable='between(t,1.0,3)' [v9overlayed];" +
                $"[v9overlayed][rotated_10] overlay=x={-width / 2.5}:y=0 :enable='between(t,1.1,3)' [v10overlayed];" +
                $"[v10overlayed][rotated_11] overlay=x={-width / 2.4}:y=0 :enable='between(t,1.2,3)' [v11overlayed];" +
                $"[v11overlayed][rotated_12] overlay=x={-width / 2.3}:y=0 :enable='between(t,1.3,3)' [rot_overlayed];" +


                $"[18] trim=start={secondDurationEnd}:end={thirdDurationEnd}, setpts=PTS-STARTPTS, fps=fps={fps}, format=argb, colorchannelmixer=aa=0.7 [purple_shift_base];" +
                $"[19] trim=start={secondDurationEnd - 0.05}:end={thirdDurationEnd}, setpts=PTS-STARTPTS, lutrgb=g=0, rgbashift=gv=30, format=argb,colorchannelmixer=aa=0.9, fps=fps={fps} [purple_shift_transparent_1];" +
                $"[20] trim=start={secondDurationEnd - 0.1}:end={thirdDurationEnd}, setpts=PTS-STARTPTS, lutrgb=g=0, rgbashift=gv=30, format=argb,colorchannelmixer=aa=0.9, fps=fps={fps} [purple_shift_transparent_2];" +
                $"[purple_shift_transparent_1][purple_shift_transparent_2] overlay=shortest=1 [purple_shift_overlayed];" +
                $"[purple_shift_overlayed][purple_shift_base] overlay=shortest=1 [purple_shift];" +

                $"[red_shift][reversed_overlayed] xfade=transition=fadewhite:duration=0.3:offset={firstDurationEnd - 0.3} [part1];" +
                $"[part1][rot_overlayed][purple_shift]  concat=n=3:v=1:a=0 [out]";

            string input = $"-i {inputVideo} -filter_complex \"{command}\" -map \"[out]\" -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
