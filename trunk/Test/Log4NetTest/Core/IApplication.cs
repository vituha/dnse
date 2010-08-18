using System;
namespace Log4NetTest.Core
{
    public interface IApplication
    {
        string Name { get; }
        string NamespacePrefix { get; }

        void InitDiagnostics();
    }
}
