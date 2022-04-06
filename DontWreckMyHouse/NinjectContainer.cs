using System;
using System.Collections.Generic;
using Ninject;
using System.IO;
using DontWreckMyHouse.DAL;
using DontWreckMyHouse.BLL;
using DontWreckMyHouse.Core.Interfaces;

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
            string guestFileDirectory = Path.Combine(projectDirectory, "data", "Guests.csv");
        }
    }
}
