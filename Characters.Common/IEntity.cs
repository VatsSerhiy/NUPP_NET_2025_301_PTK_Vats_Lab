using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters.Common
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
