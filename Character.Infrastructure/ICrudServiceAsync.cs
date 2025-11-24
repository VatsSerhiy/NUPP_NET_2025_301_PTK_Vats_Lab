using Characters.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Character.Infrastructure
{
    public interface ICrudServiceAsync<T> where T : class, IEntity
    {
        public Task<bool> CreateAsync(T element);
        public Task<T> ReadAsync(Guid id);
        public Task<IEnumerable<T>> ReadAllAsync();
        public Task<IEnumerable<T>> ReadAllAsync(int page, int amount);
        public Task<bool> UpdateAsync(T element);
        public Task<bool> RemoveAsync(T element);
        public Task<bool> SaveAsync();

    }
}
