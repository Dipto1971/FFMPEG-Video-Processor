namespace Testing.Video_Effects
{
    public class Mixed_Fruits
    {
        public Mixed_Fruits(string inputVideo, string outputVideo, double duration, int height, int width)
        {
            double firstSlowDuration = duration * 0.25;

            double secondSlowStart = firstSlowDuration;
            double secondSlowEnd = secondSlowStart + 2;

            double thirdSlowStart = secondSlowEnd;
            double thirdslowEnd = thirdSlowStart + 2;


            double fourthslowEnd = thirdslowEnd + 2;

            string command =
                $"[0:v] trim=start={0}:end={firstSlowDuration},  setpts=2*(PTS-STARTPTS) [slow1];" +


                $"color=black:{width}x{height} [collage_1_base];" +
                $"[0:v] trim=start={secondSlowStart}:end={secondSlowEnd}, setpts=2*(PTS-STARTPTS), scale={width / 3 - 2}:{height / 2 - 2}, lutrgb=b=0:r=0 [upperleft];" +
                $"[0:v] trim=start={secondSlowStart}:end={secondSlowEnd}, setpts=2*(PTS-STARTPTS), scale={width / 3}:{height / 2 - 1}, lutrgb=b=0 [upperright];" +
                $"[0:v] trim=start={secondSlowStart}:end={secondSlowEnd}, setpts=2*(PTS-STARTPTS), crop={width / 3 - 1}:{height}:{width / 3 - 1}:{0} [Middle];" +
                $"[0:v] trim=start={secondSlowStart}:end={secondSlowEnd}, setpts=2*(PTS-STARTPTS), scale={width / 3 - 2}:{height / 2 - 2}, lutrgb=g=152:b=186 [lowerleft];" +
                $"[0:v] trim=start={secondSlowStart}:end={secondSlowEnd}, setpts=2*(PTS-STARTPTS), scale={width / 3}:{height / 2 - 1}, lutrgb=g=0:b=0  [lowerright];" +

                $"[collage_1_base][Middle] overlay=shortest=1:x={width / 3}:y={0} [tmp1];" +
                $"[tmp1][upperright] overlay=shortest=1:x={width / 3 * 2}:y={0} [tmp2];" +
                $"[tmp2][upperleft] overlay=shortest=1:x={1}:y={1} [tmp3];" +
                $"[tmp3][lowerleft] overlay=shortest=1:x={0}:y={height / 2} [tmp4];" +
                $"[tmp4][lowerright] overlay=shortest=1:x={width / 3 * 2}:y={height / 2} [collage_1];" +


                $"[0:v]trim=start={thirdSlowStart}:end={thirdslowEnd},setpts=2*(PTS-STARTPTS) [collage_2_base];" +
                $"[0:v]trim=start={thirdSlowStart + 0.1},setpts=2*(PTS-STARTPTS+.1/TB),scale={width - 60}:{height - 60}[v0scaled];" +
                $"[0:v]trim=start={thirdSlowStart + 0.2},setpts=2*(PTS-STARTPTS+.2/TB),scale={width - 100}:{height - 100}[v1scaled];" +
                $"[0:v]trim=start={thirdSlowStart + 0.3},setpts=2*(PTS-STARTPTS+.3/TB),scale={width - 140}:{height - 140}[v2scaled];" +
                $"[0:v]trim=start={thirdSlowStart + 0.4},setpts=2*(PTS-STARTPTS+.4/TB),scale={width - 180}:{height - 180}[v3scaled];" +
                $"[0:v]trim=start={thirdSlowStart + 0.5},setpts=2*(PTS-STARTPTS+.5/TB),scale={width - 220}:{height - 220}[v4scaled];" +
                $"[0:v]trim=start={thirdSlowStart + 0.6},setpts=2*(PTS-STARTPTS+.6/TB),scale={width - 260}:{height - 260}[v5scaled];" +

                $"[collage_2_base][v0scaled]overlay=shortest=1:x=30:y=30 [v0overlayed];" +
                $"[v0overlayed][v1scaled]overlay=shortest=1:x=50:y=50 [v1overlayed];" +
                $"[v1overlayed][v2scaled] overlay=shortest=1:x=70:y=70 [v2overlayed];" +
                $"[v2overlayed][v3scaled] overlay=shortest=1:x=90:y=90 [v3overlayed];" +
                $"[v3overlayed][v4scaled] overlay=shortest=1:x=110:y=110 [v4overlayed];" +
                $"[v4overlayed][v5scaled] overlay=shortest=1:x=130:y=130 [collage_2];" +



                $"[0:v] split=5 [0][1][2][3][4];" +
                $"[0] trim=duration= {thirdslowEnd},setpts=(PTS-STARTPTS), tpad=stop_mode=clone:stop_duration=1000000 [0trimmed];" +
                $"[1] trim=duration= {thirdslowEnd+.3},setpts=(PTS-STARTPTS), tpad=stop_mode=clone:stop_duration=1000000 [1trimmed];" +
                $"[2] trim=duration= {thirdslowEnd+.6},setpts=(PTS-STARTPTS), tpad=stop_mode=clone:stop_duration=1000000 [2trimmed];" +
                $"[3] trim=duration= {thirdslowEnd+.9},setpts=(PTS-STARTPTS), tpad=stop_mode=clone:stop_duration=1000000 [3trimmed];" +
                $"[4] trim=duration= {thirdslowEnd+ 1.2},setpts=(PTS-STARTPTS), tpad=stop_mode=clone:stop_duration=1000000 [4trimmed];" +

                $"[0:v] trim=start={0}:end={thirdslowEnd+1.2},reverse,setpts=(PTS-STARTPTS) [5reversed];" +

                $"[0trimmed][1trimmed]xfade=transition=wiperight:duration=1:offset={0}[f0];" +
                $"[f0][2trimmed]xfade=transition=wiperight:duration=1:offset={0.3}[f1];" +
                $"[f1][3trimmed]xfade=transition=wiperight:duration=1:offset={0.6}[f2];" +
                $"[f2][4trimmed]xfade=transition=wiperight:duration=1:offset={0.9}[f3];" +
                $"[f3][5reversed] xfade=transition=wipeleft:duration=1:offset={1.9}[effect_1];" +

                $"[slow1][collage_1][collage_2][effect_1] concat=n=4:v=1:a=0 [out]";



            string input = $"-i {inputVideo} -filter_complex \"{command}\" -map \"[out]\" -vcodec libx264 -c:a copy -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}