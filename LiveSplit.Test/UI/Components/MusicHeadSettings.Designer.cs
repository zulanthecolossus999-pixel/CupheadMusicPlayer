namespace LiveSplit.UI.Components
{
    partial class MusicHeadSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusicHeadSettings));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textFilePath = new System.Windows.Forms.TextBox();
            this.guideLabel = new System.Windows.Forms.Label();
            this.volumeSliderBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.volumeSlider = new System.Windows.Forms.Label();
            this.volumeValueLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.volumeSliderBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose the directory where all the music files you want to be used are located.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(356, 31);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 20);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textFilePath
            // 
            this.textFilePath.Location = new System.Drawing.Point(6, 31);
            this.textFilePath.Name = "textFilePath";
            this.textFilePath.Size = new System.Drawing.Size(344, 20);
            this.textFilePath.TabIndex = 2;
            // 
            // guideLabel
            // 
            this.guideLabel.AutoSize = true;
            this.guideLabel.Location = new System.Drawing.Point(3, 64);
            this.guideLabel.Name = "guideLabel";
            this.guideLabel.Size = new System.Drawing.Size(500, 221);
            this.guideLabel.TabIndex = 3;
            this.guideLabel.Text = resources.GetString("guideLabel.Text");
            this.guideLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // volumeSliderBar
            // 
            this.volumeSliderBar.Location = new System.Drawing.Point(6, 330);
            this.volumeSliderBar.Maximum = 100;
            this.volumeSliderBar.Name = "volumeSliderBar";
            this.volumeSliderBar.Size = new System.Drawing.Size(307, 45);
            this.volumeSliderBar.TabIndex = 4;
            this.volumeSliderBar.Value = 100;
            this.volumeSliderBar.Scroll += new System.EventHandler(this.volumeSliderBar_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // volumeSlider
            // 
            this.volumeSlider.AutoSize = true;
            this.volumeSlider.Location = new System.Drawing.Point(12, 314);
            this.volumeSlider.Name = "volumeSlider";
            this.volumeSlider.Size = new System.Drawing.Size(42, 13);
            this.volumeSlider.TabIndex = 6;
            this.volumeSlider.Text = "Volume";
            // 
            // volumeValueLabel
            // 
            this.volumeValueLabel.AutoSize = true;
            this.volumeValueLabel.Location = new System.Drawing.Point(280, 314);
            this.volumeValueLabel.Name = "volumeValueLabel";
            this.volumeValueLabel.Size = new System.Drawing.Size(33, 13);
            this.volumeValueLabel.TabIndex = 7;
            this.volumeValueLabel.Text = "100%";
            // 
            // MusicHeadSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.volumeSlider);
            this.Controls.Add(this.volumeValueLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.volumeSliderBar);
            this.Controls.Add(this.guideLabel);
            this.Controls.Add(this.textFilePath);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.label1);
            this.Name = "MusicHeadSettings";
            this.Size = new System.Drawing.Size(543, 378);
            this.Load += new System.EventHandler(this.MusicHeadSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.volumeSliderBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textFilePath;
        private System.Windows.Forms.Label guideLabel;
        private System.Windows.Forms.TrackBar volumeSliderBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label volumeSlider;
        private System.Windows.Forms.Label volumeValueLabel;
    }
}
