using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.BLL
{
    public class ReservationService
    {
        private readonly IReservation repo;

        public ReservationService(IReservation reservation)
        {
            this.repo = reservation;
        }

        public Result<List<Reservation>> FindAllByHostId(Guid hostId2Find)
        {
            return repo.FindAllByHostId(hostId2Find);
        }
    }
}
