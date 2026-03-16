using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Dungeon_Bible.ViewModel;

namespace The_Dungeon_Bible.Model
{
    internal class CharacterModel : ObservableObject
    {
        private string _charname = string.Empty;
        private string _race = string.Empty;
        private string _class = string.Empty;
        private int _level = 1;

        public string Charname
        {
            get { return _charname; }
            set
            {
                if (_charname != value)
                {
                    _charname = value;
                    OnPropertyChanged(nameof(Charname));
                }
            }
        }

        public string Race
        {
            get { return _race; }
            set
            {
                if (_race != value)
                {
                    _race = value;
                    OnPropertyChanged(nameof(Race));
                }
            }
        }

        public string Class
        {

            get { return _class; }
            set
            {
                if (_class != value)
                {
                    _class = value;
                    OnPropertyChanged(nameof(Class));
                }
            }

        }

        public int Level
        {
            get { return _level; }
            set
            {
                if (_level != value)
                {
                    _level = value;
                    OnPropertyChanged(nameof(Level));
                }
            }

        }
    }
}
