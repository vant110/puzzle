
namespace puzzle.UserControls.Center
{
    partial class GameUC
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
            this.pictureBoxField = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxField)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxField
            // 
            this.pictureBoxField.BackColor = System.Drawing.Color.White;
            this.pictureBoxField.Location = new System.Drawing.Point(13, 13);
            this.pictureBoxField.Name = "pictureBoxField";
            this.pictureBoxField.Size = new System.Drawing.Size(600, 450);
            this.pictureBoxField.TabIndex = 0;
            this.pictureBoxField.TabStop = false;
            // 
            // GameUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pictureBoxField);
            this.Name = "GameUC";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(626, 476);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBoxField;
    }
}
