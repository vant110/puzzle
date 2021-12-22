using Microsoft.EntityFrameworkCore;
using puzzle.DTO;
using puzzle.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace puzzle.UserControls
{
    public partial class RegAndAuthControl : UserControl
    {
        public RegAndAuthControl(MainForm form)
        {
            InitializeComponent();

            buttonRegister.Click += new EventHandler((s, e) =>
            {
                Application.UseWaitCursor = true;
                try
                {
                    Init();
                    var p1 = new MySqlConnector.MySqlParameter("@login", UserDTO.Login);
                    var p2 = new MySqlConnector.MySqlParameter("@password_hash", UserDTO.PasswordHash);
                    int rowsAffected;
                    using (var db = new PuzzleContext(Db.Options))
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
                catch (InvalidOperationException ex)
                {
                    MessageBoxes.Error(ex.Message);
                }
                catch (MySqlConnector.MySqlException ex) when (ex.Number == 1062)
                {
                    // Логин UNIQUE.
                    MessageBoxes.Error("Логин занят.");
                }
                Application.UseWaitCursor = false;
            });
            buttonAuthorize.Click += new EventHandler((s, e) =>
            {
                Application.UseWaitCursor = true;
                try
                {
                    Init();
                    var p1 = new MySqlConnector.MySqlParameter("@login", UserDTO.Login);
                    var p2 = new MySqlConnector.MySqlParameter("@password_hash", UserDTO.PasswordHash);
                    int result;
                    using (var db = new PuzzleContext(Db.Options))
                    {
                        result = db.Results.FromSqlRaw("SELECT `authorize` (@login, @password_hash) AS `Value`", p1, p2).Single().Value;
                    }
                    if (result == -1)
                    {
                        MessageBoxes.Error("Неверный логин или пароль.");
                    }
                    else if (result == 0)
                    {
                        // Админ.
                        form.ConfigureOnAdminMenu();
                    }
                    else
                    {
                        // Игрок.
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBoxes.Error(ex.Message);
                }
                Application.UseWaitCursor = false;
            });
        }

        private void Init()
        {
            UserDTO.Login = textBoxLogin.Text;
            UserDTO.Password = textBoxPassword.Text;
            if (!Validator.IsLogin(UserDTO.Login))
            {
                throw new InvalidOperationException("Логин некорректен.");
            }
            if (!Validator.IsPassword(UserDTO.Password))
            {
                throw new InvalidOperationException("Пароль некорректен.");
            }

            UserDTO.PasswordHash = Hasher.HashPassword(UserDTO.Password);
        }
    }
}
