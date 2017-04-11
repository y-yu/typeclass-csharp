namespace Typeclass
{
    public interface Relation { }
    public interface Relation<K, out V> : Relation
    {
        V Get(K key);    
    }
}
