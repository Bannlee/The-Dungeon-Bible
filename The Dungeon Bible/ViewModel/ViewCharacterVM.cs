using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using The_Dungeon_Bible.Model;

namespace The_Dungeon_Bible.ViewModel
{
    internal class ViewCharacterVM : ObservableObject
    {
       public CharacterModel SelectedCharacter { get; set; }
       public UserModel? CurrentUser { get; set; }

       public Race CurrentRace { get; set; }
       
       public Class CurrentClass { get; set; }

       public int[] Currentstats { get; set; }
        public int diceresult { get; set; }

      public ICommand BackButton {  get; set; }
      public ICommand DiceRolling { get; set; }



       public ViewCharacterVM(CharacterModel character, UserModel currentuser)
        {
            SelectedCharacter = character;
            CurrentUser = currentuser;

            CurrentRace = SelectedCharacter.Charrace;
            CurrentClass = SelectedCharacter.Charclass;
            Currentstats = SelectedCharacter.Stats;

            BackButton = new RelayCommand(MoveToCharacters);
            DiceRolling = new RelayCommand(DiceRoll);
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

        public void DiceRoll(object? parameter)
        {
            Random rnd = new Random();
            diceresult = rnd.Next(1, 21);
            OnPropertyChanged(nameof(diceresult));
            
        }
    }
}
