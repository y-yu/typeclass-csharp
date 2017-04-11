namespace Typeclass
{
    public class ConcreteJsonWriteRelation :
            Relation<Type<int>, JsonWrite0<int, ConcreteJsonWriteRelation>>,
            Relation<Type<bool>, JsonWrite0<bool, ConcreteJsonWriteRelation>>,
            Relation<Type<ListType>, JsonWrite1<ListType, ConcreteJsonWriteRelation>>,
            Relation<Type<MyStruct>, JsonWrite0<MyStruct, ConcreteJsonWriteRelation>>
    {
        public JsonWrite0<int, ConcreteJsonWriteRelation> Get(Type<int> key)
        {
            return new IntJsonWrite<ConcreteJsonWriteRelation>();
        }
     
        public JsonWrite0<bool, ConcreteJsonWriteRelation> Get(Type<bool> key)
        {
            return new BoolJsonWrite<ConcreteJsonWriteRelation>();
        }
     
        public JsonWrite1<ListType, ConcreteJsonWriteRelation> Get(Type<ListType> key)
        {
            return new ListTypeJsonWrite<ConcreteJsonWriteRelation>();
        }
     
        public JsonWrite0<MyStruct, ConcreteJsonWriteRelation> Get(Type<MyStruct> key)
        {
            return new MyStructJsonWrite<ConcreteJsonWriteRelation>();
        }
    }
}
