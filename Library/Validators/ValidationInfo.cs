using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.Library.Common;
using VS.Library.Conversions;
using VS.Library.Booleans;

namespace VS.Library.Validators
{
    public class ValidationInfo<T> : IExternallyDisposable
    {
        public ValidationInfo(T value, Predicate<T> validator, string name)
        {
            Validator = NullUtils.Coalesce(validator, EmptyValidator);
            Name = NullUtils.Coalesce(name, String.Empty);
        }

        public T Value { get; private set; }
        public Predicate<T> Validator { get; private set; }
        public string Name { get; private set; }

        public bool Valid { get; private set; }

        public Predicate<T> EmptyValidator
        {
            get
            {
                return CommonPredicates<T>.True;
            }
        }

        private void DisposeManaged() {
            Value = default(T); 
            Name = null;
        }

        #region IExternallyDisposable Members

        public IDisposable GetDisposer()
        {
            return new Disposer(DisposeManaged, null);

        }

        #endregion
    }
}
