using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IHost
    {
        Host FindByEmail(string email);
        //List<Host> FindAll();
        //Host FindById(string id);
    }
}
