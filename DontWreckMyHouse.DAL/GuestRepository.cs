using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DontWreckMyHouse.DAL
{
    internal class GuestRepository : IGuest
    {
        public Guest FindByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
