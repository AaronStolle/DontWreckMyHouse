using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.Core.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guest Guest { get; set; }
        public Host Host { get; set; }
        public decimal Total { get; set; }
        public decimal Value
        {
            get
            {
                if(Guest == null)
                {
                    return decimal.Zero;
                }
                decimal weekendPrice = 0M;
                decimal weekdayPrice = 0M;
                for (var day = StartDate.Date; day <= EndDate.Date; day = day.AddDays(1))
                {
                    if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday)
                    {
                        weekendPrice += Host.WeekendRate;
                    }
                    else
                    {
                        weekdayPrice += Host.StandardRate;
                    }                    
                }
                decimal cost = weekdayPrice + weekendPrice;
                return cost;
            }
        }
    }
}
