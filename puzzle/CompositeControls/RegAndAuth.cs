using Microsoft.EntityFrameworkCore;
using puzzle.Model;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace puzzle.Services
{
    public partial class RegAndAuth : UserControl
    {
        public RegAndAuth()
        {
            InitializeComponent();

            buttonRegister.Click += new EventHandler((s, e) =>
            {
                User.Login = textBoxLogin.Text;
                User.Password = textBoxPassword.Text;
                if (!Validator.IsLogin(User.Login))
                {
                    MessageBoxes.Error("Логин некорректен.");
                    return;
                }
                if (!Validator.IsPassword(User.Password))
                {
                    MessageBoxes.Error("Пароль некорректен.");
                    return;
                }
                
                Hasher.HashPassword(User.Password);                    
                try
                {
                    int rowsAffected;
                    using (var db = new PuzzleContext(Settings.Options))
                    {
                        rowsAffected = db.Database.ExecuteSqlInterpolated($"CALL register_player ({User.Login}, {User.PasswordHash})");
                    }
                    if (rowsAffected == 0)
                    {
                        // Введен логин администратора.
                        MessageBoxes.Error("Логин занят.");
                    }
                }
                catch (MySqlConnector.MySqlException ex) when (ex.Number == 1062)
                {
                    // Логин UNIQUE.
                    MessageBoxes.Error("Логин занят.");
                }
            });
            buttonAuthorize.Click += new EventHandler((s, e) =>
            {
                User.Login = textBoxLogin.Text;
                User.Password = textBoxPassword.Text;
                if (!Validator.IsLogin(User.Login))
                {
                    MessageBoxes.Error("Логин некорректен."); 
                    return;
                }
                if (!Validator.IsPassword(User.Password))
                {
                    MessageBoxes.Error("Пароль некорректен.");
                    return;
                }
                
                Hasher.HashPassword(User.Password);
                using (var db = new PuzzleContext(Settings.Options))
                {
                    MySqlConnector.MySqlParameter param1 = new("@param1", User.Login);
                    MySqlConnector.MySqlParameter param2 = new("@param2", User.PasswordHash);
                    var result = db.Players.FromSqlRaw("CALL authorize_player (@param1, @param2)", param1, param2);      
                    foreach (var i in result)
                    {
                        Debug.WriteLine(i);
                    }
                }
                         
            });
        }
    }
}
