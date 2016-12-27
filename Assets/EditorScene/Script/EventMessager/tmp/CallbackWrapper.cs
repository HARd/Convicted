namespace EventMessager 
{
	internal class CallbackWrapper<T> where T : AbstractMessage 
	{
		private ECallback<T> _delegates;

		public void Add(ECallback<T> value) 
		{
            _delegates += value;
        }

		public void Remove(ECallback<T> value) 
		{
            _delegates -= value;
        }

        public void Call(T message) 
		{
            if (_delegates != null) 
                _delegates(message);
        }
    }
}