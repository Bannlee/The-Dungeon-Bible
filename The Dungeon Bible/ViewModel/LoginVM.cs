using The_Dungeon_Bible.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace The_Dungeon_Bible.ViewModel
{
    public class LoginVM : ObservableObject
    {
        public UserModel CurrentUser { get; set; }

        public ICommand LoginCommand { get; set; }

        public LoginVM()
        {
            DataGen.Generate();
            CurrentUser = new UserModel();
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        private void ExecuteLogin(object? parameter)
        {
          
            if (CurrentUser.Username.Trim() == "admin" && CurrentUser.Password.Trim() == "1234") 
            {
                var MainWindowViewModel = new MainWindowVM(CurrentUser);
                var login = new Views.MainWindow();

                login.DataContext = MainWindowViewModel;
                login.Show();
                Application.Current.MainWindow.Close();
            }
            

        }
    }
}
