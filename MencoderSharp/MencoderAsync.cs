using System;
using System.ComponentModel;
using System.Diagnostics;

namespace MencoderSharp
{
    /// <summary>
    /// Mencoderwrapper Async
    /// </summary>
    public class MencoderAsync : MencoderBase
    {
        /// <summary>
        /// Contains prgress parsed from mencoder
        /// </summary>
        public int progress;

        /// <summary>
        /// Contains exitcode of mencoder and everything from standarderror
        /// </summary>
        public mencoderResults Result;

        //Backgroundworkerdoku:
        //http://msdn.microsoft.com/de-de/library/system.componentmodel.backgroundworker.aspx
        private BackgroundWorker backgroundWorker1 = new BackgroundWorker();

        /// <summary>
        /// The remember last line
        /// </summary>
        private string rememberLastLine;

        /// <summary>
        /// The standard error
        /// </summary>
        private string standardError;

        //Events without custom args:
        //http://msdn.microsoft.com/en-us/library/ms182178(VS.80).aspx
        /// <summary>
        /// Fires when mencoder is done
        /// </summary>
        public event EventHandler Finished;

        /// <summary>
        /// Fires when progress has changed (progress and stdOutput have changed)
        /// </summary>
        public event EventHandler Progress;

        /// <summary>
        /// cancels the running encodingprocess
        /// </summary>
        public void cancelEncodeAsync()
        {
            this.backgroundWorker1.CancelAsync();
        }

        /// <summary>
        /// Starts the encode async with default configuration (16/9, x264@512kbit and faac@128kbit ).
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public void startEncodeAsync(Uri source, Uri destination)
        {
            startEncodeAsync(source, destination, "-vf dsize=16/9,scale=-10:-1,harddup -of lavf -lavfopts format=mp4 -ovc x264 -sws 9 -x264encopts nocabac:level_idc=30:bframes=0:bitrate=512:threads=auto:turbo=1:global_header:threads=auto", "-oac mp3lame");
        }

        /// <summary>
        /// returns imediatly after gots called
        /// </summary>
        /// <param name="source">Path to a file which can be interpreted by mencoder</param>
        /// <param name="destination">Path to a file where the mencodet should get saved</param>
        /// <param name="videoParameter">e.g. -ovc xvid -xvidencopts bitrate=800:threads=2:aspect=4/3 -vf scale -xy 720</param>
        /// <param name="audioParameter">e.g. -oac mp3lame</param>
        public void startEncodeAsync(Uri source, Uri destination, string videoParameter, string audioParameter)
        {
            MencoderParameters originalString = new MencoderParameters();
            this.backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.Result = new mencoderResults();
            originalString.source = source.OriginalString;
            originalString.destination = destination.OriginalString;
            originalString.audioParameter = audioParameter;
            originalString.videoParameter = videoParameter;
            this.backgroundWorker1.RunWorkerAsync(originalString);
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
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
            MencoderParameters mencoderParameter = new MencoderParameters();
            this.backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.Result = new mencoderResults();
            mencoderParameter.source = source;
            mencoderParameter.destination = destination;
            mencoderParameter.audioParameter = audioParameter;
            mencoderParameter.videoParameter = videoParameter;
            this.backgroundWorker1.RunWorkerAsync(mencoderParameter);
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
        }

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

        private static int parseAndReportProgress(BackgroundWorker worker, string standardOut, int progressReporting)
        {
            if (!standardOut.StartsWith("Pos:"))
            {
                worker.ReportProgress(progressReporting, standardOut);
            }
            else
            {
                int num = 0;
                char[] chrArray = new char[] { '(' };
                if (int.TryParse(standardOut.Split(chrArray)[1].Substring(0, 2).Trim(), out num) && num > progressReporting)
                {
                    progressReporting = num;
                    worker.ReportProgress(progressReporting, standardOut);
                }
            }
            return progressReporting;
        }

        /// <summary>
        /// Handles the DoWork event of the backgroundWorker1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string str;
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;
            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the
            // RunWorkerCompleted eventhandler.
            MencoderParameters argument = (MencoderParameters)e.Argument;

            try
            {
                Process process = new Process();
                process.ErrorDataReceived += new DataReceivedEventHandler(this.p_ErrorDataReceived);
                process.StartInfo.FileName = this.pathToMencoderExe;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                ProcessStartInfo startInfo = process.StartInfo;
                string[] strArrays = new string[] { "\"", argument.source, "\" ", argument.videoParameter, " ", argument.audioParameter, " -o \"", argument.destination, "\"" };
                startInfo.Arguments = string.Concat(strArrays);
                process.Start();
                process.BeginErrorReadLine();
                int num = 0;
                while (true)
                {
                    string str1 = process.StandardOutput.ReadLine();
                    str = str1;
                    if (str1 == null || backgroundWorker.CancellationPending)
                    {
                        break;
                    }
                    num = MencoderAsync.parseAndReportProgress(backgroundWorker, str, num);
                }
                if (backgroundWorker.CancellationPending)
                {
                    mencoderResults mencoderResult = new mencoderResults()
                    {
                        Exitcode = 99,
                        StandardError = this.standardError,
                        ExecutionWasSuccessfull = false,
                        StandardOutput = str
                    };
                    e.Result = mencoderResult;
                    process.Close();
                    process.CancelErrorRead();
                    process.Dispose();
                }
                else
                {
                    process.WaitForExit();
                    mencoderResults mencoderResult1 = new mencoderResults()
                    {
                        Exitcode = process.ExitCode,
                        StandardError = this.standardError,
                        ExecutionWasSuccessfull = process.ExitCode == 0,
                        StandardOutput = str
                    };
                    e.Result = mencoderResult1;
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                mencoderResults mencoderResult2 = new mencoderResults()
                {
                    Exitcode = 99,
                    StandardError = this.standardError,
                    ExecutionWasSuccessfull = false,
                    StandardOutput = exception.Message
                };
                e.Result = mencoderResult2;
            }
        }

        /// <summary>
        /// Handles the ProgressChanged event of the backgroundWorker1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProgressChangedEventArgs" /> instance containing the event data.</param>
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string userState = (string)e.UserState;
            this.progress = e.ProgressPercentage;
            if (this.progress == 0)
            {
                MencoderAsync mencoderAsync = this;
                mencoderAsync.standardError = string.Concat(mencoderAsync.standardError, userState, "\n");
            }
            else if (!string.IsNullOrEmpty(this.rememberLastLine))
            {
                this.standardError = this.standardError.Replace(this.rememberLastLine, userState);
                this.rememberLastLine = userState;
            }
            else
            {
                this.rememberLastLine = userState;
                MencoderAsync mencoderAsync1 = this;
                mencoderAsync1.standardError = string.Concat(mencoderAsync1.standardError, userState, "\n");
            }
            this.OnProgress(EventArgs.Empty);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                throw e.Error;
            }
            Result = (mencoderResults)e.Result;
            OnFinished(e);
        }

        /// <summary>
        /// Handles the ErrorDataReceived event of the p control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataReceivedEventArgs" /> instance containing the event data.</param>
        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            MencoderAsync mencoderAsync = this;
            mencoderAsync.standardError = string.Concat(mencoderAsync.standardError, e.Data);
        }
    }
}