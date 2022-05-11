﻿using Lab1.Models;
using Lab1.Repositories;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Diagnostics.CodeAnalysis;

namespace Lab1.Commands
{
    public class SortFunctionsCommand : Command<SortFunctionsCommand.SortFunctionsSettings>
    {
        public class SortFunctionsSettings : CommandSettings
        {

        }

        private readonly IFunctionsRepository _functionsRepository;
        private readonly FunctionColorMatcher _colorMatcher;

        public SortFunctionsCommand(IFunctionsRepository functionsRepository, FunctionColorMatcher colorMatcher)
        {
            _functionsRepository = functionsRepository;
            _colorMatcher = colorMatcher;
        }


        public override int Execute([NotNull] CommandContext context, [NotNull] SortFunctionsSettings settings)
        {
            var functions = _functionsRepository.GetFunctions();
            functions.Sort(new FunctionComparator());

            string storageFileName = "sort_functions.xml";
            var result = new XmlFunctionsRepository(storageFileName, functions);

            var table = new Table();
            table.AddColumn(new TableColumn(new Markup("[white]Name of Function[/]")));
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
            return 0;
        }
    }
}