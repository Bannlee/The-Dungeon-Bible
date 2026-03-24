using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Dungeon_Bible.Model;

namespace The_Dungeon_Bible.ViewModel
{
    public class EditCharacterVM : ObservableObject
    {
        public CharacterModel SelectedCharacter { get; }


        public EditCharacterVM(CharacterModel character)
        {
            SelectedCharacter = character;
        }
    }
}
