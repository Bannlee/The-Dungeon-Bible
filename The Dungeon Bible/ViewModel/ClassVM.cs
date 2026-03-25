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

        public void ExecuteUpdateClass(object? par)
        {
            int index = Classes.IndexOf(SelectedClass);

            if (index >= 0)
            {
                Class classcore = Classes[index];

                classcore.ClassName = newclass.ClassName;
                classcore.ClassFeature = newclass.ClassFeature;
                classcore.HitDie = newclass.HitDie;
            }

        }

        public void ExecuteSaveClass(object? par)
        {
            Classes.Add(new Class {ClassName = newclass.ClassName, ClassFeature = newclass.ClassFeature, HitDie = newclass.HitDie});

            newclass.ClassName = string.Empty;
            newclass.ClassFeature = string.Empty;
            newclass.HitDie = 0;
            
        }

        public void ExecuteClear(object? par) 
        {
            newclass.ClassName = string.Empty;
            newclass.ClassFeature = string.Empty;
            newclass.HitDie = 0;
        }

        public void ExecuteDelete(object? par)
        {
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

