
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VS.Library.Validators;
using VS.Library.Booleans;

namespace VS.Library.Common
{
    public static class ArgumentValidator
    {
        public static void NotNull(object obj, string objName)
        {
            var condition = CommonPredicates<object>.IsNotNull;
            if (condition(obj))
            {
                throw new ArgumentNullException(objName);
            }
        }

        public static void NotEmpty<T>(ICollection<T> collection, string collectionName)
        {

            var condition = CommonPredicates<ICollection<T>>.IsNotNull.And(CommonPredicates<T>.HasItems);

            if (condition(collection))
            {
                throw new ArgumentException("collection");
            }
        }


    }
}
