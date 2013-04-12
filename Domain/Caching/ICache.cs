using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Caching
{
    public interface ICache : IDisposable
    {
        int GetOrCreate<T>(string name, Func<int> getOrCreate) where T : Dimension, new();
    }
}
