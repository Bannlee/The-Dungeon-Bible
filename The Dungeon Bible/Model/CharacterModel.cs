using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Dungeon_Bible.ViewModel;

namespace The_Dungeon_Bible.Model
{
    public class CharacterModel : ObservableObject
    {
        private string _charname = string.Empty;
        private Race? _race = null;
        private Class? _class = null;
        private int[] _stats = new int[6];
        private int _level = 1;
        private int _currenthp = 1;
        private int _maxhp = 1;
        private string _users = string.Empty;
        private string _images = string.Empty;
        private int _characterid = 0;

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

        public Race? Charrace
        {
            get { return _race; }
            set
            {
                if (_race != value)
                {
                    _race = value;
                    OnPropertyChanged(nameof(Charrace));
                }
            }
        }

        public Class? Charclass
        {

            get { return _class; }
            set
            {
                if (_class != value)
                {
                    _class = value;
                    OnPropertyChanged(nameof(Charclass));
                }
            }

        }

        public int[] Stats
        {
            get { return _stats; }
            set
            {
                if (_stats != value)
                {
                    _stats= value;
                    OnPropertyChanged(nameof(Stats));
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

        public int Currenthp
        {
            get { return _currenthp; }
            set
            {
                if (_currenthp != value)
                {
                    _currenthp = value;
                    OnPropertyChanged(nameof(Currenthp));
                }
            }
        }

        public int Maxhp
        {
            get { return _maxhp; }
            set
            {
                if (_maxhp != value)
                {
                    _maxhp = value;
                    OnPropertyChanged(nameof(Maxhp));
                }
            }
        }
        public string Users
        {
            get { return _users; }
            set
            {
                if (_users != value)
                {
                    _users = value;
                    OnPropertyChanged(nameof(Users));
                }
            }
        }

        public string Images
        {
            get { return _images; }
            set
            {
                if (_images != value)
                {
                    _images = value;
                    OnPropertyChanged(nameof(Images));
                }
            }
        }

        public int CharacterID
        {
            get { return _characterid; }
            set
            {
                if (_characterid != value)
                {
                    _characterid = value;
                    OnPropertyChanged(nameof(CharacterID));
                }
            }
        }


    }
}
