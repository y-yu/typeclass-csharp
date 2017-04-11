namespace Typeclass
{
    public interface JsonWrite0<T, R> where R : Relation
    {
        string ToJsonString(T obj, HDict<R> dict);
    }
     
    public interface JsonWrite1<C, R> where R : Relation
    {
        string ToJsonString<E, ER>(Apply<C, E> obj, HDict<ER> dict)
            where ER : R, Relation<Type<E>, JsonWrite0<E, ER>>, new();
    }
     
    public static class JsonWriteExtension
    {
        public static string ToJsonString<K, R>(this K obj, HDict<R> dict)
            where R : Relation<Type<K>, JsonWrite0<K, R>>, new()
        {
            JsonWrite0<K, R> w = dict.Get<R, Type<K>, JsonWrite0<K, R>>(new Type<K>());
            return w.ToJsonString(obj, dict);
        }
     
        public static string ToJsonString<C, K, R>(this Apply<C, K> obj, HDict<R> dict)
            where R : Relation<Type<C>, JsonWrite1<C, R>>,
                      Relation<Type<K>, JsonWrite0<K, R>>, new()
        {
            JsonWrite1<C, R> w = dict.Get<R, Type<C>, JsonWrite1<C, R>>(new Type<C>());
            return w.ToJsonString(obj, dict);
        }
    }
}
