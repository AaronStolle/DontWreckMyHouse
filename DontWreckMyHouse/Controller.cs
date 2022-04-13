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
            view.DisplayHeader("Cancel a Reservation");
            Guest guest = GetGuestEmail();
            if (guest == null)
            {
                return;
            }
            Host host = GetHostEmail();
            if (host == null)
            {
                return;
            }
            view.DisplayHeader($"{host.LastName}: {host.City}, {host.State}");
            var records = reservationService.FindAllByHostId(host.Id);
            if (!records.Success)
            {
                Console.WriteLine(records.Message);

            }
            BuildReservationData(records.Data);
            var reservations = view.DisplayFutureReservations4GuestHost(records.Data, guest, host);
            view.DisplayReservations(reservations);
            view.EnterToContinue();
            Reservation reservation = FindByReservationId(reservations);
            if (reservation == null)
            {
                return;
            }
            if (!io.ReadBool($"Are you sure you want to delete the reservation for {reservation.StartDate}-{reservation.EndDate}? [y/n]: "))
            {
                view.DisplayHeader("Failure");
                return;
            }
            else
            {
                bool resultToDelete = reservationService.Delete(reservation);
                view.DisplayHeader("Success");
                io.PrintLine($"Reservation {reservation.Id} deleted");
            }
        }

        private void EditReservation()
        {
            view.DisplayHeader("Edit a Reservation");
            Guest guest = GetGuestEmail();
            if (guest == null)
            {
                return;
            }
            Host host = GetHostEmail();
            if (host == null)
            {
                return;
            }
            view.DisplayHeader($"{host.LastName}: {host.City}, {host.State}");
            var records = reservationService.FindAllByHostId(host.Id);
            if (!records.Success)
            {
                Console.WriteLine(records.Message);

            }
            BuildReservationData(records.Data);
            var reservations = view.DisplayFutureReservations4GuestHost(records.Data, guest, host);
            view.DisplayReservations(reservations);
            view.EnterToContinue();
            Reservation oldReservation = FindByReservationId(reservations);
            if(oldReservation == null)
            {
                return ;
            }
            Reservation newReservation = view.EditReservation4GuestHost(oldReservation, guest, host);
            Result<Reservation> result = reservationService.Reservation2Edit(oldReservation, newReservation);
            if (!result.Success)
            {
                io.PrintLine(result.Message);
            }
            else
            {
                if (view.Summary(result))
                {
                    bool resultToEdit = reservationService.Edit(newReservation);
                    view.DisplayHeader("Success");
                    io.PrintLine($"Reservation {newReservation.Id} updated");
                }
                else
                {
                    return;
                }
            }
        }

        private Reservation FindByReservationId(List<Reservation> reservations)
        {
            Result<Reservation> reservation = new();
            var reservationId = io.ReadInt("Reservation ID: ");
            reservation = reservationService.FindById(reservations, reservationId);
            if (!reservation.Success)
            {
                Console.WriteLine(reservation.Message);
            }
            return reservation.Data;
        }

        private void AddReservation()
        {
            view.DisplayHeader("Make a Reservation");
            Guest guest = GetGuestEmail();
            if (guest == null)
            {
                return;
            }
            Host host = GetHostEmail(); 
            if(host == null)
            {
                return;
            }
            view.DisplayHeader($"{host.LastName}: {host.City}, {host.State}");
            var records = reservationService.FindAllByHostId(host.Id);
            if (!records.Success)
            {
                Console.WriteLine(records.Message);
                
            }
            BuildReservationData(records.Data);
            view.DisplayFutureReservations(records.Data);
            view.EnterToContinue();
            Reservation reservation = view.CreateNewReservation(guest, host);
            Result<Reservation> result = reservationService.Make(reservation);
            if (!result.Success)
            {
                io.PrintLine(result.Message);
            }
            else
            {
                if (view.Summary(result))
                {
                    Result<Reservation> resultToAdd = reservationService.Add(reservation);
                    view.DisplayHeader("Success");
                    io.PrintLine($"Reservation {resultToAdd.Data.Id} created");
                }
                else
                {
                    return;
                }
            }
        }

        private Host GetHostEmail()
        {
            Result<Host> host = new();
            var hostEmail = io.ReadRequiredString("Enter Host Email: ");
            host = hostService.FindByEmail(hostEmail);
            if (!host.Success)
            {
                Console.WriteLine(host.Message);                
            }return host.Data;
        }

        private Guest GetGuestEmail()
        {
            Result<Guest> guest = new();
            var guestEmail = io.ReadRequiredString("Enter Guest Email: ");
            guest = guestService.FindByEmail(guestEmail);
            if (!guest.Success)
            {
                Console.WriteLine(guest.Message);                
            }return guest.Data;
        }


        private void ViewByHostEmail()
        {
            view.DisplayHeader("View Reservations for Host");
            Host host = GetHostEmail();
            view.DisplayHeader($"{host.LastName}: {host.City}, {host.State}");
            var records = reservationService.FindAllByHostId(host.Id);
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
