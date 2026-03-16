using The_Dungeon_Bible.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace The_Dungeon_Bible.ViewModel
{
    internal class CharacterVM : ObservableObject
    {
        public ObservableCollection<CharacterModel> Characterlist { get; set; }

        public CharacterModel newcharacter { get; set; }

        public ICommand SaveCharacter { get; set; }
        public ICommand ClearEntries {  get; set; }

        public CharacterVM()
        {
            Characterlist = new ObservableCollection<CharacterModel>();

            Characterlist.Add(new CharacterModel {Charname = "River", Race = "Human", Class = "Druid", Level = 5});
            Characterlist.Add(new CharacterModel { Charname = "Sultan", Race = "Firbolg", Class = "Paladin", Level = 10 });
            Characterlist.Add(new CharacterModel { Charname = "Wise", Race = "Dragonborn", Class = "Wizard", Level = 12 });

            newcharacter = new CharacterModel();
            SaveCharacter = new RelayCommand(ExecuteSaveCharacter);
            ClearEntries = new RelayCommand(ExecuteClear);
        }

      
        public void ExecuteSaveCharacter(object? par)
        {
            Characterlist.Add(new CharacterModel { Charname = newcharacter.Charname, Race = newcharacter.Race, Class = newcharacter.Class, Level = newcharacter.Level });

            newcharacter.Charname = string.Empty;
            newcharacter.Race = string.Empty;
            newcharacter.Class = string.Empty;
            newcharacter.Level = 1;
        }

        public void ExecuteClear(object? par)
        {
            newcharacter.Charname = string.Empty;
            newcharacter.Race = string.Empty;
            newcharacter.Class = string.Empty;
            newcharacter.Level = 1;
        }
        
            


    }
}
