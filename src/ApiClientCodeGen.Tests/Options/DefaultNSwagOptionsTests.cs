﻿using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Options.NSwag;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NJsonSchema.CodeGeneration.CSharp;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Options
{
    [TestClass]
    public class DefaultNSwagOptionsTests 
    {
        private readonly INSwagOptions sut = new DefaultNSwagOptions();

        [TestMethod, Xunit.Fact]
        public void ClassStyle_Be_Poco()
            => sut.ClassStyle.Should().Be(CSharpClassStyle.Poco);

        [TestMethod, Xunit.Fact]
        public void GenerateClientInterfaces_Be_True()
            => sut.GenerateClientInterfaces.Should().BeTrue();

        [TestMethod, Xunit.Fact]
        public void GenerateDtoTypes_Be_True()
            => sut.GenerateDtoTypes.Should().BeTrue();

        [TestMethod, Xunit.Fact]
        public void InjectHttpClient_Be_True()
            => sut.InjectHttpClient.Should().BeTrue();

        [TestMethod, Xunit.Fact]
        public void UseBaseUrl_Be_False()
            => sut.UseBaseUrl.Should().BeFalse();

        [TestMethod, Xunit.Fact]
        public void UseDocumentTitle_Be_True()
            => sut.UseDocumentTitle.Should().BeTrue();
    }
}