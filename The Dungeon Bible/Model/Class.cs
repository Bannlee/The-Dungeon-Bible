using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Dungeon_Bible.ViewModel;

namespace The_Dungeon_Bible.Model
{
    public class Class : ObservableObject
    {

        private string _classname = string.Empty;
        private string _classfeature = string.Empty;
        private int? _hitdie = null;


        public string ClassName
        {
            get { return _classname; }
            set
            {
                if (_classname != value)
                {
                    _classname = value;
                    OnPropertyChanged(nameof(ClassName));
                }
            }
        }

        public string ClassFeature
        {
            get { return _classfeature; }
            set
            {
                if (_classfeature != value)
                {
                    _classfeature = value;
                    OnPropertyChanged(nameof(ClassFeature));
                }
            }
        }

        public int? HitDie
        {
            get { return _hitdie; }
            set
            {
                if (_hitdie != value)
                {
                    _hitdie = value;
                    OnPropertyChanged(nameof(HitDie));
                }
            }
        }


    }
}
