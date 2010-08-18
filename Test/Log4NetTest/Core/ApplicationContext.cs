using System;
using System.Diagnostics.Contracts;

namespace Log4NetTest.Core
{
    public static class ApplicationContext
    {
        public static IApplication Current { get; private set; }

        public static void Initialize(IApplication application) 
        {
            Contract.Requires(application != null);
            Contract.EndContractBlock();

            if (Current != null)
	        {
                throw new InvalidOperationException("Already initialized");	 
	        };

            application.InitDiagnostics();
            Current = application;
        }
    }
}
