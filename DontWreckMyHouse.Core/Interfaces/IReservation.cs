using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IReservation
    {
        Result<List<Reservation>> FindAllByHostId(Guid hostId);
        Result<Reservation> Add(Reservation reservation);
        bool Edit(Reservation reservation);
        bool Delete(Reservation reservation);
    }
}
