//Source is under GPLv3 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;

namespace MencoderSharp
{
    /// <summary>
    /// The <see cref="MencoderSharp"/> Namespace contains wrapperclasses for async and sync calls of mencoder. Keep the mencoderbinary (mencoder.exe) in the same directory as the instancing assembly. Documentation and Assembly - http://code.google.com/p/mencoder-sharp/ (GPLv3) Stefan Seeland
    /// </summary>

    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc
    {
    }


    /// <summary>
    /// Mencoderparamters
    /// </summary>
    struct MencoderParameters
    {
        public string source;
        public string destination;
        public string videoParameter;
        public string audioParameter;
    }

    /// <summary>
    /// Mencoderwrapper synchron
    /// </summary>
    public class Mencoder
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
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = @"mencoder.exe";
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
                return true;
            }
            catch (Exception)
            {

                throw;

            }
        }
        // Howto create a Async Method that throws events when finished
        //http://msdn.microsoft.com/en-us/library/e7a34yad.aspx
        // The method to be executed asynchronously.
    }

    /// <summary>
    /// Mencoderwrapper Async
    /// </summary>
    public class MencoderAsync
    {
        /// <summary>
        /// Contains exitcode of mencoder and everything from standarderror
        /// </summary>
        public string result;
        /// <summary>
        /// Contains standardoutput of mencoder, filled asynchron
        /// </summary>
        public string stdOutput;
        /// <summary>
        /// Contains prgress paresed from mencoder
        /// </summary>
        public int progress;
        //Backgroundworkerdoku:
        //http://msdn.microsoft.com/de-de/library/system.componentmodel.backgroundworker.aspx
        private BackgroundWorker backgroundWorker1 = new BackgroundWorker();

        /// <summary>
        /// returns imediatly after gots called
        /// </summary>
        /// <param name="source">Path to a file which can be interpreted by mencoder</param>
        /// <param name="destination">Path to a file where the mencodet should get saved</param>
        /// <param name="videoParameter">e.g. -ovc xvid -xvidencopts bitrate=800:threads=2:aspect=4/3 -vf scale -xy 720</param>
        /// <param name="audioParameter">e.g. -oac mp3lame</param>
        public void startEncodeAsync(Uri source, Uri destination, string videoParameter, string audioParameter)
        {
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

            result = null;
            stdOutput = null;
            MencoderParameters parameters;
            parameters.source = source.OriginalString;
            parameters.destination = destination.OriginalString;
            parameters.audioParameter = audioParameter;
            parameters.videoParameter = videoParameter;
            backgroundWorker1.RunWorkerAsync(parameters);
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }
        /// <summary>
        /// Starts the encode async.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="videoParameter">The video parameter.</param>
        /// <param name="audioParameter">The audio parameter.</param>
        public void startEncodeAsync(string source, string destination, string videoParameter, string audioParameter)
        {
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

            result = null;
            stdOutput = null;
            MencoderParameters parameters;
            parameters.source = source;
            parameters.destination = destination;
            parameters.audioParameter = audioParameter;
            parameters.videoParameter = videoParameter;
            backgroundWorker1.RunWorkerAsync(parameters);
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        /// <summary>
        /// Starts the encode async.
        /// </summary>
        /// <param name="sources">The sources.</param>
        /// <param name="destinations">The destinations.</param>
        /// <param name="videoParameter">The video parameter.</param>
        /// <param name="audioParameter">The audio parameter.</param>
        public void startEncodeAsync(List<Uri> sources, List<Uri> destinations, string videoParameter, string audioParameter)
        {
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

            result = null;
            stdOutput = null;
            List<MencoderParameters> parameters = new List<MencoderParameters>();
            int foo = 0;
            foreach (var source in sources)
            {
                parameters.Add(new MencoderParameters { audioParameter = audioParameter, source = source.OriginalString, destination = destinations[foo].OriginalString, videoParameter = videoParameter });
                foo++;
            }
            backgroundWorker1.RunWorkerAsync(parameters);
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }
        /// <summary>
        /// cancels the running encodingprocess 
        /// </summary>
        public void cancelEncodeAsync()
        {
            this.backgroundWorker1.CancelAsync();
        }
        //Events without custom args:
        //http://msdn.microsoft.com/en-us/library/ms182178(VS.80).aspx
        /// <summary>
        /// Fires when mencoder is done
        /// </summary>
        public event EventHandler Finished;

        /// <summary>
        /// Raises the <see cref="E:Finished" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnFinished(EventArgs e)
        {
            if (Finished != null)
            {
                Finished(this, e);
            }
        }
        /// <summary>
        /// Fires when progress has changed (progress and stdOutput have changed)
        /// </summary>
        public event EventHandler Progress;
        /// <summary>
        /// Raises the <see cref="E:Progress" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnProgress(EventArgs e)
        {
            if (Progress != null)
            {
                Progress(this, e);
            }
        }
        /// <summary>
        /// Handles the DoWork event of the backgroundWorker1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            MencoderParameters parameter = (MencoderParameters)(e.Argument);
            try
            {
                Process p = new Process();
                //Asynchron read of standardoutput:
                //http://msdn.microsoft.com/de-de/library/system.diagnostics.process.beginoutputreadline.aspx
                p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
                p.StartInfo.FileName = @"mencoder.exe";
                //http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = "\"" + parameter.source+ "\" " + parameter.videoParameter + " " + parameter.audioParameter + " -o \"" + parameter.destination + "\"";
                p.Start();
                //nur eins darf synchron gelesen werden!! http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandarderror.aspx
                p.BeginErrorReadLine();
                string standardOut;
                while (((standardOut = p.StandardOutput.ReadLine()) != null) && (!worker.CancellationPending))
                {


                    if (standardOut.StartsWith("Pos:"))
                    {
                        string progress = standardOut.Split('(')[1].Substring(0, 2).Trim();
                        worker.ReportProgress(Convert.ToInt32(progress), standardOut);
                    }
                    else
                    {
                        worker.ReportProgress(0, standardOut);
                    }
                    //if (worker.CancellationPending)
                    //{
                    //    e.Cancel = true;
                    //    e.Result = "Canceld!";
                    //    //p.CancelErrorRead();
                    //    p.Close();
                    //    break;
                    //}
                }
                if (!worker.CancellationPending)
                {
                    p.WaitForExit();
                    e.Result = "Mencoder Exited with the Exitcode: " + p.ExitCode.ToString() + "\n" + standardError;
                }
                else
                {
                    e.Result = "Canceld!";
                    p.Close();
                    p.CancelErrorRead();
                    p.Dispose();

                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
                throw;
            }
        }
        /// <summary>
        /// Handles the DoWork event of the backgroundWorker2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            List<MencoderParameters> parameters = (List<MencoderParameters>)(e.Argument);
            try
            {
                foreach (var parameter in parameters)
                {
                    Process p = new Process();
                    //Asynchron read of standardoutput:
                    //http://msdn.microsoft.com/de-de/library/system.diagnostics.process.beginoutputreadline.aspx
                    p.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
                    p.StartInfo.FileName = @"mencoder.exe";
                    //http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.Arguments = "\"" + parameter.source + "\" " + parameter.videoParameter + " " + parameter.audioParameter + " -o \"" + parameter.destination + "\"";
                    p.Start();
                    //nur eins darf synchron gelesen werden!! http://msdn.microsoft.com/de-de/library/system.diagnostics.processstartinfo.redirectstandarderror.aspx
                    p.BeginErrorReadLine();
                    string standardOut;
                    while (((standardOut = p.StandardOutput.ReadLine()) != null) && (!worker.CancellationPending))
                    {
                        if (standardOut.StartsWith("Pos:"))
                        {
                            string progress = standardOut.Split('(')[1].Substring(0, 2).Trim();
                            
                            worker.ReportProgress(Convert.ToInt32(progress) / parameters.Count, standardOut);
                        }
                        else
                        {
                            worker.ReportProgress(0, standardOut);
                        }
                    }
                    if (!worker.CancellationPending)
                    {
                        p.WaitForExit();
                        e.Result = "Mencoder Exited with the Exitcode: " + p.ExitCode.ToString() + "\n" + standardError;
                    }
                    else
                    {
                        e.Result = "Canceld!";
                        p.Close();
                        p.CancelErrorRead();
                        p.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
                throw;
            }
        }
        /// <summary>
        /// The standard error
        /// </summary>
        private string standardError;
        /// <summary>
        /// Handles the ErrorDataReceived event of the p control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataReceivedEventArgs" /> instance containing the event data.</param>
        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            standardError += e.Data;
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                throw e.Error;
            }
            result = (string)e.Result;
            //OnFinished(new EventArgs());
            OnFinished(e);
        }
        /// <summary>
        /// The remember last line
        /// </summary>
        private string rememberLastLine;
        /// <summary>
        /// Handles the ProgressChanged event of the backgroundWorker1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProgressChangedEventArgs" /> instance containing the event data.</param>
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string userstate = (string)e.UserState;

            progress = e.ProgressPercentage;

            if (progress == 0)
            {
                stdOutput += userstate + "\n";
            }
            else
            {
                if (rememberLastLine == null)
                {
                    rememberLastLine = userstate;
                    stdOutput += userstate + "\n";
                }
                else
                {
                    stdOutput = stdOutput.Replace(rememberLastLine, userstate);
                    rememberLastLine = userstate;
                }
            }
            OnProgress(EventArgs.Empty);
        }

        
    }
}