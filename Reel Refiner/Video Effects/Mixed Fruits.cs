namespace Testing.Video_Effects
{
    public class Mixed_Fruits
    {
        public Mixed_Fruits(string inputVideo, string outputVideo, double duration, int height, int width)
        {
            string rainbowCollage =
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
                $"[tmp4][lowerright] overlay=shortest=1:x=480:y=640";



            string input = $"-i {inputVideo} -filter_complex \"{rainbowCollage}\" -map \"[out]\" -c:a copy -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
