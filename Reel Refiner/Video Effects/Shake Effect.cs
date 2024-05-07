using RellRefiner.Utils;

namespace ReelRefiner.Video_Effects
{
    public class Shake_Effect
    {
        public Shake_Effect(string inputVideo, string outputVideo, double duration)
        {
            string shake =
                $"[0:v] split=4 [start][middle][last1][last2]; " +

                $"[last1] trim=start={duration - 1.5}:end={duration},setpts=PTS-STARTPTS [lastframe1]; " +
                $"[last2] trim=start={duration - 1.5}:end={duration},setpts=PTS-STARTPTS [lastframe2]; " +
                $"[lastframe1] trim=start={0}:end={0.7},setpts=PTS-STARTPTS [laststart]; " +
                $"[lastframe2] trim=start={0.7}:end={1},setpts=PTS-STARTPTS [lastend]; " +

                $"[lastend] crop=w=iw:h=ih-50:y=50, pad=iw:ih+50:0:0:black, " +
                $"chromashift=crh=-10:crv=-10:cbh=10:cbv=10:enable='between(t,0,0.15)', " +
                $"chromashift=crh=10:crv=10:cbh=-10:cbv=-10:enable='between(t,0.15,3)'," +
                $"eq=saturation=3:contrast=1:enable='between(t,0,0.3)' [lastendcropped]; " +

                $"[start] trim=start={0}:end={0.15},setpts=PTS-STARTPTS," +
                $"crop=w=iw:h=ih-50:y=50," +
                $"pad=iw:ih+50:0:0:black," +

                $"chromashift=crh=-10:crv=-10:cbh=10:cbv=10:enable='between(t,0,0.1)'," +
                $"chromashift=crh=10:crv=10:cbh=-10:cbv=-10:enable='between(t,0.1,0.2)'," +
                $"chromashift=crh=-10:crv=-10:cbh=10:cbv=10:enable='between(t,0.2,0.3)'," +
                $"eq=saturation=3:contrast=1:enable='between(t,0,0.3)' [startfiltered]; " +

                $"[laststart][lastendcropped][startfiltered][middle] concat=n=4:v=1:a=0 [out]";

            string input = $"-i {inputVideo} -filter_complex \"{shake}\" -map \"[out]\" -c:a copy -y {outputVideo}";

            // Utils.Utils.ExecuteFFmpegCommand(shake);
            Utils.ExecuteFFmpegCommand(input);
        }
    }
}
