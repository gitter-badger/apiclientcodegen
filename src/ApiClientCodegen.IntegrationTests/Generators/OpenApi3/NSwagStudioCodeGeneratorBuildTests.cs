﻿using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.General;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows;
using FluentAssertions;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators.OpenApi3
{
    
    [Xunit.Trait("Category", "SkipWhenLiveUnitTesting")]
    public class NSwagStudioCodeGeneratorBuildTests : TestWithResources
    {
        private static Mock<IGeneralOptions> optionsMock;
        private static IGeneralOptions options;
        private readonly string code;

        public NSwagStudioCodeGeneratorBuildTests()
        {
            optionsMock = new Mock<IGeneralOptions>();
            optionsMock.Setup(c => c.NSwagPath).Returns(PathProvider.GetNSwagPath());
            options = optionsMock.Object;

            var contents = NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                    new EnterOpenApiSpecDialogResult(File.ReadAllText("Swagger_v3.json"), "Swagger", "https://petstore.swagger.io/v2/swagger.json"))
                .GetAwaiter()
                .GetResult();

            File.WriteAllText("Swagger_v3.nswag", contents);
            new NSwagStudioCodeGenerator(Path.GetFullPath("Swagger_v3.nswag"), options, new ProcessLauncher())
                .GenerateCode(new Mock<IProgressReporter>().Object)
                .Should()
                .BeNull();

            code = File.ReadAllText(Path.GetFullPath("PetstoreClient.cs"));
        }

        [Xunit.Fact]
        public void GeneratedCode_Can_Build_In_NetCoreApp() 
            => BuildHelper.BuildCSharp(ProjectTypes.DotNetCoreApp, code, SupportedCodeGenerator.NSwagStudio);

        [Xunit.Fact]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary() 
            => BuildHelper.BuildCSharp(ProjectTypes.DotNetStandardLibrary, code, SupportedCodeGenerator.NSwagStudio);
    }
}