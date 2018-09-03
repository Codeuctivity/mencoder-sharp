[![Build status](https://ci.appveyor.com/api/projects/status/xmx1t9ieuxqjeeh8?svg=true)](https://ci.appveyor.com/project/stesee/mencoder-sharp)


# mencoder-sharp
Automatically exported from code.google.com/p/mencoder-sharp

You need to decode, encode or do some processing stuff on multimedia streams, files or whatever mencoder can read? You want to implement your solution in C# or some other .net language? Here you are! This is an assembly which wrappes calls to e.g. reencode some source with a other codec. 

Nugetpackage published: Install-Package MencoderSharp 

Documentation @Nudoq Watch the demo: https://github.com/stesee/mencoder-sharp/releases/download/untagged-526dd962a0a4c202b73a/MencoderSharpDemo.wmv

Features: 
•Synchron or Asynchron executing 
•No Gui-Freezing in Asyncmode 
•Progress recognition of mencoder 
•Works on Win32 & Win64 
•Tested@ Win xp, Vista, 7 & 8 
•mp4 / h.264 works fine 
•Streaming source from ftp ... 


Sample: 
MencoderSharp.MencoderAsync mencoderAsync = new MencoderSharp.MencoderAsync();
private void buttonStart_Click(object sender, EventArgs e)
        {
            mencoderAsync.startEncodeAsync(@"c:\examplevideo.wmv", @"c:\encodedVideo.avi", textBoxVideoParameter.Text, textBoxAudioParamter.Text);
        }

        private void MencoderSharpDemo_Load(object sender, EventArgs e)
        {
            mencoderAsync.Finished += new EventHandler(this.mencoder_Finished);
            mencoderAsync.Progress += new EventHandler(this.mencoder_Progress);
        }

        private void mencoder_Finished(object sender, EventArgs e)
        {
            richTextBox1.Text = mencoderAsync.result;
        }
        private void mencoder_Progress(object sender, EventArgs e)
        {
            progressBar1.Value = mencoderAsync.progress;
        }
    }

See http://debianisttoll.blogspot.com/2013/03/create-mp4-with-c.html http://debianisttoll.blogspot.com/2013/03/create-mp4-with-c.html to learn how to create mp4s with mencodersharp. 
