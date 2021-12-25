using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace puzzle.ViewModel
{
    class LevelVM : INotifyPropertyChanged
    {
        private sbyte id;
        private string name;
        private sbyte horizontalFragmentCount;
        private sbyte verticalFragmentCount;
        private sbyte fragmentTypeId;
        private sbyte assemblyTypeId;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public sbyte Id
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
        public sbyte HorizontalFragmentCount
        {
            get { return horizontalFragmentCount; }
            set
            {
                horizontalFragmentCount = value;
                VerticalFragmentCount = (sbyte)Math.Round(HorizontalFragmentCount / ((double)600 / 450));
                NotifyPropertyChanged();
            }
        }
        public sbyte VerticalFragmentCount
        {
            get { return verticalFragmentCount; }
            private set
            {
                verticalFragmentCount = value;
                NotifyPropertyChanged();
            }
        }
        public sbyte FragmentTypeId
        {
            get { return fragmentTypeId; }
            set
            {
                fragmentTypeId = value;
                NotifyPropertyChanged();
            }
        }
        public sbyte AssemblyTypeId
        {
            get { return assemblyTypeId; }
            set
            {
                assemblyTypeId = value;
                NotifyPropertyChanged();
            }
        }
    }
}
