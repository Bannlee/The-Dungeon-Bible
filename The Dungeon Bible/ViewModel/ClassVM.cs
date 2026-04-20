using Microsoft.Data.SqlClient;
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
    internal class ClassVM : ObservableObject
    {
        public UserModel? CurrentUser { get; }
        public ObservableCollection<Class> Classes { get; set; }
        public Class newclass { get; set; }
        private Class selectedclass;

        public ICommand SaveClass {  get; set; }
        public ICommand ClearEntries { get; set; }
        public ICommand DeleteClass { get; set; }
        public ICommand BackButton { get; set; }

        public ICommand UpdateClass { get; set; }


        public ClassVM(UserModel currentUser)
        {
            CurrentUser = currentUser;
            Classes = DataGen.Classes;
            newclass = new Class();

            SaveClass = new RelayCommand(ExecuteSaveClass);
            ClearEntries = new RelayCommand(ExecuteClear);
            DeleteClass = new RelayCommand(ExecuteDelete);
            BackButton = new RelayCommand(BacktoMenu);
            UpdateClass = new RelayCommand(ExecuteUpdateClass);

        }

        public async void ExecuteUpdateClass(object? par)
        {
            int index = Classes.IndexOf(SelectedClass);
            Class classcore = Classes[index];
            string target = classcore.ClassName;

            if (index >= 0)
            {              
                classcore.ClassName = newclass.ClassName;
                classcore.ClassFeature = newclass.ClassFeature;
                classcore.HitDie = newclass.HitDie;
            }

            string connectionString = @"Server=CCL2-09;Database=Dungeon Database;User Id=sa;Password=ccl2;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE CClasses SET ClassName = @classname, ClassFeature = @classfeature, HitDie = @hitdie WHERE ClassName = @classcore;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync();

                        command.Parameters.AddWithValue("@classname", newclass.ClassName);
                        command.Parameters.AddWithValue("@classfeature", newclass.ClassFeature);
                        command.Parameters.AddWithValue("@hitdie", newclass.HitDie);
                        command.Parameters.AddWithValue("@classcore", target);

                        await command.ExecuteNonQueryAsync();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection failed: " + ex.Message);
                return;
            }

            newclass.ClassName = string.Empty;
            newclass.ClassFeature = string.Empty;
            newclass.HitDie = 0;


        }

        public async void ExecuteSaveClass(object? par)
        {
            if (newclass.ClassName != string.Empty && newclass.ClassFeature != string.Empty)
            {
                Classes.Add(new Class { ClassName = newclass.ClassName, ClassFeature = newclass.ClassFeature, HitDie = newclass.HitDie });
                
                string connectionString = @"Server=CCL2-09;Database=Dungeon Database;User Id=sa;Password=ccl2;TrustServerCertificate=True;";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO CClasses (ClassName,ClassFeature,HitDie) VALUES (@classname,@classfeature,@hitdie);";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            await connection.OpenAsync();

                            command.Parameters.AddWithValue("@classname", newclass.ClassName);
                            command.Parameters.AddWithValue("@classfeature", newclass.ClassFeature);
                            command.Parameters.AddWithValue("@hitdie", newclass.HitDie);

                           await command.ExecuteNonQueryAsync();


                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database connection failed: " + ex.Message);
                    return;
                }

                newclass.ClassName = string.Empty;
                newclass.ClassFeature = string.Empty;
                newclass.HitDie = 0;

            }

        }

        public void ExecuteClear(object? par) 
        {
            newclass.ClassName = string.Empty;
            newclass.ClassFeature = string.Empty;
            newclass.HitDie = 0;
        }

        public async void ExecuteDelete(object? par)
        {
            
            if (newclass.ClassName != string.Empty && newclass.ClassFeature != string.Empty)
            {

                string connectionString = @"Server=CCL2-09;Database=Dungeon Database;User Id=sa;Password=ccl2;TrustServerCertificate=True;";
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM CClasses WHERE ClassName = @classname;";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            await connection.OpenAsync();

                            command.Parameters.AddWithValue("@classname", newclass.ClassName);

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
            Classes.Remove(SelectedClass);
            newclass.ClassName = string.Empty;
            newclass.ClassFeature = string.Empty;
            newclass.HitDie = 0;

        }


        public Class SelectedClass
        {
            get { return selectedclass; }
            set { selectedclass = value; OnPropertyChanged(nameof(SelectedClass));

                if (SelectedClass != null)
                {
                    newclass.ClassName = selectedclass.ClassName;
                    newclass.ClassFeature = selectedclass.ClassFeature;
                    newclass.HitDie= selectedclass.HitDie;
                }
            }
        }

        private void BacktoMenu(object? parameter)
        {
            DataGen.Classes = Classes;
            var window = parameter as Window;

            var MainWindowVM = new MainWindowVM(CurrentUser);
            var MainWindowView = new Views.MainWindow();

            MainWindowView.DataContext = MainWindowVM;
            MainWindowView.Show();
            window?.Close();
        }

       
    }
}

