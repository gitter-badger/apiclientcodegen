﻿using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwag;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.AutoRest;

namespace ApiClientCodeGen.CLI.Commands
{
    public interface IAutoRestCodeGeneratorFactory
    {
        ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory documentFactory);
    }

    public class AutoRestCodeGeneratorFactory : IAutoRestCodeGeneratorFactory
    {
        public ICodeGenerator Create(
            string swaggerFile,
            string defaultNamespace,
            IAutoRestOptions options,
            IProcessLauncher processLauncher,
            IOpenApiDocumentFactory documentFactory)
            => new AutoRestCSharpCodeGenerator(
                swaggerFile,
                defaultNamespace,
                options,
                processLauncher,
                documentFactory);
    }
}