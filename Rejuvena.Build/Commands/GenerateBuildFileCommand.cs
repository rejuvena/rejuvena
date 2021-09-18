#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using Rejuvena.BuildConfigurer;
using Spectre.Console;

namespace Rejuvena.Build.Commands
{
    [Command]
    public class GenerateBuildFileCommand : ICommand
    {
        public class RejuvenaBuild : BuildFile
        {
            public RejuvenaBuild()
            {
                DisplayName = "Rejuvena";
                Author = "Tomat and ENNWAY";
                ModReferences = SortAfter = new List<string> {"TomatoLib"};
                BuildIgnore = new List<string>
                {
                    ".git/*",
                    ".vs/*",
                    "bin/*",
                    "obj/*",
                    "TomatoLib/*",
                    "Rejuvena.Tests/*",
                    "Rejuvena.Build/*",
                    "workshop.json"
                };
            }
        }

        [CommandOption("version", 'v', Description = "Assembly version.", IsRequired = true)]
        public string Version { get; init; } = "0.0.0.1";

        [CommandOption("output-dir", 'o', Description = "File output directory.", IsRequired = true)]
        public string OutputDir { get; init; }

        public virtual async ValueTask ExecuteAsync(IConsole console)
        {
            RejuvenaBuild build = new()
            {
                Version = System.Version.Parse(Version)
            };

            AnsiConsole.WriteLine($"Writing to file: {OutputDir}");

            await File.WriteAllTextAsync(OutputDir, build.WriteFile());
        }
    }
}