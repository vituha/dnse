using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;

namespace VS.Library.Operation.Walk {

	/// <summary>
	/// <see cref="ObjectWalker.ObjectHit"/> event arguments
	/// </summary>
	public class ObjectHitEventArgs: EventArgs {
		/// <summary>
		/// object being hit
		/// </summary>
        public object Source { get; set; }
    }

	/// <summary>
	/// <see cref="ObjectWalker.ContainerHit"/> event arguments
	/// </summary>
	public class ContainerHitEventArgs : ObjectHitEventArgs {
		/// <summary>
		/// handlers can set this to <c>true</c> to skip walking over the container
		/// </summary>
        public bool SkipTraverse { get; set; }
    }

    /// <summary>
    /// General all-purpose class to traverse object graphs. 
    /// Client can hook up on hit events and register custom container parsers.
    /// By default, ICollection is the only supported container.
    /// This behavior can be changed by registering additional container adapters or 
    /// by overriding <see cref="AdaptContainer"/> method.
    /// </summary>
    /// <remarks>
    ///     TODO: Add more comments!!!
    /// </remarks>
    public class ObjectWalker {
		/// <summary>
		/// All registered "by-type" container adapters (called on "per object type" basis)
		/// These take precedence over <see cref="_containerAdapters"/>!
		/// Use "by-type" adapters to avoid iteration over custom adapters
		/// </summary>
    	private readonly Dictionary<Type, Func<object, IEnumerable>> _byTypeAdapters = new Dictionary<Type, Func<object, IEnumerable>>();

    	/// <summary>
    	/// All registered container adapters
    	/// </summary>
    	private readonly List<Func<object, IEnumerable>> _customAdapters = new List<Func<object, IEnumerable>>();

    	/// <summary>
    	/// Current path in object graph for internal use (in REVERSE order!)
    	/// </summary>
    	private readonly Stack<object> _path = new Stack<object>();

		public bool IsWalking { get; private set; }

		/// <summary>
		/// Current path within object graph in REVERSE order (starting from the innermost object)
		/// </summary>
        public object[] CurrentPath { 
            get {
                return _path.ToArray();
            } 
        }

		/// <summary>
		/// Default constructor
		/// </summary>
        public ObjectWalker() {
            RegisterCustomAdapter(AdaptContainer);
        }

		/// <summary>
		/// Registers a container adapter (delegate that knows how to walk over non IEnumerable container)
		/// </summary>
		/// <param name="adapter"></param>
        public void RegisterCustomAdapter(Func<object, IEnumerable> adapter) {
            if(adapter == null) {
				throw new ArgumentNullException("adapter");
            }
			EnsureNotWalking();
            _customAdapters.Add(adapter);
        }

		/// <summary>
		/// Registers a container adapter (delegate that knows how to walk over non IEnumerable container)
		/// </summary>
		/// <param name="type">type of object adapter is for</param>
		/// <param name="adapter"></param>
        public void RegisterTypeAdapter(Type type, Func<object, IEnumerable> adapter) {
            if(type == null) {
				throw new ArgumentNullException("type");
            }
            if(adapter == null) {
				throw new ArgumentNullException("adapter");
            }
			EnsureNotWalking();
            _byTypeAdapters.Add(type, adapter);
        }

		/// <summary>
		/// Registers container adapters for known types such as 
		/// DictionaryEntry, KeyValuePair, Pair, Triplet, etc
		/// </summary>
		public void RegisterAllKnownAdapters() {
			RegisterTypeAdapter(typeof(DictionaryEntry), AdaptDictionaryEntry);
			RegisterTypeAdapter(typeof(System.Web.UI.Pair), AdaptPair);
			RegisterTypeAdapter(typeof(System.Web.UI.Triplet), AdaptTriplet);
			RegisterCustomAdapter(AdaptCommonGeneric);
		}

    	/// <summary>
		/// Fired when terminator (non-container) object is hit
		/// </summary>
        public event EventHandler<ObjectHitEventArgs> ObjectHit;

		/// <summary>
		/// Fired when container object is hit
		/// </summary>
		public event EventHandler<ContainerHitEventArgs> ContainerHit;

