namespace Testing.Video_Effects
{
    public class Vhs_Rewind_Effect
    {
        public Vhs_Rewind_Effect(string inputVideo, string outputVideo, double duration)
        {
            double firstSlowDuration = duration * 0.2667;
            double firstNormal = firstSlowDuration + 0.5;
            double secondSlowDuration = firstNormal + duration * 0.13334;
            double secondNormal = secondSlowDuration + duration * 0.1;
            double reverseDuration = secondNormal + duration * 0.06667 * 1.5;

            string wave =
               $"[0:v] split=7 [part1][part2][part3][part4][part5][part6][part7]; " +

               $"[part1] trim=start=0:end={firstSlowDuration}, setpts=2.5*(PTS-STARTPTS) [slow1]; " +
               $"[part2] trim=start={firstSlowDuration}:end={firstNormal}, setpts=PTS-STARTPTS [normal1]; " +
               $"[part3] trim=start={firstNormal}:end={secondSlowDuration}, setpts=2.5*(PTS-STARTPTS) [slow2]; " +
               $"[part4] trim=start={secondSlowDuration}:end={secondNormal}, setpts=PTS-STARTPTS [normal2]; " +
               $"[part5] trim=start={secondNormal}:end={reverseDuration}, setpts=(PTS-STARTPTS)/1.5 [slow3]; " +

               $"[part6] trim=start={secondNormal}:end={reverseDuration}, reverse, setpts=0.7*(PTS-STARTPTS)," +
               $"rgbashift=rh=-8:gh=8, tinterlace=mode=interleave_top, fieldorder=tff," +
               $"hue=s=0.2 [fastrewind]; " +

               $"[part7] trim=start={secondNormal}:end={duration},setpts=2.5*(PTS-STARTPTS) [slow4]; " +
               $"[slow1][normal1][slow2][normal2][slow3][fastrewind][slow4] concat=n=7:v=1:a=0 [out]";


            string input = $"-i {inputVideo} -filter_complex \"{wave}\" -map \"[out]\" -c:a copy -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
