using The_Dungeon_Bible.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Security.Cryptography.X509Certificates;
using The_Dungeon_Bible.Views;
using System.Windows;

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

        public ICommand EditCharacterCommand { get;}
       
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

            EditCharacterCommand = new RelayCommand(EditCharacter);
        }

        private void EditCharacter(object? parameter)
        {
            if (parameter is not CharacterModel selectedCharacter)
                return;

            var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

            var editVM = new EditCharacterVM(selectedCharacter);
            var editView = new EditCharacterView
            {
                DataContext = editVM
            };

            editView.Show();
            window?.Close();
        }


    }
}
