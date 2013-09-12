namespace MencoderSharpDemo
{
    partial class MencoderSharpDemo
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelSource = new System.Windows.Forms.Label();
            this.labelDestinationPath = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonSelectSource = new System.Windows.Forms.Button();
            this.buttonSelectDestination = new System.Windows.Forms.Button();
            this.textBoxVideoParameter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAudioParamter = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(440, 133);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start Encoding";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Location = new System.Drawing.Point(135, 17);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(62, 13);
            this.labelSource.TabIndex = 1;
            this.labelSource.Text = "Sourcepath";
            // 
            // labelDestinationPath
            // 
            this.labelDestinationPath.AutoSize = true;
            this.labelDestinationPath.Location = new System.Drawing.Point(135, 49);
            this.labelDestinationPath.Name = "labelDestinationPath";
            this.labelDestinationPath.Size = new System.Drawing.Size(81, 13);
            this.labelDestinationPath.TabIndex = 2;
            this.labelDestinationPath.Text = "Destinationpath";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // buttonSelectSource
            // 
            this.buttonSelectSource.Location = new System.Drawing.Point(12, 12);
            this.buttonSelectSource.Name = "buttonSelectSource";
            this.buttonSelectSource.Size = new System.Drawing.Size(117, 23);
            this.buttonSelectSource.TabIndex = 3;
            this.buttonSelectSource.Text = "Select Source";
            this.buttonSelectSource.UseVisualStyleBackColor = true;
            this.buttonSelectSource.Click += new System.EventHandler(this.buttonSelectSource_Click);
            // 
            // buttonSelectDestination
            // 
            this.buttonSelectDestination.Location = new System.Drawing.Point(12, 41);
            this.buttonSelectDestination.Name = "buttonSelectDestination";
            this.buttonSelectDestination.Size = new System.Drawing.Size(117, 23);
            this.buttonSelectDestination.TabIndex = 4;
            this.buttonSelectDestination.Text = "Select Destination";
            this.buttonSelectDestination.UseVisualStyleBackColor = true;
            this.buttonSelectDestination.Click += new System.EventHandler(this.buttonSelectDestination_Click);
            // 
            // textBoxVideoParameter
            // 
            this.textBoxVideoParameter.Location = new System.Drawing.Point(138, 81);
            this.textBoxVideoParameter.Name = "textBoxVideoParameter";
            this.textBoxVideoParameter.Size = new System.Drawing.Size(377, 20);
            this.textBoxVideoParameter.TabIndex = 5;
            this.textBoxVideoParameter.Text = "-vf dsize=16/9,scale=-10:-1,harddup -of lavf -lavfopts format=mp4 -ovc x264 -sws " +
    "9 -x264encopts nocabac:level_idc=30:bframes=0:bitrate=512:threads=auto:turbo=1:g" +
    "lobal_header:threads=auto";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Videoparameter";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Audioparameter";
            // 
            // textBoxAudioParamter
            // 
            this.textBoxAudioParamter.Location = new System.Drawing.Point(138, 107);
            this.textBoxAudioParamter.Name = "textBoxAudioParamter";
            this.textBoxAudioParamter.Size = new System.Drawing.Size(377, 20);
            this.textBoxAudioParamter.TabIndex = 8;
            this.textBoxAudioParamter.Text = "-oac faac -faacopts mpeg=4:object=2:raw:br=128";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 162);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(503, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 191);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(503, 103);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "";
            // 
            // MencoderSharpDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 311);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.textBoxAudioParamter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxVideoParameter);
            this.Controls.Add(this.buttonSelectDestination);
            this.Controls.Add(this.buttonSelectSource);
            this.Controls.Add(this.labelDestinationPath);
            this.Controls.Add(this.labelSource);
            this.Controls.Add(this.buttonStart);
            this.Name = "MencoderSharpDemo";
            this.Text = "MencoderDemo";
            this.Load += new System.EventHandler(this.MencoderSharpDemo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.Label labelDestinationPath;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonSelectSource;
        private System.Windows.Forms.Button buttonSelectDestination;
        private System.Windows.Forms.TextBox textBoxVideoParameter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAudioParamter;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

