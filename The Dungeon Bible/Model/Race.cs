using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Dungeon_Bible.ViewModel;

namespace The_Dungeon_Bible.Model
{
    public class Race : ObservableObject
    {
        private string? _racename = string.Empty;
        private string? _racialfeature = string.Empty;
        private string? _raciallore = string.Empty;


        public string? RaceName
        {
            get { return _racename; }
            set
            {
                if (_racename != value)
                {
                    _racename = value;
                    OnPropertyChanged(nameof(RaceName));
                }
            }
        }

        public string? RacialFeature
        {
            get { return _racialfeature; }
            set
            {
                if (_racialfeature != value)
                {
                    _racialfeature = value;
                    OnPropertyChanged(nameof(RacialFeature));
                }
            }
        }

        public string? RacialLore
        {
            get { return _raciallore; }
            set
            {
                if (_raciallore != value)
                {
                    _raciallore = value;
                    OnPropertyChanged(nameof(RacialLore));
                }
            }
        }

    }
}
