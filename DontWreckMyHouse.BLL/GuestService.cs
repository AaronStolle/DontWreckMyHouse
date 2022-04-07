using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL
{
    public class GuestService
    {
        private readonly IGuest guest;

        public GuestService(IGuest guest)
        {
            this.guest = guest;
        }
    }
}
