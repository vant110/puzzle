using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using puzzle.Model;
using puzzle.Services;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace puzzle.UserControls.Center
{
    public partial class GameUC : UserControl
    {
        private readonly Game game;
        private readonly MainForm form;

        internal GameUC(PuzzleModel puzzle, Game game, MainForm form)
        {
            InitializeComponent();
            this.game = game;
            this.form = form;

            DrawField();
            if (game.AssemblyTypeId == 2)
            {
                DrawTape();
            }

            int source = 0; // 1 - поле, 2 - лента.
            Point location1 = new(0, 0);

            pictureBoxField.AllowDrop = true;
            pictureBoxField.MouseDown += new MouseEventHandler((s, e) =>
            {
                source = 1;
                location1 = e.Location;

                int index = game.Field.GetFragmentIndex(
                    location1,
                    pictureBoxField.Size);
                if (game.Field.Fragments[index] is null
                    || game.Field.FragmentInOriginalPosition(index)) return;

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
                int index1 = 0;
                if (source == 1)
                {
                    index1 = game.Field.GetFragmentIndex(
                        location1,
                        pictureBoxField.Size);
                }
                else if (source == 2)
                {
                    index1 = game.Tape.GetFragmentIndex(
                        location1,
                        pictureBoxTape.Size);
                }

                int index2 = game.Field.GetFragmentIndex(
                    PointToClient(new Point(e.X - pictureBoxField.Location.X, e.Y - pictureBoxField.Location.Y)),
                    pictureBoxField.Size);

                if (source == 1
                    && index1 == index2) return;
                if (game.Field.Fragments[index2] is not null
                    && game.Field.Fragments[index2].InOriginalPosition) return;

                if (source == 1)
                {
                    game.Field.SwapFragments(index1, index2);
                    game.Field.DrawFragment(index1, pictureBoxField.Image);
                }
                else if (source == 2)
                {
                    bool fieldFragmentIsNull = game.Field.Fragments[index2] is null;
                    game.AddFragmentOnFieldFromTape(index1, index2);
                    if (fieldFragmentIsNull)
                    {
                        DrawTape();
                    }
                    else
                    {
                        game.Tape.DrawFragment(index1, pictureBoxTape.Image);
                    }
                    pictureBoxTape.Refresh();
                }
                game.Field.DrawFragment(index2, pictureBoxField.Image);
                pictureBoxField.Refresh();


                if (game.CountingMethodId == 1)
                {
                    if (source == 1
                        && game.Field.Fragments[index1] is not null
                        && game.Field.Fragments[index1].InOriginalPosition)
                    {
                        game.Score++;
                    }
                    if (game.Field.Fragments[index2].InOriginalPosition)
                    {
                        game.Score++;
                    }
                    if (!((source == 1
                        && game.Field.Fragments[index1] is not null
                        && game.Field.Fragments[index1].InOriginalPosition)
                        || game.Field.Fragments[index2].InOriginalPosition))
                    {
                        game.Score--;
                    }
                    form.topControl.labelValue.Text = game.Score.ToString();
                }

                if ((source == 1
                    && game.Field.Fragments[index1] is not null
                    && game.Field.Fragments[index1].InOriginalPosition)
                    || game.Field.Fragments[index2].InOriginalPosition)
                {
                    if (form.soundOn)
                    {
                        form.soundPlayer?.Play();
                    }

                    if (game.Field.EachFragmentInOriginalPosition())
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

                        try
                        {
                            var p1 = new MySqlParameter("@p1", puzzle.Id);
                            var p2 = new MySqlParameter("@p2", game.CountingMethodId);
                            var p3 = new MySqlParameter("@p3", ResultDTO.PlayerId);
                            var p4 = new MySqlParameter("@p4", game.Score);
                            var p5 = new MySqlParameter("@p5", game.Time);
                            using (var db = new PuzzleContext(Db.Options))
                            {
                                db.Database.ExecuteSqlRaw("CALL `try_set_record` (@p1, @p2, @p3, @p4, @p5)", p1, p2, p3, p4, p5);
                            }
                            // ??? Установлен рекорд.
                        }
                        catch (Exception ex)
                        {
                            MessageBoxes.Error(ex.Message);
                        }

                        form.DisplayGameChoice();
                    }
                }
            });

            pictureBoxTape.AllowDrop = true;
            pictureBoxTape.MouseDown += new MouseEventHandler((s, e) =>
            {
                if (game.Tape.Fragments.Length == 0) return;

                source = 2;
                location1 = e.Location;

                pictureBoxTape.DoDragDrop(
                    new object(),
                    DragDropEffects.Move);
            });
            pictureBoxTape.DragEnter += new DragEventHandler((s, e) =>
            {
                if (source == 1)
                {
                    e.Effect = DragDropEffects.Move;
                }
            });
            pictureBoxTape.DragDrop += new DragEventHandler((s, e) =>
            {
                int index1 = game.Field.GetFragmentIndex(
                    location1,
                    pictureBoxField.Size);
                
                game.AddFragmentOnTape(index1);
                game.Field.DrawFragment(index1, pictureBoxField.Image);
                pictureBoxField.Refresh();
                DrawTape();
                pictureBoxTape.Refresh();
            });

            if (game.CountingMethodId == 1)
            {
                form.topControl.labelMethod.Text = "Очки:";
                form.topControl.labelValue.Text = game.Score.ToString();
            }
            else if (game.CountingMethodId == 2)
            {
                form.topControl.labelMethod.Text = "Время:";
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
            var maxFieldHeight = form.ClientSize.Height
                - form.topControl.Height
                - pictureBoxField.Location.Y * 2;
            if (game.AssemblyTypeId == 1)
            {
                pictureBoxField.Height = maxFieldHeight;
                pictureBoxField.Width = pictureBoxField.Height * 4 / 3;
            }
            else
            {
                pictureBoxField.Width = (form.ClientSize.Width
                    - pictureBoxField.Location.X * 3
                    - SystemInformation.VerticalScrollBarWidth)
                    * game.Field.NHorizontal
                    / (game.Field.NHorizontal + 1);
                pictureBoxField.Height = pictureBoxField.Width * 3 / 4;

                if (pictureBoxField.Height > maxFieldHeight)
                {
                    pictureBoxField.Height = maxFieldHeight;
                    pictureBoxField.Width = pictureBoxField.Height * 4 / 3;
                }
            }
            pictureBoxField.Width -= pictureBoxField.Width % game.Field.NHorizontal;
            pictureBoxField.Height -= pictureBoxField.Height % game.Field.NVertical;
            if (game.AssemblyTypeId == 1)
            {
                pictureBoxField.Location = new(
                    (form.ClientSize.Width - pictureBoxField.Width) / 2,
                    pictureBoxField.Location.Y);
            }
            else
            {
                pictureBoxField.Location = new(
                    pictureBoxField.Location.X,
                    (form.ClientSize.Height - form.topControl.Height
                        - pictureBoxField.Height) / 2);
            }

            var bitmap = new Bitmap(pictureBoxField.Width, pictureBoxField.Height);
            game.Field.Draw(bitmap);
            pictureBoxField.Image = bitmap;
        }
        private void DrawTape()
        {
            panelTape.Show();

            if (game.Tape.Fragments.Length == 0)
            {
                var image = new Bitmap(
                    pictureBoxField.Width / game.Field.NHorizontal,
                    pictureBoxField.Height / game.Field.NVertical);
                pictureBoxTape.Image = image;
                pictureBoxTape.Height = pictureBoxTape.Image.Height;
                return;
            }

            pictureBoxTape.Width = pictureBoxField.Width / game.Field.NHorizontal;
            pictureBoxTape.Height = pictureBoxField.Height / game.Field.NVertical * game.Tape.Fragments.Length;

            panelTape.Width = pictureBoxTape.Width + SystemInformation.VerticalScrollBarWidth;

            var bitmap = new Bitmap(pictureBoxTape.Width, pictureBoxTape.Height);
            game.Tape.Draw(bitmap);
            pictureBoxTape.Image = bitmap;
            if (game.Tape.Fragments.Length != 0)
            {
                pictureBoxTape.Height = pictureBoxTape.Image.Height;
            }
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
