using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using puzzle.CompositeControls;
using puzzle.Dialogs;
using puzzle.Services;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace puzzle
{
    public partial class MainForm : Form
    {
        private readonly Size smallSize = new(313, 243);
        private readonly Size normalSize = new(800, 600);

        private readonly TopControl topControl;
        private readonly BottomControl bottomControl;
        private Control fillControl;
        private Control rightControl;

        public MainForm()
        {
            InitializeComponent();

            #region Top
            topControl = new TopControl
            {
                Dock = DockStyle.Top
            };
            panelTop.Controls.Add(topControl);
            #endregion
            #region Bottom
            bottomControl = new BottomControl
            {
                Dock = DockStyle.Bottom
            };
            panelBottom.Controls.Add(bottomControl);
            #endregion
            ConfigureOnRegAndAuth();

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

        private void ChangeFill(Control newFillControl)
        {
            panelCenter.Controls.Remove(fillControl);
            fillControl = newFillControl;
            panelCenter.Controls.Add(fillControl);
        }

        private void ChangeRight(Control newRightControl)
        {
            panelCenter.Controls.Remove(rightControl);
            rightControl = newRightControl;
            panelCenter.Controls.Add(rightControl);
        }

        public void ConfigureOnRegAndAuth()
        {
            var newFillControl = new RegAndAuthControl(this)
            {
                Dock = DockStyle.Fill
            };
            Size = smallSize;
            CenterToScreen();
            ChangeFill(newFillControl);
            ChangeRight(null);

            topControl.ButtonBackVisible = false;
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;
            topControl.LabelTitleText = "Регистрация/авторизация";

            panelBottom.Hide();
        }

        public void ConfigureOnAdminMenu()
        {
            var newFillControl = new AdminMenuControl(this)
            {
                Dock = DockStyle.Fill
            };
            Size = smallSize;
            CenterToScreen();
            ChangeFill(newFillControl);
            ChangeRight(null);

            topControl.ButtonBackVisible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                ConfigureOnRegAndAuth();
            });
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;
            topControl.LabelTitleText = "Меню администратора";

            panelBottom.Hide();
        }

        public void ConfigureOnGallery()
        {
            Size = normalSize;
            CenterToScreen();
            var newFillControl = new CompositeControls.ListControl()
            {
                Dock = DockStyle.Fill,
                ComboBoxLevelVisible = false
            };
            ChangeFill(newFillControl);

            var newRightControl = new ImageAndMethodsControl()
            {
                Dock = DockStyle.Right,
                PanelMethodsVisible = false
            };
            ChangeRight(newRightControl);

            topControl.ButtonBackVisible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                ConfigureOnAdminMenu();
            });
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;
            topControl.LabelTitleText = "Галерея";

            panelBottom.Show();
            bottomControl.ButtonDeleteClick = new EventHandler((s, e) =>
            {
                //!!!
            });
            bottomControl.ButtonUpdateVisible = false;
            bottomControl.ButtonLoadVisible = false;
            bottomControl.ButtonInsertOrNewGameText = "Добавить";
            bottomControl.ButtonInsertOrNewGameClick = new EventHandler((s, e) =>
            {
                new InsertImageForm().ShowDialog();
            });
        }
    }
}
