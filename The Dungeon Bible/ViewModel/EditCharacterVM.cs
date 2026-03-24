using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using The_Dungeon_Bible.Model;

namespace The_Dungeon_Bible.ViewModel
{
    public class EditCharacterVM : ObservableObject
    {
        public UserModel? CurrentUser;
        public CharacterModel SelectedCharacter { get; }
        public ObservableCollection<Class> Classes { get; set; }
        public ObservableCollection<Race> Races { get; set; }
        public ObservableCollection<CharacterModel> Characterlist { get; set; }


        public string savemessage { get; set; }

        public int[] Currentstats { get; set; }

        public ICommand SaveChar {  get; set; }
        public ICommand ClearEntries {  get; set; }
        public ICommand Return {  get; set; }


        public EditCharacterVM(CharacterModel character, UserModel currentuser)
        {
            CurrentUser = currentuser;
            SelectedCharacter = character;
            Classes = DataGen.Classes;
            Races = DataGen.Races;  
            Characterlist = DataGen.Characterlist;
            Currentstats = SelectedCharacter.Stats;
            savemessage = string.Empty;

            SaveChar = new RelayCommand(ExecuteSaveChar);
            ClearEntries = new RelayCommand(ExecuteClear);
            Return = new RelayCommand(MoveToCharacters);
        }

        public void ExecuteSaveChar(object? par)
        {
            int index = Characterlist.IndexOf(SelectedCharacter);

            if (index >= 0)
            {
                CharacterModel character = Characterlist[index];

                character.Charname = SelectedCharacter.Charname;
                character.Charrace = SelectedCharacter.Charrace;
                character.Charclass = SelectedCharacter.Charclass;
                character.Stats = new int[]
                {
            Currentstats[0],
            Currentstats[1],
            Currentstats[2],
            Currentstats[3],
            Currentstats[4],
            Currentstats[5]
                };

                savemessage = "Saved!";
                OnPropertyChanged(nameof(savemessage));
            }
            else
            {
                MessageBox.Show("Error: Character not found in list.");
            }
        }

        public void ExecuteClear(object? par)
        {
            SelectedCharacter.Charname = string.Empty;
            SelectedCharacter.Charrace = null;
            SelectedCharacter.Charclass = null;
            Currentstats[0] = 0;
            Currentstats[1] = 0;
            Currentstats[2] = 0;
            Currentstats[3] = 0;
            Currentstats[4] = 0;
            Currentstats[5] = 0;
            OnPropertyChanged(nameof(Currentstats));
        }

        private void MoveToCharacters(object? parameter)
        {
            var window = parameter as Window;

            var CharWindowVM = new CharacterVM(CurrentUser);
            var CharWindowView = new Views.CharacterMenu();

            CharWindowView.DataContext = CharWindowVM;
            CharWindowView.Show();
            window?.Close();
        }


    }
}
