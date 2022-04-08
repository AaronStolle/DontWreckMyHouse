using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL
{
    public class HostService
    {
        private readonly IHost host;

        public HostService(IHost host)
        {
            this.host = host;
        }

        public Result<Host> FindByEmail(string email)
        {
            return host.FindByEmail(email);
        }
        public Host FindById(Guid guid)
        {
            return host.FindAll().Data.FirstOrDefault(h => h.Id == guid);
        }
    }
}
