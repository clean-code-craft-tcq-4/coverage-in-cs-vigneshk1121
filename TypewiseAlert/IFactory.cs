using System;

namespace TypewiseAlert
{
    public interface IFactory<T1, T2>
    {
        T1 this[T2 type] { get; }

        T1 GetType(T2 type);

        void RegisterType(T2 type, Func<T1> factoryMethod);
    }
}