using The_Dungeon_Bible.ViewModel;
using The_Dungeon_Bible.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace The_Dungeon_Bible.Views
{
    /// <summary>
    /// Interaction logic for CharacterMenu.xaml
    /// </summary>
    public partial class CharacterMenu : Window
    {
        public CharacterMenu()
        {
            InitializeComponent();
            this.DataContext = new CharacterVM();
        }
    }
}
