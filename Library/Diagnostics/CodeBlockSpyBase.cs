using System;
using System.Diagnostics;
using System.Reflection;

namespace VS.Library.Diagnostics
{
    public abstract class CodeBlockSpyBase
    {
        protected internal class Pin : IDisposable
        {
            private CodeBlockSpyBase CodeSpyBase;

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
            
            private bool disposed = false;
            public Pin(CodeBlockSpyBase CodeSpyBase, object instance, MethodBase method, object context)
            {
                Debug.Assert(CodeSpyBase != null);
                this.CodeSpyBase = CodeSpyBase;
                this.instance = instance;
                this.method = method;
                this.context = context;
            }

            #region IDisposable Members

            public void Dispose()
            {
                if (!disposed)
                {
                    CodeSpyBase.DoBlockExited(this);
                    this.disposed = true;
                }
            }
            #endregion
        }

        public IDisposable DoSpy(object instance, MethodBase method, object context)
        {
            Pin pin = new Pin(this, instance, method, context);
            DoBlockEntered(pin);
            return pin;
        }

        protected abstract void DoBlockEntered(Pin pin);

        protected abstract void DoBlockExited(Pin pin);
    }
}
