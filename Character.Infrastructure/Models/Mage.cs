using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character.Infrastructure.Models
{
    public class Mage : CharacterClassModel
    {
        //public Mage(string name) : base(name, 80, 1)
        //{
        //    CanUseSpell = true;
        //    Luck = 5.0f;
        //    MoveSpeed = 14;
        //}
        public void CastSpell()
        {
            Console.WriteLine($"{Name} used magic!");
        }
    }
}
