
namespace puzzle.UserControls
{
    partial class ImageAndMethodsUC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageAndMethodsUC));
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton0 = new System.Windows.Forms.RadioButton();
            this.panelMethods = new System.Windows.Forms.Panel();
            this.buttonSaved0 = new System.Windows.Forms.Button();
            this.imageListPictograms = new System.Windows.Forms.ImageList(this.components);
            this.buttonSaved2 = new System.Windows.Forms.Button();
            this.buttonSaved1 = new System.Windows.Forms.Button();
            this.buttonRecords2 = new System.Windows.Forms.Button();
            this.buttonRecords1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.panelMethods.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.BackColor = System.Drawing.Color.White;
            this.pictureBoxImage.Location = new System.Drawing.Point(13, 19);
            this.pictureBoxImage.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(400, 300);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImage.TabIndex = 0;
            this.pictureBoxImage.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Способ подсчета результата:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 31);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(70, 17);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "по очкам";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(3, 61);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(82, 17);
            this.radioButton2.TabIndex = 3;
            this.radioButton2.Text = "по времени";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton0
            // 
            this.radioButton0.AutoSize = true;
            this.radioButton0.Location = new System.Drawing.Point(3, 91);
            this.radioButton0.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.radioButton0.Name = "radioButton0";
            this.radioButton0.Size = new System.Drawing.Size(82, 17);
            this.radioButton0.TabIndex = 4;
            this.radioButton0.Text = "не ведется";
            this.radioButton0.UseVisualStyleBackColor = true;
            // 
            // panelMethods
            // 
            this.panelMethods.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMethods.Controls.Add(this.buttonSaved0);
            this.panelMethods.Controls.Add(this.buttonSaved2);
            this.panelMethods.Controls.Add(this.buttonSaved1);
            this.panelMethods.Controls.Add(this.buttonRecords2);
            this.panelMethods.Controls.Add(this.buttonRecords1);
            this.panelMethods.Controls.Add(this.label1);
            this.panelMethods.Controls.Add(this.radioButton0);
            this.panelMethods.Controls.Add(this.radioButton1);
            this.panelMethods.Controls.Add(this.radioButton2);
            this.panelMethods.Location = new System.Drawing.Point(13, 335);
            this.panelMethods.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.panelMethods.Name = "panelMethods";
            this.panelMethods.Size = new System.Drawing.Size(400, 118);
            this.panelMethods.TabIndex = 5;
            // 
            // buttonSaved0
            // 
            this.buttonSaved0.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonSaved0.BackColor = System.Drawing.Color.Transparent;
            this.buttonSaved0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSaved0.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonSaved0.FlatAppearance.BorderSize = 0;
            this.buttonSaved0.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonSaved0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonSaved0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaved0.ImageIndex = 0;
            this.buttonSaved0.ImageList = this.imageListPictograms;
            this.buttonSaved0.Location = new System.Drawing.Point(347, 89);
            this.buttonSaved0.Margin = new System.Windows.Forms.Padding(0, 0, 13, 0);
            this.buttonSaved0.Name = "buttonSaved0";
            this.buttonSaved0.Size = new System.Drawing.Size(20, 20);
            this.buttonSaved0.TabIndex = 9;
            this.buttonSaved0.UseVisualStyleBackColor = false;
            // 
            // imageListPictograms
            // 
            this.imageListPictograms.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListPictograms.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPictograms.ImageStream")));
            this.imageListPictograms.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPictograms.Images.SetKeyName(0, "floppy-disk.png");
            this.imageListPictograms.Images.SetKeyName(1, "trophy1.png");
            // 
            // buttonSaved2
            // 
            this.buttonSaved2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonSaved2.BackColor = System.Drawing.Color.Transparent;
            this.buttonSaved2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSaved2.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonSaved2.FlatAppearance.BorderSize = 0;
            this.buttonSaved2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonSaved2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonSaved2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaved2.ImageIndex = 0;
            this.buttonSaved2.ImageList = this.imageListPictograms;
            this.buttonSaved2.Location = new System.Drawing.Point(347, 59);
            this.buttonSaved2.Margin = new System.Windows.Forms.Padding(0, 0, 13, 0);
            this.buttonSaved2.Name = "buttonSaved2";
            this.buttonSaved2.Size = new System.Drawing.Size(20, 20);
            this.buttonSaved2.TabIndex = 8;
            this.buttonSaved2.UseVisualStyleBackColor = false;
            // 
            // buttonSaved1
            // 
            this.buttonSaved1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonSaved1.BackColor = System.Drawing.Color.Transparent;
            this.buttonSaved1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSaved1.Cursor = System.Windows.Forms.Cursors.Default;
            this.buttonSaved1.FlatAppearance.BorderSize = 0;
            this.buttonSaved1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonSaved1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonSaved1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaved1.ImageIndex = 0;
            this.buttonSaved1.ImageList = this.imageListPictograms;
            this.buttonSaved1.Location = new System.Drawing.Point(347, 29);
            this.buttonSaved1.Margin = new System.Windows.Forms.Padding(0, 0, 13, 0);
            this.buttonSaved1.Name = "buttonSaved1";
            this.buttonSaved1.Size = new System.Drawing.Size(20, 20);
            this.buttonSaved1.TabIndex = 7;
            this.buttonSaved1.UseVisualStyleBackColor = false;
            // 
            // buttonRecords2
            // 
            this.buttonRecords2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonRecords2.BackColor = System.Drawing.Color.Transparent;
            this.buttonRecords2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonRecords2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRecords2.FlatAppearance.BorderSize = 0;
            this.buttonRecords2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonRecords2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonRecords2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRecords2.ImageIndex = 1;
            this.buttonRecords2.ImageList = this.imageListPictograms;
            this.buttonRecords2.Location = new System.Drawing.Point(380, 59);
            this.buttonRecords2.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRecords2.Name = "buttonRecords2";
            this.buttonRecords2.Size = new System.Drawing.Size(20, 20);
            this.buttonRecords2.TabIndex = 6;
            this.buttonRecords2.UseVisualStyleBackColor = false;
            // 
            // buttonRecords1
            // 
            this.buttonRecords1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonRecords1.BackColor = System.Drawing.Color.Transparent;
            this.buttonRecords1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonRecords1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRecords1.FlatAppearance.BorderSize = 0;
            this.buttonRecords1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonRecords1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonRecords1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRecords1.ImageIndex = 1;
            this.buttonRecords1.ImageList = this.imageListPictograms;
            this.buttonRecords1.Location = new System.Drawing.Point(380, 29);
            this.buttonRecords1.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRecords1.Name = "buttonRecords1";
            this.buttonRecords1.Size = new System.Drawing.Size(20, 20);
            this.buttonRecords1.TabIndex = 5;
            this.buttonRecords1.UseVisualStyleBackColor = false;
            // 
            // ImageAndMethodsUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.panelMethods);
            this.Controls.Add(this.pictureBoxImage);
            this.Name = "ImageAndMethodsUC";
            this.Padding = new System.Windows.Forms.Padding(13, 19, 13, 13);
            this.Size = new System.Drawing.Size(426, 466);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.panelMethods.ResumeLayout(false);
            this.panelMethods.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.PictureBox pictureBoxImage;
        public System.Windows.Forms.Panel panelMethods;
        public System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.RadioButton radioButton2;
        public System.Windows.Forms.RadioButton radioButton0;
        public System.Windows.Forms.Button buttonRecords1;
        private System.Windows.Forms.ImageList imageListPictograms;
        public System.Windows.Forms.Button buttonSaved0;
        public System.Windows.Forms.Button buttonSaved2;
        public System.Windows.Forms.Button buttonSaved1;
        public System.Windows.Forms.Button buttonRecords2;
    }
}
