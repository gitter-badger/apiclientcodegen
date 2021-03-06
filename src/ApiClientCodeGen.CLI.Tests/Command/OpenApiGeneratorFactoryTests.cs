using ApiClientCodeGen.CLI.Commands;
using ApiClientCodeGen.CLI.Tests.Infrastructure;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using FluentAssertions;
using Xunit;

namespace ApiClientCodeGen.CLI.Tests.Command
{
    public class OpenApiGeneratorFactoryTests
    {
        [Theory, AutoMoqData]
        public void Create_Should_Return_NotNull(
            OpenApiGeneratorFactory sut,
            string swaggerFile,
            string defaultNamespace,
            IGeneralOptions options,
            IProcessLauncher processLauncher)
            => sut.Create(
                    swaggerFile,
                    defaultNamespace,
                    options,
                    processLauncher)
                .Should()
                .NotBeNull();
    }
}