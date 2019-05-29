﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.AddNew
{
    public class NewRestClientCommand : ICommandInitializer
    {
        public const string ContextGuid = "7CEC8679-C1B8-48BF-9FA4-5FAA38CBE0FA";
        public const string Name = "NSwag Studio Context";
        public const string Expression = "nswag";
        public const string TermValue = "HierSingleSelectionName:.nswag$";

        protected int CommandId { get; } = 0x100;
        protected Guid CommandSet { get; } = new Guid("E4B99F94-D11F-4CAA-ADCD-24302C232938");

        public async Task InitializeAsync(AsyncPackage package, CancellationToken token)
        {
            await package.SetupCommandAsync(CommandSet, CommandId, OnExecuteAsync, token);
        }

        private static async Task OnExecuteAsync(DTE dte, AsyncPackage package)
        {
            var result = EnterOpenApiSpecDialog.GetResult();
            if (result == null)
                return;

            var selectedItem = ProjectExtensions.GetSelectedItem();
            var folder = FindFolder(selectedItem, dte);
            if (string.IsNullOrWhiteSpace(folder))
            {
                Trace.WriteLine("Unable to get folder name");
                return;
            }

            var contents = result.OpenApiSpecification;
            if (result.SelectedCodeGenerator == SupportedCodeGenerator.NSwagStudio)
            {
                var outputNamespace = ProjectExtensions.GetActiveProject(dte)?.GetTopLevelNamespace();
                contents = await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                    result.OpenApiSpecification,
                    result.Url,
                    outputNamespace);
            }

            var filePath = Path.Combine(folder, result.OutputFilename);
            File.WriteAllText(filePath, contents);

            var fileInfo = new FileInfo(filePath);
            var project = ProjectExtensions.GetActiveProject(dte);
            var projectItem = project.AddFileToProject(dte, fileInfo);

            if (result.SelectedCodeGenerator != SupportedCodeGenerator.NSwagStudio)
            {
                var customTool = result.SelectedCodeGenerator.GetCustomToolName();
                projectItem.Properties.Item("CustomTool").Value = customTool;
            }
            else
            {
                var generator = new NSwagStudioCodeGenerator(filePath);
                generator.GenerateCode(null);
                dynamic nswag = JsonConvert.DeserializeObject(contents);
                var nswagOutput = nswag.codeGenerators.swaggerToCSharpClient.output.ToString();
                project.AddFileToProject(dte, new FileInfo(Path.Combine(folder, nswagOutput)));
            }

            await project.InstallMissingPackagesAsync(package, result.SelectedCodeGenerator);
        }

        private static string FindFolder(object item, DTE dte)
        {
            switch (item)
            {
                case ProjectItem projectItem:
                    return File.Exists(projectItem.FileNames[1])
                        ? Path.GetDirectoryName(projectItem.FileNames[1])
                        : projectItem.FileNames[1];

                case Project project:
                    return project.GetRootFolder(dte);

                default:
                    return null;
            }
        }
    }
}