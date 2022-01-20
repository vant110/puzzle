using puzzle.Services;
using System;
using System.Diagnostics;
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
                string[] help = new string[14];
                help[0] = $@"{Application.StartupPath}help\help.html";
                for (int i = 1; i <= 13; i++)
                {
                    help[i] = $@"{Application.StartupPath}help\images\{i}.jpg";
                }

                try
                {                    
                    if (!LocalStorage.HelpExists(help))
                    {
                        throw new Exception("Справка не найдена.");
                    }
                    if (Hasher.Hash(LocalStorage.LoadHelp(help)) != "ctRt50GMt7MSQVWVPkbmCeLKQAPD7ByYFUZW8gNuO4GcHJB8d5BT/OFCXUA/EgYyRH5APs3Apk5RCaEDDW15Jg==")
                    {
                        throw new Exception("Справка повреждена.");
                    }

                    Process.Start(new ProcessStartInfo(help[0]) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBoxes.Error(ex.Message);
                }
            });
        }
    }
}
