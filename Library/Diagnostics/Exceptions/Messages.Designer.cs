﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VS.Library.Diagnostics.Exceptions {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("VS.Library.Diagnostics.Exceptions.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to cast from type &apos;{0}&apos; to type &apos;{1}&apos;.
        /// </summary>
        internal static string InvalidCast {
            get {
                return ResourceManager.GetString("InvalidCast", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Requested object is currently unavailable.
        /// </summary>
        internal static string ObjectUnavailable {
            get {
                return ResourceManager.GetString("ObjectUnavailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Object is in unusable state.
        /// </summary>
        internal static string ObjectUnusable {
            get {
                return ResourceManager.GetString("ObjectUnusable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This instance cannot be used because cleanup has already been done.
        /// </summary>
        internal static string ObjectUnusable_CleanedUp {
            get {
                return ResourceManager.GetString("ObjectUnusable_CleanedUp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Object &apos;{0}&apos; is in unusable state.
        /// </summary>
        internal static string ObjectUnusable1 {
            get {
                return ResourceManager.GetString("ObjectUnusable1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected null value.
        /// </summary>
        internal static string UnexpectedNull {
            get {
                return ResourceManager.GetString("UnexpectedNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected null value in &apos;{0}&apos;.
        /// </summary>
        internal static string UnexpectedNull1 {
            get {
                return ResourceManager.GetString("UnexpectedNull1", resourceCulture);
            }
        }
    }
}
