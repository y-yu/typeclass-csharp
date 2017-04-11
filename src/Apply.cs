namespace Typeclass
{
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
}
