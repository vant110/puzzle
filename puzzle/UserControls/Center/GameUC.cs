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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace puzzle.UserControls.Center
{
    public partial class GameUC : UserControl
    {
        private Game game;

        internal GameUC(Game game, MainForm form)
        {
            InitializeComponent();
            this.game = game;

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

                if (index1 == index2) return;     

                game.SwapFragments(index1, index2);
                game.DrawFragment(index1, pictureBoxField.Image);
                game.DrawFragment(index2, pictureBoxField.Image);
                pictureBoxField.Refresh();

                if (game.AllFragmentsInOriginalPosition())
                {
                    MessageBoxes.Info("Пазл собран.");
                    form.DisplayGameChoice();
                }
            });
        }

        private void DrawField()
        {
            var bitmap = new Bitmap(pictureBoxField.Width, pictureBoxField.Height);
            game.DrawField(bitmap);
            pictureBoxField.Image = bitmap;
        }
    }
}
