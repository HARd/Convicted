namespace EventMessager 
{
    public abstract class AbstractMessage 
	{
        protected bool _processed = true;

        public virtual bool Processed 
		{
            get { return _processed; }
            set { _processed = value; }
        }
    }
}