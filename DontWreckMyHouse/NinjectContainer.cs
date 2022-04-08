using System;
using System.Collections.Generic;
using Ninject;
using System.IO;
using DontWreckMyHouse.DAL;
using DontWreckMyHouse.BLL;
using DontWreckMyHouse.Core.Interfaces;
using DontWreckMyHouse.Core.Models;

namespace DontWreckMyHouse
{
    public class NinjectContainer
    {
        public static StandardKernel kernel { get; private set; }

        public static void Configure()
        {
            kernel = new StandardKernel();

            kernel.Bind<ConsoleIO>().To<ConsoleIO>();
            kernel.Bind<View>().To<View>();

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string guestFileDirectory = Path.Combine(projectDirectory,"..\\DontWreckMyHouse.DAL", "data", "guests.csv");
            string hostFileDirectory = Path.Combine(projectDirectory, "..\\DontWreckMyHouse.DAL", "data", "hosts.csv");
            string reservationFileDirectory = Path.Combine(projectDirectory, "..\\DontWreckMyHouse.DAL", "data", "reservations");

            kernel.Bind<IGuest>().To<GuestRepository>().WithConstructorArgument(guestFileDirectory);
            kernel.Bind<IHost>().To<HostRepository>().WithConstructorArgument(hostFileDirectory);
            kernel.Bind<IReservation>().To<ReservationRepository>().WithConstructorArgument(reservationFileDirectory);

            kernel.Bind<ICustomeFormatter<Host>>().To<HostFormatter>();
            kernel.Bind<ICustomeFormatter<Guest>>().To<GuestFormatter>();
            kernel.Bind<ICustomeFormatter<Reservation>>().To<ReservationFormatter>();

            kernel.Bind<GuestService>().To<GuestService>();
            kernel.Bind<HostService>().To<HostService>();
            kernel.Bind<ReservationService>().To<ReservationService>();
        }
    }
}
