using System;

namespace Immutability
{
    public class Immutable : IImmutable
    {
        private struct Mutator : IImmutableKey
        {
            private readonly Immutable _immutable;
            public Mutator(Immutable immutable)
            {
                _immutable = immutable;
            }

            public void Unlock()
            {
                _immutable.Unlock();
            }
        }

        public Immutable()
            : this(false)
        {
        }

        public Immutable(bool isImmutable)
        {
            IsImmutable = isImmutable;
        }

        public IImmutableKey Lock()
        {
            if (IsImmutable)
            {
                throw new ImmutabilityException("Object is already immutable");
            }

            if (!TryLock()) return null;

            IsImmutable = true;
            OnImmutabilityChanged();
            return new Mutator(this);
        }

        private void Unlock()
        {
            if (!IsImmutable)
            {
                throw new ImmutabilityException("Object is not immutable");
            }

            if (!TryUnlock()) return;

            IsImmutable = false;
            OnImmutabilityChanged();
        }

        protected virtual bool TryLock() 
        {
            return true;
        }

        protected virtual bool TryUnlock()
        {
            return true;
        }

        public bool IsImmutable
        {
            get; private set;
        }

        protected virtual void OnImmutabilityChanged() { }

        public override string ToString()
        {
            return String.Format("IsImmutable: {0}", IsImmutable);
        }

        protected void ThrowIfImmutable()
        {
            if (IsImmutable)
            {
                throw new ImmutabilityException("Object is immutable");
            }
        }
    }
}