using Lab1.Models;
using Lab1.Repositories;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;

namespace Lab1.Commands
{
    public class RemoveFunctionCommand : Command<RemoveFunctionCommand.RemoveFunctionSettings>
    {
        public class RemoveFunctionSettings : CommandSettings
        {

        }

        private readonly IFunctionsRepository _functionsRepository;
        private readonly FunctionColorMatcher _colorMatcher;

        public RemoveFunctionCommand(IFunctionsRepository functionsRepository, FunctionColorMatcher colorMatcher)
        {
            _functionsRepository = functionsRepository;
            _colorMatcher = colorMatcher;
        }

        public void Print(List<Function> functions)
        {
            var table = new Table();
            table.AddColumn(new TableColumn(new Markup("[white]Name of function[/]")));
            table.AddColumn(new TableColumn("[white]Function[/]"));
            table.AddColumn(new TableColumn("[white]Derivative[/]"));
            table.AddColumn(new TableColumn("[white]Antiderivative[/]"));

            foreach (Function f in functions)
            {
                var color = _colorMatcher[f.GetType()];
                table.AddRow($"{color}{f.GetType().Name}[/]",
                    $"{color}{f.ToString()}[/]",
                    $"{color}{f.GetDerivative()}[/]",
                    $"{color}{f.GetAntiderivative()} + C[/]");
            }
            AnsiConsole.Write(table);
        }

        public override int Execute([NotNull] CommandContext context, [NotNull] RemoveFunctionSettings settings)
        {
            var functions = _functionsRepository.GetFunctions();
            Print(functions);

            var index = AnsiConsole.Prompt(
                new TextPrompt<int>("[blue]Enter the index of the function to remove: [/]"));

            _functionsRepository.RemoveFunction(index);
            AnsiConsole.Clear();
            AnsiConsole.WriteLine("\t\tItem removed");
            functions = _functionsRepository.GetFunctions();
            Print(functions);
            return 0;
        }
    }
}
