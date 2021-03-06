﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core.Generators
{
    public interface IProcessLauncher
    {
        void Start(
            string command,
            string arguments,
            string workingDirectory = null);

        void Start(
            string command,
            string arguments,
            Action<string> onOutputData,
            Action<string> onErrorData,
            string workingDirectory = null);
    }

    [ExcludeFromCodeCoverage]
    public class ProcessLauncher : IProcessLauncher
    {
        private static readonly object SyncLock = new object();

        public void Start(
            string command,
            string arguments,
            string workingDirectory = null)
            => Start(
                command,
                arguments,
                o => Trace.WriteLine(o),
                e => Trace.WriteLine(e),
                workingDirectory);

        public void Start(
            string command,
            string arguments,
            Action<string> onOutputData,
            Action<string> onErrorData,
            string workingDirectory = null)
        {
            Trace.WriteLine("Executing:");
            Trace.WriteLine($"{command} {arguments}");

            lock (SyncLock)
                StartInternal(
                    command,
                    arguments,
                    onOutputData,
                    onErrorData,
                    workingDirectory);
        }

        private static void StartInternal(
            string command,
            string arguments,
            Action<string> onOutputData,
            Action<string> onErrorData,
            string workingDirectory = null)
        {
            var processInfo = new ProcessStartInfo(command, arguments);
            using (var process = new Process {StartInfo = processInfo})
            {
                process.OutputDataReceived += (s, e) => onOutputData?.Invoke(e.Data);
                process.ErrorDataReceived += (s, e) => onErrorData?.Invoke(e.Data);
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.EnvironmentVariables["CORECLR_ENABLE_PROFILING"] = "0";
                process.StartInfo.EnvironmentVariables["COR_ENABLE_PROFILING"] = "0";

                if (workingDirectory != null)
                    process.StartInfo.WorkingDirectory = workingDirectory;

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();

                if (process.ExitCode != 0)
                    throw new InvalidOperationException($"{command} failed");
            }
        }
    }
}