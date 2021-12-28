using Microsoft.EntityFrameworkCore;
using puzzle.Model;
using puzzle.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace puzzle.UserControls
{
    public partial class RegAndAuthUC : UserControl
    {
        public RegAndAuthUC(MainForm form)
        {
            InitializeComponent();

            buttonRegister.Click += new EventHandler((s, e) =>
            {
                try
                {
                    string login = textBoxLogin.Text;
                    string password = textBoxPassword.Text;
                    if (!Validator.IsLogin(login))
                    {
                        throw new Exception("Логин некорректен.");
                    }
                    if (!Validator.IsPassword(password))
                    {
                        throw new Exception("Пароль некорректен.");
                    }

                    string passwordHash = Hasher.HashPassword(password, login);

                    var p1 = new MySqlConnector.MySqlParameter("@p1", login);
                    var p2 = new MySqlConnector.MySqlParameter("@p2", passwordHash);
                    int rowsAffected;
                    using (var db = new PuzzleContext(Db.Options))
                    {
                        rowsAffected = db.Database.ExecuteSqlRaw("CALL `register_player` (@p1, @p2)", p1, p2);
                    }
                    if (rowsAffected != 1)
                    {
                        // Введен логин администратора.
                        throw new Exception("Логин занят.");
                    }
                    MessageBoxes.Info("Успешно.");
                }
                catch (MySqlConnector.MySqlException ex) when (ex.Number == 1062)
                {
                    MessageBoxes.Error("Логин занят.");
                }
                catch (Exception ex)
                {
                    MessageBoxes.Error(ex.Message);
                }
            });
            buttonAuthorize.Click += new EventHandler((s, e) =>
            {
                try
                {
                    string login = textBoxLogin.Text;
                    string password = textBoxPassword.Text;
                    if (!Validator.IsLogin(login))
                    {
                        throw new Exception("Логин некорректен.");
                    }
                    if (!Validator.IsPassword(password))
                    {
                        throw new Exception("Пароль некорректен.");
                    }

                    string passwordHash = Hasher.HashPassword(password, login);

                    var p1 = new MySqlConnector.MySqlParameter("@p1", login);
                    var p2 = new MySqlConnector.MySqlParameter("@p2", passwordHash);
                    using (var db = new PuzzleContext(Db.Options))
                    {
                        ResultDTO.PlayerId = (short)db.Results.FromSqlRaw("SELECT `authorize` (@p1, @p2) AS `Value`", p1, p2).Single().Value;
                        if (ResultDTO.PlayerId == -1)
                        {
                            throw new Exception("Неверный логин или пароль.");
                        }
                    }

                    if (ResultDTO.PlayerId == 0)
                    {
                        // Админ.
                        form.DisplayAdminMenu();
                    }
                    else
                    {
                        // Игрок.
                        form.games = Db.LoadGames();
                        form.DisplayGameChoice();
                    }
                }
                catch (Exception ex)
                {
                    MessageBoxes.Error(ex.Message);
                }
            });
        }
    }
}
