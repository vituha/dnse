using System;
using System.Collections.Generic;
using System.Text;
using VS.Library.Generics.Common;

namespace VS.Library.Cache
{
    public class LazyAction : ActionBase
    {
        private D startDelegate;
        private D endDelegate;

        public LazyAction()
        {
        }

        public LazyAction(D endDelegate)
            : this(null, endDelegate)
        {
        }

        public LazyAction(D startDelegate, D endDelegate)
        {
            ReSpawn(startDelegate, endDelegate);
        }

        public void ReSpawn(D startDelegate, D endDelegate)
        {
            LazyAction.ValidateDelegate(endDelegate);
            this.startDelegate = startDelegate;
            this.endDelegate = endDelegate;
            base.ReSpawn();
        }

        protected override void DoStartAction()
        {
            if (startDelegate != null)
                startDelegate();
        }

        protected override void DoEndAction()
        {
            endDelegate();
        }

        #region - Dispose Pattern -
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                this.startDelegate = this.endDelegate = null;
            }
        }
        #endregion

        private static void ValidateDelegate(D _delegate)
        {
            if (_delegate == null)
            {
                throw new ArgumentException("null passed as delegate");
            }
        }
    }
}
