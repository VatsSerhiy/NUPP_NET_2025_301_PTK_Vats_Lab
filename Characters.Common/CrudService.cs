using Characters.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters
{
    public class CrudService<T> : ICrudService<T> where T : IEntity
    {
        private readonly List<T> _items = new List<T>();

        public void Create(T element)
        {
            if(element != null)
            {
                _items.Add(element);
            }
        }

        public T Read(Guid id)
        {
            var temp = _items.FirstOrDefault(x => x.Id == id);
            if(temp == null)
            {
                throw new Exception();
            }
            return temp;
        }

        public IEnumerable<T> ReadAll()
        {
            return _items;
        }

        public void Remove(T element)
        {
            var temp = _items.FirstOrDefault(x => x.Id == element.Id);
            if(temp != null)
            {
                _items.Remove(element);
            }
        }

        public void Update(T element)
        {
            var temp = _items.FirstOrDefault(x => x.Id == element.Id);
            if(temp != null)
            {
                Guid oldId = temp.Id;
                _items.Remove(temp);
                element.Id = oldId;
                _items.Add(element);
            }
        }
    }
}
