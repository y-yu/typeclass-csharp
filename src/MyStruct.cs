namespace Typeclass
{
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
}
