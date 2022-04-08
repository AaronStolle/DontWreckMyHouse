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
        private readonly IGuest repo;

        public GuestService(IGuest guest)
        {
            this.repo = guest;
        }



        public Result<Guest> FindByEmail(string email)
        {
            return repo.FindByEmail(email);
        }
        public Guest FindById(int id)
        {
            return repo.FindAll().Data.FirstOrDefault(g => g.Id == id);
        }
    }
}
