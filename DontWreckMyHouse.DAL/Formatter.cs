using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.DAL
{
    public class GuestFormatter : ICustomeFormatter<Guest>
    {
        public Guest Deserialize(string data)
        {
            string[] fields = data.Trim().Split(",");
            //guest_id,first_name,last_name,email,phone,state
            return new Guest()
            {
                Id = int.Parse(fields[0]),
                FirstName = fields[1],
                LastName = fields[2],
                Email = fields[3],
                Phone = fields[4],
                State = fields[5]
            };
        }

        public Guest Deserialize(string data, Host host)
        {
            throw new NotImplementedException();
        }

        public string Serialize(Guest data)
        {
            return $"{data.Id},{data.FirstName},{data.LastName},{data.Email},{data.Phone},{data.State}";
        }
    }
    public class HostFormatter : ICustomeFormatter<Host>
    {
        public Host Deserialize(string data)
        {
            string[] fields = data.Trim().Split(",");
            //id,last_name,email,phone,address,city,state,postal_code,standard_rate,weekend_rate
            return new Host()
            {
                Id = Guid.Parse(fields[0]),
                LastName = fields[1],
                Email = fields[2],
                Phone = fields[3],
                Address = fields[4],
                City = fields[5],
                State = fields[6],
                PostalCode = int.Parse(fields[7]),
                StandardRate = decimal.Parse(fields[8]),
                WeekendRate = decimal.Parse(fields[9])
            };
        }

        public Host Deserialize(string data, Host host)
        {
            throw new NotImplementedException();
        }

        public string Serialize(Host data)
        {
            return $"{data.Id},{data.LastName},{data.Email},{data.Phone},{data.Address},{data.City},{data.State},{data.PostalCode},{data.StandardRate},{data.WeekendRate}";
        }
    }
    public class ReservationFormatter : ICustomeFormatter<Reservation>
    {        
        // 
        public Reservation Deserialize(string data)
        {
            string[] fields = data.Trim().Split(",");
            //id,start_date,end_date,guest_id,total
            Reservation result = new Reservation();
            result.Id = int.Parse(fields[0]);
            result.StartDate = DateTime.Parse(fields[1]);
            result.EndDate = DateTime.Parse(fields[2]);
            result.Total = decimal.Parse(fields[4]);
            Guest guest = new Guest();
            result.guest = guest;
            result.guest.Id = int.Parse(fields[3]);
            Host host = new Host();
            result.host = host;

            return result;
        }

        public string Serialize(Reservation data)
        {
            return $"{data.Id},{data.StartDate},{data.EndDate},{data.guest.Id},{data.Total}";
        }
    }
}
