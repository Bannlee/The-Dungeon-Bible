using The_Dungeon_Bible.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace The_Dungeon_Bible.ViewModel
{
    internal class CharacterVM : ObservableObject
    {
        public ObservableCollection<CharacterModel> Characterlist { get; set; }

        public CharacterVM()
        {
            Characterlist = new ObservableCollection<CharacterModel>();

            Characterlist.Add(new CharacterModel {Charname = "River", Race = "Human", Class = "Druid", Level = 5});
            Characterlist.Add(new CharacterModel { Charname = "Sultan", Race = "Firbolg", Class = "Paladin", Level = 10 });
            Characterlist.Add(new CharacterModel { Charname = "Wise", Race = "Dragonborn", Class = "Wizard", Level = 12 });
        }
            


    }
}
