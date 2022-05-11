using Lab1.Commands;
using Lab1.Infrastructure;
using Lab1.Models;
using Lab1.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IFunctionsRepository, XmlFunctionsRepository>();

            var colorMap = new FunctionColorMatcher();
            colorMap[typeof(ConstantFunction)] = "[yellow]";
            colorMap[typeof(LinearFunction)] = "[green]";
            colorMap[typeof(QuadraticFunction)] = "[magenta]";
            colorMap[typeof(SineFunction)] = "[cyan]";
            colorMap[typeof(CosineFunction)] = "[blue]";
            serviceCollection.AddSingleton<FunctionColorMatcher>(colorMap);

            var registrar = new TypeRegistrar(serviceCollection);
            var app = new CommandApp(registrar);

            app.Configure(config =>
            {
                config.AddCommand<AddFunctionCommand>("add");
                config.AddCommand<RemoveFunctionCommand>("remove");
                config.AddCommand<ClearFunctionsCommand>("clear");
                config.AddCommand<PrintAllFunctionsCommand>("print");
                config.AddCommand<EqualsFunctionsCommand>("equals");
                config.AddCommand<GetDerivativeCommand>("derivative");
                config.AddCommand<CalculateCommand>("calculate");
                config.AddCommand<GetMinValueDerivativesCommand>("minvalue");
                config.AddCommand<SortFunctionsCommand>("sort");

            });

            app.Run(args);
        }
    }
}

