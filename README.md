# mencoder-sharp

[![Build status](https://ci.appveyor.com/api/projects/status/xmx1t9ieuxqjeeh8?svg=true)](https://ci.appveyor.com/project/stesee/mencoder-sharp) [![Codacy Badge](https://api.codacy.com/project/badge/Grade/38787d123f9749ee9debbcf9ce9e8913)](https://www.codacy.com/manual/stesee/mencoder-sharp?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Codeuctivity/mencoder-sharp&amp;utm_campaign=Badge_Grade) [![Nuget](https://img.shields.io/nuget/v/MencoderSharp.svg)](https://www.nuget.org/packages/MencoderSharp/)

You need to decode, encode or do some processing stuff on multimedia streams, files or whatever mencoder can read? You want to implement your solution in C# or some other .net language? Here you are! This is an assembly which wrappes calls to e.g. reencode some source with a other codec.

Nugetpackage
[MencoderSharp](https://www.nuget.org/packages/MencoderSharp/):

```Nuget
Install-Package MencoderSharp
```

Documentation [@Nudoq](http://www.nudoq.org/#!/Packages/MencoderSharp/MencoderSharp/Mencoder/M/encodeToMp4)
Watch the [demo](https://github.com/stesee/mencoder-sharp/releases/download/untagged-526dd962a0a4c202b73a/MencoderSharpDemo.wmv)

Features:

* Sync or Async execution
* No Gui-Freezing in Asyncmode
* Progress recognition of mencoder
* Comes with an mencoder bin that works on win32 & win64
* Can make use of any mencoder bin in place (Windows & Linux)
* mp4 / h.264 works fine
* Streaming source from ftp, http, ... (That was the reason I created this project)

Sample:

```csharp
var mencoderAsync = new MencoderSharp.MencoderAsync();

private void buttonStart_Click(object sender, EventArgs e)
{
    buttonStart.Enabled = false;
    progressBar1.Visible = true;
    mencoderAsync.StartEncodeAsync(labelSource.Text, labelDestinationPath.Text, textBoxVideoParameter.Text, textBoxAudioParamter.Text);
}

private void MencoderSharpDemo_Load(object sender, EventArgs e)
{
    mencoderAsync.Finished += this.mencoder_Finished;
    mencoderAsync.ProgressChanged += this.mencoder_Progress;
}

private void mencoder_Finished(object sender, EventArgs e)
{
    richTextBox1.Text = mencoderAsync.Result.StandardOutput;
}
private void mencoder_Progress(object sender, EventArgs e)
{
    progressBar1.Value = mencoderAsync.Progress;
}
```

Old Repository: <http://code.google.com/p/mencoder-sharp>
