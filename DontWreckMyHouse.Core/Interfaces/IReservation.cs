﻿using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IReservation
    {
        List<Reservation> FindAll(Guid hostId);
        Result<Reservation> FindById(int id);
        Result<Reservation> Add(Reservation reservation);
        Result<Reservation> Edit(Reservation reservation);
        Result<Reservation> Delete(int id);
    }
}
