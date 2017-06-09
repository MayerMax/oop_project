using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using ooPproject;

namespace oopProject
{
    public class Program
    {
        static void Main()
        {
            var container = new StandardKernel();
            container.Bind<IDatabase>().To<CSVDatabase>();
            container.Bind<IFootballDatabase>().To<FootballDatabase>();
            container.Bind<IBall>().To<Ball>();
            container.Bind<IAction>().To<GetFromDeckAction>();
            container.Bind<IAction>().To<InterceptionAction>();
            container.Bind<IAction>().To<PassAction>();
            container.Bind<IAction>().To<PressureAction>();
            container.Bind<IAction>().To<ShootAction>();
            container.Bind<IAction>().To<SwapAction>();
            container.Bind<IAction>().To<SwapInSquadAction>();
            container.Bind<ISuccess>().To<Success>();

            try
            {
                var controller = container.Get<ConsoleController>();
                controller.Loop();
            }
            catch (DatabaseException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
