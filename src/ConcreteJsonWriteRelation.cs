namespace Typeclass
{
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
}
