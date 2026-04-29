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
using Microsoft.Data.SqlClient;


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
        public ICommand UpdateRace { get; set; }

        public ICommand BackButton { get; set; }


        public RaceVM(UserModel? currentUser)
        {
            CurrentUser = currentUser;
            Races = DataGen.Races;
            newrace = new Race();
            SaveRace = new AsyncRelayCommand(ExecuteSaveRace);
            ClearEntries = new RelayCommand(ExecuteClear);
            DeleteRace = new AsyncRelayCommand(ExecuteDeleteRace);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
            BackButton = new RelayCommand(BacktoMenu);
            UpdateRace = new AsyncRelayCommand(ExecuteUpdateRace);

        }

        public async Task ExecuteUpdateRace(object? par)
        {
            int index = Races.IndexOf(SelectedRace);
            Race racecore = Races[index];
            string target = racecore.RaceName;

            if (index >= 0)
            {
                racecore.RaceName = newrace.RaceName;
                racecore.RacialFeature = newrace.RacialFeature;
                racecore.RacialLore = newrace.RacialLore;
            }

            string connectionString = @"Server=LAPTOP-SM2BQGTD;Database=Dungeon Database;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Races SET RaceName = @racename, RacialFeature = @racefeature, RacialLore = @racelore WHERE RaceName = @racecore;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync();

                        command.Parameters.AddWithValue("@racename", newrace.RaceName);
                        command.Parameters.AddWithValue("@racefeature", newrace.RacialFeature);
                        command.Parameters.AddWithValue("@racelore", newrace.RacialLore);
                        command.Parameters.AddWithValue("@racecore", target);

                        await command.ExecuteNonQueryAsync();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection failed: " + ex.Message);
                return;
            }
        }

        public async Task ExecuteSaveRace(object? par)
        {
            if (newrace.RaceName != string.Empty && newrace.RacialFeature != string.Empty)
            {
                Races.Add(new Race { RaceName = newrace.RaceName, RacialFeature = newrace.RacialFeature, RacialLore = newrace.RacialLore });

                string connectionString = @"Server=LAPTOP-SM2BQGTD;Database=Dungeon Database;Trusted_Connection=True;TrustServerCertificate=True;";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Races (RaceName,RacialFeature,RacialLore) VALUES (@racename,@racefeature,@racelore);";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            await connection.OpenAsync();

                            command.Parameters.AddWithValue("@racename", newrace.RaceName);
                            command.Parameters.AddWithValue("@racefeature", newrace.RacialFeature);
                            command.Parameters.AddWithValue("@racelore", newrace.RacialLore);

                            await command.ExecuteNonQueryAsync();


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database connection failed: " + ex.Message);
                    return;
                }

                newrace.RaceName = string.Empty;
                newrace.RacialFeature = string.Empty;
                newrace.RacialLore = string.Empty;

            }

        }
        public void ExecuteClear(object? par) 
        {
            newrace.RaceName = string.Empty;
            newrace.RacialFeature = string.Empty;
            newrace.RacialLore = string.Empty;
        }

        public async Task ExecuteDeleteRace(object? par)
        {
            if (newrace.RaceName != string.Empty && newrace.RacialFeature != string.Empty)
            {

                string connectionString = @"Server=LAPTOP-SM2BQGTD;Database=Dungeon Database;Trusted_Connection=True;TrustServerCertificate=True;";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Races WHERE RaceName = @racename;";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            await connection.OpenAsync();

                            command.Parameters.AddWithValue("@racename", newrace.RaceName);

                            int rows = await command.ExecuteNonQueryAsync();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database connection failed: " + ex.Message);
                    return;
                }

            }
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
