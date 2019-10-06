using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace MencoderSharpTest
{
    public class MencoderSharpIntegrationTest
    {
        [Fact]
        public void ShouldEncodeToH264Mp4()
        {
            var mencoderSync = new MencoderSharp.Mencoder();
            var result = mencoderSync.encodeToMp4("./TestFiles/HelloWorld.avi", "./TestOutput.mp4");
            Assert.True(result);
        }

        private bool asyncTaskRunning;

        [Fact]
        public void ShouldStartEncodeAsync()
        {
            progress = 0;
            asyncTaskRunning = false;
            MencoderSharp.MencoderAsync mencoderAsync = new MencoderSharp.MencoderAsync();
            mencoderAsync.Finished += new EventHandler(this.mencoder_Finished);
            mencoderAsync.ProgressChanged += new EventHandler(this.mencoder_Progress);
            mencoderAsync.startEncodeAsync("./TestFiles/HelloWorld.avi", "./TestOutput.mp4");
            asyncTaskRunning = true;
            while (asyncTaskRunning)
            {
                Task.Delay(1000);
            }
            Assert.True(progress > 0);
            Assert.True(mencoderAsync.Result.ExecutionWasSuccessfull, mencoderAsync.Result.StandardError);
        }

        [Fact]
        public void ShouldStartEncodeSync()
        {
            asyncTaskRunning = false;
            MencoderSharp.MencoderAsync mencoderAsync = new MencoderSharp.MencoderAsync();
            mencoderAsync.Finished += new EventHandler(this.mencoder_Finished);
            mencoderAsync.ProgressChanged += new EventHandler(this.mencoder_Progress);
            mencoderAsync.startEncodeAsync("./TestFiles/small.mp4", "./SmallTestOutput.mp4");
            asyncTaskRunning = true;
            while (asyncTaskRunning)
            {
                Task.Delay(1000);
            }
            Assert.True(progress > 0);
            Assert.True(mencoderAsync.Result.ExecutionWasSuccessfull, mencoderAsync.Result.StandardError);
        }

        private int progress;

        private void mencoder_Progress(object sender, EventArgs e)
        {
            var infos = (MencoderSharp.MencoderAsync)sender;
            progress = infos.Progress;
        }

        private void mencoder_Finished(object sender, EventArgs e)
        {
            asyncTaskRunning = false;
        }
    }
}