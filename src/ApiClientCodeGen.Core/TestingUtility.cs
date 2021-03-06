using System;
using System.Linq;
using System.Reflection;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Core
{
    public static class TestingUtility
    {
        static TestingUtility()
        {
            IsRunningFromUnitTest = AppDomain.CurrentDomain.GetAssemblies().Any(IsTestFramework);
        }

        private static bool IsTestFramework(Assembly assembly) 
            => assembly.FullName.Contains("Xunit") 
            || assembly.FullName.Contains("Test");

        public static bool IsRunningFromUnitTest { get; }
    }
}