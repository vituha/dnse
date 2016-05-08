namespace Log4NetTest.ConsoleHost
{
    internal class ConsoleApplcation : Core.ApplicationBase
    {
        public ConsoleApplcation() 
            : base(typeof(ConsoleApplcation).Name)
        { 
        }

        public override string NamespacePrefix
        {
            get { return "Log4NetTest"; }
        }
    }
}
