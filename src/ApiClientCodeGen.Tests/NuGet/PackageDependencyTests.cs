﻿using System;
using AutoFixture;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.NuGet;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.NuGet
{
    [TestClass]
    public class PackageDependencyTests
    {
        private string name;
        private Version version;
        private bool forceUpdate;
        private bool isSystemLibrary;
        private PackageDependency sut;

        [TestInitialize]
        public void Init()
        {
            var fixture = new Fixture();
            name = fixture.Create<string>();
            version = fixture.Create<Version>();
            forceUpdate = fixture.Create<bool>();
            isSystemLibrary = fixture.Create<bool>();
            sut = new PackageDependency(name, version, forceUpdate, isSystemLibrary);
        }

        [TestMethod, Xunit.Fact]
        public void Equals_Compares_Values()
            => sut.Equals(
                    new PackageDependency(sut.Name, sut.Version, sut.ForceUpdate))
                .Should()
                .BeTrue();

        [TestMethod, Xunit.Fact]
        public void GetHashCode_Compares_Values()
            => sut.GetHashCode()
                .Should()
                .Be(new PackageDependency(sut.Name, sut.Version, sut.ForceUpdate).GetHashCode());

        [TestMethod, Xunit.Fact]
        public void Name_Set()
            => sut.Name.Should().Be(name);

        [TestMethod, Xunit.Fact]
        public void Version_Set()
            => sut.Version.Should().Be(version);

        [TestMethod, Xunit.Fact]
        public void ForceUpdate_Set()
            => sut.ForceUpdate.Should().Be(forceUpdate);

        [TestMethod, Xunit.Fact]
        public void IsSystemLibrary_Set()
            => sut.IsSystemLibrary.Should().Be(isSystemLibrary);

        [TestMethod, Xunit.Fact]
        public void Name_NotBeNullOrWhiteSpace()
            => sut.Name.Should().NotBeNullOrWhiteSpace();

        [TestMethod, Xunit.Fact]
        public void Version_NotBeNull()
            => sut.Version.Should().NotBeNull();
    }
}
