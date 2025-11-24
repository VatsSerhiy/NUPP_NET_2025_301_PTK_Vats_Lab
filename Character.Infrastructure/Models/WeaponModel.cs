using Characters.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character.Infrastructure.Models
{
    public class WeaponModel : IEntity
    {
        public Guid Id { get; set; }

        public string? WeaponName { get; set; }
        public int WeaponLevel { get; set; }
        public float WeaponDamage { get; set; }


        public Guid OwnerId { get; set; }
       
        public Warrior? Owner { get; set; }
    }
}
