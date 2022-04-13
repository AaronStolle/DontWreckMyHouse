using DontWreckMyHouse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse
{
    public class View
    {
        private readonly ConsoleIO io;

        public View(ConsoleIO io)
        {
            this.io = io;
        }

        public MainMenuOption SelectMainMenuOption()
        {
            DisplayHeader("Main Menu");
            int min = int.MaxValue;
            int max = int.MinValue;
            MainMenuOption[] options = Enum.GetValues<MainMenuOption>();
            for (int i = 0; i < options.Length; i++)
            {
                MainMenuOption option = options[i];
                Console.WriteLine(options[i].ToLabel());
                min = Math.Min(min, i);
                max = Math.Max(max, i);
            }

            string message = $"Select [{min}-{max}]: ";
            return options[io.ReadInt(message, min, max)];
        }

        public void EnterToContinue()
        {
            io.ReadString("Press [Enter] to continue.");
        }

        // display only
        public void DisplayHeader(string message)
        {
            io.PrintLine("");
            io.PrintLine(message);
            io.PrintLine(new string('=', message.Length));
        }

        public void DisplayException(Exception ex)
        {
            DisplayHeader("A critical error occurred:");
            io.PrintLine(ex.Message);
        }

        internal void DisplayReservations(List<Reservation> records)
        {
            foreach (var record in records)
            {
                DisplayReservation(record);
            }
        }

        private void DisplayReservation(Reservation record)
        {
            Console.WriteLine($"ID: {record.Id}, {record.StartDate} - {record.EndDate}, Guest: {record.guest.FirstName} {record.guest.LastName}, Email: {record.guest.Email}");
        }

        
        internal void DisplayFutureReservations(List<Reservation> records)
        {
            foreach (var record in records)
            {
                if(record.StartDate > DateTime.Today)
                {
                    DisplayReservation(record);
                    if(record == null)
                    {
                        io.PrintLine("There were no future reservations found");
                    }
                }
                
            }
        }

        internal Reservation CreateNewReservation(Guest guest, Host host)
        {
            Reservation reservation = new Reservation();
            reservation.guest = guest;
            reservation.host = host;
            reservation.StartDate = io.ReadDate("Start (MM/dd/yyyy): ");
            reservation.EndDate = io.ReadDate("End (MM/dd/yyyy): ");
            return reservation;
        }

        internal List<Reservation> DisplayFutureReservations4GuestHost(List<Reservation>? records, Guest guest, Host host)
        {
            foreach (var record in records)
            {
                if (record.StartDate > DateTime.Today && record.guest == guest && record.host == host)
                {
                    DisplayReservation(record);
                    if (record == null)
                    {
                        io.PrintLine("There were no future reservations found for that guest and host");
                    }
                }
                
            }return records;
        }

        
        internal Reservation EditReservation4GuestHost(Reservation oldReservation, Guest guest, Host host)
        {
            Reservation reservation = new Reservation();
            reservation.guest = guest;
            reservation.host=host;
            reservation.StartDate = io.ReadDateOrNull($"Start {oldReservation.StartDate} (MM/dd/yyyy): ");
            reservation.EndDate = io.ReadDateOrNull($"Start {oldReservation.EndDate} (MM/dd/yyyy): ");
            return reservation;
        }
        internal bool Summary(Result<Reservation> result)
        {
            DisplayHeader("Summary");
            io.PrintLine($"Start: {result.Data.StartDate}");
            io.PrintLine($"End: {result.Data.EndDate}");
            io.PrintLine($"Total: {result.Data.Total}");
            if (!io.ReadBool("Is this okay? [y/n]: "))
            {
                DisplayHeader("Failure");
                return false;
            }
            else
            {
                return true;
            }
            

        }
    }
}
