
namespace puzzle.Services
{
    partial class List
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
            this.comboBoxLevel = new System.Windows.Forms.ComboBox();
            this.listView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // comboBoxLevel
            // 
            this.comboBoxLevel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxLevel.FormattingEnabled = true;
            this.comboBoxLevel.Location = new System.Drawing.Point(13, 13);
            this.comboBoxLevel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
            this.comboBoxLevel.Name = "comboBoxLevel";
            this.comboBoxLevel.Size = new System.Drawing.Size(374, 21);
            this.comboBoxLevel.TabIndex = 0;
            // 
            // listView
            // 
            this.listView.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(13, 54);
            this.listView.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(374, 433);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            // 
            // List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Controls.Add(this.comboBoxLevel);
            this.Name = "List";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(400, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxLevel;
        private System.Windows.Forms.ListView listView;
    }
}
