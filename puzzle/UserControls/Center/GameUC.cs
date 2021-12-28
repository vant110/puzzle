using puzzle.Model;
using puzzle.Services;
using puzzle.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puzzle.UserControls.Center
{
    public partial class GameUC : UserControl
    {
        private Game game;
        private MainForm form;


        internal GameUC(Game game, MainForm form)
        {
            InitializeComponent();
            this.game = game;
            this.form = form;

            DrawField();

            Point location1 = new(0, 0);

            pictureBoxField.AllowDrop = true;
            pictureBoxField.MouseDown += new MouseEventHandler((s, e) =>
            {
                location1 = e.Location;
                if (game.FragmentInOriginalPosition(
                    game.GetFragmentIndex(
                        location1, 
                        pictureBoxField.Size)))
                {
                    return;
                }

                pictureBoxField.DoDragDrop(
                    new object(),
                    DragDropEffects.Move);
            });

            pictureBoxField.DragEnter += new DragEventHandler((s, e) => 
            {
                e.Effect = DragDropEffects.Move;
            });

            pictureBoxField.DragDrop += new DragEventHandler((s, e) =>
            {
                var index1 = game.GetFragmentIndex(
                    location1, 
                    pictureBoxField.Size);
                var index2 = game.GetFragmentIndex(
                    PointToClient(new Point(e.X, e.Y)), 
                    pictureBoxField.Size);

                if (index1 == index2
                    || game.Field[index2].InOriginalPosition) return;   

                game.SwapFragments(index1, index2);
                game.DrawFragment(index1, pictureBoxField.Image);
                game.DrawFragment(index2, pictureBoxField.Image);
                pictureBoxField.Refresh();

                if (game.CountingMethodId == 1) 
                {
                    if (game.Field[index1].InOriginalPosition)
                    {
                        game.Score++;
                    }
                    if (game.Field[index2].InOriginalPosition)
                    {
                        game.Score++;
                    }
                    if (!game.Field[index1].InOriginalPosition
                        && !game.Field[index2].InOriginalPosition)
                    {
                        game.Score--;
                    }
                    form.topControl.labelValue.Text = game.Score.ToString();
                }

                if (game.Field[index1].InOriginalPosition
                    || game.Field[index2].InOriginalPosition)
                {
                    if (form.soundOn)
                    {
                        form.soundPlayer?.Play();
                    }

                    if (game.AllFragmentsInOriginalPosition())
                    {
                        form.topControl.timer.Stop();

                        var message = "Пазл собран.";
                        if (game.CountingMethodId == 1)
                        {
                            message += $" Очки: {game.Score}";
                        }
                        else if (game.CountingMethodId == 2)
                        {
                            message += $" Время: {form.topControl.labelValue.Text}";
                        }
                        MessageBoxes.Info(message);

                        form.DisplayGameChoice();
                    }
                }            
            });

            if (game.CountingMethodId == 1)
            {
                form.topControl.labelValue.Text = game.Score.ToString();
            }
            else if (game.CountingMethodId == 2)
            {
                form.topControl.timer.Tick += new EventHandler((s, e) =>
                {
                    game.Time++;
                    DisplayTime();
                });
                form.topControl.timer.Start();

                DisplayTime();
            }
        }

        private void DrawField()
        {
            var bitmap = new Bitmap(pictureBoxField.Width, pictureBoxField.Height);
            game.DrawField(bitmap);
            pictureBoxField.Image = bitmap;
        }

        private void DisplayTime()
        {
            int h = game.Time / 3600;
            int m = game.Time / 60 % 60;
            int sec = game.Time % 60;
            string hStr = h.ToString();
            string mStr = m.ToString();
            string secStr = sec.ToString();
            if (h < 10)
            {
                hStr = '0' + hStr;
            }
            if (m < 10)
            {
                mStr = '0' + mStr;
            }
            if (sec < 10)
            {
                secStr = '0' + secStr;
            }
            form.topControl.labelValue.Text = $"{hStr}:{mStr}:{secStr}";
        }
    }
}
