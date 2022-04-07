using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface ICustomeFormatter<T>
    {
        public T Deserialize(string data);

        public string Seralize(T data);
    }
}
