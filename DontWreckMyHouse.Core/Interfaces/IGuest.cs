using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IGuest
    {
        Result<Guest> FindByEmail(string email);
        //Guest FindById(int id);

    }
}
