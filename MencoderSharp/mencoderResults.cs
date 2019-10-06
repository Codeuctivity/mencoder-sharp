namespace MencoderSharp
{
    /// <summary>
    /// DTO holding information of the finished mencoder session
    /// </summary>
    public class MencoderResults
    {
        /// <summary>
        /// Is true if mencoder finsished succesfull. Is false if something went wrong. 
        /// </summary>
        public bool ExecutionWasSuccessfull { get; internal set; }
        /// <summary>
        /// Mencoder stdout
        /// </summary>
        public string StandardOutput { get; internal set; }
        /// <summary>
        /// Mencoder stderr
        /// </summary>
        public string StandardError { get; internal set; }
        /// <summary>
        /// Mendocer exitcode
        /// </summary>
        public int Exitcode { get; internal set; }
    }
}