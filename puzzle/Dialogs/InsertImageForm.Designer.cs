
namespace puzzle.Dialogs
{
    partial class InsertImageForm
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
            this.labelName = new System.Windows.Forms.Label();
            this.labelImage = new System.Windows.Forms.Label();
            this.labelFile = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonFile = new System.Windows.Forms.Button();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelName.Location = new System.Drawing.Point(33, 23);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(55, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Название";
            // 
            // labelImage
            // 
            this.labelImage.AutoSize = true;
            this.labelImage.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelImage.Location = new System.Drawing.Point(13, 59);
            this.labelImage.Name = "labelImage";
            this.labelImage.Size = new System.Drawing.Size(75, 13);
            this.labelImage.TabIndex = 1;
            this.labelImage.Text = "Изображение";
            // 
            // labelFile
            // 
            this.labelFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelFile.Location = new System.Drawing.Point(190, 59);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(204, 13);
            this.labelFile.TabIndex = 2;
            this.labelFile.Text = "Название файла";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(94, 20);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.textBoxName.MaxLength = 30;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.PlaceholderText = "Название изображения";
            this.textBoxName.Size = new System.Drawing.Size(300, 21);
            this.textBoxName.TabIndex = 3;
            // 
            // buttonFile
            // 
            this.buttonFile.AutoSize = true;
            this.buttonFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonFile.Location = new System.Drawing.Point(94, 54);
            this.buttonFile.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.buttonFile.Name = "buttonFile";
            this.buttonFile.Size = new System.Drawing.Size(90, 23);
            this.buttonFile.TabIndex = 4;
            this.buttonFile.Text = "Выбрать файл";
            this.buttonFile.UseVisualStyleBackColor = true;
            // 
            // buttonInsert
            // 
            this.buttonInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInsert.AutoSize = true;
            this.buttonInsert.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonInsert.Location = new System.Drawing.Point(327, 90);
            this.buttonInsert.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(67, 23);
            this.buttonInsert.TabIndex = 5;
            this.buttonInsert.Text = "Добавить";
            this.buttonInsert.UseVisualStyleBackColor = true;
            // 
            // InsertImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(407, 126);
            this.Controls.Add(this.buttonInsert);
            this.Controls.Add(this.buttonFile);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.labelImage);
            this.Controls.Add(this.labelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertImageForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление изображения";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelImage;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonFile;
        private System.Windows.Forms.Button buttonInsert;
    }
}