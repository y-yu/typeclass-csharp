using System;
using System.Linq;
using Typeclass.HDict;

namespace Typeclass
{
    public class IntJsonWrite<R> : JsonWrite0<int, R> where R : Relation
    {
        public string ToJsonString(int obj, HDict<R> dict)
        {
            return obj.ToString();
        }
    }

    public class IntJsonWrite2<R> : JsonWrite0<int, R> where R : Relation
    {
        public string ToJsonString(int obj, HDict<R> dict)
        {
            return "{" + obj.ToString() + "}";
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

    public class ListTypeJsonWrite<R> : JsonWrite1<ListType, R> where R : Relation
    {
        public string ToJsonString<E, ER>(Apply<ListType, E> obj, HDict<ER> dict)
            where ER : R, Relation<Type<E>, JsonWrite0<E, ER>>, new()
        {
            return "[" + String.Join(", ", ListType.From<E>(obj).Select( x => x.ToJsonString(dict) )) + "]";
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
}
