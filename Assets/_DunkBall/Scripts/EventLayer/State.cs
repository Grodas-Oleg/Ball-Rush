using System.Collections.Generic;
using UnityEngine.Events;

namespace _DunkBall.Scripts.EventLayer
{
    public class State<T>
    {
        protected UnityAction<T> _action;
        public T Value { get; protected set; }

        public void Subscribe(UnityAction<T> action)
        {
            _action += action;
        }

        public void Unsubscibe(UnityAction<T> action)
        {
            _action -= action;
        }

        public void Publish(T value)
        {
            Value = value;
            _action?.Invoke(Value);
        }

        public void Repeat()
        {
            _action?.Invoke(Value);
        }

        public void Reset()
        {
            Value = default;
            _action?.Invoke(Value);
        }

        public void ResetWithOutCall()
        {
            Value = default;
        }
    }

    public class ListState<T> : State<List<T>>
    {
        public ListState()
        {
            Value = new List<T>();
        }

        public void Add(T element)
        {
            Value.Add(element);
            _action?.Invoke(Value);
        }

        public void Remove(T element)
        {
            Value.Remove(element);
            _action?.Invoke(Value);
        }

        public void Clear()
        {
            Value.Clear();
            _action?.Invoke(Value);
        }
    }
}