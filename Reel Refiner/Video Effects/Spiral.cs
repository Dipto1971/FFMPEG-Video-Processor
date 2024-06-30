using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Video_Effects
{
    public class Spiral
    {
        public Spiral(string inputVideo, string outputVideo, double duration, int fps, int height, int width)
        {
            Console.WriteLine("Total duration: " + duration);

            double firstDurationStart = 0.6;
            double firstDurationEnd = 1.0;

            double secondDurationStart = 1.0;
            double secondDurationEnd = 1.6;

            double thirdDurationStart = 1.6;
            double thirdDurationEnd = 2.2;

            double fourthDurationStart = 2.2;
            double fourthDurationEnd = 4.2;

            double fifthDurationStart = 4.2;
            double fifthDurationEnd = 7.2;

            double sixthDurationStart = 7.2;
            double sixthDurationEnd = duration;

            string command =
                $"[0:v] split=7 [start][v1][v2][v3][v4][v5][v6];" +
                
                $"[start] trim=start=0:end=0.6 [started];" +

                $"[v1] trim=start={firstDurationStart}:end={firstDurationEnd}, zoompan=z='1.2':x='iw/2-(iw/zoom/2)':y='ih/2-(ih/zoom/2)':d=1:s={width}*{height}, setpts=PTS-STARTPTS [v1_zoomed];" +
                $"[v2] trim=start={secondDurationStart}:end={secondDurationEnd}, zoompan=z='1.4':x='iw/2-(iw/zoom/2)':y='ih/2-(ih/zoom/2)':d=1:s={width}*{height}, setpts=PTS-STARTPTS [v2_zoomed];" +
                $"[v3] trim=start={thirdDurationStart}:end={thirdDurationEnd}, zoompan=z='1.6':x='iw/2-(iw/zoom/2)':y='ih/2-(ih/zoom/2)':d=1:s={width}*{height}, setpts=PTS-STARTPTS [v3_zoomed];" +
                
                $"[v4] trim=start={fourthDurationStart}:end={fourthDurationEnd}, setpts=PTS-STARTPTS [v4_trimmed];" +
                $"[v5] trim=start={fifthDurationStart}:end={fifthDurationEnd}, setpts=PTS-STARTPTS [v5_trimmed];" +
                
                $"[v4_trimmed][v5_trimmed] xfade=transition=fadewhite:duration= 0.5:offset={1.5} [v4v5_transition];" +
                
                $"[v6] trim=start={sixthDurationStart}:end={sixthDurationEnd}, setpts=1.1*(PTS-STARTPTS) [v6_trimmed];" +
                $"[v4v5_transition][v6_trimmed] xfade=transition=fadewhite:duration= 0.5:offset={4} [v4v5_v6_transition];" +


                $"[started][v1_zoomed][v2_zoomed][v3_zoomed][v4v5_v6_transition] concat=n=5:v=1:a=0 [out];" ;

            string input = $"-i {inputVideo} -filter_complex \"{command}\" -map \"[out]\" -y {outputVideo}";

            Utils.Utils.ExecuteFFmpegCommand(input);
        }
    }
}
