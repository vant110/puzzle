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
                if (NewImage.Image != null)
                {
                    NewImage.Image.Close();
                    NewImage.Path = null;
                    labelFile.Text = "Название файла";
                }
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Изображения (*.png)|*.png";
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        NewImage.Image = openFileDialog.OpenFile();
                        NewImage.Path = openFileDialog.SafeFileName;
                        labelFile.Text = NewImage.Path;
                    }
                }
            });
            buttonInsert.Click += new EventHandler((s, e) =>
            {
                Application.UseWaitCursor = true;
                try
                {
                    NewImage.Name = textBoxName.Text;
                    if (!Validator.IsImageName(NewImage.Name))
                    {
                        throw new Exception("Название изображения некорректно.");
                    }
                    if (NewImage.Image == null)
                    {
                        throw new Exception("Выберите файл изображения.");
                    }

                    Hasher.HashImage(NewImage.Image);
                    var p1 = new MySqlConnector.MySqlParameter("@name", NewImage.Name);
                    var p2 = new MySqlConnector.MySqlParameter("@path", NewImage.Path);
                    var p3 = new MySqlConnector.MySqlParameter("@image_hash", NewImage.Hash);
                    int rowsAffected;
                    using (var db = new PuzzleContext(Settings.Options))
                    {
                        rowsAffected = db.Database.ExecuteSqlRaw("CALL `insert_image` (@name, @path, @image_hash)", p1, p2, p3);
                    }
                    if (rowsAffected != 1)
                    {
                        throw new Exception("Ошибка.");
                    }
                    LocalStorage.SaveNewImage();
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
                Application.UseWaitCursor = false;
            });
        }
    }
}
