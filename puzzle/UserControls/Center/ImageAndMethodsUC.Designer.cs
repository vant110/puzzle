﻿
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
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton0 = new System.Windows.Forms.RadioButton();
            this.panelMethods = new System.Windows.Forms.Panel();
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
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Способ подсчета результата:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 26);
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
            this.radioButton2.Location = new System.Drawing.Point(3, 56);
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
            this.radioButton0.Location = new System.Drawing.Point(3, 86);
            this.radioButton0.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.radioButton0.Name = "radioButton0";
            this.radioButton0.Size = new System.Drawing.Size(82, 17);
            this.radioButton0.TabIndex = 4;
            this.radioButton0.Text = "не ведется";
            this.radioButton0.UseVisualStyleBackColor = true;
            // 
            // panelMethods
            // 
            this.panelMethods.AutoSize = true;
            this.panelMethods.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMethods.Controls.Add(this.label1);
            this.panelMethods.Controls.Add(this.radioButton0);
            this.panelMethods.Controls.Add(this.radioButton1);
            this.panelMethods.Controls.Add(this.radioButton2);
            this.panelMethods.Location = new System.Drawing.Point(13, 332);
            this.panelMethods.Margin = new System.Windows.Forms.Padding(0, 13, 0, 0);
            this.panelMethods.Name = "panelMethods";
            this.panelMethods.Size = new System.Drawing.Size(166, 106);
            this.panelMethods.TabIndex = 5;
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
            this.Size = new System.Drawing.Size(426, 451);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.panelMethods.ResumeLayout(false);
            this.panelMethods.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.PictureBox pictureBoxImage;
        public System.Windows.Forms.Panel panelMethods;
        public System.Windows.Forms.RadioButton radioButton1;
        public System.Windows.Forms.RadioButton radioButton2;
        public System.Windows.Forms.RadioButton radioButton0;
    }
}
