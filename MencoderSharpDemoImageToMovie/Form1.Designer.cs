namespace MencoderSharpDemoImageToMovie
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBoxAudioParamter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxVideoParameter = new System.Windows.Forms.TextBox();
            this.buttonSelectDestination = new System.Windows.Forms.Button();
            this.buttonSelectSource = new System.Windows.Forms.Button();
            this.labelDestinationPath = new System.Windows.Forms.Label();
            this.labelSource = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 191);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(503, 103);
            this.richTextBox1.TabIndex = 21;
            this.richTextBox1.Text = "";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 162);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(503, 23);
            this.progressBar1.TabIndex = 20;
            // 
            // textBoxAudioParamter
            // 
            this.textBoxAudioParamter.Location = new System.Drawing.Point(138, 107);
            this.textBoxAudioParamter.Name = "textBoxAudioParamter";
            this.textBoxAudioParamter.Size = new System.Drawing.Size(377, 20);
            this.textBoxAudioParamter.TabIndex = 19;
            this.textBoxAudioParamter.Text = "-oac copy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Audioparameter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Videoparameter";
            // 
            // textBoxVideoParameter
            // 
            this.textBoxVideoParameter.Location = new System.Drawing.Point(138, 81);
            this.textBoxVideoParameter.Name = "textBoxVideoParameter";
            this.textBoxVideoParameter.Size = new System.Drawing.Size(377, 20);
            this.textBoxVideoParameter.TabIndex = 16;
            this.textBoxVideoParameter.Text = "-vf scale,harddup -zoom -xy 800 -ofps 25 -mf w=800:h=600:fps=1:type=jpg -ovc lavc" +
    " -lavcopts vcodec=mpeg4:mbd=2:trell -oac copy";
            // 
            // buttonSelectDestination
            // 
            this.buttonSelectDestination.Location = new System.Drawing.Point(12, 41);
            this.buttonSelectDestination.Name = "buttonSelectDestination";
            this.buttonSelectDestination.Size = new System.Drawing.Size(117, 23);
            this.buttonSelectDestination.TabIndex = 15;
            this.buttonSelectDestination.Text = "Select Destination";
            this.buttonSelectDestination.UseVisualStyleBackColor = true;
            this.buttonSelectDestination.Click += new System.EventHandler(this.buttonSelectDestination_Click);
            // 
            // buttonSelectSource
            // 
            this.buttonSelectSource.Location = new System.Drawing.Point(12, 12);
            this.buttonSelectSource.Name = "buttonSelectSource";
            this.buttonSelectSource.Size = new System.Drawing.Size(117, 23);
            this.buttonSelectSource.TabIndex = 14;
            this.buttonSelectSource.Text = "Select Source";
            this.buttonSelectSource.UseVisualStyleBackColor = true;
            this.buttonSelectSource.Click += new System.EventHandler(this.buttonSelectSource_Click);
            // 
            // labelDestinationPath
            // 
            this.labelDestinationPath.AutoSize = true;
            this.labelDestinationPath.Location = new System.Drawing.Point(135, 49);
            this.labelDestinationPath.Name = "labelDestinationPath";
            this.labelDestinationPath.Size = new System.Drawing.Size(81, 13);
            this.labelDestinationPath.TabIndex = 13;
            this.labelDestinationPath.Text = "Destinationpath";
            // 
            // labelSource
            // 
            this.labelSource.AutoSize = true;
            this.labelSource.Location = new System.Drawing.Point(135, 17);
            this.labelSource.Name = "labelSource";
            this.labelSource.Size = new System.Drawing.Size(62, 13);
            this.labelSource.TabIndex = 12;
            this.labelSource.Text = "Sourcepath";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(440, 133);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 11;
            this.buttonStart.Text = "Start Encoding";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "jpg";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 362);
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
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBoxAudioParamter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxVideoParameter;
        private System.Windows.Forms.Button buttonSelectDestination;
        private System.Windows.Forms.Button buttonSelectSource;
        private System.Windows.Forms.Label labelDestinationPath;
        private System.Windows.Forms.Label labelSource;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

