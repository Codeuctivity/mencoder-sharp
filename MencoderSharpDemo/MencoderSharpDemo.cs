using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MencoderSharpDemo
{
    /// <summary>
    /// Demoimplementation of Mencodersharp - Async
    /// </summary>
    public partial class MencoderSharpDemo : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MencoderSharpDemo" /> class.
        /// </summary>
        public MencoderSharpDemo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the buttonSelectSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void buttonSelectSource_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the buttonSelectDestination control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void buttonSelectDestination_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        /// <summary>
        /// Handles the FileOk event of the saveFileDialog1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelEventArgs" /> instance containing the event data.</param>
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            labelDestinationPath.Text = saveFileDialog1.FileName;
        }

        /// <summary>
        /// Handles the FileOk event of the openFileDialog1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelEventArgs" /> instance containing the event data.</param>
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            labelSource.Text = openFileDialog1.FileName;
        }

        private readonly MencoderSharp.MencoderAsync mencoderAsync = new MencoderSharp.MencoderAsync();

        /// <summary>
        /// Handles the Click event of the buttonStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            progressBar1.Visible = true;
            mencoderAsync.StartEncodeAsync(labelSource.Text, labelDestinationPath.Text, textBoxVideoParameter.Text, textBoxAudioParamter.Text);
            //mencoderAsync.startEncodeAsync(new Uri(labelSource.Text), new Uri(labelDestinationPath.Text));
        }

        /// <summary>
        /// Handles the Load event of the MencoderSharpDemo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void MencoderSharpDemo_Load(object sender, EventArgs e)
        {
            mencoderAsync.Finished += new EventHandler(mencoder_Finished);
            mencoderAsync.ProgressChanged += new EventHandler(mencoder_Progress);
        }

        /// <summary>
        /// Handles the Finished event of the mencoder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void mencoder_Finished(object sender, EventArgs e)
        {
            richTextBox1.Text = mencoderAsync.Result.StandardOutput + mencoderAsync.Result.StandardError;
            buttonStart.Enabled = true;
            progressBar1.Visible = false;
        }

        /// <summary>
        /// Handles the Progress event of the mencoder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void mencoder_Progress(object sender, EventArgs e)
        {
            progressBar1.Value = mencoderAsync.Progress;
        }
    }
}