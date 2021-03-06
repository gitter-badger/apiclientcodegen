﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core
{
    public static class DependencyDownloader
    {
        public static void InstallAutoRest() => InstallNpmPackage("autorest");

        public static void InstallNSwag() => InstallNpmPackage("nswag");

        private static void InstallNpmPackage(string packageName)
        {
            Trace.WriteLine($"Attempting to install {packageName} through NPM");
            
            var processLauncher = new ProcessLauncher();
            var npmPath = NpmHelper.GetNpmPath();
            processLauncher.Start(
                npmPath,
                $"install -g {packageName}");

            Trace.WriteLine($"{packageName} installed successfully through NPM");
        }

        public static string InstallOpenApiGenerator(string path = null, bool forceDownload = false)
            => InstallJarFile(
                path,
                "openapi-generator-cli.jar",
                Resource.OpenApiGenerator_MD5,
                Resource.OpenApiGenerator_DownloadUrl,
                forceDownload);

        public static string InstallSwaggerCodegenCli(string path = null, bool forceDownload = false)
            => InstallJarFile(
                path,
                "swagger-codegen-cli.jar",
                Resource.SwaggerCodegenCli_MD5,
                Resource.SwaggerCodegenCli_DownloadUrl,
                forceDownload);

        private static string InstallJarFile(string path, string jar, string md5, string url, bool forceDownload)
        {
            if (string.IsNullOrWhiteSpace(path))
                path = Path.Combine(Path.GetTempPath(), jar);

            if (!File.Exists(path) || FileHelper.CalculateChecksum(path) != md5 || forceDownload)
            {
                Trace.WriteLine($"{jar} not found. Attempting to download {jar}");
                
                var tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.jar");
                new WebClient().DownloadFile(url, tempFile);

                Trace.WriteLine($"{jar} downloaded successfully");

                try
                {
                    if (File.Exists(path))
                        File.Delete(path);
                    File.Move(tempFile, path);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }
            }

            return path;
        }
    }
}