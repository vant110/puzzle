
namespace puzzle.CompositeControls
{
    partial class AdminMenuControl
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
            this.buttonGallery = new System.Windows.Forms.Button();
            this.buttonPuzzles = new System.Windows.Forms.Button();
            this.buttonLevels = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonGallery
            // 
            this.buttonGallery.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonGallery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonGallery.Location = new System.Drawing.Point(3, 3);
            this.buttonGallery.Name = "buttonGallery";
            this.buttonGallery.Size = new System.Drawing.Size(120, 23);
            this.buttonGallery.TabIndex = 0;
            this.buttonGallery.Text = "Галерея";
            this.buttonGallery.UseVisualStyleBackColor = true;
            // 
            // buttonPuzzles
            // 
            this.buttonPuzzles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonPuzzles.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonPuzzles.Location = new System.Drawing.Point(3, 75);
            this.buttonPuzzles.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.buttonPuzzles.Name = "buttonPuzzles";
            this.buttonPuzzles.Size = new System.Drawing.Size(120, 23);
            this.buttonPuzzles.TabIndex = 1;
            this.buttonPuzzles.Text = "Пазлы";
            this.buttonPuzzles.UseVisualStyleBackColor = true;
            // 
            // buttonLevels
            // 
            this.buttonLevels.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonLevels.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonLevels.Location = new System.Drawing.Point(3, 39);
            this.buttonLevels.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.buttonLevels.Name = "buttonLevels";
            this.buttonLevels.Size = new System.Drawing.Size(120, 23);
            this.buttonLevels.TabIndex = 2;
            this.buttonLevels.Text = "Уровни сложности";
            this.buttonLevels.UseVisualStyleBackColor = true;
            // 
            // AdminMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonLevels);
            this.Controls.Add(this.buttonPuzzles);
            this.Controls.Add(this.buttonGallery);
            this.Name = "AdminMenu";
            this.Size = new System.Drawing.Size(126, 101);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGallery;
        private System.Windows.Forms.Button buttonPuzzles;
        private System.Windows.Forms.Button buttonLevels;
    }
}
