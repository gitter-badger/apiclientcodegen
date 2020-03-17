﻿using System.IO;
using System.Threading.Tasks;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Generators.NSwagStudio
{
    [TestClass]
    [DeploymentItem("Resources/Swagger.nswag")]
    [DeploymentItem("Resources/Swagger.json")]
    public class NSwagStudioFileHelperTests
    {
        private Mock<INSwagStudioOptions> mock;

        [TestInitialize]
        public async Task InitAsync()
        {
            mock = new Mock<INSwagStudioOptions>();

            await NSwagStudioFileHelper.CreateNSwagStudioFileAsync(
                new EnterOpenApiSpecDialogResult(File.ReadAllText("Swagger.json"), "Swagger", "https://petstore.swagger.io/v2/swagger.json"),
                mock.Object);
        }

        [TestMethod, Xunit.Fact]
        public void Reads_InjectHttpClient_From_Options()
            => mock.Verify(c => c.InjectHttpClient);

        [TestMethod, Xunit.Fact]
        public void Reads_GenerateClientInterfaces_From_Options()
            => mock.Verify(c => c.GenerateClientInterfaces);

        [TestMethod, Xunit.Fact]
        public void Reads_GenerateDtoTypes_From_Options()
            => mock.Verify(c => c.GenerateDtoTypes);

        [TestMethod, Xunit.Fact]
        public void Reads_UseBaseUrl_From_Options()
            => mock.Verify(c => c.UseBaseUrl);

        [TestMethod, Xunit.Fact]
        public void Reads_ClassStyle_From_Options()
            => mock.Verify(c => c.ClassStyle);

        [TestMethod, Xunit.Fact]
        public void Reads_GenerateResponseClasses_From_Options()
            => mock.Verify(c => c.GenerateResponseClasses);

        [TestMethod, Xunit.Fact]
        public void Reads_GenerateJsonMethods_From_Options()
            => mock.Verify(c => c.GenerateJsonMethods);

        [TestMethod, Xunit.Fact]
        public void Reads_RequiredPropertiesMustBeDefined_From_Options()
            => mock.Verify(c => c.RequiredPropertiesMustBeDefined);

        [TestMethod, Xunit.Fact]
        public void Reads_GenerateDefaultValues_From_Options()
            => mock.Verify(c => c.GenerateDefaultValues);

        [TestMethod, Xunit.Fact]
        public void Reads_GenerateDataAnnotations_From_Options()
            => mock.Verify(c => c.GenerateDataAnnotations);
    }
}
