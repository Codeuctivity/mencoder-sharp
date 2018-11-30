using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace UnitTestMencoderSharp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodSyncEncode()
        {
            MencoderSharp.Mencoder mencoderSync = new MencoderSharp.Mencoder();
            Assert.IsTrue(mencoderSync.encodeToMp4(new Uri(getDirectoryOfAssembly() + "\\HelloWorld.avi"), new Uri(testContextInstance.ResultsDirectory + "\\TestOutput.mp4")));
        }

        private static string getDirectoryOfAssembly()
        {
            return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private bool asyncTaskRunning;

        [TestMethod]
        public void TestMethodAsync()
        {
            asyncTaskRunning = false;
            MencoderSharp.MencoderAsync mencoderAsync = new MencoderSharp.MencoderAsync();
            mencoderAsync.Finished += new EventHandler(this.mencoder_Finished);
            mencoderAsync.Progress += new EventHandler(this.mencoder_Progress);
            mencoderAsync.startEncodeAsync(new Uri(getDirectoryOfAssembly() + "\\HelloWorld.avi"), new Uri(testContextInstance.ResultsDirectory + "\\TestOutput.mp4"));
            asyncTaskRunning = true;
            while (asyncTaskRunning)
            {
                Task.Delay(1000);
            }
            Assert.IsTrue(progress > 0);
            Assert.IsTrue(mencoderAsync.Result.ExecutionWasSuccessfull, mencoderAsync.Result.StandardError);
        }

        [TestMethod]
        public void TestMethodAsyncMp4AsSource()
        {
            asyncTaskRunning = false;
            MencoderSharp.MencoderAsync mencoderAsync = new MencoderSharp.MencoderAsync();
            mencoderAsync.Finished += new EventHandler(this.mencoder_Finished);
            mencoderAsync.Progress += new EventHandler(this.mencoder_Progress);
            mencoderAsync.startEncodeAsync(new Uri(getDirectoryOfAssembly() + "\\small.mp4"), new Uri(testContextInstance.ResultsDirectory + "\\SmallTestOutput.mp4"));
            asyncTaskRunning = true;
            while (asyncTaskRunning)
            {
                Task.Delay(1000);
            }
            Assert.IsTrue(progress > 0);
            Assert.IsTrue(mencoderAsync.Result.ExecutionWasSuccessfull, mencoderAsync.Result.StandardError);
        }

        private int progress = 0;

        private void mencoder_Progress(object sender, EventArgs e)
        {
            var infos = (MencoderSharp.MencoderAsync)sender;
            progress = infos.progress;
        }

        private void mencoder_Finished(object sender, EventArgs e)
        {
            asyncTaskRunning = false;
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
    }
}