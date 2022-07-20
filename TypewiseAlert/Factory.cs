using System;
using System.Collections.Generic;

namespace TypewiseAlert
{
    public class Factory<T1, T2> : IFactory<T1, T2>
    {
        private readonly Dictionary<T2, Func<T1>> factoryVariable;

        public Factory()
        {
            factoryVariable = new Dictionary<T2, Func<T1>>();
        }

        public T1 this[T2 coolingType] => GetType(coolingType);


        public T1 GetType(T2 type)
        {
            return factoryVariable[type]();
        }

        public void RegisterType(T2 type, Func<T1> factoryMethod)
        {
            factoryVariable[type] = factoryMethod;
        }
    }
}
