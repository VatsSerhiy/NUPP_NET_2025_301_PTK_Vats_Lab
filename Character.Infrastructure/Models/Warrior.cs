using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character.Infrastructure.Models
{
    public class Warrior : CharacterClassModel
    {
        public List<WeaponModel> Weapons { get; set; } = new List<WeaponModel>();

    }
}
