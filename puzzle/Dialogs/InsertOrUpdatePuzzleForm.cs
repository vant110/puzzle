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
                MyPuzzle.Instance = null;
                if (pictureBoxField.Image != null)
                {
                    pictureBoxField.Image.Dispose();
                    pictureBoxField.Image = null;
                }
                if (pictureBoxImage.Image != null)
                {
                    pictureBoxImage.Image.Dispose();
                    pictureBoxImage.Image = null;
                }
                var selectedItem = (ImageVM)comboBoxImage.SelectedItem;
                if (selectedItem == null)
                {
                    return;
                }

                image = Image.FromStream(selectedItem.Image);
                pictureBoxImage.Image = image;
            });
            comboBoxImage.DataSource = form.bindingSourceGallery.DataSource;

            comboBoxLevel.SelectedValueChanged += new EventHandler((s, e) =>
            {
                MyPuzzle.Instance = null;
                if (pictureBoxField.Image != null)
                {
                    pictureBoxField.Image.Dispose();
                    pictureBoxField.Image = null;
                }
                var selectedItem = (LevelVM)comboBoxLevel.SelectedItem;
                if (selectedItem == null)
                {
                    return;
                }

                level = selectedItem;
            });
            comboBoxLevel.DataSource = form.bindingSourceLevels.DataSource;

            buttonMix.Click += new EventHandler((s, e) =>
            {
                if (pictureBoxField.Image != null)
                {
                    pictureBoxField.Image.Dispose();
                    pictureBoxField.Image = null;
                }

                CreatePuzzle();
                DrawField();
            });

            Shown += new EventHandler((s, e) =>
            {
                DrawField();
            });

            FormClosed += new FormClosedEventHandler((s, e) => 
            {
                if (pictureBoxField.Image != null)
                {
                    pictureBoxField.Image.Dispose();
                    pictureBoxField.Image = null;
                }
                if (MyPuzzle.Instance != null)
                {
                    MyPuzzle.Instance = null;
                }
            });
        }

        private void CreatePuzzle()
        {
            if (image == null || level == null)
            {
                return;
            }

            MyPuzzle.Instance = new(
                level.FragmentTypeId,
                level.AssemblyTypeId,
                level.HorizontalFragmentCount,
                level.VerticalFragmentCount,
                image);
            MyPuzzle.Instance.Mix();
        }

        private void DrawField()
        {
            if (MyPuzzle.Instance == null) return;

            var bitmap = new Bitmap(pictureBoxField.Width, pictureBoxField.Height);
            MyPuzzle.Instance.DrawField(bitmap);
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
