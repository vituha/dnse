using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace VS.Library.Comparison
{
    /// <summary>
    /// Represents a simple <see cref="IComparer"/> implementation which compares objects by comparing their specific properties
    /// </summary>
    /// <example>
    /// ArrayList.Sort(myBusinessObjectList, new PropertyComparer("Priority", true))
    /// </example>
    public class PropertyComparer : IComparer
    {
        private string[] propertyNames;
        private bool negative;
        private IComparer[] customComparers;

        #region Constructors
        /// <summary>
        /// Creates a default positive comparer on "Key" property
        /// </summary>
        public PropertyComparer()
            : this("Key", false, Comparer.Default)
        {
        }
        /// <summary>
        /// Creates a default positive comparer
        /// </summary>
        /// <param name="propertyName">name of property to compare</param>
        public PropertyComparer(string propertyName)
            : this(propertyName, false, Comparer.Default)
        {
        }
        /// <summary>
        /// Creates a default comparer
        /// </summary>
        /// <param name="propertyName">name of property to compare</param>
        /// <param name="negative">true if result should be negated</param>
        public PropertyComparer(string propertyName, bool negative)
            : this(propertyName, negative, Comparer.Default)
        {
        }
        /// <summary>
        /// Creates a custom comparer
        /// </summary>
        /// <param name="propertyName">name of property to compare</param>
        /// <param name="negative">true if result should be negated</param>
        /// <param name="customComparer">comparer to use</param>
        public PropertyComparer(string propertyName, bool negative, IComparer customComparer)
        {
            this.propertyNames = new string[] { propertyName };
            this.negative = negative;
            this.customComparers = new IComparer[] { customComparer };
        }
        /// <summary>
        /// Creates a default comparer
        /// </summary>
        /// <param name="propertyNames">names of properties to compare in order</param>
        /// <param name="negative">true if result should be negated</param>
        public PropertyComparer(string[] propertyNames, bool negative)
            : this(propertyNames, negative, new IComparer[] { Comparer.Default })
        {
        }
        /// <summary>
        /// Creates a custom comparer
        /// </summary>
        /// <param name="propertyNames">names of properties to compare in order</param>
        /// <param name="negative">true if result should be negated</param>
        /// <param name="customComparers">comparers to use</param>
        public PropertyComparer(string[] propertyNames, bool negative, IComparer[] customComparers)
        {
            this.propertyNames = propertyNames;
            this.negative = negative;
            this.customComparers = customComparers;
        }
        #endregion

        /// <summary>
        /// Compares two objects.
        /// If number of participating property names is bigger than number of custom comparers then default comparers will be used for the rest.
        /// </summary>
        /// <param name="x">1st object</param>
        /// <param name="y">2nd object</param>
        /// <returns>
        /// a configured comparer's return value optionally negated if <c>negative</c> is true
        /// </returns>
        int IComparer.Compare(object x, object y)
        {
            try
            {
                int result = 0;
                if (this.propertyNames.Length > 0)
                {
                    int i = 0;
                    string propertyName;
                    IComparer comparer;
                    do
                    {
                        propertyName = this.propertyNames[i];
                        comparer = (i < this.customComparers.Length ? this.customComparers[i] : Comparer.Default);
                        object vx = x.GetType().GetProperty(propertyName).GetValue(x, null);
                        object vy = y.GetType().GetProperty(propertyName).GetValue(y, null);
                        result = comparer.Compare(vx, vy);
                        i++;
                    }
                    while (i < this.propertyNames.Length && result == 0);
                    if (this.negative) result = -result;
                }
                return result;
            }
            catch (Exception e)
            {
                ApplicationException outer = new ApplicationException("PropertyComparer: Unable to compare two objects", e);
                throw outer;
            }
        }
    }
}
