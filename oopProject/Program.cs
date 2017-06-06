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
            container.Bind<Ball>().ToSelf();
            container.Bind<Action>().To<GetFromDeckAction>();
            container.Bind<Action>().To<InterceptionAction>();
            container.Bind<Action>().To<PassAction>();
            container.Bind<Action>().To<PressureAction>();
            container.Bind<Action>().To<ShootAction>();
            container.Bind<Action>().To<SwapAction>();
            var controller = container.Get<ConsoleController>();
        }
    }
}
