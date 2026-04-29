using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;
using The_Dungeon_Bible.Model;
using The_Dungeon_Bible.ViewModel;

namespace The_Dungeon_Bible
{
    internal static class DataGen
    {

        public static ObservableCollection<Race> Races { get; set; }
        public static ObservableCollection<Class> Classes { get; set; }
        public static ObservableCollection<CharacterModel> Characterlist { get; set; }

        public static UserModel CurrentUser { get; set; }

        public static async Task Generate(UserModel currentuser)
        {
            string connectionString = @"Server=LAPTOP-SM2BQGTD;Database=Dungeon Database;Trusted_Connection=True;TrustServerCertificate=True;";
            Races = new ObservableCollection<Race>();
            CurrentUser = currentuser;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Races";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Race newrace = new Race();
                                newrace.RaceName = reader["RaceName"]?.ToString() ?? String.Empty;
                                newrace.RacialFeature = reader["RacialFeature"]?.ToString() ?? String.Empty;
                                newrace.RacialLore = reader["RacialLore"]?.ToString() ?? String.Empty;

                                Races.Add(newrace);
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

            //Races.Add(new Race {RaceName = "N/A", RacialFeature = "N/A", RacialLore = "N/A" });
            //Races.Add(new Race {RaceName = "Human", RacialFeature = "Lucky. You can reroll any d20 roll once.", RacialLore = "The most numerous race in the world. They're noted for their unremarkable features." });
            //Races.Add(new Race {RaceName = "Elf", RacialFeature = "Meditate. You don't need to sleep.", RacialLore = "The originators of magic. They're noted for their sharp ears." });
            //Races.Add(new Race {RaceName = "Dwarf", RacialFeature = "Tough. Advantage on death rolls", RacialLore = "Originally called 'Mine Dwellers'. They're noted for their short but bulk stature." });
            //Races.Add(new Race {RaceName = "Halfling", RacialFeature = "Small. Advantage on stealth rolls.", RacialLore = "Usually found as field farmers. Halflings are noted for their light and small stature." });

            Classes = new ObservableCollection<Class>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM CClasses";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Class newclass = new Class();
                                newclass.ClassName = reader["ClassName"]?.ToString() ?? String.Empty;
                                newclass.ClassFeature = reader["ClassFeature"]?.ToString() ?? String.Empty;
                                newclass.HitDie = int.Parse(reader["HitDie"]?.ToString() ?? String.Empty);

                                Classes.Add(newclass);

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
            //Classes.Add(new Class { ClassName = "N/A", ClassFeature = "N/A", HitDie = null });
            //Classes.Add(new Class { ClassName = "Fighter", ClassFeature = "Weapon Attack. Attack with a melee weapon.", HitDie = 10 });
            //Classes.Add(new Class { ClassName = "Wizard", ClassFeature = "Magic Missile. Cast a homing missile.", HitDie = 6 });
            //Classes.Add(new Class { ClassName = "Cleric", ClassFeature = "Healer. Heal an ally.", HitDie = 8 });

            Characterlist = new ObservableCollection<CharacterModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Characters";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                CharacterModel newchar = new CharacterModel();
                                newchar.Charname = reader["CharName"]?.ToString() ?? String.Empty;
                                newchar.Charrace = Races[int.Parse(reader["Raceid"]?.ToString() ?? String.Empty)];
                                newchar.Charclass = Classes[int.Parse(reader["Classid"]?.ToString() ?? String.Empty)];
                                newchar.Level = int.Parse(reader["Level"]?.ToString() ?? "1");

                                int[] stats = reader["Stat"]?.ToString().Split(',').Select(int.Parse).ToArray();
                                newchar.Stats = stats;

                                int con = stats[2];
                                newchar.Currenthp = int.Parse(reader["CurrentHP"].ToString() ?? "1");
                                newchar.Maxhp = newchar.Level * con;
                                newchar.Users = reader["Users"]?.ToString() ?? String.Empty;
                                newchar.Images = reader["Images"]?.ToString() ?? String.Empty;
                                newchar.CharacterID = int.Parse(reader["CharacterId"]?.ToString() ?? "0");


                                if (CurrentUser.Username == newchar.Users) 
                                    Characterlist.Add(newchar);
                                
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

            //Characterlist.Add(new CharacterModel { Charname = "River Chen", Charrace = Races[0], Charclass = Classes[0], Stats = new int[] { 10, 12, 14, 8, 13, 9 } });
            //Characterlist.Add(new CharacterModel { Charname = "New Character", Charrace = Races[0], Charclass = Classes[0], Stats = new int[] { 0, 0, 0, 0, 0, 0 } });
            //Characterlist.Add(new CharacterModel { Charname = "New Character", Charrace = Races[0], Charclass = Classes[0], Stats = new int[] { 0, 0, 0, 0, 0, 0 } });
        }
    }

}
