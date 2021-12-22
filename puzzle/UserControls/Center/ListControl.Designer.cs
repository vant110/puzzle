
namespace puzzle.UserControls
{
    partial class ListControl
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
            this.comboBoxLevel = new System.Windows.Forms.ComboBox();
            this.listBox = new System.Windows.Forms.ListBox();
            this.panelLevel = new System.Windows.Forms.Panel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panelLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxLevel
            // 
            this.comboBoxLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxLevel.Location = new System.Drawing.Point(0, 0);
            this.comboBoxLevel.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxLevel.Name = "comboBoxLevel";
            this.comboBoxLevel.Size = new System.Drawing.Size(374, 21);
            this.comboBoxLevel.Sorted = true;
            this.comboBoxLevel.TabIndex = 0;
            // 
            // listBox
            // 
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.HorizontalScrollbar = true;
            this.listBox.Location = new System.Drawing.Point(13, 57);
            this.listBox.Margin = new System.Windows.Forms.Padding(0);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(374, 420);
            this.listBox.TabIndex = 1;
            // 
            // panelLevel
            // 
            this.panelLevel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelLevel.BackColor = System.Drawing.Color.Transparent;
            this.panelLevel.Controls.Add(this.comboBoxLevel);
            this.panelLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLevel.Location = new System.Drawing.Point(13, 18);
            this.panelLevel.Margin = new System.Windows.Forms.Padding(0);
            this.panelLevel.Name = "panelLevel";
            this.panelLevel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 18);
            this.panelLevel.Size = new System.Drawing.Size(374, 39);
            this.panelLevel.TabIndex = 2;
            // 
            // ListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.panelLevel);
            this.Name = "ListControl";
            this.Padding = new System.Windows.Forms.Padding(13, 18, 13, 18);
            this.Size = new System.Drawing.Size(400, 495);
            this.panelLevel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ListBox listBox;
        public System.Windows.Forms.ComboBox comboBoxLevel;
        private System.Windows.Forms.Panel panelLevel;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}
