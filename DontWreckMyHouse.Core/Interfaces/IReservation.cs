using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Interfaces
{
    public interface IReservation
    {
        Result<List<Reservation>> FindAll();
        Result<Reservation> FindById(int id);
        Result<Reservation> Add(Reservation reservation);
        Result<Reservation> Edit(Reservation reservation);
        Result<Reservation> Delete(int id);
    }
}
