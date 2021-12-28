
namespace puzzle.UserControls.Center
{
    partial class listWithPictogramsUC
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(listWithPictogramsUC));
            this.panelLevel = new System.Windows.Forms.Panel();
            this.comboBoxLevels = new System.Windows.Forms.ComboBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.PuzzleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SavedGamePictogram = new System.Windows.Forms.DataGridViewImageColumn();
            this.panelLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
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
            this.panelLevel.TabIndex = 3;
            // 
            // comboBoxLevels
            // 
            this.comboBoxLevels.DisplayMember = "Name";
            this.comboBoxLevels.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBoxLevels.Location = new System.Drawing.Point(0, 0);
            this.comboBoxLevels.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxLevels.Name = "comboBoxLevels";
            this.comboBoxLevels.Size = new System.Drawing.Size(374, 21);
            this.comboBoxLevels.Sorted = true;
            this.comboBoxLevels.TabIndex = 0;
            this.comboBoxLevels.ValueMember = "Id";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.ColumnHeadersVisible = false;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PuzzleName,
            this.SavedGamePictogram});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 3, 3, 3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.GridColor = System.Drawing.Color.White;
            this.dataGridView.Location = new System.Drawing.Point(13, 57);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 25;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(374, 420);
            this.dataGridView.TabIndex = 4;
            // 
            // PuzzleName
            // 
            this.PuzzleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PuzzleName.DataPropertyName = "Name";
            this.PuzzleName.HeaderText = "Название пазла";
            this.PuzzleName.MaxInputLength = 30;
            this.PuzzleName.Name = "PuzzleName";
            this.PuzzleName.ReadOnly = true;
            // 
            // SavedGamePictogram
            // 
            this.SavedGamePictogram.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle1.NullValue")));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Red;
            this.SavedGamePictogram.DefaultCellStyle = dataGridViewCellStyle1;
            this.SavedGamePictogram.HeaderText = "";
            this.SavedGamePictogram.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.SavedGamePictogram.Name = "SavedGamePictogram";
            this.SavedGamePictogram.ReadOnly = true;
            this.SavedGamePictogram.Width = 30;
            // 
            // listWithPictogramsUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panelLevel);
            this.Name = "listWithPictogramsUC";
            this.Padding = new System.Windows.Forms.Padding(13, 18, 13, 18);
            this.Size = new System.Drawing.Size(400, 495);
            this.panelLevel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelLevel;
        public System.Windows.Forms.ComboBox comboBoxLevels;
        public System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn PuzzleName;
        private System.Windows.Forms.DataGridViewImageColumn SavedGamePictogram;
    }
}
