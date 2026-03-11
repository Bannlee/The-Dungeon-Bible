using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Dungeon_Bible.Model;

namespace The_Dungeon_Bible.ViewModel
{
    internal class MainWindowVM
    {
        public UserModel CurrentUser { get; }
        public MainWindowVM(UserModel currentUser)
        {
            CurrentUser = currentUser;


        }

      
    }
}
