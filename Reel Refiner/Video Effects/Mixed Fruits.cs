namespace Testing.Video_Effects
{
    public class Mixed_Fruits
    {
        public Mixed_Fruits(string inputVideo, string outputVideo, double duration, int height, int width)
        {
            string rainbowCollage =
                //Collage 1 start
                $"color=black:720x1280 [base]; " +
                $"[0:v] setpts=PTS-STARTPTS, scale=238:638 [upperleft]; " +
                $"[0:v] setpts=PTS-STARTPTS, scale=240:639 [upperright]; " +
                $"[0:v] setpts=PTS-STARTPTS, crop=239:1280:239:0 [Middle]; " +
                $"[0:v] setpts=PTS-STARTPTS, scale=238:638 [lowerleft]; " +
                $"[0:v] setpts=PTS-STARTPTS, scale=240:639 [lowerright]; " +

                $"[base][Middle] overlay=shortest=1:x=240:y=0 [tmp1]; " +
                $"[tmp1][upperright] overlay=shortest=1:x=480:y=0 [tmp2]; " +
                $"[tmp2][upperleft] overlay=shortest=1:x=1:y=1 [tmp3]; " +
                $"[tmp3][lowerleft] overlay=shortest=1:x=0:y=640 [tmp4]; " +
                $"[tmp4][lowerright] overlay=shortest=1:x=480:y=640 [temp5]; " +
                $"[slow1][normal1] xfade=transition=fadewhite:duration=0.3:offset=11.7 [flashOut]; " +
                $"[flashOut][temp5] concat=n=2:v=1:a=0 [out] " +
                $"[tmp4][lowerright] overlay=shortest=1:x=480:y=640" +
                //Collage 1 end
                //Collage 2 start
                $"[0:v]setpts=PTS-STARTPTS[base];" +
                $"[0:v]trim=start=0.2,setpts=PTS-STARTPTS+.2/TB,crop=680:1240:40:40[v0scaled];" +
                $"[0:v]trim=start=0.4,setpts=PTS-STARTPTS+.4/TB,crop=640:1200:80:80[v1scaled];" +
                $"[0:v]trim=start=0.6,setpts=PTS-STARTPTS+.6/TB,crop=600:1160:120:120[v2scaled];" +
                $"[0:v]trim=start=0.8,setpts=PTS-STARTPTS+.8/TB,crop=560:1120:160:160[v3scaled];" +
                $"[0:v]trim=start=1.0,setpts=PTS-STARTPTS+1.0/TB,crop=520:1080:180:180[v4scaled];" +

                $"[base][v0scaled]overlay=shortest=1:x=20:y=20 [v0overlayed];" +
                $"[v0overlayed][v1scaled]overlay=shortest=1:x=40:y=40 [v1overlayed];" +
                $"[v1overlayed][v2scaled] overlay=shortest=1:x=60:y=60 [v2overlayed];" +
                $"[v2overlayed][v3scaled] overlay=shortest=1:x=80:y=80 [v3overlayed];" +
                $"[v3overlayed][v4scaled] overlay=shortest=1:x=100:y=100" +
                //Collage 2 end
                $"" +
                $"";


            string input = $"-i {inputVideo} -filter_complex \"{rainbowCollage}\" -map \"[out]\" -c:a copy -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }