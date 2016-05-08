using System.Diagnostics.Contracts;

namespace Log4NetTest.Core
{
    public abstract class ApplicationBase : IApplication
    {
        public string Name { get; private set; }

        public abstract string NamespacePrefix { get; }

        protected ApplicationBase(string name)
        {
            Contract.Requires(name != null);
            Contract.EndContractBlock();

            Name = name;
        }

        public virtual void InitDiagnostics()
        {
            //log4net.Config.BasicConfigurator.Configure();
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("log4net.xml"));
        }
    }
}
