using The_Dungeon_Bible.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Security.Cryptography.X509Certificates;

namespace The_Dungeon_Bible.ViewModel
{
    internal class CharacterVM : ObservableObject
    {
        public UserModel? CurrentUser { get; }
        public ObservableCollection<CharacterModel> Characterlist { get; set; }
        public ObservableCollection<Class> Classes { get; set; }
        public ObservableCollection<Race> Races { get; set; }

        public Race? Char1Race { get; set; }
        public Race? Char2Race { get; set; }
        public Race? Char3Race { get; set; }
        public Class? Char1Class {  get; set; }
        public Class? Char2Class { get; set; }
        public Class? Char3Class { get; set; }
       
        public CharacterVM(UserModel? currentUser)
        {
            CurrentUser = currentUser;
            Characterlist = DataGen.Characterlist;
            Classes = DataGen.Classes;
            Races = DataGen.Races;
            Char1Race = Characterlist[0].Charrace;
            Char2Race = Characterlist[1].Charrace;
            Char3Race = Characterlist[2].Charrace;
            Char1Class = Characterlist[0].Charclass;
            Char2Class = Characterlist[1].Charclass;
            Char3Class = Characterlist[2].Charclass;

        }




    }
}
