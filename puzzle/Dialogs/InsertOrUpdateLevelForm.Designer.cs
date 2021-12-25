
namespace puzzle.Dialogs
{
    partial class InsertOrUpdateLevelForm
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
            this.buttonInsertOrUpdate = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelVertical = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxFragmentType = new System.Windows.Forms.ComboBox();
            this.comboBoxAssemblyType = new System.Windows.Forms.ComboBox();
            this.numericUpDownHorizontal = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHorizontal)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonInsertOrUpdate
            // 
            this.buttonInsertOrUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInsertOrUpdate.AutoSize = true;
            this.buttonInsertOrUpdate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonInsertOrUpdate.Location = new System.Drawing.Point(202, 189);
            this.buttonInsertOrUpdate.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.buttonInsertOrUpdate.Name = "buttonInsertOrUpdate";
            this.buttonInsertOrUpdate.Size = new System.Drawing.Size(119, 23);
            this.buttonInsertOrUpdate.TabIndex = 11;
            this.buttonInsertOrUpdate.Text = "Добавить/Изменить";
            this.buttonInsertOrUpdate.UseVisualStyleBackColor = true;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(150, 20);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.textBoxName.MaxLength = 30;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.PlaceholderText = "Название уровня сложности";
            this.textBoxName.Size = new System.Drawing.Size(171, 21);
            this.textBoxName.TabIndex = 9;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelName.Location = new System.Drawing.Point(89, 23);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(55, 13);
            this.labelName.TabIndex = 6;
            this.labelName.Text = "Название";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(13, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Количество фрагментов";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(81, 158);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 20, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Тип сборки";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Location = new System.Drawing.Point(58, 76);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "по горизонтали";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(55, 131);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 20, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Тип фрагментов";
            // 
            // labelVertical
            // 
            this.labelVertical.AutoSize = true;
            this.labelVertical.Location = new System.Drawing.Point(150, 104);
            this.labelVertical.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.labelVertical.Name = "labelVertical";
            this.labelVertical.Size = new System.Drawing.Size(35, 13);
            this.labelVertical.TabIndex = 15;
            this.labelVertical.Text = "labelV";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label5.Location = new System.Drawing.Point(68, 104);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "по вертикали";
            // 
            // comboBoxFragmentType
            // 
            this.comboBoxFragmentType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFragmentType.DisplayMember = "Name";
            this.comboBoxFragmentType.FormattingEnabled = true;
            this.comboBoxFragmentType.Location = new System.Drawing.Point(150, 128);
            this.comboBoxFragmentType.Name = "comboBoxFragmentType";
            this.comboBoxFragmentType.Size = new System.Drawing.Size(171, 21);
            this.comboBoxFragmentType.TabIndex = 18;
            this.comboBoxFragmentType.ValueMember = "FragmentTypeId";
            // 
            // comboBoxAssemblyType
            // 
            this.comboBoxAssemblyType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAssemblyType.DisplayMember = "Name";
            this.comboBoxAssemblyType.FormattingEnabled = true;
            this.comboBoxAssemblyType.Location = new System.Drawing.Point(150, 155);
            this.comboBoxAssemblyType.Name = "comboBoxAssemblyType";
            this.comboBoxAssemblyType.Size = new System.Drawing.Size(171, 21);
            this.comboBoxAssemblyType.TabIndex = 19;
            this.comboBoxAssemblyType.ValueMember = "AssemblyTypeId";
            // 
            // numericUpDownHorizontal
            // 
            this.numericUpDownHorizontal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownHorizontal.Location = new System.Drawing.Point(150, 74);
            this.numericUpDownHorizontal.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownHorizontal.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownHorizontal.Name = "numericUpDownHorizontal";
            this.numericUpDownHorizontal.Size = new System.Drawing.Size(171, 21);
            this.numericUpDownHorizontal.TabIndex = 20;
            this.numericUpDownHorizontal.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // InsertOrUpdateLevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(334, 225);
            this.Controls.Add(this.numericUpDownHorizontal);
            this.Controls.Add(this.comboBoxAssemblyType);
            this.Controls.Add(this.comboBoxFragmentType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.labelVertical);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonInsertOrUpdate);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertOrUpdateLevelForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление/Изменение уровня сложности";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHorizontal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label labelVertical;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button buttonInsertOrUpdate;
        public System.Windows.Forms.TextBox textBoxName;
        public System.Windows.Forms.ComboBox comboBoxFragmentType;
        public System.Windows.Forms.ComboBox comboBoxAssemblyType;
        public System.Windows.Forms.NumericUpDown numericUpDownHorizontal;
    }
}