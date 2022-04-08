using Ninject;
using System;

namespace DontWreckMyHouse
{
    public class program
    {
        public static void Main(string[] args)
        {
            NinjectContainer.Configure();
            Controller controller = NinjectContainer.kernel.Get<Controller>();
            controller.Run();
        }
    }
}