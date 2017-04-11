using System;
using System.Collections.Generic;
using Typeclass;
using Typeclass.HDict;

namespace TypeclassExample
{
    public class Program
    {
        public static void Main()
        {
            HDict<ConcreteJsonWriteRelation> dict = new HDict<ConcreteJsonWriteRelation>();

            JsonWrite0<int, ConcreteJsonWriteRelation> w = new IntJsonWrite2<ConcreteJsonWriteRelation>();
            HDict<ConcreteJsonWriteRelation> dict2 = dict.Add(new Type<int>(), w);

            Console.WriteLine( 1.ToJsonString(dict) );
            Console.WriteLine( true.ToJsonString(dict) );
            Console.WriteLine( ListType.To(new List<int>{1, 2, 3}).ToJsonString(dict2) );
            Console.WriteLine( new MyStruct(123, false).ToJsonString(dict) );
            // compile time error!
            // Console.WriteLine( "hoge".ToJsonString(dict) );
        }
    }
}
