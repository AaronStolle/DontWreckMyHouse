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
        public int GuestID { get; set; }
        public decimal HostStandardRate { get; set; }
        public decimal HostWeekendRate { get; set; }
        public decimal Total { get; set; }
        public decimal Value
        {
            get
            {
                if(GuestID == -1)
                {
                    return decimal.Zero;
                }
                decimal weekendPrice = 0M;
                decimal weekdayPrice = 0M;
                for (var day = StartDate.Date; day <= EndDate.Date; day = day.AddDays(1))
                {
                    if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday)
                    {
                        weekendPrice += HostWeekendRate;
                    }
                    else
                    {
                        weekdayPrice += HostStandardRate;
                    }                    
                }
                decimal cost = weekdayPrice + weekendPrice;
                return cost;
            }
        }
    }
}
