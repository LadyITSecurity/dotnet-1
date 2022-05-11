using Lab1.Repositories;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;

namespace Lab1.Commands
{
    internal class GetDerivativeCommand : Command<GetDerivativeCommand.GetDerivativeSettings>
    {
        public class GetDerivativeSettings : CommandSettings
        {

        }

        private readonly IFunctionsRepository _functionsRepository;
        private readonly FunctionColorMatcher _colorMatcher;

        public GetDerivativeCommand(IFunctionsRepository functionsRepository, FunctionColorMatcher colorMatcher)
        {
            _functionsRepository = functionsRepository;
            _colorMatcher = colorMatcher;
        }


        public override int Execute([NotNull] CommandContext context, [NotNull] GetDerivativeSettings settings)
        {
            var index = AnsiConsole.Prompt(
                new TextPrompt<int>("[blue]Enter the index of the function to calculate the derivative(antiderivative): [/]"));

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
            return 0;
        }
    }
}
