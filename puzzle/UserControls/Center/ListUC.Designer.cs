﻿
namespace puzzle.UserControls
{
    partial class ListUC
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
            this.comboBoxLevels = new System.Windows.Forms.ComboBox();
            this.listBox = new System.Windows.Forms.ListBox();
            this.panelLevel = new System.Windows.Forms.Panel();
            this.panelLevel.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxLevel
            // 
            this.comboBoxLevels.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxLevels.Location = new System.Drawing.Point(0, 0);
            this.comboBoxLevels.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxLevels.Name = "comboBoxLevel";
            this.comboBoxLevels.Size = new System.Drawing.Size(374, 21);
            this.comboBoxLevels.Sorted = true;
            this.comboBoxLevels.TabIndex = 0;
            this.comboBoxLevels.ValueMember = "Id";
            // 
            // listBox
            // 
            this.listBox.DisplayMember = "Name";
            this.listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox.FormattingEnabled = true;
            this.listBox.HorizontalScrollbar = true;
            this.listBox.Location = new System.Drawing.Point(13, 57);
            this.listBox.Margin = new System.Windows.Forms.Padding(0);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(374, 420);
            this.listBox.TabIndex = 1;
            this.listBox.ValueMember = "Id";
            // 
            // panelLevel
            // 
            this.panelLevel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelLevel.BackColor = System.Drawing.Color.Transparent;
            this.panelLevel.Controls.Add(this.comboBoxLevels);
            this.panelLevel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLevel.Location = new System.Drawing.Point(13, 18);
            this.panelLevel.Margin = new System.Windows.Forms.Padding(0);
            this.panelLevel.Name = "panelLevel";
            this.panelLevel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 18);
            this.panelLevel.Size = new System.Drawing.Size(374, 39);
            this.panelLevel.TabIndex = 2;
            // 
            // ListUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.panelLevel);
            this.Name = "ListUC";
            this.Padding = new System.Windows.Forms.Padding(13, 18, 13, 18);
            this.Size = new System.Drawing.Size(400, 495);
            this.panelLevel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.ListBox listBox;
        public System.Windows.Forms.ComboBox comboBoxLevels;
        public System.Windows.Forms.Panel panelLevel;
    }
}
