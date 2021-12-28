namespace puzzle
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.bindingSourceLevels = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceGallery = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourcePuzzles = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceFragmentTypes = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceAssemblyTypes = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceMethods = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceFilteredPuzzles = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceFilteredLevels = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceLevels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceGallery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePuzzles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFragmentTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAssemblyTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMethods)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFilteredPuzzles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFilteredLevels)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCenter
            // 
            this.panelCenter.BackColor = System.Drawing.SystemColors.Control;
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 0);
            this.panelCenter.Margin = new System.Windows.Forms.Padding(0);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(784, 565);
            this.panelCenter.TabIndex = 0;
            // 
            // panelBottom
            // 
            this.panelBottom.AutoSize = true;
            this.panelBottom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelBottom.BackColor = System.Drawing.Color.Red;
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 565);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(0);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(784, 0);
            this.panelBottom.TabIndex = 0;
            // 
            // panelTop
            // 
            this.panelTop.AutoSize = true;
            this.panelTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelTop.BackColor = System.Drawing.Color.Lime;
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(784, 0);
            this.panelTop.TabIndex = 0;
            // 
            // bindingSourcePuzzles
            // 
            this.bindingSourcePuzzles.Filter = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(784, 565);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Игра \"Puzzle\"";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceLevels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceGallery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePuzzles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFragmentTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceAssemblyTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMethods)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFilteredPuzzles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFilteredLevels)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelTop;
        public System.Windows.Forms.BindingSource bindingSourceGallery;
        public System.Windows.Forms.BindingSource bindingSourcePuzzles;
        public System.Windows.Forms.BindingSource bindingSourceLevels;
        public System.Windows.Forms.BindingSource bindingSourceFragmentTypes;
        public System.Windows.Forms.BindingSource bindingSourceAssemblyTypes;
        public System.Windows.Forms.BindingSource bindingSourceMethods;
        private System.Windows.Forms.BindingSource bindingSourceFilteredPuzzles;
        private System.Windows.Forms.BindingSource bindingSourceFilteredLevels;
    }
}

