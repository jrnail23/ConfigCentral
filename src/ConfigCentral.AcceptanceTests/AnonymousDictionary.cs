using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ConfigCentral.AcceptanceTests
{
    public class AnonymousDictionary : IDictionary<string, string>
    {
        private readonly Dictionary<string, string> _dictionary;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:ConfigCentral.AcceptanceTests.AnonymousDictionary" /> class that is
        ///     empty.
        /// </summary>
        public AnonymousDictionary()
        {
            _dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:ConfigCentral.AcceptanceTests.AnonymousDictionary" /> class and adds
        ///     values
        ///     that are based on properties from the specified object.
        /// </summary>
        /// <param name="values">An object that contains properties that will be added as elements to the new collection.</param>
        public AnonymousDictionary(object values)
        {
            _dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            AddValues(values);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:ConfigCentral.AcceptanceTests.AnonymousDictionary" /> class and adds
        ///     elements
        ///     from the specified collection.
        /// </summary>
        /// <param name="dictionary">A collection whose elements are copied to the new collection.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="dictionary" /> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="dictionary" /> contains one or more duplicate keys.</exception>
        public AnonymousDictionary(IDictionary<string, string> dictionary)
        {
            _dictionary = new Dictionary<string, string>(dictionary, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Gets a collection that contains the keys in the dictionary.
        /// </summary>
        /// <returns>
        ///     A collection that contains the keys in the dictionary.
        /// </returns>
        public Dictionary<string, string>.KeyCollection Keys
        {
            get { return _dictionary.Keys; }
        }

        /// <summary>
        ///     Gets a collection that contains the values in the dictionary.
        /// </summary>
        /// <returns>
        ///     A collection that contains the values in the dictionary.
        /// </returns>
        public Dictionary<string, string>.ValueCollection Values
        {
            get { return _dictionary.Values; }
        }
        /// <summary>
        ///     Gets the number of key/value pairs that are in the collection.
        /// </summary>
        /// <returns>
        ///     The number of key/value pairs that are in the collection.
        /// </returns>
        public int Count
        {
            get { return _dictionary.Count; }
        }

        /// <summary>
        ///     Gets or sets the value that is associated with the specified key.
        /// </summary>
        /// <returns>
        ///     The value that is associated with the specified key, or null if the key does not exist in the collection.
        /// </returns>
        /// <param name="key">The key of the value to get or set.</param>
        public string this[string key]
        {
            get
            {
                string obj;
                TryGetValue(key, out obj);
                return obj;
            }
            set { _dictionary[key] = value; }
        }

        ICollection<string> IDictionary<string, string>.Keys
        {
            get { return _dictionary.Keys; }
        }

        ICollection<string> IDictionary<string, string>.Values
        {
            get { return _dictionary.Values; }
        }

        bool ICollection<KeyValuePair<string, string>>.IsReadOnly
        {
            get { return ((ICollection<KeyValuePair<string, string>>) _dictionary).IsReadOnly; }
        }

        /// <summary>
        ///     Adds the specified value to the dictionary by using the specified key.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        public void Add(string key, string value)
        {
            _dictionary.Add(key, value);
        }

        /// <summary>
        ///     Removes all keys and values from the dictionary.
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
        }

        /// <summary>
        ///     Determines whether the dictionary contains the specified key.
        /// </summary>
        /// <returns>
        ///     true if the dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        /// <param name="key">The key to locate in the dictionary.</param>
        public bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        ///     Removes the value that has the specified key from the dictionary.
        /// </summary>
        /// <returns>
        ///     true if the element is found and removed; otherwise, false. This method returns false if <paramref name="key" /> is
        ///     not found in the dictionary.
        /// </returns>
        /// <param name="key">The key of the element to remove.</param>
        public bool Remove(string key)
        {
            return _dictionary.Remove(key);
        }

        /// <summary>
        ///     Gets a value that indicates whether a value is associated with the specified key.
        /// </summary>
        /// <returns>
        ///     true if the dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">
        ///     When this method returns, contains the value that is associated with the specified key, if the key
        ///     is found; otherwise, contains the appropriate default value for the type of the <paramref name="value" /> parameter
        ///     that you provided as an out parameter. This parameter is passed uninitialized.
        /// </param>
        public bool TryGetValue(string key, out string value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
        {
            ((ICollection<KeyValuePair<string, string>>) _dictionary).Add(item);
        }

        bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
        {
            return ((ICollection<KeyValuePair<string, string>>) _dictionary).Contains(item);
        }

        void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, string>>) _dictionary).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
        {
            return ((ICollection<KeyValuePair<string, string>>) _dictionary).Remove(item);
        }

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Determines whether the dictionary contains a specific value.
        /// </summary>
        /// <returns>
        ///     true if the dictionary contains an element that has the specified value; otherwise, false.
        /// </returns>
        /// <param name="value">The value to locate in the dictionary.</param>
        public bool ContainsValue(string value)
        {
            return _dictionary.ContainsValue(value);
        }

        /// <summary>
        ///     Returns an enumerator that you can use to iterate through the dictionary.
        /// </summary>
        /// <returns>
        ///     A structure for reading data in the dictionary.
        /// </returns>
        public Dictionary<string, string>.Enumerator GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        private void AddValues(object values)
        {
            if (values == null)
                return;
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(values))
            {
                var obj = propertyDescriptor.GetValue(values) as string;
                Add(propertyDescriptor.Name, obj);
            }
        }
    }
}