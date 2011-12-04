using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class Bag<K, V> : IDictionary<K, V>
    {
        private readonly Dictionary<K, List<V>> _dictionary;

        public Bag()
        {
            _dictionary = new Dictionary<K, List<V>>();
        }

        

        public bool ContainsKey(K key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void Add(K key, V value)
        {
            if (_dictionary.ContainsKey(key))
            {
                List<V> list = _dictionary[key];
                list.Add(value);
            }
            else
            {
                var list = new List<V>();
                list.Add(value);
                _dictionary[key] = list;
            }
        }

        public bool Remove(K key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(K key, out V value)
        {
            throw new NotImplementedException();
        }

        public V this[K key]
        {
            get { return _dictionary[key].First(); }
            set { Add(key, value); }
        }

        public ICollection<K> Keys
        {
            get { return _dictionary.Keys; }
        }

        public ICollection<V> Values
        {
            get { return _dictionary.Values.SelectMany(v => v).ToList(); }
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            IEnumerable<KeyValuePair<K, V>> allPairs =
                _dictionary.Keys.SelectMany(k => GetValues(k).Select(v => new KeyValuePair<K, V>(k, v)));

            return allPairs.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<K, V> item)
        {
            this[item.Key] = item.Value;
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return _dictionary.Keys.Contains(item.Key) && _dictionary[item.Key].Contains(item.Value);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return _dictionary.Keys.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        

        public IEnumerable<V> GetValues(K key)
        {
            return _dictionary[key];
        }
    }
}