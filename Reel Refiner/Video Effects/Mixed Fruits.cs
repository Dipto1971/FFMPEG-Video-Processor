namespace Testing.Video_Effects
{
    public class Mixed_Fruits
    {
        public Mixed_Fruits(string inputVideo, string outputVideo, double duration, int fps, int height, int width)
        {
            fps = 25;
            double firstSlowDuration = duration * 0.25;

            double secondSlowStart = firstSlowDuration;
            double secondSlowEnd = secondSlowStart + 2;

            double thirdSlowStart = secondSlowEnd - .5;
            double thirdslowEnd = thirdSlowStart + 2;

            string command =
                $"[0:v] trim=start={0}:end={firstSlowDuration}, setpts=2*(PTS-STARTPTS), fps=fps={fps} [slow1];" +


                //Collage1 start
                $"color=black:{width}x{height} [collage_1_base];" +
                $"[0:v] trim=start={secondSlowStart}:end={secondSlowEnd}, setpts=2*(PTS-STARTPTS), scale={width / 3 - 2}:{height / 2 - 2}, lutrgb=b=0:r=0, fps=fps={fps} [upperleft];" +
                $"[0:v] trim=start={secondSlowStart}:end={secondSlowEnd}, setpts=2*(PTS-STARTPTS), scale={width / 3}:{height / 2 - 1}, lutrgb=b=0, fps=fps={fps} [upperright];" +
                $"[0:v] trim=start={secondSlowStart}:end={secondSlowEnd}, setpts=2*(PTS-STARTPTS), crop={width / 3 - 1}:{height}:{width / 3 - 1}:{0}, fps=fps={fps} [Middle];" +
                $"[0:v] trim=start={secondSlowStart}:end={secondSlowEnd}, setpts=2*(PTS-STARTPTS), scale={width / 3 - 2}:{height / 2 - 2}, lutrgb=g=152:b=186, fps=fps={fps} [lowerleft];" +
                $"[0:v] trim=start={secondSlowStart}:end={secondSlowEnd}, setpts=2*(PTS-STARTPTS), scale={width / 3}:{height / 2 - 1}, lutrgb=g=0:b=0, fps=fps={fps} [lowerright];" +

                $"[collage_1_base][Middle] overlay=shortest=1:x={width / 3}:y={0} [tmp1];" +
                $"[tmp1][upperright] overlay=shortest=1:x={width / 3 * 2}:y={0} [tmp2];" +
                $"[tmp2][upperleft] overlay=shortest=1:x={1}:y={1} [tmp3];" +
                $"[tmp3][lowerleft] overlay=shortest=1:x={0}:y={height / 2} [tmp4];" +
                $"[tmp4][lowerright] overlay=shortest=1:x={width / 3 * 2}:y={height / 2} [collage_1];" +
                //Collage1 end


                //Collage2 start
                $"[0:v]trim=start={thirdSlowStart}:end={thirdslowEnd},setpts=2*(PTS-STARTPTS), fps=fps={fps} [collage_2_base];" +
                $"[0:v]trim=start={thirdSlowStart + 0.1}:end={thirdslowEnd},setpts=2*(PTS-STARTPTS+.1/TB),scale={width - 70}:{height - 70}, fps=fps={fps}[v0scaled];" +
                $"[0:v]trim=start={thirdSlowStart + 0.2}:end={thirdslowEnd},setpts=2*(PTS-STARTPTS+.2/TB),scale={width - 110}:{height - 110}, fps=fps={fps}[v1scaled];" +
                $"[0:v]trim=start={thirdSlowStart + 0.3}:end={thirdslowEnd},setpts=2*(PTS-STARTPTS+.3/TB),scale={width - 150}:{height - 150}, fps=fps={fps}[v2scaled];" +
                $"[0:v]trim=start={thirdSlowStart + 0.4}:end={thirdslowEnd},setpts=2*(PTS-STARTPTS+.4/TB),scale={width - 190}:{height - 190}, fps=fps={fps}[v3scaled];" +
                $"[0:v]trim=start={thirdSlowStart + 0.5}:end={thirdslowEnd},setpts=2*(PTS-STARTPTS+.5/TB),scale={width - 230}:{height - 230}, fps=fps={fps}[v4scaled];" +
                $"[0:v]trim=start={thirdSlowStart + 0.6}:end={thirdslowEnd},setpts=2*(PTS-STARTPTS+.6/TB),scale={width - 270}:{height - 270}, fps=fps={fps}[v5scaled];" +

                $"[collage_2_base][v0scaled] overlay=shortest=1:x=35:y=35 [v0overlayed];" +
                $"[v0overlayed][v1scaled] overlay=shortest=1:x=55:y=55 [v1overlayed];" +
                $"[v1overlayed][v2scaled] overlay=shortest=1:x=75:y=75 [v2overlayed];" +
                $"[v2overlayed][v3scaled] overlay=shortest=1:x=95:y=95 [v3overlayed];" +
                $"[v3overlayed][v4scaled] overlay=shortest=1:x=115:y=115 [v4overlayed];" +
                $"[v4overlayed][v5scaled] overlay=shortest=1:x=135:y=135 [collage_2];" +
                //Collage2 end


                //effect1 start
                $"[0:v] split=5 [0][1][2][3][4];" +
                $"[0] trim=start= {thirdslowEnd},setpts=(PTS-STARTPTS), fps=fps={fps},tpad=stop_mode=clone:stop_duration=1000000 [0trimmed];" +
                $"[1] trim=start= {thirdslowEnd},setpts=(PTS-STARTPTS), fps=fps={fps},tpad=stop_mode=clone:stop_duration=1000000 [1trimmed];" +
                $"[2] trim=start= {thirdslowEnd},setpts=(PTS-STARTPTS), fps=fps={fps},tpad=stop_mode=clone:stop_duration=1000000 [2trimmed];" +
                $"[3] trim=start= {thirdslowEnd},setpts=(PTS-STARTPTS), fps=fps={fps},tpad=stop_mode=clone:stop_duration=1000000 [3trimmed];" +
                $"[4] trim=start= {thirdslowEnd},setpts=(PTS-STARTPTS), fps=fps={fps},tpad=stop_mode=clone:stop_duration=1000000 [4trimmed];" +

                $"[0trimmed][1trimmed]xfade=transition=wiperight:duration=1:offset={0}[f0];" +
                $"[f0][2trimmed]xfade=transition=wiperight:duration=1:offset={0.3}[f1];" +
                $"[f1][3trimmed]xfade=transition=wiperight:duration=1:offset={0.6}[f2];" +
                $"[f2][4trimmed]xfade=transition=wiperight:duration=1:offset={0.9}[slide_effect];" +

                $"[0:v] trim=start={thirdslowEnd}:end={thirdslowEnd + 1.9},reverse,setpts=(PTS-STARTPTS), fps=fps={fps} [reversedpart];" +


                //reversed collage part start
                $"color=black:{width}x{height} [collage_3_base];" +
                $"[0:v] trim=start={thirdslowEnd - 2.9}:end={thirdslowEnd + 1},reverse,setpts=(PTS-STARTPTS), fps=fps={fps}, crop={width / 2 - 2}:{height}:{width / 3}:{0}, lutrgb=g=0:b=0[reversedleftpart];" +
                $"[0:v] trim=start={thirdslowEnd - 2.9}:end={thirdslowEnd + 1},reverse,setpts=(PTS-STARTPTS), fps=fps={fps}, crop={width / 2 - 2}:{height}:{width / 3}:{0}, lutrgb=r=0:b=0[reversedrightpart];" +
                $"[collage_3_base][reversedleftpart] overlay=shortest=1:x={1}:y={1} [collage_3_overlayed_1];" +
                $"[collage_3_overlayed_1][reversedrightpart] overlay=shortest=1:x={width / 2 + 1}:y={1} [collage_3_overlayed_2];" +
                //reversed collage part end


                $"[0:v] trim=start={thirdslowEnd - 1.9}:end={thirdslowEnd},reverse,setpts=2*(PTS-STARTPTS), fps=fps={fps}, hue=s=0.2 [0saturation];" +


                // Adding xfade=fadewhite transitions
                $"[slow1][collage_1] xfade=transition=fadewhite:duration=0.5:offset={firstSlowDuration} [transition1];" +
                $"[transition1][collage_2] xfade=transition=fadewhite:duration=0.5:offset={secondSlowEnd} [transition2];" +
                $"[transition2][slide_effect] xfade=transition=fadewhite:duration=0.5:offset={thirdslowEnd} [transition3];" +
                $"[transition3][reversedpart] xfade=transition=wipeleft:duration=1:offset={thirdslowEnd + 1.9} [transition4];" +
                $"[transition4][collage_3_overlayed_2] xfade=transition=fadewhite:duration=0.5:offset={thirdslowEnd + 2.9} [transition5];" +
                $"[transition5][0saturation] xfade=transition=fadewhite:duration=0.5:offset={thirdslowEnd + 3.9}[out]";



            string input = $"-i {inputVideo} -filter_complex \"{command}\" -map \"[out]\" -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}