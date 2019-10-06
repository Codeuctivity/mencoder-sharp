using System;
using System.Diagnostics;

namespace MencoderSharp
{
    /// <summary>
    /// Mencoderwrapper synchron
    /// </summary>
    public class Mencoder : MencoderBase
    {
        /// <summary>
        /// The standard error
        /// </summary>
        public string standardError;

        /// <summary>
        /// Mencoders the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        public bool EncodeToMp4(string source, string destination)
        {
            return Mencode(source, destination, "-vf dsize=16/9,scale=-10:-1,harddup -of lavf -lavfopts format=mp4 -ovc x264 -sws 9 -x264encopts nocabac:level_idc=30:bframes=0:bitrate=512:threads=auto:turbo=1:global_header:threads=auto", "-oac mp3lame");
        }

        /// <summary>
        /// Mencoders the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="videoParameter">The video parameter.</param>
        /// <param name="audioParameter">The audio parameter.</param>
        /// <returns>True if task finishes without error</returns>
        public bool Mencode(string source, string destination, string videoParameter, string audioParameter)
        {
            Process p = new Process();
            p.StartInfo.FileName = PathToExternalMencoderBin;
            //http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.Arguments = "\"" + source + "\" " + videoParameter + " " + audioParameter + " -o \"" + destination + "\"";
            p.Start();
            standardError = p.StandardError.ReadToEnd();
            p.WaitForExit();
            return p.ExitCode.Equals(0);
        }
    }
}