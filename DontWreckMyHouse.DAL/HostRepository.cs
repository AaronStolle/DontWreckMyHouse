using System;
using System.Collections.Generic;
using System.Linq;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse.DAL
{
    public class HostRepository : IHost
    {
        public Result<Host> FindByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
