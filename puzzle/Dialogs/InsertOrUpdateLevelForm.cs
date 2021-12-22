using puzzle.DTO;
using puzzle.Services;
using System;
using System.Windows.Forms;

namespace puzzle.Dialogs
{
    public partial class InsertOrUpdateLevelForm : Form
    {
        private EventHandler buttonInsertOrUpdateClick;

        public InsertOrUpdateLevelForm()
        {
            InitializeComponent();

            LevelDTO.HorizontalFragmentCount = (int)numericUpDownHorizontal.Value;
            labelVertical.Text = LevelDTO.VerticalFragmentCount.ToString();
            numericUpDownHorizontal.ValueChanged += new EventHandler((s, e) => 
            {
                LevelDTO.HorizontalFragmentCount = (int)numericUpDownHorizontal.Value;
                labelVertical.Text = LevelDTO.VerticalFragmentCount.ToString();
            });

            comboBoxFragmentType.DataSource = Db.Instance.FragmentTypes.Local.ToBindingList();
            comboBoxFragmentType.DisplayMember = "Name";
            comboBoxFragmentType.ValueMember = "FragmentTypeId";

            comboBoxAssemblyType.DataSource = Db.Instance.AssemblyTypes.Local.ToBindingList();
            comboBoxAssemblyType.DisplayMember = "Name";
            comboBoxAssemblyType.ValueMember = "AssemblyTypeId";
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
