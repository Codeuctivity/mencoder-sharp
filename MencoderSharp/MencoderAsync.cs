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
        public int Progress { get; private set; }

        /// <summary>
        /// Contains exitcode of mencoder and everything from standarderror
        /// </summary>
        public MencoderResults Result { get; private set; }

        //Backgroundworkerdocs:
        //http://msdn.microsoft.com/de-de/library/system.componentmodel.backgroundworker.aspx
        private BackgroundWorker backgroundWorker1 = new BackgroundWorker();

        /// <summary>
        /// The remember last line
        /// </summary>
        private string rememberLastLine = string.Empty;

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
        public event EventHandler ProgressChanged;

        /// <summary>
        /// cancels the running encodingprocess
        /// </summary>
        public void CancelEncodeAsync()
        {
            backgroundWorker1.CancelAsync();
        }

        /// <summary>
        /// Starts the encode async with default configuration (16/9, x264@512kbit and faac@128kbit ).
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public void StartEncodeAsync(string source, string destination)
        {
            StartEncodeAsync(source, destination, "-vf dsize=16/9,scale=-10:-1,harddup -of lavf -lavfopts format=mp4 -ovc x264 -sws 9 -x264encopts nocabac:level_idc=30:bframes=0:bitrate=512:threads=auto:turbo=1:global_header:threads=auto", "-oac mp3lame");
        }

        /// <summary>
        /// Starts the encode async.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="videoParameter">The video parameter.</param>
        /// <param name="audioParameter">The audio parameter.</param>
        public void StartEncodeAsync(string source, string destination, string videoParameter, string audioParameter)
        {
            var mencoderParameter = new MencoderParameters();
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.DoWork += new DoWorkEventHandler(BackgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(BackgroundWorker1_ProgressChanged);
            Result = new MencoderResults();
            mencoderParameter.Source = source;
            mencoderParameter.Destination = destination;
            mencoderParameter.AudioParameter = audioParameter;
            mencoderParameter.VideoParameter = videoParameter;
            backgroundWorker1.RunWorkerAsync(mencoderParameter);
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        /// <summary>
        /// Raises the <see cref="E:Finished" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnFinished(EventArgs e)
        {
            Finished?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:Progress" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected virtual void OnProgress(EventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }

        private static int ParseAndReportProgress(BackgroundWorker worker, string standardOut, int progressReporting)
        {
            if (!standardOut.StartsWith("Pos:"))
            {
                worker.ReportProgress(progressReporting, standardOut);
            }
            else
            {
                var chrArray = new char[] { '(' };
                if (int.TryParse(standardOut.Split(chrArray)[1].Substring(0, 2).Trim(), out var num) && num > progressReporting)
                {
                    worker.ReportProgress(num, standardOut);
                    return num;
                }
            }
            return progressReporting;
        }

        /// <summary>
        /// Handles the DoWork event of the backgroundWorker1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs" /> instance containing the event data.</param>
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string standardOut;
            // Get the BackgroundWorker that raised this event.
            var backgroundWorker = sender as BackgroundWorker;
            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the
            // RunWorkerCompleted eventhandler.
            var argument = (MencoderParameters)e.Argument;

            try
            {
                using (var process = new Process())
                {
                    process.ErrorDataReceived += new DataReceivedEventHandler(P_ErrorDataReceived);
                    process.StartInfo.FileName = base.PathToExternalMencoderBin;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    var startInfo = process.StartInfo;
                    var strArrays = new string[] { "\"", argument.Source, "\" ", argument.VideoParameter, " ", argument.AudioParameter, " -o \"", argument.Destination, "\"" };
                    startInfo.Arguments = string.Concat(strArrays);
                    process.Start();
                    process.BeginErrorReadLine();
                    var num = 0;
                    while (true)
                    {
                        var standardOutLine = process.StandardOutput.ReadLine();
                        standardOut = standardOutLine;
                        if (standardOutLine == null || backgroundWorker.CancellationPending)
                        {
                            break;
                        }
                        num = ParseAndReportProgress(backgroundWorker, standardOut, num);
                    }
                    if (backgroundWorker.CancellationPending)
                    {
                        var mencoderResult = new MencoderResults
                        {
                            Exitcode = 99,
                            StandardError = standardError,
                            ExecutionWasSuccessfull = false,
                            StandardOutput = standardOut
                        };
                        e.Result = mencoderResult;
                        process.Close();
                        process.CancelErrorRead();
                    }
                    else
                    {
                        process.WaitForExit();
                        var mencoderResult1 = new MencoderResults
                        {
                            Exitcode = process.ExitCode,
                            StandardError = standardError,
                            ExecutionWasSuccessfull = process.ExitCode == 0,
                            StandardOutput = standardOut
                        };
                        e.Result = mencoderResult1;
                    }
                }
            }
            catch (Exception exception1)
            {
                var exception = exception1;
                var mencoderResult = new MencoderResults
                {
                    Exitcode = 99,
                    StandardError = standardError,
                    ExecutionWasSuccessfull = false,
                    StandardOutput = exception.Message
                };
                e.Result = mencoderResult;
            }
        }

        /// <summary>
        /// Handles the ProgressChanged event of the backgroundWorker1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ProgressChangedEventArgs" /> instance containing the event data.</param>
        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var userState = (string)e.UserState;
            Progress = e.ProgressPercentage;
            if (Progress == 0)
            {
                var mencoderAsync = this;
                mencoderAsync.standardError = string.Concat(mencoderAsync.standardError, userState, "\n");
            }
            else if (!string.IsNullOrEmpty(rememberLastLine))
            {
                standardError = standardError.Replace(rememberLastLine, userState);
                rememberLastLine = userState;
            }
            else
            {
                rememberLastLine = userState;
                var mencoderAsync = this;
                mencoderAsync.standardError = string.Concat(mencoderAsync.standardError, userState, "\n");
            }
            OnProgress(EventArgs.Empty);
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                throw e.Error;
            }
            Result = (MencoderResults)e.Result;
            OnFinished(e);
        }

        /// <summary>
        /// Handles the ErrorDataReceived event of the p control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataReceivedEventArgs" /> instance containing the event data.</param>
        private void P_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            var mencoderAsync = this;
            mencoderAsync.standardError = string.Concat(mencoderAsync.standardError, e.Data);
        }
    }
}