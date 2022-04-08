using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IHost
    {
        Result<Host> FindByEmail(string email);
        Result<List<Host>> FindAll();
        
    }
}
