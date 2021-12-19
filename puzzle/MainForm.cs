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
        private Control fillControl;
        private BottomControl bottomControl;
        
        public MainForm()
        {
            InitializeComponent();
            
            CreateTopControl();
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
            optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Trace);
            //string s = BCrypt.Net.BCrypt.HashPassword("admin");
            //Debug.WriteLine("");
            
        }

        private void CreateTopControl()
        {
            topControl = new TopControl
            {
                Dock = DockStyle.Top
            };
            Controls.Add(topControl);
        }

        private void CreateBottomControl()
        {
            bottomControl = new BottomControl
            {
                Dock = DockStyle.Bottom
            };
            Controls.Add(bottomControl);
        }

        private void ChangeFillControl(Control newControl)
        {
            Controls.Remove(fillControl);
            fillControl = newControl;
            Controls.Add(fillControl);
        }

        private void CreateRegAndAuth()
        {
            var newControl = new RegAndAuth
            {
                Dock = DockStyle.Fill
            };
            ChangeFillControl(newControl);

            topControl.ButtonBackVisible = false;
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;

            topControl.LabelTitleText = "Регистрация/авторизация";
        }
    }
}
