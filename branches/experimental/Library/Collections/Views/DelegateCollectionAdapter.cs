namespace VS.Library.Collections.Views
{
    using System;
    using System.Collections.Generic;

    public class DelegateCollectionAdapter<TInput, TOutput> : CollectionAdapterBase<TInput, TOutput>
    {
        private readonly Func<TInput, TOutput> _adapter;
        private readonly Func<TOutput, TInput> _reverseAdapter;

        public DelegateCollectionAdapter(ICollection<TInput> source, Func<TInput, TOutput> adapter, Func<TOutput, TInput> reverseAdapter = null)
            : base(source)
        {
            if (adapter == null) throw new ArgumentNullException("adapter");
            _adapter = adapter;

            _reverseAdapter = reverseAdapter; // this can be null
        }

        protected override TOutput Adapt(TInput item)
        {
            return _adapter(item);
        }

        protected override TInput AdaptBack(TOutput item)
        {
            Func<TOutput, TInput> adapter = _reverseAdapter;
            if (adapter == null)
            {
                return base.AdaptBack(item);
            }
            return adapter(item);
        }
    }
}
