using Microsoft.EntityFrameworkCore;
using puzzle.Model;
using puzzle.Services;
using System;
using System.Windows.Forms;

namespace puzzle.Dialogs
{
    public partial class InsertImageForm : Form
    {
        public InsertImageForm()
        {
            InitializeComponent();

            buttonFile.Click += new EventHandler((s, e) =>
            {
                if (ImageDTO.Image != null)
                {
                    ImageDTO.Image.Close();
                    ImageDTO.Path = null;
                    labelFile.Text = "Название файла";
                }
                using var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Изображения (*.png)|*.png";
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImageDTO.Image = openFileDialog.OpenFile();
                    ImageDTO.Path = openFileDialog.SafeFileName;
                    labelFile.Text = ImageDTO.Path;
                }
            });
            buttonInsert.Click += new EventHandler((s, e) =>
            {
                try
                {
                    ImageDTO.Name = textBoxName.Text;
                    if (!Validator.IsImageName(ImageDTO.Name))
                    {
                        throw new Exception("Название изображения некорректно.");
                    }
                    if (ImageDTO.Image == null)
                    {
                        throw new Exception("Выберите файл изображения.");
                    }

                    ImageDTO.Image = LocalStorage.Resize(ImageDTO.Image);
                    ImageDTO.Hash = Hasher.HashImage(ImageDTO.Image, false);
                    var p1 = new MySqlConnector.MySqlParameter("@name", ImageDTO.Name);
                    var p2 = new MySqlConnector.MySqlParameter("@path", ImageDTO.Path);
                    var p3 = new MySqlConnector.MySqlParameter("@image_hash", ImageDTO.Hash);
                    int rowsAffected = Db.Instance.Database.ExecuteSqlRaw("CALL `insert_image` (@name, @path, @image_hash)", p1, p2, p3);
                    if (rowsAffected != 1)
                    {
                        throw new Exception("Ошибка.");
                    }
                    LocalStorage.Save(ImageDTO.Image, ImageDTO.Path);
                    MessageBoxes.Info("Успешно.");
                }
                catch (MySqlConnector.MySqlException ex) when (ex.Number == 1062)
                {
                    if (ex.Message.Contains("'gallery.name'"))
                    {
                        MessageBoxes.Error("Название изображения занято.");
                    }
                    else if (ex.Message.Contains("'gallery.path'"))
                    {
                        MessageBoxes.Error("Название файла занято.");
                    }
                    else
                    {
                        MessageBoxes.Error(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxes.Error(ex.Message);
                }
            });

            FormClosing += new FormClosingEventHandler((s, e) =>
            {
                if (ImageDTO.Image != null) {
                    ImageDTO.Image.Close();
                }
            });
        }
    }
}
