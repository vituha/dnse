using System;
using System.Diagnostics;
using System.Reflection;

namespace VS.Library.Diagnostics
{
    public class Pin : IDisposable
    {
        private CodeSpyBase CodeSpyBase;

        private object instance;

        public object Instance
        {
            get { return instance; }
        }
        private MethodBase method;

        public MethodBase Method
        {
            get { return method; }
        }
        private object context;

        public object Context
        {
            get { return context; }
        }

        public Pin(CodeSpyBase codeSpyBase, object instance, MethodBase method, object context)
        {
            Debug.Assert(codeSpyBase != null);
            this.CodeSpyBase = codeSpyBase;
            this.instance = instance;
            this.method = method;
            this.context = context;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (CodeSpyBase != null)
                {
                    CodeSpyBase.DoBlockExited(this);
                    this.CodeSpyBase = null;
                }
            }
        }
        #endregion
    }
    
    public abstract class CodeSpyBase
	{
		public IDisposable BeginSpy(object instance, MethodBase method, object context)
		{
			Pin pin = new Pin(this, instance, method, context);
			DoBlockEntered(pin);
			return pin;
		}

		protected abstract void DoBlockEntered(Pin pin);

		protected internal abstract void DoBlockExited(Pin pin);
	}
}
