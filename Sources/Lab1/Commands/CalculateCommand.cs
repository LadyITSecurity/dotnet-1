using Lab1.Repositories;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;

namespace Lab1.Commands
{
    public class CalculateCommand : Command<CalculateCommand.CalculateSettings>
    {
        public class CalculateSettings : CommandSettings
        {

        }

        private readonly IFunctionsRepository _functionsRepository;
        private readonly FunctionColorMatcher _colorMatcher;

        public CalculateCommand(IFunctionsRepository functionsRepository, FunctionColorMatcher colorMatcher)
        {
            _functionsRepository = functionsRepository;
            _colorMatcher = colorMatcher;
        }


        public override int Execute([NotNull] CommandContext context, [NotNull] CalculateSettings settings)
        {
            var index = AnsiConsole.Prompt(
                new TextPrompt<int>("[blue]Enter function index to calculate function value: [/]"));

            var functions = _functionsRepository.GetFunctions();
            var f = functions[index];

            var table = new Table();
            table.AddColumn(new TableColumn(new Markup("[white]Name of function[/]")));
            table.AddColumn(new TableColumn("[white]Function[/]"));
            table.AddColumn(new TableColumn("[white]Derivative[/]"));
            table.AddColumn(new TableColumn("[white]Antiderivative[/]"));
            var color = _colorMatcher[f.GetType()];
            table.AddRow($"{color}{f.GetType().Name}[/]",
                $"{color}{f.ToString()}[/]",
                $"{color}{f.GetDerivative()}[/]",
                $"{color}{f.GetAntiderivative()} + C[/]");
            AnsiConsole.Write(table);
            AnsiConsole.WriteLine(Math.Round(f.Calculate(
                        AnsiConsole.Prompt(new TextPrompt<double>("[blue]Enter the x value to calculate the value of the function: [/]"))), 3).ToString());
            return 0;
        }
    }
}
