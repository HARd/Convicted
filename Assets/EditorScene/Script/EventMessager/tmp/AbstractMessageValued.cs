namespace EventMessager 
{
	public abstract class AbstractMessageValued<T> : AbstractMessage 
	{
        public readonly T Value;

        protected AbstractMessageValued(T value) 
		{
            Value = value;
        }
    }
}