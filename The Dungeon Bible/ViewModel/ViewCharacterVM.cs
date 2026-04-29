using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Data.SqlClient;
using The_Dungeon_Bible.Model;

namespace The_Dungeon_Bible.ViewModel
{
    internal class ViewCharacterVM : ObservableObject
    {
        public ObservableCollection<CharacterModel> Characterlist { get; set; }
        public CharacterModel SelectedCharacter { get; set; }
       public UserModel? CurrentUser { get; set; }

       public Race CurrentRace { get; set; }
       
       public Class CurrentClass { get; set; }

       public int[] Currentstats { get; set; }
       public ObservableCollection<int> dicerolls { get; set; }

       public int diceresult { get; set; }

       

        private int _bonus;


        public ICommand BackButton {  get; set; }
      public ICommand DiceRolling { get; set; }

        public ICommand SetStatCommand { get; set; }

        public ICommand RollAbility { get; set; }



       public ViewCharacterVM(CharacterModel character, UserModel currentuser)
        {
            dicerolls = new ObservableCollection<int> { 4, 6, 8, 10, 12, 20, 100 };
            SelectedCharacter = character;
            CurrentUser = currentuser;

            CurrentRace = SelectedCharacter.Charrace;
            CurrentClass = SelectedCharacter.Charclass;
            Currentstats = SelectedCharacter.Stats;

            BackButton = new RelayCommand(MoveToCharacters);
            DiceRolling = new RelayCommand(DiceRoll);

            SetStatCommand = new RelayCommand(SelectStat);
            RollAbility = new RelayCommand(AbilityRoll);
        }

        private int _selectedDice;
        public int SelectedDice
        {
            get => _selectedDice;
            set
            {
                _selectedDice = value;
                OnPropertyChanged(nameof(SelectedDice));
            }
        }
        public int Bonus
        {
            get => _bonus;
            set
            {
                _bonus = value;
                OnPropertyChanged(nameof(Bonus));
            }
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

        public async void Savehp()
        {
            int index = Characterlist.IndexOf(SelectedCharacter);
            CharacterModel charactercore = Characterlist[index];
            string target = charactercore.Charname;
            try
            {
                string connectionString = @"Server=LAPTOP-SM2BQGTD;Database=Dungeon Database;Trusted_Connection=True;TrustServerCertificate=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Characters SET CurrentHP = @currenthp WHERE Charname = @charactercore";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync();

                        command.Parameters.AddWithValue("@currenthp", SelectedCharacter.Currenthp);
                        command.Parameters.AddWithValue("@charactercore", target);                       
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

        public void DiceRoll(object? parameter)
        {
            Random rnd = new Random();
            int max = 20;

            if (SelectedDice > 0)
                max = SelectedDice;
            else
                max = 20;

            diceresult = rnd.Next(1, max + 1);

            OnPropertyChanged(nameof(diceresult));
        }

        public void SelectStat(object? parameter)
        {

            if (parameter == null)
                return;

            int index = Convert.ToInt32(parameter);

            if (Currentstats == null || index < 0 || index >= Currentstats.Length)
                return;

            int statValue = Currentstats[index];

            Bonus = (statValue - 10) / 2;

            Random rnd = new Random();

            int roll = rnd.Next(1, 21);

            diceresult = roll + Bonus;

            OnPropertyChanged(nameof(diceresult));
        }

        public void AbilityRoll(object? parameter)
        {
            Random rnd = new Random();
            int? max = CurrentClass.HitDie+1;

            diceresult = rnd.Next(1, (max ?? 20));

            OnPropertyChanged(nameof(diceresult));
        }

    }
}
