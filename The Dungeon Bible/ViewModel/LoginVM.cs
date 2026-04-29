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
            CurrentUser = new UserModel();
            LoginCommand = new AsyncRelayCommand(ExecuteLogin);
        }

        private async Task ExecuteLogin(object? parameter)
        {

            string connectionString = @"Server=LAPTOP-SM2BQGTD;Database=Dungeon Database;Trusted_Connection=True;TrustServerCertificate=True;";
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
                DataGen.Generate(CurrentUser);
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
