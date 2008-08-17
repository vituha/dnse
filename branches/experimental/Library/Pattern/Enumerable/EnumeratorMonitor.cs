using System;
using System.Collections.Generic;
using System.Text;

namespace VS.Library.Pattern.Enumerable
{
    /// <summary>
    /// A IEnumerator wrapper. Fires events at different stages of enumeration
    /// </summary>
    /// <typeparam name="T">T for IEnumerator</typeparam>
    public class EnumeratorMonitor<T> : IEnumerator<T>, IEnumerable<T>
    {
        private IEnumerator<T> enumerator;
        private bool active;

        #region Constructors
        protected EnumeratorMonitor(IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
        }
        #endregion

        #region Events
        public event EventHandler EnumerationStarting;
        protected virtual void OnEnumerationStarting()
        {
            if (EnumerationStarting != null)
            {
                EnumerationStarting(this, EventArgs.Empty);
            }
        }

        public event EventHandler EnumerationProgress;
        protected virtual void OnEnumerationProgress()
        {
            if (EnumerationProgress != null)
            {
                EnumerationProgress(this, EventArgs.Empty);
            }
        }
        public event EventHandler EnumerationEnded;
        protected virtual void OnEnumerationEnded()
        {
            if (EnumerationEnded != null)
            {
                EnumerationEnded(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Interfaces
        public T Current
        {
            get { return this.enumerator.Current; }
        }

        public void Dispose()
        {
            this.enumerator.Dispose();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.enumerator.Current; }
        }

        public bool MoveNext()
        {
            if (active == false)
            {
                OnEnumerationStarting();
                active = true;
            }
            bool atEnd = !this.enumerator.MoveNext();
            if (atEnd)
            {
                OnEnumerationEnded();
                active = true;
            }
            else
            {
                OnEnumerationProgress();
            }
            return !atEnd;
        }

        public void Reset()
        {
            this.enumerator.Reset();
        }
        #endregion

        public static EnumeratorMonitor<T> Create(IEnumerator<T> enumerator)
        {
            return new EnumeratorMonitor<T>(enumerator);
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this;
        }

        #endregion
    }
}
