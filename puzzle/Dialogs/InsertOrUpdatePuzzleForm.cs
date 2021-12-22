using puzzle.Data;
using puzzle.DTO;
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

            comboBoxImage.DataSource = Db.Instance.Galleries.Local.ToBindingList();
            comboBoxImage.DisplayMember = "Name";
            comboBoxImage.ValueMember = "ImageId";
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
            });

            comboBoxLevel.DataSource = Db.Instance.DifficultyLevels.Local.ToBindingList();
            comboBoxLevel.DisplayMember = "Name";
            comboBoxLevel.ValueMember = "DifficultyLevelId";
            comboBoxLevel.SelectedValueChanged += new EventHandler((s, e) =>
            {
                if (pictureBoxField.Image != null)
                {
                    pictureBoxField.Image.Dispose();
                    pictureBoxField.Image = null;
                }
                if (comboBoxImage.SelectedItem == null) return;
                difficultyLevel = (DifficultyLevel)comboBoxLevel.SelectedItem;
            });
        }

        private void TryCreatePuzzle()
        {
            if (gallery != null
                && image != null)
            {
                MyPuzzle myPuzzle = new(
                    difficultyLevel.FragmentTypeId,
                    difficultyLevel.AssemblyTypeId,
                    difficultyLevel.HorizontalFragmentCount,
                    difficultyLevel.VerticalFragmentCount,
                    image);
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
