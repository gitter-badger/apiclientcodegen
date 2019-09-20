﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class NotInstalledException : Exception
    {
        public NotInstalledException() { }
        public NotInstalledException(string message) : base(message) { }
        public NotInstalledException(string message, Exception inner) : base(message, inner) { }
        protected NotInstalledException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
