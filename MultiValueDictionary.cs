using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiValueDictionary
{
    public class MultiValueDicationary<TKey, TType>
    {
        private Dictionary<TKey, List<TType>> dictionary;

        public IEnumerable<TKey> Keys
        {
            get
            {
                return dictionary.Keys;
            }
        }

        public MultiValueDicationary()
        {
            dictionary = new Dictionary<TKey, List<TType>>();
        }

        //Adds an entry to the dictionary. If a key exists it adds a value to its list and if not it crates adds the key and adds the value to the list.
        public void Add(TKey key, TType value)
        {
            if (dictionary.ContainsKey(key) && dictionary[key].Contains(value))
            {
                throw new Exception("ERROR, member already exists for key.");
            }


            if (KeyExists(key))
            {
                dictionary[key].Add(value);
            }
            else
            {
                dictionary[key] = new List<TType> { value };
            }

        }

        //Checks to see if the given key already exists
        public bool KeyExists(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        //Checks the key and value to see if it exists
        public bool MemberExists(TKey key, TType value)
        {
            if (!dictionary.ContainsKey(key))
            {
                throw new Exception("ERROR, the key does not exist.");
            }

            return dictionary[key].Contains(value);

        }

        //Enumberator to be able to iterate over the multivalue dictionary
        public IEnumerator<KeyValuePair<TKey, List<TType>>> GetEnumerator()
        {
            foreach (var item in dictionary)
            {
                yield return item;
            }

        }

        //Removes a value for a given key if there are multiple and removes the entry completely if there's only one value
        public void Remove(TKey key, TType value)
        {
            if (!KeyExists(key))
            {
                throw new Exception("ERROR, key does not exist.");
            }

            if (!MemberExists(key, value))
            {
                throw new Exception("ERROR, member does not exist.");
            }

            if (dictionary[key].Count > 1)
            {
                dictionary[key].Remove(value);
            }
            else
            {
                dictionary.Remove(key);
            }
        }

        //Removes all values for a key and removes the key from the dictionary
        public void RemoveAll(TKey key)
        {
            if (!dictionary.ContainsKey(key))
            {
                throw new Exception("Error, key does not exist.");
            }

            dictionary.Remove(key);
        }

        //Returns all values in the dictionary
        public IEnumerable<TType> AllMembers()
        {
            foreach (var item in dictionary.Values)
            {
                foreach (var v in item)
                {
                    yield return v;
                }
            }
        }


        //Returns the values for a given key
        public IEnumerable<TType> Members(TKey key)
        {
            if (!KeyExists(key))
            {
                throw new Exception("ERROR, key does not exist.");
            }

            return dictionary[key];
        }

        //Returns the count for the values of a given key
        public int Count(TKey key)
        {
            if (!KeyExists(key))
            {
                throw new Exception("ERROR, key does not exist.");
            }

            return dictionary[key].Count();
        }

        //Clears all entries in dictionary.
        public void Clear()
        {
            dictionary.Clear();
        }

    }
}
