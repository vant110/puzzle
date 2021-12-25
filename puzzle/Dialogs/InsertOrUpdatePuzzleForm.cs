using puzzle.Data;
using puzzle.Model;
using puzzle.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace puzzle.Dialogs
{
    public partial class InsertOrUpdatePuzzleForm : Form
    { 
        private EventHandler buttonInsertOrUpdateClick;

        private Gallery gallery;
        private Image image;
        private DifficultyLevel difficultyLevel;

        public InsertOrUpdatePuzzleForm()
        {
            InitializeComponent();

            //comboBoxImage.DataSource = Db.Instance.Galleries.Local.ToBindingList();
            comboBoxImage.DisplayMember = "Name";
            comboBoxImage.ValueMember = "ImageId";
            if (comboBoxImage.SelectedItem != null)
            {
                gallery = (Gallery)comboBoxImage.SelectedItem;
                image = LocalStorage.LoadImage(gallery.Path);
                pictureBoxImage.Image = image;
            }
            comboBoxImage.SelectedValueChanged += new EventHandler((s, e) =>
            {
                if (pictureBoxImage.Image != null)
                {
                    pictureBoxImage.Image.Dispose();
                    pictureBoxImage.Image = null;
                }
                if (comboBoxImage.SelectedItem == null) return;
                gallery = (Gallery)comboBoxImage.SelectedItem;
                image = LocalStorage.LoadImage(gallery.Path);
                pictureBoxImage.Image = image;
                TryCreatePuzzle();
            });

            //comboBoxLevel.DataSource = Db.Instance.DifficultyLevels.Local.ToBindingList();
            comboBoxLevel.DisplayMember = "Name";
            comboBoxLevel.ValueMember = "DifficultyLevelId";
            if (comboBoxLevel.SelectedItem != null)
            {
                difficultyLevel = (DifficultyLevel)comboBoxLevel.SelectedItem;
                TryCreatePuzzle();
            }
            comboBoxLevel.SelectedValueChanged += new EventHandler((s, e) =>
            {
                if (comboBoxLevel.SelectedItem == null) return;
                difficultyLevel = (DifficultyLevel)comboBoxLevel.SelectedItem;
                TryCreatePuzzle();
            });

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
            if (gallery != null
                && image != null
                && difficultyLevel != null)
            {
                if (pictureBoxField.Image != null)
                {
                    pictureBoxField.Image.Dispose();
                    pictureBoxField.Image = null;
                }
                MyPuzzle.Instance = new(
                    difficultyLevel.FragmentTypeId,
                    difficultyLevel.AssemblyTypeId,
                    difficultyLevel.HorizontalFragmentCount,
                    difficultyLevel.VerticalFragmentCount,
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
