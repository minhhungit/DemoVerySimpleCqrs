using d60.Cirqus;
using d60.Cirqus.Config.Configurers;
using d60.Cirqus.MsSql.Config;
using d60.Cirqus.MsSql.Views;
using d60.Cirqus.Views;
using d60.Cirqus.Views.ViewManagers;
using DemoVerySimpleCqrs.Domain.Views;
using System.Collections.Generic;

namespace DemoVerySimpleCqrs
{
    public class Bootstrapper
    {
        public static string EventStoreConnectionStringName { get; } = "EventStore";
        public static string EventStoreTableName { get; } = "DomainEvents";
        public static string ViewPositionTableName { get; } = "ViewPosition";

        public static ICommandProcessor CommandBus { get; private set; }
        static StandardViewManagerProfiler Profiler { get; set; }

        public static IOptionalConfiguration<ICommandProcessor> GetCommandProcessorConfig()
        {
            var config = CommandProcessor.With()
                .EventStore(e =>
                {
                    e.UseSqlServer(EventStoreConnectionStringName, EventStoreTableName);
                    e.EnableCaching(5000);
                })
                .EventDispatcher(e =>
                {
                    Profiler = new StandardViewManagerProfiler();
                    var views = GetViewDispatcher();

                    foreach (var v in views)
                    {
                        var va = new List<IViewManager>();
                        va.Add(v);
                        e.UseViewManagerEventDispatcher(va.ToArray()).WithProfiler(Profiler);
                    }

                    //e.UseEventDispatcher(context =>
                    //{
                    //    return new ConsoleOutEventDispatcher(context.Get<IEventStore>());
                    //});
                })
                .AggregateRootRepository(e =>
                {
                    e.EnableInMemorySnapshotCaching(10000);
                });

            return config;
        }

        public static List<IViewManager> GetViewDispatcher()
        {
            var managers = new List<IViewManager>();

            managers.Add(new MsSqlViewManager<FirstView>(EventStoreConnectionStringName, "FirstViewData",
                positionTableName: ViewPositionTableName, automaticallyCreateSchema: true));

            managers.Add(new MsSqlViewManager<SecondView>(EventStoreConnectionStringName, "SecondViewData",
                positionTableName: ViewPositionTableName, automaticallyCreateSchema: true));

            return managers;
        }

        public void Start()
        {
            var config = GetCommandProcessorConfig();
            CommandBus = config.Create();
        }

        public void Stop()
        {
            CommandBus.Dispose();
        }
    }
}
