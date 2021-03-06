﻿using System;
using System.Diagnostics.CodeAnalysis;
using ApiClientCodeGen.CLI.Logging;
using McMaster.Extensions.CommandLineUtils;

namespace ApiClientCodeGen.CLI.Commands
{
    [ExcludeFromCodeCoverage]
    [Command(Name = "run", OptionsComparison = StringComparison.InvariantCultureIgnoreCase)]
    [Subcommand(
        typeof(AutoRestCommand),
        typeof(NSwagCommand),
        typeof(SwaggerCodegenCommand),
        typeof(OpenApiGeneratorCommand))]
    public class RootCommand
    {
        [Option(VerboseOption.Template, CommandOptionType.NoValue, Description = VerboseOption.Description)]
        public bool Verbose { get; set; }

        public int OnExecute(CommandLineApplication app)
        {
            app.ShowHelp(false);
            return 0;
        }
    }
}
