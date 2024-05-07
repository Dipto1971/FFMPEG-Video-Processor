namespace ReelRefiner.Video_Effects
{
    public class Vhs_Rewind_Effect
    {
        public Vhs_Rewind_Effect(string inputVideo, string outputVideo, double duration)
        {
            string wave =
               $"[0:v] split=7 [part1][part2][part3][part4][part5][part6][part7]; " +

               $"[part1] trim=start=0:end=2, setpts=2*(PTS-STARTPTS) [slow1]; " +
               $"[part2] trim=start=2:end=2.3, setpts=PTS-STARTPTS [normal1]; " +
               $"[part3] trim=start=2.3:end=2.6, setpts=2*(PTS-STARTPTS) [slow2]; " +
               $"[part4] trim=start=2.6:end=2.9, setpts=PTS-STARTPTS [normal2]; " +
               $"[part5] trim=start=2.9:end=4.9, setpts=2*(PTS-STARTPTS) [slow3]; " +

               $"[part6] trim=start=2.9:end=4.9, reverse, setpts=0.5*(PTS-STARTPTS)," +
               $"rgbashift=rh=-6:gh=6, tinterlace=mode=interleave_top, fieldorder=tff," +
               $"hue=s=0.0, eq=saturation=5:brightness=0:contrast=1.2 [fastrewind]; " +

               $"[part7] trim=start=4.9:end=6,setpts=2*(PTS-STARTPTS) [slow4]; " +

               $"[slow1][normal1][slow2][normal2][slow3][fastrewind][slow4] concat=n=7:v=1:a=0 [out]";

            string input = $"-i {inputVideo} -filter_complex \"{wave}\" -map \"[out]\" -c:a copy -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
