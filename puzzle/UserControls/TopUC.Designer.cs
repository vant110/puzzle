
namespace puzzle.UserControls
{
    partial class TopUC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopUC));
            this.buttonBack = new System.Windows.Forms.Button();
            this.imageListPictograms = new System.Windows.Forms.ImageList(this.components);
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonSound = new System.Windows.Forms.Button();
            this.buttonImage = new System.Windows.Forms.Button();
            this.labelMethod = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.buttonBack.Location = new System.Drawing.Point(0, 0);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(0);
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
            this.labelTitle.Location = new System.Drawing.Point(33, 4);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(13, 4, 0, 0);
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
            this.buttonHelp.Margin = new System.Windows.Forms.Padding(0);
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
            this.buttonSound.Margin = new System.Windows.Forms.Padding(0, 0, 13, 0);
            this.buttonSound.Name = "buttonSound";
            this.buttonSound.Size = new System.Drawing.Size(20, 20);
            this.buttonSound.TabIndex = 3;
            this.buttonSound.UseVisualStyleBackColor = false;
            // 
            // buttonImage
            // 
            this.buttonImage.BackColor = System.Drawing.Color.Transparent;
            this.buttonImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonImage.FlatAppearance.BorderSize = 0;
            this.buttonImage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonImage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonImage.ImageIndex = 6;
            this.buttonImage.ImageList = this.imageListPictograms;
            this.buttonImage.Location = new System.Drawing.Point(73, 0);
            this.buttonImage.Margin = new System.Windows.Forms.Padding(13, 0, 0, 0);
            this.buttonImage.Name = "buttonImage";
            this.buttonImage.Size = new System.Drawing.Size(20, 20);
            this.buttonImage.TabIndex = 5;
            this.buttonImage.UseVisualStyleBackColor = false;
            // 
            // labelMethod
            // 
            this.labelMethod.AutoSize = true;
            this.labelMethod.Location = new System.Drawing.Point(106, 4);
            this.labelMethod.Margin = new System.Windows.Forms.Padding(13, 4, 0, 0);
            this.labelMethod.Name = "labelMethod";
            this.labelMethod.Size = new System.Drawing.Size(75, 13);
            this.labelMethod.TabIndex = 6;
            this.labelMethod.Text = "Время:/Очки:";
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(181, 4);
            this.labelValue.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(51, 13);
            this.labelValue.TabIndex = 7;
            this.labelValue.Text = "00:00:05";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.buttonBack);
            this.flowLayoutPanel1.Controls.Add(this.labelTitle);
            this.flowLayoutPanel1.Controls.Add(this.buttonImage);
            this.flowLayoutPanel1.Controls.Add(this.labelMethod);
            this.flowLayoutPanel1.Controls.Add(this.labelValue);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(232, 20);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // TopUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.buttonSound);
            this.Name = "TopUC";
            this.Padding = new System.Windows.Forms.Padding(13);
            this.Size = new System.Drawing.Size(700, 46);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Timer timer;
        public System.Windows.Forms.Label labelMethod;
        public System.Windows.Forms.Label labelValue;
        public System.Windows.Forms.Label labelTitle;
        public System.Windows.Forms.Button buttonHelp;
        public System.Windows.Forms.Button buttonSound;
        public System.Windows.Forms.ImageList imageListPictograms;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.Button buttonBack;
        public System.Windows.Forms.Button buttonImage;
    }
}
