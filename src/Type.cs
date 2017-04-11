namespace Typeclass
{
    public class Type<T>
    {
        override public bool Equals(object a)
        {
            return this.GetType() == a.GetType();
        }
        
        override public int GetHashCode()
        {
            return 1;
        }
    }
}
