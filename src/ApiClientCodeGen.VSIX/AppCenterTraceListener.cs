using System;
using System.Diagnostics;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.VisualStudio.Threading;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient
{
    public class AppCenterTraceListener : TraceListener
    {
        public AppCenterTraceListener()
        {
            AppCenter.Start(
                "aa732165-2dbb-44ec-ad72-89c6c0c62d5f",
                typeof(Analytics), typeof(Crashes));
        }

        public static void Initialize()
            => Trace.Listeners.Add(new AppCenterTraceListener());

        public override void Write(string message)
        {
        }

        public override void WriteLine(string message)
        {
        }

        public override void Write(object o)
        {
            base.Write(o);

            if (o is Exception exception)
                System.Threading.Tasks.Task
                    .Run(() => Crashes.TrackError(exception))
                    .Forget();
        }
    }
}
