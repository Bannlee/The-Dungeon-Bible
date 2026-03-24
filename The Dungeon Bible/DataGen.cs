using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The_Dungeon_Bible.ViewModel;
using The_Dungeon_Bible.Model;
using System.Windows.Media.TextFormatting;

namespace The_Dungeon_Bible
{
     internal static class DataGen
    {
        
        public static ObservableCollection<Race> Races {  get; set; }
        public static ObservableCollection<Class> Classes { get; set; }
        public static ObservableCollection<CharacterModel> Characterlist { get; set; }

        public static void Generate()
        {
            Races = new ObservableCollection<Race>();

            Races.Add(new Race {RaceName = "N/A", RacialFeature = "N/A", RacialLore = "N/A" });
            Races.Add(new Race {RaceName = "Human", RacialFeature = "Lucky. You can reroll any d20 roll once.", RacialLore = "The most numerous race in the world. They're noted for their unremarkable features." });
            Races.Add(new Race {RaceName = "Elf", RacialFeature = "Meditate. You don't need to sleep.", RacialLore = "The originators of magic. They're noted for their sharp ears." });
            Races.Add(new Race {RaceName = "Dwarf", RacialFeature = "Tough. Advantage on death rolls", RacialLore = "Originally called 'Mine Dwellers'. They're noted for their short but bulk stature." });
            Races.Add(new Race {RaceName = "Halfling", RacialFeature = "Small. Advantage on stealth rolls.", RacialLore = "Usually found as field farmers. Halflings are noted for their light and small stature." });

            Classes = new ObservableCollection<Class>();
            Classes.Add(new Class {ClassName = "N/A", ClassFeature = "N/A", HitDie = null });
            Classes.Add(new Class {ClassName = "Fighter", ClassFeature = "Weapon Attack. Attack with a melee weapon.", HitDie = 10 });
            Classes.Add(new Class {ClassName = "Wizard", ClassFeature = "Magic Missile. Cast a homing missile.", HitDie = 6 });
            Classes.Add(new Class {ClassName = "Cleric", ClassFeature = "Healer. Heal an ally.", HitDie = 8 });

            Characterlist = new ObservableCollection<CharacterModel>();

            Characterlist.Add(new CharacterModel { Charname = "River Chen", Charrace = Races[1], Charclass = Classes[2], Stats = new int[] { 10, 12, 14, 8, 13, 9 } });
            Characterlist.Add(new CharacterModel { Charname = "New Character", Charrace = Races[0], Charclass = Classes[0], Stats = new int[] { 0, 0, 0, 0, 0, 0 } });
            Characterlist.Add(new CharacterModel { Charname = "New Character", Charrace = Races[0], Charclass = Classes[0], Stats = new int[] { 0, 0, 0, 0, 0, 0 } });
        }
    }

}