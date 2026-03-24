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
        public UserModel CurrentUser { get; }
        public ClassVM(UserModel currentUser)
        {
            CurrentUser = currentUser;
        }
    }
}
