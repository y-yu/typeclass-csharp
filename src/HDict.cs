using System.Collections.Generic;

namespace Typeclass.HDict
{    
    public class HDict<T> where T : Relation
    {
        private Dictionary<object, object> underlying;

        public HDict(Dictionary<object, object> underlying)
        {
            this.underlying = underlying;
        }
         
        public HDict() : this(new Dictionary<object, object>()) { }
         
        internal bool __TryGetValue<K, V>(K key, out V value)
        {
            object v;
            if (underlying.TryGetValue(key, out v))
            {
                value = (V)v;
                return true;
            }
            else
            {
                value = default(V);
                return false;
            }
        }
         
        internal HDict<T> __Add<K, V>(K key, V value)
        {
            var dict = new Dictionary<object, object>(underlying);
            if (dict.ContainsKey(key))
            {
                dict.Remove(key);
            }
            dict.Add(key, value);
            return new HDict<T>(dict);
        }
    }

    public static class HDictExtensions
    {
        public static V Get<R, K, V>(this HDict<R> dict, K key) where R : Relation<K, V>, new()
        {
            V v;
            if (dict.__TryGetValue(key, out v))
            {
                return v;
            }
            else
            {
                v = new R().Default(key);
                dict.__Add(key, v);
                return v;
            }
        }

        public static HDict<R> Add<R, K, V>(this HDict<R> dict, K key, V value) where R : Relation<K, V>
        {
            return dict.__Add(key, value);
        }
    }
}
