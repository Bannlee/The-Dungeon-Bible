using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Dungeon_Bible.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.TextFormatting;
using System.Windows;


namespace The_Dungeon_Bible.ViewModel 
{
  
    internal class RaceVM : ObservableObject
    {
        public UserModel? CurrentUser { get; }
        public ObservableCollection<Race> Races { get; set; }
        public Race newrace { get; set; }

        private Race selectedrace;
        public ICommand SaveRace { get; set; }
        public ICommand ClearEntries { get; set; }
        public ICommand DeleteRace {  get; set; }

        public ICommand BackButton { get; set; }

        public RaceVM(UserModel? currentUser)
        {
            CurrentUser = currentUser;
            Races = DataGen.Races;
            newrace = new Race();
            SaveRace = new RelayCommand(ExecuteSaveRace);
            ClearEntries = new RelayCommand(ExecuteClear);
            DeleteRace = new RelayCommand(ExecuteDeleteRace);
            BackButton = new RelayCommand(BacktoMenu);

        }

        public void ExecuteSaveRace(object? par)
        {
            Races.Add(new Race { RaceName = newrace.RaceName, RacialFeature = newrace.RacialFeature, RacialLore = newrace.RacialLore });

           newrace.RaceName = string.Empty;
           newrace.RacialFeature = string.Empty;
           newrace.RacialLore= string.Empty; 
        }

        public void ExecuteClear(object? par) 
        {
            newrace.RaceName = string.Empty;
            newrace.RacialFeature = string.Empty;
            newrace.RacialLore = string.Empty;
        }

        public void ExecuteDeleteRace(object? par)
        {
            Races.Remove(SelectedRace);

            newrace.RaceName = string.Empty;
            newrace.RacialFeature = string.Empty;
            newrace.RacialLore = string.Empty;
        }

        public Race SelectedRace
        {
            get { return selectedrace; }
            set
            {
                selectedrace = value;
                OnPropertyChanged(nameof(SelectedRace));

                if (SelectedRace!= null)
                {
                    newrace.RaceName = selectedrace.RaceName;
                    newrace.RacialFeature = selectedrace.RacialFeature;
                    newrace.RacialLore = selectedrace.RacialLore;
                    
                }
            }
        }

        private void BacktoMenu(object? parameter)
        {
            DataGen.Races = Races;
            var window = parameter as Window;

            var MainWindowVM = new MainWindowVM(CurrentUser);
            var MainWindowView = new Views.MainWindow();

            MainWindowView.DataContext = MainWindowVM;
            MainWindowView.Show();
            window?.Close();
        }
    }
}
