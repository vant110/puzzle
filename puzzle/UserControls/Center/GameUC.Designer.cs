
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
            this.pictureBoxTape = new System.Windows.Forms.PictureBox();
            this.panelTape = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTape)).BeginInit();
            this.panelTape.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxField
            // 
            this.pictureBoxField.BackColor = System.Drawing.Color.White;
            this.pictureBoxField.Location = new System.Drawing.Point(13, 13);
            this.pictureBoxField.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxField.Name = "pictureBoxField";
            this.pictureBoxField.Size = new System.Drawing.Size(549, 412);
            this.pictureBoxField.TabIndex = 0;
            this.pictureBoxField.TabStop = false;
            // 
            // pictureBoxTape
            // 
            this.pictureBoxTape.BackColor = System.Drawing.Color.White;
            this.pictureBoxTape.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTape.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxTape.Name = "pictureBoxTape";
            this.pictureBoxTape.Size = new System.Drawing.Size(183, 500);
            this.pictureBoxTape.TabIndex = 1;
            this.pictureBoxTape.TabStop = false;
            // 
            // panelTape
            // 
            this.panelTape.AutoScroll = true;
            this.panelTape.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelTape.Controls.Add(this.pictureBoxTape);
            this.panelTape.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTape.Location = new System.Drawing.Point(574, 13);
            this.panelTape.Margin = new System.Windows.Forms.Padding(0);
            this.panelTape.Name = "panelTape";
            this.panelTape.Size = new System.Drawing.Size(197, 474);
            this.panelTape.TabIndex = 2;
            this.panelTape.Visible = false;
            // 
            // GameUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelTape);
            this.Controls.Add(this.pictureBoxField);
            this.Name = "GameUC";
            this.Padding = new System.Windows.Forms.Padding(13);
            this.Size = new System.Drawing.Size(784, 500);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTape)).EndInit();
            this.panelTape.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBoxField;
        public System.Windows.Forms.PictureBox pictureBoxTape;
        public System.Windows.Forms.Panel panelTape;
    }
}
