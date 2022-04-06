using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IGuest
    {
        Guest FindByEmail(string email);
        //Guest FindById(int id);

    }
}
