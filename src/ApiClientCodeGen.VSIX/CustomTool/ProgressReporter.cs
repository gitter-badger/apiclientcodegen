﻿using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.CustomTool
{
    public class ProgressReporter : IProgressReporter
    {
        private readonly IVsGeneratorProgress pGenerateProgress;

        public ProgressReporter(IVsGeneratorProgress pGenerateProgress)
        {
            this.pGenerateProgress = pGenerateProgress;
        }

        public void Progress(int progress)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            pGenerateProgress.Progress((uint)progress, 100);
        }
    }
}
