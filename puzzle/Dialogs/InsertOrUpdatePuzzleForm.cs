using puzzle.Model;
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
                Game.Instance = null;
                pictureBoxField.Image?.Dispose();
                pictureBoxImage.Image?.Dispose();
                var selectedItem = (ImageVM)comboBoxImage.SelectedItem;
                if (selectedItem == null) return;

                image = Image.FromStream(selectedItem.Image);
                pictureBoxImage.Image = image;
            });
            comboBoxImage.DataSource = form.bindingSourceGallery.DataSource;

            comboBoxLevel.SelectedValueChanged += new EventHandler((s, e) =>
            {
                Game.Instance = null;
                pictureBoxField.Image?.Dispose();
                var selectedItem = (LevelVM)comboBoxLevel.SelectedItem;
                if (selectedItem == null) return;

                level = selectedItem;
            });
            comboBoxLevel.DataSource = form.bindingSourceLevels.DataSource;

            buttonMix.Click += new EventHandler((s, e) =>
            {
                pictureBoxField.Image?.Dispose();

                CreatePuzzle();
                DrawField();
            });

            Shown += new EventHandler((s, e) =>
            {
                DrawField();
            });

            FormClosed += new FormClosedEventHandler((s, e) =>
            {
                pictureBoxField.Image?.Dispose();
                Game.Instance = null;
            });
        }

        private void CreatePuzzle()
        {
            if (image is null || level is null)
            {
                return;
            }

            Game.Instance = new(
                level.FragmentTypeId,
                level.AssemblyTypeId,
                level.HorizontalFragmentCount,
                level.VerticalFragmentCount,
                image);
            Game.Instance.Mix();
        }

        private void DrawField()
        {
            if (Game.Instance is null) return;

            var bitmap = new Bitmap(pictureBoxField.Width, pictureBoxField.Height);
            Game.Instance.DrawField(bitmap);
            pictureBoxField.Image = bitmap;
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
