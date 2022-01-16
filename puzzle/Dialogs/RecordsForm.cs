using puzzle.Services;
using puzzle.ViewModel;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace puzzle.Dialogs
{
    public partial class RecordsForm : Form
    {
        public RecordsForm()
        {
            InitializeComponent();

            comboBoxPuzzle.SelectedValueChanged += new EventHandler((s, e) => DisplayRecords());
            comboBoxMethod.SelectedValueChanged += new EventHandler((s, e) => DisplayRecords());
        }

        private void DisplayRecords()
        {
            if (comboBoxPuzzle.SelectedValue is null
                || comboBoxMethod.SelectedValue is null) return;

            dataGridView.AutoGenerateColumns = false;
            dataGridView.Columns[1].DataPropertyName = (sbyte)comboBoxMethod.SelectedValue == 1
                ? "Score"
                : "Time";
            dataGridView.Columns[1].HeaderText = (sbyte)comboBoxMethod.SelectedValue == 1
                ? "Очки"
                : "Время";
            dataGridView.DataSource = new BindingList<RecordVM>(Db.LoadRecords(
                (short)comboBoxPuzzle.SelectedValue,
                (sbyte)comboBoxMethod.SelectedValue));
        }
    }
}
