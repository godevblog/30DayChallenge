using System;
using System.Reactive.Linq;
using Autofac;
using RXLib.TimeCounter;

namespace DependencyInjection
{
	class Program
	{
		static void Main()
		{
			Console.CursorVisible = false;

			var container = InitContainer();

			container.Resolve<Clock>();

			for (var i = 0; i < 7; i++)
			{
				container.Resolve<Part>();
			}

			Console.SetCursorPosition(0, Console.WindowHeight - 1);
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}

		private static IContainer InitContainer()
		{
			var builder = new ContainerBuilder();

			builder
				.Register(c => Observable.Interval(TimeSpan.FromMilliseconds(100)))
				.As<IObservable<long>>()
				.SingleInstance();

			builder.RegisterType<ObservableTimeCounter>().SingleInstance();

			builder.RegisterType<Clock>();
			builder.RegisterType<Part>();

			builder.RegisterInstance(new Object());

			var container = builder.Build();
			return container;
		}
	}
}
