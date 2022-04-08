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
                
                min = Math.Min(min, i);
                max = Math.Max(max, i);
            }

            string message = $"Select [{min}-{max - 1}]: ";
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
            DisplayHeader("Reservations");
            foreach (var record in records)
            {
                DisplayReservation(record);
                Console.WriteLine("--------------");
            }
        }

        private void DisplayReservation(Reservation record)
        {
            Console.WriteLine($"ID: {record.Id}, {record.StartDate} - {record.EndDate}, Guest: {record.guest.FirstName} {record.guest.LastName}, Email: {record.guest.Email}");
        }
    }
}
