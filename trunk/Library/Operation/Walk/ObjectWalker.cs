using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace VS.Library.Operation.Walk {
    public class ObjectHitEventArgs: EventArgs {
        public object Source { get; set; }
    }

    public class ContainerHitEventArgs : ObjectHitEventArgs {
        public bool SkipTraverse { get; set; }
    }

    /// <summary>
    /// General all-purpose class to traverse object graphs. 
    /// Client can hook up on hit events and register custom container parsers.
    /// By default, ICollection is the only supported container
    /// </summary>
    /// <remarks>
    ///     TODO: Add more comments!!!
    /// </remarks>
    public class ObjectWalker {

        private List<Func<object, IEnumerable>> Parsers;
        private Func<object, IEnumerable>[] ParserArray;
        private Stack<object> Path { get; set; }

        public int TotalCount { get; private set; }

        public object[] CurrentPath { 
            get {
                object[] path = Path.ToArray();
                Array.Reverse(path);
                return path; 
            } 
        }

        public ObjectWalker() {
            Path = new Stack<object>();
            Parsers = new List<Func<object, IEnumerable>>() { DefaultParser };
        }

        public void RegisterParser(Func<object, IEnumerable> parser) {
            if(parser == null) {
                throw new ArgumentNullException("parser");
            }
            Parsers.Add(parser);
        }

        public event EventHandler<ObjectHitEventArgs> ObjectHit;

        public event EventHandler<ContainerHitEventArgs> ContainerHit;

        public void Walk(object source) {
            InitWalk();
            try {
                DispatchObject(source);
            } finally {
                CleanupWalk();
            }
        }

        private void InitWalk() {
            TotalCount = 0;
            ParserArray = Parsers.ToArray();
        }

        private void CleanupWalk() {
            Path.Clear();
            ParserArray = null;
        }

        private IEnumerable DefaultParser(object source) {
            var collection = source as ICollection;
            if (collection != null) {
                return collection;
            }
            return null;
        }

        private void DispatchObject(object source) 
        {
            TotalCount++;

            IEnumerable container = null;
            foreach(var parser in Parsers) {
                container = parser(source);
                if(container != null) {
                    break;                    
                }
            }
            if(container == null) {
                if(ObjectHit != null) {
                    var args = new ObjectHitEventArgs { Source = source };
                    ObjectHit(this, args);
                }
            } else {
                bool skipTraverse = false;
                if (ContainerHit != null) {
                    var args = new ContainerHitEventArgs {
                        Source = source
                    };
		            ContainerHit(this, args);
                    skipTraverse = args.SkipTraverse;
            	}
                if (skipTraverse == false) {
                    Path.Push(source);
		            foreach (object item in container) {
		                 DispatchObject(item);
	                }
                    Path.Pop();
	            }
            }
        }
    }
}
