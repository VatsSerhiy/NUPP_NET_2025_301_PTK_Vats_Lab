using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    public class CrudServiceAsync<T> : ICrudServiceAsync<T>, IEnumerable<T> where T : IObject
    {
        private readonly Dictionary<Guid, T> _values = new();
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private readonly object _lock = new();
        private readonly string _filePath;

        public string FilePath => _filePath;

        public CrudServiceAsync(string filePath = "data.txt")
        {
            _filePath = filePath;
        }

        public async Task<bool> CreateAsync(T element)
        {
            await _semaphore.WaitAsync();
            try
            {
                if (_values.ContainsKey(element.Id))
                    return false;

                _values.Add(element.Id, element);
                return true;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<T> ReadAsync(Guid id)
        {
            await _semaphore.WaitAsync();
            try
            {
                return _values.TryGetValue(id, out T value) ? value : default;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                return _values.Values.ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            await _semaphore.WaitAsync();
            try
            {
                return _values.Values
                    .Skip(page * amount)
                    .Take(amount)
                    .ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> UpdateAsync(T element)
        {
            await _semaphore.WaitAsync();
            try
            {
                if (!_values.ContainsKey(element.Id))
                    return false;

                _values[element.Id] = element;
                return true;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> RemoveAsync(T element)
        {
            await _semaphore.WaitAsync();
            try
            {
                return _values.Remove(element.Id);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> SaveAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                using (StreamWriter sw = new StreamWriter(_filePath))
                {
                    foreach (var elem in _values)
                    {
                        await sw.WriteLineAsync($"{elem.Key}|{elem.Value}");
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> LoadAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                if (!File.Exists(_filePath))
                    return false;

                _values.Clear();
                using (StreamReader sr = new StreamReader(_filePath))
                {
                    string line;
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 2)
                        {
                            Guid key = Guid.Parse(parts[0]);
                            T value = (T)Convert.ChangeType(parts[1], typeof(T));
                            _values.Add(key, value);
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (_lock)
            {
                return _values.Values.ToList().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}