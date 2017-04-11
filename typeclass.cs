using System;
using System.Collections.Generic;
using System.Linq;

public interface Relation { }
public interface Relation<K, out V> : Relation
{
    V Get(K key);    
}

public class HDict<T> where T : Relation
{
    private Dictionary<object, object> underlying;

    public HDict(Dictionary<object, object> underlying)
    {
        this.underlying = underlying;
    }

    public HDict() : this(new Dictionary<object, object>()) { }

    internal bool TryGetValue<K, V>(K key, out V value)
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

    internal HDict<T> Add<K, V>(K key, V value)
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
        if (dict.TryGetValue(key, out v))
        {
            return v;
        }
        else
        {
            v = new R().Get(key);
            dict.Add(key, v);
            return v;
        }
    }
    
    public static bool TryGetValue<T, K, V>(this HDict<T> dict, K key, out V value) where T : Relation<K, V>
    {
        return dict.TryGetValue(key, out value);
    }

    public static HDict<T> Add<T, K, V>(this HDict<T> dict, K key, V value) where T : Relation<K, V>
    {
        return dict.Add(key, value);
    }
}

public class Apply<C, E>
{
    private object applied;
        
    private Apply(object a)
    {
        this.applied = a;
    }
           
    public static Apply<C, E> To<Applied>(Applied applied)
    {
        return new Apply<C, E>(applied);
    }
        
    public static Applied From<Applied>(Apply<C, E> apply)
    {
        return (Applied)apply.applied;
    }
}

public class Type<T> {
    override public bool Equals(object a)
    {
        return this.GetType() == a.GetType();
    }
    
    override public int GetHashCode()
    {
        return 1;
    }
}

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
    public static string ToJsonString<K, R>(this K obj, HDict<R> dict) where R : Relation<Type<K>, JsonWrite0<K, R>>, new()
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

public class IntJsonWrite<R> : JsonWrite0<int, R> where R : Relation
{
    public string ToJsonString(int obj, HDict<R> dict)
    {
        return obj.ToString();
    }
}

public class BoolJsonWrite<R> : JsonWrite0<bool, R> where R : Relation
{
    public string ToJsonString(bool obj, HDict<R> dict)
    {
        if (obj)
        {
            return "true";
        }
        else
        {
            return "false";
        }
    }
}

public class ListType
{
    public static Apply<ListType, E> To<E>(List<E> value)
    {
        return Apply<ListType, E>.To<List<E>>(value);
    }
        
    public static List<E> From<E>(Apply<ListType, E> value)
    {
        return Apply<ListType, E>.From<List<E>>(value);
    }
}

public class ListTypeJsonWrite<R> : JsonWrite1<ListType, R> where R : Relation
{
    public string ToJsonString<E, ER>(Apply<ListType, E> obj, HDict<ER> dict)
        where ER : R, Relation<Type<E>, JsonWrite0<E, ER>>, new()
    {
        return "[" + String.Join(", ", ListType.From<E>(obj).Select( x => x.ToJsonString(dict) )) + "]";
    }
}

public struct MyStruct
{
    public int Age { get; }
    public bool Flag { get; }

    public MyStruct(int a, bool f)
    {
        this.Age  = a;
        this.Flag = f;
    }
}

public class MyStructJsonWrite<R> : JsonWrite0<MyStruct, R>
    where R : Relation<Type<int>, JsonWrite0<int, R>>,
              Relation<Type<bool>, JsonWrite0<bool, R>>, new()
{
    public string ToJsonString(MyStruct obj, HDict<R> dict)
    {
        string age  = obj.Age.ToJsonString(dict);
        string flag = obj.Flag.ToJsonString(dict);
        
        return $@"{{ ""age"" : {age}, ""flag"" : {flag} }}";
    }
}

public class ConcreteJsonWriteRelation :
        Relation<Type<int>, IntJsonWrite<ConcreteJsonWriteRelation>>,
        Relation<Type<bool>, BoolJsonWrite<ConcreteJsonWriteRelation>>,
        Relation<Type<ListType>, ListTypeJsonWrite<ConcreteJsonWriteRelation>>,
        Relation<Type<MyStruct>, MyStructJsonWrite<ConcreteJsonWriteRelation>>
{
    public IntJsonWrite<ConcreteJsonWriteRelation> Get(Type<int> key)
    {
        return new IntJsonWrite<ConcreteJsonWriteRelation>();
    }

    public BoolJsonWrite<ConcreteJsonWriteRelation> Get(Type<bool> key)
    {
        return new BoolJsonWrite<ConcreteJsonWriteRelation>();
    }

    public ListTypeJsonWrite<ConcreteJsonWriteRelation> Get(Type<ListType> key)
    {
        return new ListTypeJsonWrite<ConcreteJsonWriteRelation>();
    }

    public MyStructJsonWrite<ConcreteJsonWriteRelation> Get(Type<MyStruct> key)
    {
        return new MyStructJsonWrite<ConcreteJsonWriteRelation>();
    }
}

public class Test
{
    public static void Main()
    {
        HDict<ConcreteJsonWriteRelation> dict = new HDict<ConcreteJsonWriteRelation>();
        
        Console.WriteLine( 1.ToJsonString(dict) );
        Console.WriteLine( true.ToJsonString(dict) );
        Console.WriteLine( ListType.To(new List<int>{1, 2, 3}).ToJsonString(dict) );
        Console.WriteLine( new MyStruct(123, false).ToJsonString(dict) );
        // compile time error!
        // Console.WriteLine( "hoge".ToJsonString(dict) );
    }
}
