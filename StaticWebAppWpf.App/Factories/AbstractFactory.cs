using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticWebAppWpf.App.Factories
{
    public class AbstractFactory<T> : IAbstractFactory<T>
    {
        private readonly Func<T> _factory;

        public AbstractFactory(Func<T> factory)
        {
            _factory = factory;
        }

        public T Create()
        {
            return _factory();
        }
    }
}
