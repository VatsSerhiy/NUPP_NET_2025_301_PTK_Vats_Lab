using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters
{
    public class Warrior : CharacterClass
    {
        public List<String> WeaponCanUse = new List<String> { "Sword", "Mace", "Axe"};

        public Warrior(string name, float moveSpeed) : base(name, 100, 1)
        {
            MoveSpeed = moveSpeed;
        }


    }
}
