using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace oopProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var holder = new MongDbHolder("FullData.csv", "Pictures");
            //holder.CreateFromCSV();
            //Console.ReadKey();
            var container = new StandardKernel();
            container.Bind<IDatabase>().To<MongoDatabase>();
            container.Bind<IFootballDatabase>().To<FootballDatabase>();
            container.Bind<IAction>().To<GetFromDeckAction>();
            container.Bind<IAction>().To<InterceptionAction>();
            container.Bind<IAction>().To<PassAction>();
            container.Bind<IAction>().To<PressureAction>();
            container.Bind<IAction>().To<ShootAction>();
            container.Bind<IAction>().To<SwapAction>();
            container.Bind<ISuccess>().To<Success>();
            var controller = container.Get<ConsoleController>();
            controller.Loop();
        }
    }
}
