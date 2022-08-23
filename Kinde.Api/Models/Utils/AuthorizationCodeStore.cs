using System.Collections.Concurrent;

namespace Kinde.Api.Models.Utils
{
    public class AuthorizationCodeStore<TKey, TValue> where TKey : IEquatable<TKey>
    {
        protected IDictionary<TKey, TValue> _dictionary;

        public event EventHandler<ItemAddedEventArgs<TKey, TValue>> ItemAdded = null!;
        public AuthorizationCodeStore()
        {
            _dictionary = new ConcurrentDictionary<TKey, TValue>();
        }
        public AuthorizationCodeStore(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        public void Add(TKey key, TValue value)
        {
            _dictionary[key] = value;
            ItemAdded?.Invoke(this, new ItemAddedEventArgs<TKey, TValue>() { Key = key, Value = value });
        }

        public TValue Get(TKey key)
        {
            return _dictionary[key];
        }
        public void Remove(TKey key)
        {
            _dictionary.Remove(key);
        }
    }
}
