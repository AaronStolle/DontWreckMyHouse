using System;
using System.Collections.Generic;
using System.Linq;
using DontWreckMyHouse.Core.Models;
using DontWreckMyHouse.BLL;

namespace DontWreckMyHouse
{
    public class Controller
    {
        private readonly ReservationService reservationService;
        private readonly GuestService guestService;
        private readonly HostService hostService;
        private readonly View view;
        private readonly ConsoleIO io;

        public Controller(ReservationService reservationService, GuestService guestService, HostService hostService, View view, ConsoleIO io)
        {
            this.reservationService = reservationService;
            this.guestService = guestService;
            this.hostService = hostService;
            
            this.view = view;
            this.io = io;
        }

        public void Run()
        {
            view.DisplayHeader("Welcome to Reservation Program");
            try
            {
                RunAppLoop();
            }
            catch (Exception ex)
            {
                view.DisplayException(ex);
            }
            view.DisplayHeader("Goodbye.");
        }

        private void RunAppLoop()
        {
            MainMenuOption option;
            do
            {
                option = view.SelectMainMenuOption();
                switch (option)
                {
                    case MainMenuOption.ViewReservations:
                        ViewByHostEmail();
                        break;
                    case MainMenuOption.AddReservation:
                        AddReservation();
                        break;
                    case MainMenuOption.EditReservation:
                        EditReservation();
                        break;
                    case MainMenuOption.RemoveReservation:
                        CancelReservation();
                        break;
                }
            }while(option != MainMenuOption.Exit);
        }

        private void CancelReservation()
        {
            throw new NotImplementedException();
        }

        private void EditReservation()
        {
            throw new NotImplementedException();
        }

        private void AddReservation()
        {
            throw new NotImplementedException();
        }

        private void ViewByHostEmail()
        {
            var email = io.ReadRequiredString("Enter Host Email");
            var host = hostService.FindByEmail(email);
            if (!host.Success)
            {
                Console.WriteLine(host.Message);
                return;
            }
            var records = reservationService.FindAllByHostId(host.Data.Id);
            if (!records.Success)
            {
                Console.WriteLine(records.Message);
                return ;
            }
            BuildReservationData(records.Data);
            view.DisplayReservations(records.Data);
            view.EnterToContinue();
        }

        private void BuildReservationData(List<Reservation>? data)
        {
            foreach(var reservation in data)
            {
                Guest guest = new Guest();
                guest = guestService.FindById(reservation.guest.Id);
                reservation.guest = guest;
                Host host = new();
                host = hostService.FindById(reservation.host.Id);
                reservation.host = host;
                
            }
            
        }
    }
}
