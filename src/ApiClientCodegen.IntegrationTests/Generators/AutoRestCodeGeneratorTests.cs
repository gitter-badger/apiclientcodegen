﻿using System;
using System.IO;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.AutoRest;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Build;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Utility;
using FluentAssertions;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.IntegrationTests.Generators
{
    [TestClass]
    [TestCategory("SkipWhenLiveUnitTesting")]
    [DeploymentItem("Resources/Swagger.json")]
    public class AutoRestCodeGeneratorTests
    {
        private static readonly Mock<IVsGeneratorProgress> mock = new Mock<IVsGeneratorProgress>();
        private static string code = null;

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            var codeGenerator = new AutoRestCSharpCodeGenerator(
                Path.GetFullPath("Swagger.json"),
                typeof(AutoRestCodeGeneratorTests).Namespace);

            code = codeGenerator.GenerateCode(mock.Object);
        }

        [ClassCleanup]
        public static void CleanUp()
            => DependencyUninstaller.UninstallAutoRest();

        [TestMethod]
        public void AutoRest_CSharp_Generated_Code_NotNullOrWhitespace()
            => code.Should().NotBeNullOrWhiteSpace();

        [TestMethod]
        public void AutoRest_CSharp_Reports_Progres()
            => mock.Verify(
                c => c.Progress(It.IsAny<uint>(), It.IsAny<uint>()), 
                Times.AtLeastOnce);

        [TestMethod]
        public void GeneratedCode_Can_Build_In_NetCoreApp()
            => BuildHelper.BuildCSharp(ProjectTypes.DotNetCoreApp, code, SupportedCodeGenerator.AutoRest);

        [TestMethod]
        public void GeneratedCode_Can_Build_In_NetStandardLibrary()
            => BuildHelper.BuildCSharp(ProjectTypes.DotNetStandardLibrary, code, SupportedCodeGenerator.AutoRest);
    }
}
