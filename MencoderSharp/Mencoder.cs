using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;

namespace MencoderSharp
{
    /// <summary>
    /// Mencoderwrapper synchron
    /// </summary>
    public class Mencoder : MencoderBase
    {
        /// <summary>
        /// Mencoders the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="videoParameter">The video parameter.</param>
        /// <param name="audioParameter">The audio parameter.</param>
        /// <returns>True if task finishes without error</returns>
        public bool mencoder(Uri source, Uri destination, string videoParameter, string audioParameter)
        {
            Process p = new Process();
            p.StartInfo.FileName = pathToMencoderExe;
            //http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.Arguments = "\"" + source.LocalPath + "\" " + videoParameter + " " + audioParameter + " -o \"" + destination.LocalPath + "\"";
            p.Start();
            //nur eins darf synchron gelesen werden!! http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandarderror.aspx
            string test;
            test = p.StandardError.ReadToEnd();
            p.WaitForExit();
            if (p.ExitCode.Equals(0))
                return true;
            return false;
        }
        // Howto create a Async Method that throws events when finished
        //http://msdn.microsoft.com/en-us/library/e7a34yad.aspx
        // The method to be executed asynchronously.
    }
}
