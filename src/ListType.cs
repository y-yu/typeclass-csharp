using System.Collections.Generic;

namespace Typeclass
{
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
}
