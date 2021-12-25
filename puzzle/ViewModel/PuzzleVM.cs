using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace puzzle.ViewModel
{
    class PuzzleVM : INotifyPropertyChanged
    {
        private short id;
        private string name;
        private short imageId;
        private sbyte difficultyLevelId;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public short Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }
        public short ImageId
        {
            get { return imageId; }
            set
            {
                imageId = value;
                NotifyPropertyChanged();
            }
        }
        public sbyte DifficultyLevelId
        {
            get { return difficultyLevelId; }
            set
            {
                difficultyLevelId = value;
                NotifyPropertyChanged();
            }
        }
    }
}
