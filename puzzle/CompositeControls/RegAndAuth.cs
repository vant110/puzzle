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
            /*
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
                    var p1 = new MySqlConnector.MySqlParameter("@login", User.Login);
                    var p2 = new MySqlConnector.MySqlParameter("@password_hash", User.PasswordHash);
                    var results = db.Rs.From("CALL authorize_player (@login, @password_hash)", p1, p2).ToList();/*
                    foreach (var r in results)
                    {
                        Debug.WriteLine(r.RId);
                    }
                }                         
            });*/
        }
    }
}