		/// <summary>
		/// Walks over given <paramref name="objectGraph"/> firing hit events
		/// </summary>
		/// <param name="objectGraph">object graph to walk over</param>
        public void Walk(object objectGraph) {
			EnsureNotWalking();

            BeginWalk();
            try {
                DispatchObject(objectGraph);
            } finally {
                EndWalk();
			}
        }

        private void BeginWalk() {
        	IsWalking = true;
        }

        private void EndWalk() {
            _path.Clear(); // clear path
			IsWalking = false;
        }

		/// <summary>
		/// Default container adapter. 
		/// Override this to add/modify default container recognition capabilities
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
        protected virtual IEnumerable AdaptContainer(object source) {
            return source as ICollection; // collection are natively supported containers
        }

		/// <summary>
		/// Analyses curent <paramref name="source"/> object and fires corresponding hit event
		/// </summary>
		/// <param name="source"></param>
		/// <remarks>
		/// Method iterates over registered container adapters and calls them in order.
		/// If non-<c>null</c> enumerable is returned by some adapter <see cref="ContainerHit"/> event is fired
		/// If all of the adapters return <c>null</c>, <see cref="ObjectHit"/> event is fired
		/// </remarks>
        private void DispatchObject(object source) 
        {
			// Try to see whether this is a continer by adapting it
            IEnumerable container = null;

			Func<object, IEnumerable> adapter;
			if (source != null && _byTypeAdapters.TryGetValue(source.GetType(), out adapter)) {
				container = adapter(source);
			} else {
				foreach(var parser in _customAdapters) {
					container = parser(source);
					if(container != null) {
						break;                    
					}
				}
			}

			if(container == null) {
				// not a container
                if(ObjectHit != null) {
                    var args = new ObjectHitEventArgs {
                    	Source = source
                    };
                    ObjectHit(this, args);
                }
            } else {
				// container
                bool skipTraverse = false;
                if (ContainerHit != null) {
                    var args = new ContainerHitEventArgs {
                        Source = source
                    };
		            ContainerHit(this, args);
                    skipTraverse = args.SkipTraverse;
            	}
				// walk over the container?
                if (!skipTraverse) {
					// save path
                    _path.Push(source);
					// walk
					foreach (object item in container) {
		                 DispatchObject(item);
	                }
					// restore path
                    _path.Pop();
	            }
            }
        }

#region Private Helpers

		private void EnsureNotWalking() {
	       	if (IsWalking) {
			    throw new InvalidOperationException("operation not allowed during walking");
        	}
		}

#endregion Private Helpers

		/// <summary>
		/// By-type adapter
		/// </summary>
		public static IEnumerable AdaptDictionaryEntry(object source) {
			Debug.Assert(source is DictionaryEntry);
			var de = (DictionaryEntry) source;
			return new object[] { de.Key, de.Value };
		}

		/// <summary>
		/// By-type adapter
		/// </summary>
		public static IEnumerable AdaptPair(object source) {
			var pair = source as System.Web.UI.Pair;
			Debug.Assert(pair != null);
			return new object[] { pair.First, pair.Second };
		}

		/// <summary>
		/// By-type adapter
		/// </summary>
		public static IEnumerable AdaptTriplet(object source) {
			var pair = source as System.Web.UI.Triplet;
			Debug.Assert(pair != null);
			return new object[] { pair.First, pair.Second, pair.Third };
		}

		/// <summary>
		/// Custom adapter.
		/// Adapts common generic classes such as <see cref="KeyValuePair{TKey,TValue}"/>
		/// </summary>
		public static IEnumerable AdaptCommonGeneric(object source) {
			IEnumerable result = null;
			if (source != null) {
				Type type = source.GetType();
				if (type.IsGenericType) { // process generic types
					Type typeDefinition = type.GetGenericTypeDefinition();

					// try KeyValuePair
					if (typeDefinition == typeof(KeyValuePair<,>)) {
						result = EnumeratePropertyValues(source, "Key", "Value");
					}

					// try KeyValuePair
					if (typeDefinition == typeof(KeyValuePair<,>)) {
						result = EnumeratePropertyValues(source, "Key", "Value");
					}
				}
			}
			return result;
		}

		/// <summary>
		/// Wraps <paramref name="source"/>'s property values into enumerable
		/// </summary>
		/// <param name="source"></param>
		/// <param name="propNames"></param>
		/// <returns></returns>
		private static IEnumerable EnumeratePropertyValues(object source, params string[] propNames) {
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
