using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters
{
    public static class CharacterExtensions
    {
        public static void HealFull(this CharacterClass character, int healHp)
        {
            character.HitPoint = healHp;
            Console.WriteLine($"{character.Name} healing on {healHp}!");
        }
    }
}
