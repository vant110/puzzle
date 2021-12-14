
namespace puzzle.Services
{
    partial class TopControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopControl));
            this.buttonBack = new System.Windows.Forms.Button();
            this.imageListPictograms = new System.Windows.Forms.ImageList(this.components);
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonSound = new System.Windows.Forms.Button();
            this.buttonPauseOrPlay = new System.Windows.Forms.Button();
            this.buttonImageOrPuzzle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.Transparent;
            this.buttonBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBack.FlatAppearance.BorderSize = 0;
            this.buttonBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.ImageIndex = 0;
            this.buttonBack.ImageList = this.imageListPictograms;
            this.buttonBack.Location = new System.Drawing.Point(13, 13);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(20, 20);
            this.buttonBack.TabIndex = 0;
            this.buttonBack.UseVisualStyleBackColor = false;
            // 
            // imageListPictograms
            // 
            this.imageListPictograms.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListPictograms.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPictograms.ImageStream")));
            this.imageListPictograms.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPictograms.Images.SetKeyName(0, "left-arrow.png");
            this.imageListPictograms.Images.SetKeyName(1, "info.png");
            this.imageListPictograms.Images.SetKeyName(2, "sound-on.png");
            this.imageListPictograms.Images.SetKeyName(3, "sound-off.png");
            this.imageListPictograms.Images.SetKeyName(4, "pause.png");
            this.imageListPictograms.Images.SetKeyName(5, "play.png");
            this.imageListPictograms.Images.SetKeyName(6, "image.png");
            this.imageListPictograms.Images.SetKeyName(7, "puzzle.png");
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(46, 17);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(27, 13);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Title";
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelp.BackColor = System.Drawing.Color.Transparent;
            this.buttonHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHelp.FlatAppearance.BorderSize = 0;
            this.buttonHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHelp.ImageIndex = 1;
            this.buttonHelp.ImageList = this.imageListPictograms;
            this.buttonHelp.Location = new System.Drawing.Point(667, 13);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(20, 20);
            this.buttonHelp.TabIndex = 2;
            this.buttonHelp.UseVisualStyleBackColor = false;
            // 
            // buttonSound
            // 
            this.buttonSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSound.BackColor = System.Drawing.Color.Transparent;
            this.buttonSound.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSound.FlatAppearance.BorderSize = 0;
            this.buttonSound.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonSound.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSound.ImageIndex = 2;
            this.buttonSound.ImageList = this.imageListPictograms;
            this.buttonSound.Location = new System.Drawing.Point(634, 13);
            this.buttonSound.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.buttonSound.Name = "buttonSound";
            this.buttonSound.Size = new System.Drawing.Size(20, 20);
            this.buttonSound.TabIndex = 3;
            this.buttonSound.UseVisualStyleBackColor = false;
            // 
            // buttonPauseOrPlay
            // 
            this.buttonPauseOrPlay.BackColor = System.Drawing.Color.Transparent;
            this.buttonPauseOrPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonPauseOrPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPauseOrPlay.FlatAppearance.BorderSize = 0;
            this.buttonPauseOrPlay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonPauseOrPlay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonPauseOrPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPauseOrPlay.ImageIndex = 4;
            this.buttonPauseOrPlay.ImageList = this.imageListPictograms;
            this.buttonPauseOrPlay.Location = new System.Drawing.Point(86, 13);
            this.buttonPauseOrPlay.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.buttonPauseOrPlay.Name = "buttonPauseOrPlay";
            this.buttonPauseOrPlay.Size = new System.Drawing.Size(20, 20);
            this.buttonPauseOrPlay.TabIndex = 4;
            this.buttonPauseOrPlay.UseVisualStyleBackColor = false;
            // 
            // buttonImageOrPuzzle
            // 
            this.buttonImageOrPuzzle.BackColor = System.Drawing.Color.Transparent;
            this.buttonImageOrPuzzle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonImageOrPuzzle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonImageOrPuzzle.FlatAppearance.BorderSize = 0;
            this.buttonImageOrPuzzle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonImageOrPuzzle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonImageOrPuzzle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonImageOrPuzzle.ImageIndex = 6;
            this.buttonImageOrPuzzle.ImageList = this.imageListPictograms;
            this.buttonImageOrPuzzle.Location = new System.Drawing.Point(119, 13);
            this.buttonImageOrPuzzle.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.buttonImageOrPuzzle.Name = "buttonImageOrPuzzle";
            this.buttonImageOrPuzzle.Size = new System.Drawing.Size(20, 20);
            this.buttonImageOrPuzzle.TabIndex = 5;
            this.buttonImageOrPuzzle.UseVisualStyleBackColor = false;
            // 
            // TopControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.buttonImageOrPuzzle);
            this.Controls.Add(this.buttonPauseOrPlay);
            this.Controls.Add(this.buttonSound);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.buttonBack);
            this.Name = "TopControl";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(700, 46);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.ImageList imageListPictograms;
        private System.Windows.Forms.Button buttonSound;
        private System.Windows.Forms.Button buttonPauseOrPlay;
        private System.Windows.Forms.Button buttonImageOrPuzzle;
    }
}
