using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using puzzle.Services;
using puzzle.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace puzzle
{
    public partial class MainForm : Form
    {
        private TopControl topControl;
        private Control centerControl;
        private BottomControl bottomControl;
        
        public MainForm()
        {
            InitializeComponent();
                        
            CreateTopControl();
            panelBottom.Hide();
            CreateRegAndAuth();

            #region Инициализация options
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");
            
            var optionsBuilder = new DbContextOptionsBuilder<PuzzleContext>();
            Settings.Options = optionsBuilder
                .UseMySql(connectionString, ServerVersion.Parse("8.0.27-mysql"))
                .Options;
            #endregion
            optionsBuilder.LogTo(message => Debug.WriteLine(message));            
        }

        private void CreateTopControl()
        {
            topControl = new TopControl
            {
                Dock = DockStyle.Top
            };
            panelTop.Controls.Add(topControl);
        }

        private void CreateBottomControl()
        {
            bottomControl = new BottomControl
            {
                Dock = DockStyle.Bottom
            };
            panelBottom.Controls.Add(bottomControl);
        }

        private void ChangeCenterControl(Control newControl)
        {
            panelCenter.Controls.Remove(centerControl);
            centerControl = newControl;
            panelCenter.Controls.Add(centerControl);
        }

        private void CreateRegAndAuth()
        {
            var newControl = new RegAndAuth()
            {
                Dock = DockStyle.Fill
            };
            Size = new Size(newControl.Width + 125, newControl.Height + topControl.Height + 100);
            ChangeCenterControl(newControl);

            topControl.ButtonBackVisible = false;
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;

            topControl.LabelTitleText = "Регистрация/авторизация";
        }
    }
}
