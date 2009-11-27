using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace VS.Library.Operation.Walk {
    public static class ObjectWalkerFactory {

        public static ObjectWalker CreateWalker() {
            ObjectWalker result = new ObjectWalker();

            result.RegisterParser(CollectionFromObject);

            return result;
        }

        public static IEnumerable CollectionFromObject(object source) { 
            if (source != null) {
                Type type = source.GetType();
                if (type.IsGenericType) { // process generic types
                    Type typeDefinition = type.GetGenericTypeDefinition();

                    // try KeyValuePair
                    if (typeDefinition == typeof(KeyValuePair<,>)) {
                        return EnumerateObjectProps(source, "Key", "Value");
                    }
                } else { // non-generic type

                    // try KeyValuePair
                    if (type == typeof(DictionaryEntry)) {
                        var de = (DictionaryEntry)source;
                        return new object[] { de.Key, de.Value };
                    }
                }
            }
            return null;
        }

        private static IEnumerable EnumerateObjectProps(object source, params string[] propNames) {
            Type type = source.GetType();
            foreach (string propName in propNames) {
                PropertyInfo pi = type.GetProperty(propName);
                if (pi == null) {
                    throw new ArgumentException("source object does not support one or more properties requested", "propNames");
                }
                yield return pi.GetValue(source, null);
            }
        }
    }
}
