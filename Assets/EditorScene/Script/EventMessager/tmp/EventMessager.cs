using System;
using System.Collections.Generic;

namespace EventMessager 
{
	public delegate void ECallback<T>(T message) where T : AbstractMessage;


	public class EventMessager 
	{
		private readonly Dictionary<Type, object> _handlers;

		// INSTANCE

		public EventMessager()
		{
			_handlers = new Dictionary<Type, object>();
		}


		public void Add<T>(ECallback<T> value) where T : AbstractMessage 
		{
			var type = typeof (T);
			if (!_handlers.ContainsKey(type)) 
			{
				_handlers.Add(type, new CallbackWrapper<T>());
			}
			((CallbackWrapper<T>) _handlers[type]).Add(value);
		}

		public void Remove<T>(ECallback<T> value) where T : AbstractMessage 
		{
			var type = typeof (T);
			if (_handlers.ContainsKey(type)) 
			{
				((CallbackWrapper<T>) _handlers[type]).Remove(value);
			}
		}

		public void Call<T>(T message) where T : AbstractMessage
		{
			var type = message.GetType();
			if (_handlers.ContainsKey(type)) {
				((CallbackWrapper<T>) _handlers[type]).Call(message);
			}
		}

		// STATIC

		private static readonly EventMessager _instance = new EventMessager();

		public static void SAdd<T>(ECallback<T> value) where T : AbstractMessage 
		{
			_instance.Add(value);
		}

		public static void SRemove<T>(ECallback<T> value) where T : AbstractMessage 
		{
			_instance.Remove(value);
		}

		public static void SCall<T>(T message) where T : AbstractMessage 
		{
			_instance.Call(message);
		}
	}
}