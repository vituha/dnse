using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;

namespace VS.Library.Generics.Comparison
{
    public class FieldComparer<T, FieldT>: Comparer<T>
    {
        private FieldInfo info;
        private IComparer<FieldT> fieldComparer;

        public FieldComparer(string fieldName, IComparer<FieldT> fieldComparer)
        {
            info = typeof(T).GetField(fieldName);
            this.fieldComparer = fieldComparer;
        }

        public FieldComparer(string fieldName)
            : this(fieldName, Comparer<FieldT>.Default) { }

        public override int Compare(T x, T y)
        {
            return fieldComparer.Compare((FieldT)info.GetValue(x), (FieldT)info.GetValue(y));
        }
    }    
}
