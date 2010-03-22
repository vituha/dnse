using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Collections;
using VS.Library.Operation.Walk;
using System.Diagnostics;

namespace VS.Library.UT.Operation.Walk {
    [TestFixture]
    public class TreeWalkerTest {
        public static readonly object[] TestGraph = new object[] {
            new Dictionary<string, int> {
                {"First", 1},
                {"Second", 2}
            },
            new Hashtable {
                {"Monday", 1},
                {"Friday", 5}
            },
            new ArrayList { 5, false, "he-he" }
        };

        [Test]
        public void Walk() {
            Stats.Clear();
            ObjectWalker walker = new ObjectWalker();
			walker.RegisterAllKnownAdapters();
            walker.ObjectHit += new EventHandler<ObjectHitEventArgs>(walker_ObjectHit);
            walker.ContainerHit += new EventHandler<ContainerHitEventArgs>(walker_ContainerHit);
        	TotalCount = 0;
            Console.WriteLine("\nWalking over the TestGraph...\n");
            walker.Walk(TestGraph);
            DisplayStats();
        }

        private Dictionary<Type, int> Stats = new Dictionary<Type, int>();
		private int TotalCount { get; set; }

        private void DisplayStats() {
            Console.WriteLine("\nTestGraph Statistics:");

            Console.WriteLine("\nTotal objects: " + TotalCount);
            Console.WriteLine("\nPer Type:");

            foreach (var pair in Stats) {
                Type type = pair.Key;
                int stat = pair.Value;
                Console.WriteLine(FormatTypeString(type) + ": " + stat.ToString());
            }
        }

        private static string FormatTypeString(Type type) {
            StringBuilder sb = new StringBuilder(type.Name);
            if (type.IsGenericType) {
                Type[] args = type.GetGenericArguments();
                string[] argNames = new string[args.Length];
                for (int i = 0; i < args.Length; i++) {
			        argNames[i] = args[i].Name;
			    }
                sb.Append("{");
                sb.Append(String.Join(",", argNames));
                sb.Append("}");
            }
            return sb.ToString();
        }

        void walker_ContainerHit(object sender, ContainerHitEventArgs e) {
            RegisterObject(sender as ObjectWalker, e.Source);
        }

        void walker_ObjectHit(object sender, ObjectHitEventArgs e) {
            RegisterObject(sender as ObjectWalker, e.Source);
        }

        private void RegisterObject(ObjectWalker walker, object obj) {
			TotalCount++;
            if (obj != null) {
                Type type = obj.GetType();
                int counter;
                if (Stats.TryGetValue(type, out counter)) {
                    Stats[type] = counter + 1;
                } else {
                    Stats[type] = 1;
                }
            }

            object[] path = walker.CurrentPath;
            string[] pathNames = new string[path.Length + 1];
            for (int i = 0; i < path.Length; i++) {
                pathNames[i] = FormatTypeString(path[i].GetType());
            }
            pathNames[pathNames.Length - 1] = obj != null ? FormatTypeString(obj.GetType()) : "<null>";

            Console.WriteLine(String.Format("({0}) {1}", path.Length, String.Join("/", pathNames)));
        }
    }
}
