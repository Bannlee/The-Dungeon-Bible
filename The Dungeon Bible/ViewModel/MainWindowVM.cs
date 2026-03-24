using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using The_Dungeon_Bible.Model;

namespace The_Dungeon_Bible.ViewModel
{
    internal class MainWindowVM
    {

        public UserModel CurrentUser { get; }
        public ICommand CharButton { get; set; }
        public ICommand RaceButton { get; set; }

        public ICommand ClassButton { get; set; }
        public MainWindowVM(UserModel currentUser)
        {
            CurrentUser = currentUser;
            CharButton = new RelayCommand(MoveToCharacters);
            RaceButton = new RelayCommand(MoveToRaces);
            ClassButton = new RelayCommand(MoveToClasses);

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

        private void MoveToRaces(object? parameter)
        {
            var window = parameter as Window;

            var RaceWindowVM = new RaceVM(CurrentUser);
            var RaceWindowView = new Views.RaceView();

            RaceWindowView.DataContext = RaceWindowVM;
            RaceWindowView.Show();
            window?.Close();
        }

        private void MoveToClasses(object? parameter)
        {
            var window = parameter as Window;

            var ClassWindowVM = new ClassVM(CurrentUser);
            var ClassWindowView = new Views.ClassView();

            ClassWindowView.DataContext = ClassWindowVM;
            ClassWindowView.Show();
            window?.Close();
        }

    }
}
