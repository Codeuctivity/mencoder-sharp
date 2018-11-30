using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestMencoder2
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethodSyncEncode()
        {
            var mencoderSync = new MencoderSharp2.Mencoder();
            var result = mencoderSync.encodeToMp4(new Uri(getDirectoryOfAssembly() + "\\TestFiles\\HelloWorld.avi"), new Uri(getDirectoryOfAssembly() + "\\TestOutput.mp4"));
            Assert.True(result);
        }

        private static string getDirectoryOfAssembly()
        {
            var location = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            return location;
        }

        private bool asyncTaskRunning;

        [Fact]
        public void TestMethodAsync()
        {
            asyncTaskRunning = false;
            MencoderSharp2.MencoderAsync mencoderAsync = new MencoderSharp2.MencoderAsync();
            mencoderAsync.Finished += new EventHandler(this.mencoder_Finished);
            mencoderAsync.Progress += new EventHandler(this.mencoder_Progress);
            mencoderAsync.startEncodeAsync(new Uri(getDirectoryOfAssembly() + "\\TestFiles\\HelloWorld.avi"), new Uri(getDirectoryOfAssembly() + "\\TestOutput.mp4"));
            asyncTaskRunning = true;
            while (asyncTaskRunning)
            {
                Task.Delay(1000);
            }
            Assert.True(progress > 0);
            Assert.True(mencoderAsync.Result.ExecutionWasSuccessfull, mencoderAsync.Result.StandardError);
        }

        [Fact]
        public void TestMethodAsyncMp4AsSource()
        {
            asyncTaskRunning = false;
            MencoderSharp2.MencoderAsync mencoderAsync = new MencoderSharp2.MencoderAsync();
            mencoderAsync.Finished += new EventHandler(this.mencoder_Finished);
            mencoderAsync.Progress += new EventHandler(this.mencoder_Progress);
            mencoderAsync.startEncodeAsync(new Uri(getDirectoryOfAssembly() + "\\TestFiles\\small.mp4"), new Uri(getDirectoryOfAssembly() + "\\SmallTestOutput.mp4"));
            asyncTaskRunning = true;
            while (asyncTaskRunning)
            {
                Task.Delay(1000);
            }
            Assert.True(progress > 0);
            Assert.True(mencoderAsync.Result.ExecutionWasSuccessfull, mencoderAsync.Result.StandardError);
        }

        private int progress = 0;

        private void mencoder_Progress(object sender, EventArgs e)
        {
            var infos = (MencoderSharp2.MencoderAsync)sender;
            progress = infos.progress;
        }

        private void mencoder_Finished(object sender, EventArgs e)
        {
            asyncTaskRunning = false;
        }
    }
}