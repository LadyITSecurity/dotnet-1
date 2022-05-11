using Lab1.Models;
using Lab1.Repositories;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;

namespace Lab1.Commands
{
    public class EqualsFunctionsCommand : Command<EqualsFunctionsCommand.EqualsFunctionsSettings>
    {
        public class EqualsFunctionsSettings : CommandSettings
        {

        }

        private readonly IFunctionsRepository _functionsRepository;
        private readonly FunctionColorMatcher _colorMatcher;

        public EqualsFunctionsCommand(IFunctionsRepository functionsRepository, FunctionColorMatcher colorMatcher)
        {
            _functionsRepository = functionsRepository;
            _colorMatcher = colorMatcher;
        }


        public override int Execute([NotNull] CommandContext context, [NotNull] EqualsFunctionsSettings settings)
        {
            var functions = _functionsRepository.GetFunctions();
            var index1 = AnsiConsole.Prompt(
                new TextPrompt<int>("[blue]Enter the index of the first function in the collection to compare: [/]"));

            var index2 = AnsiConsole.Prompt(
                new TextPrompt<int>("[blue]Enter the index of the second function in the collection to compare: [/]"));

            List<Function> result = new();
            result.Add(functions[index1]);
            result.Add(functions[index2]);

            var table = new Table();
            table.AddColumn(new TableColumn(new Markup("[white]Name of function[/]")));
            table.AddColumn(new TableColumn("[white]Function[/]"));
            table.AddColumn(new TableColumn("[white]Derivative[/]"));
            table.AddColumn(new TableColumn("[white]Antiderivative[/]"));
            foreach (Function f in result)
            {
                var color = _colorMatcher[f.GetType()];
                table.AddRow($"{color}{f.GetType().Name}[/]",
                    $"{color}{f.ToString()}[/]",
                    $"{color}{f.GetDerivative()}[/]",
                    $"{color}{f.GetAntiderivative()} + C[/]");
            }

            AnsiConsole.Write(table);
            AnsiConsole.WriteLine(result[0].Equals(result[1]));
            return 0;
        }
    }
}
