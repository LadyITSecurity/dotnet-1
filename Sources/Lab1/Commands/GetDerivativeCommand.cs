﻿using Lab1.Repositories;
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
    internal class GetDerivativeCommand : Command<GetDerivativeCommand.GetDerivativeSettings>
    {
        public class GetDerivativeSettings : CommandSettings
        {

        }

        private readonly IFunctionsRepository _functionsRepository;

        public GetDerivativeCommand(IFunctionsRepository functionsRepository)
        {
            _functionsRepository = functionsRepository;
        }


        public override int Execute([NotNull] CommandContext context, [NotNull] GetDerivativeSettings settings)
        {
            if (_functionsRepository == null)
                return 1;

            AnsiConsole.WriteLine(_functionsRepository.GetFunction(
                AnsiConsole.Prompt(new TextPrompt<int>("[blue]Индекс функции в коллекции для вычисления производной: [/]")))
                        .GetDerivative().ToString());
            return 0;
        }
    }
}