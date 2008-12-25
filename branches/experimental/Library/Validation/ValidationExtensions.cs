namespace VS.Library.Validation {
    using System;
    using System.Diagnostics;
    using VS.Library.Common;

    /// <summary>
    /// Implements useful extensions to help validate an object's state
    /// When moving to .Net Framework 3.5 or above, consider convert everything here into C# 3.0 extension methods
    /// </summary>
    [DebuggerNonUserCode]
    public static class ValidationExtensions
    {
        /// <summary>
        /// Ensures value is not null by throwing an exception otherwise.
        /// This is for simple cases only and should not be abused. 
        /// For complex validation, using <see cref="ValidateArgument"/> method is preferred.
        /// </summary>
        /// <param name="value">Value to be validated</param>
        /// <param name="name">Name of value to be validated</param>
        /// <exception cref="ArgumentNullException">value is null</exception>
        public static void RequireNotNull(this object value, string name)
        {
            Validate(value, name).Require();
        }

        /// <summary>
        /// Ensures argument value is not null by throwing an exception otherwise.
        /// This is for simple cases only and should not be abused. 
        /// For complex validation, using <see cref="ValidateArgument"/> method is preferred.
        /// </summary>
        /// <param name="argumentValue">Value to be validated</param>
        /// <param name="argumentName">Name of argument to be validated</param>
        /// <exception cref="ArgumentNullException">value is null</exception>
        public static void RequireArgumentNotNull(this object argumentValue, string argumentName)
        {
            ValidateArgument(argumentValue, argumentName).Require();
        }

        /// <summary>
        /// Ensures value passed to a setter is not null by throwing an exception otherwise.
        /// This is for simple cases only and should not be abused. 
        /// For complex validation, using <see cref="ValidateSetterValue"/> method is preferred.
        /// </summary>
        /// <param name="setterValue">Value to be validated</param>
        /// <exception cref="ArgumentNullException">value is null</exception>
        public static void RequireSetterValueNotNull(this object setterValue)
        {
            ValidateSetterValue(setterValue).Require();
        }

        /// <summary>
        /// Creates a validator object for a value.
        /// The validator can be used to test the value against multiple predicates
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="value">Value to create validator for</param>
        /// <param name="name">Name of the variable holding the value</param>
        /// <returns>Created Validator object</returns>
        public static Validator<T> Validate<T>(this T value, string name)
        {
            return new Validator<T>(value, name);
        }

        /// <summary>
        /// Creates a method argument validator object for a value.
        /// The validator can be used to test the value against multiple predicates
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="argumentValue">Value to create validator for</param>
        /// <param name="argumentName">Name of the argument holding the value</param>
        /// <returns>Created Validator object</returns>
        public static Validator<T> ValidateArgument<T>(this T argumentValue, string argumentName)
        {
            return new ArgumentValidator<T>(argumentValue, argumentName);
        }

        /// <summary>
        /// Creates a method argument validator object for a value. argumentName is assumed to be "value"
        /// The validator can be used to test the value against multiple predicates
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="setterValue">Value to create validator for</param>
        /// <returns>Created Validator object</returns>
        public static Validator<T> ValidateSetterValue<T>(this T setterValue)
        {
            return new Validator<T>(setterValue, "value");
        }
    }

    /// <summary>
    /// Basic validator class.
    /// Implements functionality for testing the value against multiple predicates
    /// </summary>
    /// <typeparam name="T">Type of value being validated</typeparam>
    [DebuggerNonUserCode]
    public class Validator<T>
    {
        /// <summary>
        /// Value being validated
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Name of varible that holds the value
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value to validate</param>
        /// <param name="name">Name of the variable holding the value</param>
        public Validator(T value, string name)
        {
            Value = value;
            Name = name;
        }

        private void EvaluateRequirements(params Predicate<T>[] tests)
        {
            try
            {
                Predicate<T> pred;
                for (int i = 0; i < tests.Length; i++)
                {
                    pred = tests[i];
                    if (pred == null)
                    {
                        throw new ArgumentException("tests[" + i.ToString() + "]");
                    }
                    if (!pred(Value))
                    {
                        throw CreateException(GetFailureMessage(pred, i));
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is RequirementNotMetException || ex is ArgumentException)
                {
                    throw; // Exception is expected. Re-throw original exception
                }
                // Wrap unexpected exception
                throw new InvalidOperationException("Predicate evaluation has thrown an exception", ex);
            }
        }

        /// <summary>
        /// Performs step-by-step evaluation of supplied tests on the value if it is not null
        /// </summary>
        /// <remarks>Use this method to validate values that must be not null and pass the tests. 
        /// Note: Before evaluating any test, this method checks value against null 
        /// so it is not necessary to do null checks in tests
        /// </remarks>
        /// <param name="tests">Predicates to test the value against</param>
        /// <returns>this Validator instance to enable call chaining</returns>
        /// <exception cref="ArgumentException"><c>null</c> was supplied in arguments as a predicate</exception>
        /// <exception cref="RequirementNotMetException">Predicate evaluation returned <c>false</c></exception>
        /// <exception cref="ArgumentNullException">value is null</exception>
        public Validator<T> Require(params Predicate<T>[] tests)
        {
            if (Value == null)
            {
                throw new ArgumentNullException(Name);
            }

            if (tests.Length > 0)
            {
                EvaluateRequirements(tests);
            }
            return this;
        }

        /// <summary>
        /// Performs step-by-step evaluation of supplied tests on the value if it is not null. 
        /// Skips all evaluation an returns if the value is null
        /// </summary>
        /// <remarks>Use this method to validate values that must be either null or pass the tests</remarks>
        /// <param name="tests">Predicates to test the value against</param>
        /// <returns>this Validator instance to enable call chaining</returns>
        /// <exception cref="ArgumentNullException"><c>null</c> was supplied in arguments as a predicate</exception>
        /// <exception cref="RequirementNotMetException">Predicate evaluation returned <c>false</c></exception>
        public Validator<T> RequireIfNotNull(params Predicate<T>[] tests)
        {
            Debug.Assert(tests.Length > 0, "Unnecessary call. Either specify at least one test or remove the call to this method");

            if (Value != null)
            {
                EvaluateRequirements(tests);
            }
            return this;
        }

        protected virtual Exception CreateException(string message)
        {
            return new RequirementNotMetException(message);
        }

        /// <summary>
        /// Performs step-by-step evaluation of supplied tests on the value in DEBUG mode only.
        /// Uses Debug.Assert() to process the result of predicate evaluation.
        /// </summary>
        /// <param name="tests">Predicates to test the value against</param>
        /// <exception cref="ArgumentNullException"><c>null</c> was supplied in arguments as a predicate</exception>
        [Conditional("DEBUG")]
        public void Assert(params Predicate<T>[] tests)
        {
            Predicate<T> pred;
            for (int i = 0; i < tests.Length; i++)
            {
                pred = tests[i];
                if (pred == null)
                {
                    throw new ArgumentNullException("tests[" + i.ToString() + "]");
                }
                Debug.Assert(pred(Value), GetFailureMessage(pred, i));
            }
        }

        private string GetFailureMessage(Predicate<T> pred, int step)
        {
            Type t = typeof(T);
#if DEBUG
            if (t.IsPrimitive || t.IsEnum || t == typeof(string))
            {
                return String.Format(
                    "Validation failed for '{0}': {1}({2}) at step {3}",
                    Name,
                    DelegateExtensions.ResolveNameInternal(pred, true),
                    Value == null ? "null" : Value.ToString(),
                    step);
            }
            else
            {
                return String.Format(
                    "Validation failed for '{0}': {1}; Value.ToString() = {2}; Step = {3}",
                    Name,
                    DelegateExtensions.ResolveNameInternal(pred, true),
                    Value == null ? "null" : @"""" + Value.ToString() + @"""",
                    step);
            }
#else
			// Do not expose any value in release mode
			return String.Format(
				"Validation failed for '{0}': {1} at step {2}",
				Name,
				DelegateExtensions.ResolveNameInternal(pred, true),
				step
			);
#endif
        }
    }

    /// <summary>
    /// Validator that can be used for method arguments validation.
    /// Wraps <see cref="RequirementNotMetException"/> with <see cref="ArgumentException"/>
    /// </summary>
    /// <typeparam name="T">Type of value being validated</typeparam>
    [DebuggerNonUserCode]
    public class ArgumentValidator<T> : Validator<T>
    {
        public ArgumentValidator(T value, string name) : base(value, name) { }

        protected override Exception CreateException(string message)
        {
            return new ArgumentException(Name, base.CreateException(message));
        }
    }
}
