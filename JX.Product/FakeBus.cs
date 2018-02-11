using System;
using System.Collections.Generic;
using System.Threading;

namespace JX.Product
{
    public class FakeBus
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> _routes = new Dictionary<Type, List<Action<IMessage>>>();

        public void RegisterHandler<T>(Action<T> handler) where T : IMessage
        {
            if (!_routes.TryGetValue(typeof(T), out List<Action<IMessage>> handlers))
            {
                handlers = new List<Action<IMessage>>();
                _routes.Add(typeof(T), handlers);
            }

            handlers.Add((x => handler((T)x)));
        }

        public void Send<T>(IMessage command)
        {
            _routes.TryGetValue(typeof(T), out List<Action<IMessage>> handlers);
            handlers[0](command);
        }

        public void Publish<T>(IMessage @event)
        {
            _routes.TryGetValue(@event.GetType(), out List<Action<IMessage>> handlers);
            foreach (var handler in handlers)
            {
                var handler1 = handler;
                ThreadPool.QueueUserWorkItem(x => handler1(@event));
            }
        }
    }

    public interface IMessage
    {
    }
}
