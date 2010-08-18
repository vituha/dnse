// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Log4NetTest.ConsoleHost
{
    using System;
    using log4net;
    using Log4NetTest.Core;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILog logger = 
            LogManager.GetLogger(typeof(Program).FullName.Strip(ApplicationContext.Current.NamespacePrefix));

        /// <summary>
        /// The main routine.
        /// </summary>
        private static void Main()
        {
            ApplicationContext.Initialize(new ConsoleApplcation());
            new Program().Run();
        }

        /// <summary>
        /// The run.
        /// </summary>
        private void Run()
        {
            logger.InfoFormat("App is running");
            Console.ReadKey();
        }
    }
}