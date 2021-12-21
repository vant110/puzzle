using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using puzzle.CompositeControls;
using puzzle.Data;
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
            Db.Options = optionsBuilder
                .UseMySql(connectionString, ServerVersion.Parse("8.0.27-mysql"))
                .Options;
            #endregion
            optionsBuilder.LogTo(message => Debug.WriteLine(message));

            Db.Instance = new PuzzleContext(Db.Options);
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

        private void DisposeImage(ImageAndMethodsControl right)
        {
            if (right.pictureBoxImage.Image == null) return;
            right.pictureBoxImage.Image.Dispose();
            right.pictureBoxImage.Image = null;
        }

        public void ConfigureOnRegAndAuth()
        {
            var fill = new RegAndAuthControl(this)
            {
                Dock = DockStyle.Fill
            };
            Size = smallSize;
            CenterToScreen();
            ChangeFill(fill);
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
            var fill = new AdminMenuControl(this)
            {
                Dock = DockStyle.Fill
            };
            Size = smallSize;
            CenterToScreen();
            ChangeFill(fill);
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
            Db.LoadGalleries();

            Size = normalSize;
            CenterToScreen();
            #region Right
            var right = new ImageAndMethodsControl()
            {
                Dock = DockStyle.Right,
                PanelMethodsVisible = false
            };
            ChangeRight(right);
            #endregion
            #region Fill
            var fill = new CompositeControls.ListControl()
            {
                Dock = DockStyle.Fill,
            };
            ChangeFill(fill);

            fill.comboBoxLevel.Visible = false;
            fill.listBox.DataSource = Db.Instance.Galleries.Local.ToBindingList();
            fill.listBox.DisplayMember = "Name";
            fill.listBox.ValueMember = "ImageId";
            fill.listBox.ClearSelected();
            fill.listBox.SelectedValueChanged += new EventHandler((s, e) =>
            {
                DisposeImage(right);
                if (fill.listBox.SelectedItems.Count == 0) return;
                string path = ((Gallery)fill.listBox.SelectedItem).Path;
                right.pictureBoxImage.Image = LocalStorage.LoadImage(path);
            });
            #endregion
            #region Top
            topControl.ButtonBackVisible = true;
            topControl.ButtonBackClick = new EventHandler((s, e) =>
            {
                ConfigureOnAdminMenu();
            });
            topControl.ButtonPauseOrPlayVisible = false;
            topControl.ButtonImageOrPuzzleVisible = false;
            topControl.ButtonSoundVisible = false;
            topControl.LabelTitleText = "Галерея";
            #endregion
            #region Bottom
            panelBottom.Show();
            bottomControl.ButtonDeleteClick = new EventHandler((s, e) =>
            {
                if (fill.listBox.SelectedItems.Count == 0) return;
                try { 
                    var p1 = new MySqlConnector.MySqlParameter("@id", fill.listBox.SelectedValue);
                    int rowsAffected = Db.Instance.Database.ExecuteSqlRaw("CALL `delete_image` (@id)", p1);
                    if (rowsAffected != 1)
                    {
                        throw new Exception("Ошибка.");
                    }
                    DisposeImage(right);
                    LocalStorage.Delete(((Gallery)fill.listBox.SelectedItem).Path);
                    Db.LoadGalleries();
                    fill.listBox.ClearSelected();
                    DisposeImage(right);
                    MessageBoxes.Info("Успешно.");
                }
                catch (Exception ex)
                {
                    MessageBoxes.Error(ex.Message);
                }
            });
            bottomControl.ButtonUpdateVisible = false;
            bottomControl.ButtonLoadVisible = false;
            bottomControl.ButtonInsertOrNewGameText = "Добавить";
            bottomControl.ButtonInsertOrNewGameClick = new EventHandler((s, e) =>
            {
                new InsertImageForm().ShowDialog();
                fill.listBox.ClearSelected();
                DisposeImage(right);
            });
            #endregion
        }
    }
}
