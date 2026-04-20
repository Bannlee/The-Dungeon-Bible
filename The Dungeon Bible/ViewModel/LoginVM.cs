using The_Dungeon_Bible.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Data.SqlClient;

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

        private async void ExecuteLogin(object? parameter)
        {

            string connectionString = @"Server=CCL2-09;Database=Dungeon Database;User Id=sa;Password=ccl2;TrustServerCertificate=True;";
            bool isLoginValid = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync();
                        command.Parameters.AddWithValue("@username", CurrentUser.Username);
                        command.Parameters.AddWithValue("@password", CurrentUser.Password);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                isLoginValid = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection failed: " + ex.Message);
                return;
            }

            if (isLoginValid) 
            {
                var MainWindowViewModel = new MainWindowVM(CurrentUser);
                var login = new Views.MainWindow();

                login.DataContext = MainWindowViewModel;
                login.Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
    }
}
