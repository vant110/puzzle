using puzzle.Model;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace puzzle.Services
{
    public partial class RegAndAuth : UserControl
    {
        public RegAndAuth()
        {
            InitializeComponent();

            #region Логин
            textBoxLogin.Validating += new CancelEventHandler((s, e) =>
            {
                string input = textBoxLogin.Text;
                if (Validator.IsLogin(input))
                {
                    User.Login = input;
                }
                else
                {
                    e.Cancel = true;
                }
            });
            #endregion
            #region Пароль
            textBoxPassword.Validating += new CancelEventHandler((s, e) =>
            {
                string input = textBoxPassword.Text;
                if (Validator.IsLogin(input))
                {
                    User.Password = input;
                }
                else
                {
                    e.Cancel = true;
                }
            });
            #endregion
            #region Войти
            buttonAuthorize.Click += new EventHandler((s, e) =>
            {
                if (Validator.IsLogin(User.Login)
                && Validator.IsPassword(User.Password))
                {
                    Hasher.HashPassword(User.Password);

                }
            });
            #endregion
        }
    }
}
