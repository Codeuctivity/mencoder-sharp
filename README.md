# mencoder-sharp

[![Build status](https://ci.appveyor.com/api/projects/status/xmx1t9ieuxqjeeh8?svg=true)](https://ci.appveyor.com/project/stesee/mencoder-sharp)

You need to decode, encode or do some processing stuff on multimedia streams, files or whatever mencoder can read? You want to implement your solution in C# or some other .net language? Here you are! This is an assembly which wrappes calls to e.g. reencode some source with a other codec.

Nugetpackage
[MencoderSharp](https://www.nuget.org/packages/MencoderSharp/):

```Nuget
Install-Package MencoderSharp
```

Documentation [@Nudoq](http://www.nudoq.org/#!/Packages/MencoderSharp/MencoderSharp/Mencoder/M/encodeToMp4)
Watch the [demo](https://github.com/stesee/mencoder-sharp/releases/download/untagged-526dd962a0a4c202b73a/MencoderSharpDemo.wmv)

Features:

* Synchron or Asynchron executing
* No Gui-Freezing in Asyncmode
* Progress recognition of mencoder
* Works on Win32 & Win64  
* mp4 / h.264 works fine
* Streaming source from ftp, http, ...

Sample:

```csharp
MencoderSharp.MencoderAsync mencoderAsync = new MencoderSharp.MencoderAsync();
private void buttonStart_Click(object sender, EventArgs e)
{
    mencoderAsync.startEncodeAsync(@"c:\inputVideo.wmv", @"c:\outputVideo.mp4");
}

private void MencoderSharpDemo_Load(object sender, EventArgs e)
{
    mencoderAsync.Finished += new EventHandler(this.mencoder_Finished);
    mencoderAsync.ProgressChanged += new EventHandler(this.mencoder_Progress);
}

private void mencoder_Finished(object sender, EventArgs e)
{
    richTextBox1.Text = mencoderAsync.result;
}
private void mencoder_Progress(object sender, EventArgs e)
{
    progressBar1.Value = mencoderAsync.progress;
}
```

Old Repository: <http://code.google.com/p/mencoder-sharp>
