using Core.Services;
using System;
using UniRx;

namespace Project.Services
{
    public class EventService : BaseService
    {
        public void TriggerEvent<T>(T eventData) where T : class
        {
            MessageBroker.Default.Publish(eventData);
        }

        public IObservable<T> ObserveEvent<T>()
        {
            return MessageBroker.Default.Receive<T>();
        }
    }
}
