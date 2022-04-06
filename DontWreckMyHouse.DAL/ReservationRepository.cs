using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DontWreckMyHouse.DAL
{
    public class ReservationRepository : IReservation
    {
        public Result<Reservation> Add(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Result<Reservation> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Result<Reservation> Edit(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Result<List<Reservation>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Result<Reservation> FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
