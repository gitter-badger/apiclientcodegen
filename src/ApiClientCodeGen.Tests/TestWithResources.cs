﻿using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests.Resources;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Tests
{
    [ExcludeFromCodeCoverage]
    public abstract class TestWithResources
    {
        private static readonly object SyncLock = new object();

        protected TestWithResources()
        {
            lock (SyncLock)
            {
                CreateFileFromEmbeddedResource("Swagger.json");
                CreateFileFromEmbeddedResource("Swagger.yaml");
                CreateFileFromEmbeddedResource("Swagger.nswag");
                CreateFileFromEmbeddedResource("Swagger_v3.json");
                CreateFileFromEmbeddedResource("Swagger_v3.yaml");
                CreateFileFromEmbeddedResource("Swagger_v3.nswag");
            }
            
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12 |
                                                   SecurityProtocolType.Ssl3;
        }

        private static void CreateFileFromEmbeddedResource(string resourceName)
        {
            var directory = Directory.GetCurrentDirectory();
            using var source = EmbeddedResources.GetStream(resourceName);
            using var writer = File.Create(Path.Combine(directory, resourceName));
            source.CopyTo(writer);
        }

        protected string ReadAllText(string resourceName)
        {
            using var stream = EmbeddedResources.GetStream(resourceName);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}