namespace MencoderSharp
{
    /// <summary>
    /// Mencoderparamters
    /// </summary>
    internal struct MencoderParameters
    {
        /// <summary>
        /// Path or Url to source, e.g. c:\somepath\video.mp4
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Path to source, e.g. c:\somepath\out.mp4
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Mencoder specific paramters, e.g. "-vf dsize=16/9,scale=-10:-1,harddup -of lavf -lavfopts format=mp4 -ovc x264 -sws 9 -x264encopts nocabac:level_idc=30:bframes=0:bitrate=512:threads=auto:global_header:threads=auto"
        /// </summary>
        public string VideoParameter { get; set; }

        /// <summary>
        /// Mencoder specific paramters, e.g. "-oac mp3lame"
        /// </summary>
        public string AudioParameter { get; set; }
    }
}