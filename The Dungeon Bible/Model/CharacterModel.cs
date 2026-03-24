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
        private Race? _race = null;
        private Class? _class = null;
        private int[] _stats = new int[6];

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

    }
}
