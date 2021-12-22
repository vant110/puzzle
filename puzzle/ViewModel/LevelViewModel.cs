using puzzle.Dialogs;
using puzzle.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace puzzle.ViewModel
{
    class LevelViewModel : INotifyPropertyChanged
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
            get
            {
                return id;
            }
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (!Validator.IsLevelName(value))
                {
                    throw new Exception("Название уровня сложности некорректно.");
                }
                name = value;
                NotifyPropertyChanged();
            }
        }
        public sbyte HorizontalFragmentCount
        {
            get
            {
                return horizontalFragmentCount;
            }
            set
            {
                /* ???
                if (value < 3 || value > 10)
                {
                    throw new Exception("Количество фрагментов по горизонтали некорректно.");
                }
                */
                horizontalFragmentCount = value;
                NotifyPropertyChanged();
            }
        }
        public sbyte VerticalFragmentCount
        {
            get
            {
                return verticalFragmentCount;
            }
            set
            {
                verticalFragmentCount = value;
                NotifyPropertyChanged();
            }
        }
        public sbyte FragmentTypeId
        {
            get
            {
                return fragmentTypeId;
            }
            set
            {
                fragmentTypeId = value;
                NotifyPropertyChanged();
            }
        }
        public sbyte AssemblyTypeId
        {
            get
            {
                return assemblyTypeId;
            }
            set
            {
                assemblyTypeId = value;
                NotifyPropertyChanged();
            }
        }
    }
}
