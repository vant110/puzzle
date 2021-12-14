
namespace puzzle.Services
{
    partial class LevelInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelVertical = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelHorizontal = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelFragmentType = new System.Windows.Forms.Label();
            this.labelAssemblyType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Количество фрагментов";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 112);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 20, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Тип сборки:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "по горизонтали:";
            // 
            // labelVertical
            // 
            this.labelVertical.AutoSize = true;
            this.labelVertical.Location = new System.Drawing.Point(150, 46);
            this.labelVertical.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.labelVertical.Name = "labelVertical";
            this.labelVertical.Size = new System.Drawing.Size(35, 13);
            this.labelVertical.TabIndex = 3;
            this.labelVertical.Text = "labelV";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 46);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "по вертикали:";
            // 
            // labelHorizontal
            // 
            this.labelHorizontal.AutoSize = true;
            this.labelHorizontal.Location = new System.Drawing.Point(150, 28);
            this.labelHorizontal.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.labelHorizontal.Name = "labelHorizontal";
            this.labelHorizontal.Size = new System.Drawing.Size(36, 13);
            this.labelHorizontal.TabIndex = 5;
            this.labelHorizontal.Text = "labelH";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 79);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 20, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Тип фрагментов:";
            // 
            // labelFragmentType
            // 
            this.labelFragmentType.AutoSize = true;
            this.labelFragmentType.Location = new System.Drawing.Point(150, 79);
            this.labelFragmentType.Margin = new System.Windows.Forms.Padding(3, 20, 3, 0);
            this.labelFragmentType.Name = "labelFragmentType";
            this.labelFragmentType.Size = new System.Drawing.Size(41, 13);
            this.labelFragmentType.TabIndex = 7;
            this.labelFragmentType.Text = "labelFT";
            // 
            // labelAssemblyType
            // 
            this.labelAssemblyType.AutoSize = true;
            this.labelAssemblyType.Location = new System.Drawing.Point(150, 112);
            this.labelAssemblyType.Margin = new System.Windows.Forms.Padding(3, 20, 3, 0);
            this.labelAssemblyType.Name = "labelAssemblyType";
            this.labelAssemblyType.Size = new System.Drawing.Size(42, 13);
            this.labelAssemblyType.TabIndex = 8;
            this.labelAssemblyType.Text = "labelAT";
            // 
            // LevelInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelAssemblyType);
            this.Controls.Add(this.labelFragmentType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelHorizontal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelVertical);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LevelInfo";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(400, 500);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelVertical;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelHorizontal;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelFragmentType;
        private System.Windows.Forms.Label labelAssemblyType;
    }
}
