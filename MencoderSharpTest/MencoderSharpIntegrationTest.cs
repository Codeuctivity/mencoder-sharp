using System;
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
                Assert.True(result);
            }
        }

        private bool asyncTaskRunning;

        [Fact]
        public void ShouldStartEncodeAsync()
        {
            progress = 0;
            asyncTaskRunning = false;
            using (var mencoderAsync = new MencoderSharp.MencoderAsync())
            {
                mencoderAsync.Finished += new EventHandler(Mencoder_Finished);
                mencoderAsync.ProgressChanged += new EventHandler(Mencoder_Progress);
                mencoderAsync.StartEncodeAsync("./TestFiles/HelloWorld.avi", "./TestOutput.mp4");
                asyncTaskRunning = true;
                while (asyncTaskRunning)
                {
                    Task.Delay(1000);
                }
                Assert.True(progress > 0);
                Assert.True(mencoderAsync.Result.ExecutionWasSuccessfull, mencoderAsync.Result.StandardError);
            }
        }

        [Fact]
        public void ShouldStartEncodeSync()
        {
            asyncTaskRunning = false;
            using (var mencoderAsync = new MencoderSharp.MencoderAsync())
            {
                mencoderAsync.Finished += new EventHandler(Mencoder_Finished);
                mencoderAsync.ProgressChanged += new EventHandler(Mencoder_Progress);
                mencoderAsync.StartEncodeAsync("./TestFiles/small.mp4", "./SmallTestOutput.mp4");
                asyncTaskRunning = true;
                while (asyncTaskRunning)
                {
                    Task.Delay(1000);
                }
                Assert.True(progress > 0);
                Assert.True(mencoderAsync.Result.ExecutionWasSuccessfull, mencoderAsync.Result.StandardError);
            }
        }

        [Fact]
        public void ShouldNotFailOnMultipleDisposeCalls()
        {
            asyncTaskRunning = false;
            var mencoderAsync = new MencoderSharp.MencoderAsync();
            mencoderAsync.Dispose();
            mencoderAsync.Dispose();
        }

        private int progress;

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