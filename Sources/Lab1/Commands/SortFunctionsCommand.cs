﻿using Lab1.Models;
using Lab1.Repositories;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Commands
{
    public class SortFunctionsCommand : Command<SortFunctionsCommand.SortFunctionsSettings>
    {
        public class SortFunctionsSettings : CommandSettings
        {

        }

        private readonly IFunctionsRepository _functionsRepository;

        public SortFunctionsCommand(IFunctionsRepository functionsRepository)
        {
            _functionsRepository = functionsRepository;
        }


        public override int Execute([NotNull] CommandContext context, [NotNull] SortFunctionsSettings settings)
        {
            var functions = _functionsRepository.GetFunctions();
            int countFunctionType = 5;
            int functionCount = functions.Count;
            List<string> functionTypes = new List<string>()
                { "ConstantFunction", "LinearFunction", "QuadraticFunction", "SinusFunction", "CosinusFunction" };
            int tmpIndex = 0;
            for (int i = 0; i < countFunctionType; i++)
            {
                for (int j = tmpIndex; j < functionCount; j++)
                {
                    if (functions[j].GetType().Name == functionTypes[i])
                    {
                        functions.Insert(tmpIndex, functions[j]);
                        functions.RemoveAt(j + 1);
                        tmpIndex++;
                    }
                }
            }

            string storageFileName = "sort_functions.xml";
            var result = new XmlFunctionsRepository(storageFileName, functions);
            

            var table = new Table();
            table.AddColumn(new TableColumn(new Markup("[white]NameOfFunction[/]")));
            table.AddColumn(new TableColumn("[white]Function[/]"));
            table.AddColumn(new TableColumn("[white]Derivative[/]"));
            table.AddColumn(new TableColumn("[white]Antiderivative[/]"));

            foreach (Function f in functions)
            {
                switch (f.GetType().Name)
                {
                    case "ConstantFunction":
                        table.AddRow($"[yellow]{f.GetType().Name}[/]", $"[yellow]{f.ToString()}[/]", $"[yellow]{f.GetDerivative()}[/]", $"[yellow]{f.GetAntiderivative()} + C[/]");
                        break;
                    case "LinearFunction":
                        table.AddRow($"[green]{f.GetType().Name}[/]", $"[green]{f.ToString()}[/]", $"[green]{f.GetDerivative()}[/]", $"[green]{f.GetAntiderivative()} + C[/]");
                        break;
                    case "QuadraticFunction":
                        table.AddRow($"[magenta]{f.GetType().Name}[/]", $"[magenta]{f.ToString()}[/]", $"[magenta]{f.GetDerivative()}[/]", $"[magenta]{f.GetAntiderivative()} + C[/]");
                        break;
                    case "SinusFunction":
                        table.AddRow($"[cyan]{f.GetType().Name}[/]", $"[cyan]{f.ToString()}[/]", $"[cyan]{f.GetDerivative()}[/]", $"[cyan]{f.GetAntiderivative()} + C[/]");
                        break;
                    case "CosinusFunction":
                        table.AddRow($"[blue]{f.GetType().Name}[/]", $"[blue]{f.ToString()}[/]", $"[blue]{f.GetDerivative()}[/]", $"[blue]{f.GetAntiderivative()} + C[/]");
                        break;
                }
            }
            AnsiConsole.Write(table);
            return 0;
        }
    }
}