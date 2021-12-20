using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using puzzle.Model;
using puzzle.Services;

namespace puzzle.Dialogs
{
    public partial class InsertImageForm : Form
    {
        public InsertImageForm()
        {
            InitializeComponent();

            buttonFile.Click += new EventHandler((s, e) =>
            {
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

                    Hasher.HashImage(NewImage.Image);

                    var p1 = new MySqlConnector.MySqlParameter("@login", User.Login);
                    var p2 = new MySqlConnector.MySqlParameter("@password_hash", User.PasswordHash);
                    int rowsAffected;
                    using (var db = new PuzzleContext(Settings.Options))
                    {
                        rowsAffected = db.Database.ExecuteSqlRaw("CALL `register_player` (@login, @password_hash)", p1, p2);
                    }
                    if (rowsAffected == 1)
                    {
                        MessageBoxes.Info("Регистрация прошла успешно.");
                    }
                    else
                    {
                        // Введен логин администратора.
                        MessageBoxes.Error("Логин занят.");
                    }
                }
                catch (MySqlConnector.MySqlException ex)// when (ex.Number == 1062)
                {
                    MessageBoxes.Error(ex.Message);
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
