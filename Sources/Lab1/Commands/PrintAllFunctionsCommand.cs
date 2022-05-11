using Lab1.Models;
using Lab1.Repositories;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;

namespace Lab1.Commands
{
    public class PrintAllFunctionsCommand : Command<PrintAllFunctionsCommand.PrintAllFunctionsSettings>
    {
        public class PrintAllFunctionsSettings : CommandSettings
        {

        }

        private readonly IFunctionsRepository _functionsRepository;
        private readonly FunctionColorMatcher _colorMatcher;

        public PrintAllFunctionsCommand(IFunctionsRepository functionsRepository, FunctionColorMatcher colorMatcher)
        {
            _functionsRepository = functionsRepository;
            _colorMatcher = colorMatcher;
        }


        public override int Execute([NotNull] CommandContext context, [NotNull] PrintAllFunctionsSettings settings)
        {
            var functions = _functionsRepository.GetFunctions();

            if (functions.Count == 0)
            {
                AnsiConsole.WriteLine("The list is empty!");
                return 0;
            }

            var table = new Table();
            table.AddColumn(new TableColumn(new Markup("[white]Name of function[/]")));
            table.AddColumn(new TableColumn("[white]Function[/]"));
            table.AddColumn(new TableColumn("[white]Derivative[/]"));
            table.AddColumn(new TableColumn("[white]Antiderivative[/]"));

            int count = 0;
            int maxCount = 10;
            foreach (Function f in functions)
            {
                var color = _colorMatcher[f.GetType()];
                table.AddRow($"{color}{f.GetType().Name}[/]",
                    $"{color}{f.ToString()}[/]",
                    $"{color}{f.GetDerivative()}[/]",
                    $"{color}{f.GetAntiderivative()} + C[/]");
                count += 1;
                if (count >= maxCount && functions.Count > maxCount)
                {
                    table.AddRow("[white]...[/]", "[white]...[/]", "[white]...[/]");
                }
            }
            AnsiConsole.Write(table);
            return 0;
        }
    }
}
