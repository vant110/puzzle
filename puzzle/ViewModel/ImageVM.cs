using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace puzzle.ViewModel
{
    class ImageVM : INotifyPropertyChanged
    {
        private short id;
        private string name;
        private string path;
        private string imageHash;

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
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                NotifyPropertyChanged();
            }
        }
        public string ImageHash
        {
            get { return imageHash; }
            set
            {
                imageHash = value;
                NotifyPropertyChanged();
            }
        }

        public Stream Image { get; set; }
    }
}
