using puzzle.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puzzle.Dialogs
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();

            linkLabelAbout.Click += new EventHandler((s, e) =>
            {
                try
                {
                    System.Diagnostics.Process.Start("");
                }
                catch
                {
                    MessageBoxes.Error("Не удалось открыть файл справки.");
                }
            });
        }
    }
}
