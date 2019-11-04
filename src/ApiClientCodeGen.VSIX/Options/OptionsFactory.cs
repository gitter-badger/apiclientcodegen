﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.Shell;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Options
{
    [ExcludeFromCodeCoverage]
    public class OptionsFactory : IOptionsFactory
    {
        public TOptions Create<TOptions, TDialogPage>()
            where TOptions : class
            => VsPackage.Instance.GetDialogPage(typeof(TDialogPage)) as TOptions;
    }
}
