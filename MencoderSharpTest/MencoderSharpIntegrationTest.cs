using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace MencoderSharpTest
{
    public class MencoderSharpIntegrationTest
    {
        [Fact]
        public void ShouldEncodeToH264Mp4()
        {
            using (var mencoderSync = new MencoderSharp.Mencoder())
            {
                var result = mencoderSync.EncodeToMp4("./TestFiles/HelloWorld.avi", "./TestOutput.mp4");
                pathToExternalMencoderBin = mencoderSync.PathToExternalMencoderBin;
                Assert.True(result);
            }
            AssertVeraPdfGotDisposed();
        }

        private bool asyncTaskRunning;

        [Fact]
        public void ShouldStartEncodeAsync()
        {
            progress = 0;
            asyncTaskRunning = false;
            using (var mencoderAsync = new MencoderSharp.MencoderAsync())
            {
                mencoderAsync.Finished += Mencoder_Finished;
                mencoderAsync.ProgressChanged += Mencoder_Progress;
                mencoderAsync.StartEncodeAsync("./TestFiles/HelloWorld.avi", "./TestOutput.mp4");
                pathToExternalMencoderBin = mencoderAsync.PathToExternalMencoderBin;
                asyncTaskRunning = true;
                while (asyncTaskRunning)
                {
                    Task.Delay(1000).Wait();
                }
                Assert.True(progress > 0);
                Assert.True(mencoderAsync.Result.ExecutionWasSuccessfull, mencoderAsync.Result.StandardError);
            }
            AssertVeraPdfGotDisposed();
        }

        [Fact]
        public void ShouldDisposeWithoutExceptionWhileEncodeAsync()
        {
            using (var mencoderAsync = new MencoderSharp.MencoderAsync())
            {
                mencoderAsync.Finished += Mencoder_Finished;
                mencoderAsync.ProgressChanged += Mencoder_Progress;
                mencoderAsync.StartEncodeAsync("./TestFiles/HelloWorld.avi", "./TestOutput.mp4");
                while (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(mencoderAsync.PathToExternalMencoderBin)).Length == 0)
                {
                    Task.Delay(50).Wait();
                }
            }
            AssertVeraPdfGotDisposed();
        }

        [Fact]
        public void ShouldStartEncodeAsyncAndCancle()
        {
            progress = 0;
            asyncTaskRunning = false;
            using (var mencoderAsync = new MencoderSharp.MencoderAsync())
            {
                mencoderAsync.Finished += Mencoder_Finished;
                mencoderAsync.ProgressChanged += Mencoder_Progress;
                mencoderAsync.StartEncodeAsync("./TestFiles/HelloWorld.avi", "./TestOutput.mp4");
                pathToExternalMencoderBin = mencoderAsync.PathToExternalMencoderBin;
                asyncTaskRunning = true;
                var runOnce = true;
                while (asyncTaskRunning)
                {
                    Task.Delay(500).Wait();
                    if (runOnce)
                    {
                        mencoderAsync.CancelEncodeAsync();
                        runOnce = false;
                    }
                }

                Assert.Equal(99, mencoderAsync.Result.Exitcode);
                Assert.False(mencoderAsync.Result.ExecutionWasSuccessfull);
            }
            AssertVeraPdfGotDisposed();
        }

        [Fact]
        public void ShouldStartEncodeSync()
        {
            using (var mencoderSync = new MencoderSharp.Mencoder())
            {
                Assert.True(mencoderSync.Mencode("./TestFiles/small.mp4", "./SmallTestOutput.mp4", "-vf dsize=16/9,scale=-10:-1,harddup -of lavf -lavfopts format=mp4 -ovc x264 -sws 9 -x264encopts nocabac:level_idc=30:bframes=0:bitrate=512:threads=auto:global_header:threads=auto", "-oac mp3lame"));
                pathToExternalMencoderBin = mencoderSync.PathToExternalMencoderBin;
            }
            AssertVeraPdfGotDisposed();
        }

        [Fact]
        public void ShouldDetectEncodeSyncFail()
        {
            using (var mencoderSync = new MencoderSharp.Mencoder())
            {
                Assert.False(mencoderSync.Mencode("./TestFiles/DoesNotExist", "./SmallTestOutput.mp4", "-vf dsize=16/9,scale=-10:-1,harddup -of lavf -lavfopts format=mp4 -ovc x264 -sws 9 -x264encopts nocabac:level_idc=30:bframes=0:bitrate=512:threads=auto:global_header:threads=auto", "-oac mp3lame"));
                pathToExternalMencoderBin = mencoderSync.PathToExternalMencoderBin;
            }
            AssertVeraPdfGotDisposed();
        }

        [Fact]
        public void ShouldNotFailOnMultipleDisposeCalls()
        {
            asyncTaskRunning = false;
            var mencoderAsync = new MencoderSharp.MencoderAsync();
            pathToExternalMencoderBin = mencoderAsync.PathToExternalMencoderBin;
            mencoderAsync.Dispose();
            AssertVeraPdfGotDisposed();
            mencoderAsync.Dispose();
        }

        private void AssertVeraPdfGotDisposed()
        {
            Assert.False(File.Exists(pathToExternalMencoderBin));
        }

        private int progress;
        private string pathToExternalMencoderBin;

        private void Mencoder_Progress(object sender, EventArgs e)
        {
            var infos = (MencoderSharp.MencoderAsync)sender;
            progress = infos.Progress;
        }

        private void Mencoder_Finished(object sender, EventArgs e)
        {
            asyncTaskRunning = false;
        }
    }
}