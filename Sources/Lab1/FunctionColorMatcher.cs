using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Lab1
{
    public class FunctionColorMatcher : IDictionary<Type, string>
    {
        private readonly Dictionary<Type, string> _colorMap = new();

        public string this[Type key] { get => ((IDictionary<Type, string>)_colorMap)[key]; set => ((IDictionary<Type, string>)_colorMap)[key] = value; }

        public ICollection<Type> Keys => ((IDictionary<Type, string>)_colorMap).Keys;

        public ICollection<string> Values => ((IDictionary<Type, string>)_colorMap).Values;

        public int Count => ((ICollection<KeyValuePair<Type, string>>)_colorMap).Count;

        public bool IsReadOnly => ((ICollection<KeyValuePair<Type, string>>)_colorMap).IsReadOnly;

        public void Add(Type key, string value)
        {
            ((IDictionary<Type, string>)_colorMap).Add(key, value);
        }

        public void Add(KeyValuePair<Type, string> item)
        {
            ((ICollection<KeyValuePair<Type, string>>)_colorMap).Add(item);
        }

        public void Clear()
        {
            ((ICollection<KeyValuePair<Type, string>>)_colorMap).Clear();
        }

        public bool Contains(KeyValuePair<Type, string> item)
        {
            return ((ICollection<KeyValuePair<Type, string>>)_colorMap).Contains(item);
        }

        public bool ContainsKey(Type key)
        {
            return ((IDictionary<Type, string>)_colorMap).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Type, string>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<Type, string>>)_colorMap).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Type, string>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<Type, string>>)_colorMap).GetEnumerator();
        }

        public bool Remove(Type key)
        {
            return ((IDictionary<Type, string>)_colorMap).Remove(key);
        }

        public bool Remove(KeyValuePair<Type, string> item)
        {
            return ((ICollection<KeyValuePair<Type, string>>)_colorMap).Remove(item);
        }

        public bool TryGetValue(Type key, [MaybeNullWhen(false)] out string value)
        {
            return ((IDictionary<Type, string>)_colorMap).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_colorMap).GetEnumerator();
        }
    }
}
