using puzzle.Data;
using puzzle.Model;
using puzzle.Services;
using puzzle.ViewModel;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace puzzle.Dialogs
{
    public partial class InsertOrUpdatePuzzleForm : Form
    { 
        private EventHandler buttonInsertOrUpdateClick;

        private Image image;
        private LevelVM level;

        public InsertOrUpdatePuzzleForm(MainForm form)
        {
            InitializeComponent();

            comboBoxImage.SelectedValueChanged += new EventHandler((s, e) =>
            {
                if (pictureBoxImage.Image != null)
                {
                    pictureBoxImage.Image.Dispose();
                    pictureBoxImage.Image = null;
                }
                if (comboBoxImage.SelectedItem == null) return;

                image = Image.FromStream(((ImageVM)comboBoxImage.SelectedItem).Image);
                pictureBoxImage.Image = image;
                TryCreatePuzzle();
            });
            comboBoxImage.DataSource = form.bindingSourceGallery.DataSource;

            comboBoxLevel.SelectedValueChanged += new EventHandler((s, e) =>
            {
                if (comboBoxLevel.SelectedItem == null) return;
                level = (LevelVM)comboBoxLevel.SelectedItem;
                TryCreatePuzzle();
            });
            comboBoxLevel.DataSource = form.bindingSourceLevels.DataSource;

            buttonMix.Click += new EventHandler((s, e) =>
            {
                if (pictureBoxField.Image != null)
                {
                    pictureBoxField.Image.Dispose();
                    pictureBoxField.Image = null;
                }
                MyPuzzle.Instance.Mix();
                using var graphics = pictureBoxField.CreateGraphics();
                MyPuzzle.Instance.DrawField(graphics);
            });
        }

        private void TryCreatePuzzle()
        {
            if (image != null
                && level != null)
            {
                if (pictureBoxField.Image != null)
                {
                    pictureBoxField.Image.Dispose();
                    pictureBoxField.Image = null;
                }
                MyPuzzle.Instance = new(
                    level.FragmentTypeId,
                    level.AssemblyTypeId,
                    level.HorizontalFragmentCount,
                    level.VerticalFragmentCount,
                    image);
                MyPuzzle.Instance.SplitIntoFragments();
                MyPuzzle.Instance.Mix();
                using var graphics = pictureBoxField.CreateGraphics();
                MyPuzzle.Instance.DrawField(graphics);
            }
        }

        public EventHandler ButtonInsertOrUpdateClick
        {
            set
            {
                buttonInsertOrUpdate.Click -= buttonInsertOrUpdateClick;
                buttonInsertOrUpdateClick = value;
                buttonInsertOrUpdate.Click += buttonInsertOrUpdateClick;
            }
        }
    }
}
