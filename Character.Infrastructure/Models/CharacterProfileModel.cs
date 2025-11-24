using Characters.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character.Infrastructure.Models
{
    public class CharacterProfileModel : IEntity
    {
        public Guid Id { get; set; }

        public Guid IdCharacter { get; set; }
        public CharacterClassModel? Character { get; set; }


    }
}
