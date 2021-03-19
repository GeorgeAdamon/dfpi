using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

    public abstract class ValueBinderBase<U>: VFXBinderBase
    {
        public          MonoBehaviour   Target = null;

        protected abstract ExposedProperty Property { get; }

        private U Value => Target == null ? default : ((IValue<U>) Target).Value;
        
        private delegate void SetValueDelegate(int id, U value);
        private delegate bool HasValueDelegate(int id);

        private SetValueDelegate SetValueMethod;
        private HasValueDelegate HasValueMethod;
        
        // VFX BINDER FUNCTIONS
        //---------------------------------------------------------------------
        public override bool IsValid(VisualEffect component)
        {
            // Lazy Initialization. Reflection runs the first time this method is called
            HasValueMethod ??= ConstructDelegate<HasValueDelegate>(component, VFXPrefix.Has);
            
            return Target != null && Target is IValue<U> && HasValueMethod((int)Property);
        }
        
        public override void UpdateBinding(VisualEffect component)
        {
            // Lazy Initialization. Reflection runs the first time this method is called
            SetValueMethod ??= ConstructDelegate<SetValueDelegate>(component, VFXPrefix.Set);
          
            SetValueMethod?.Invoke(Property, Value);
        }
        
        public override string ToString()
        {
            return $"[{typeof(U).Name}] Value : '{Property}' -> {(Target == null ? "(null)" : Target.name)}";
        }
       
        // UTILITY FUNCTIONS
        //---------------------------------------------------------------------
        private enum VFXPrefix
        {
            Set,
            Get,
            Has
        }
        
        /// <summary>
        /// Search <see cref="VisualEffect"/> component and construct the right method delegate,
        /// based on a <see cref="Type"/> and a prefix.
        /// </summary>
        private static T ConstructDelegate<T>(VisualEffect component, VFXPrefix prefix)where T: Delegate
        {
            var methodName = $"{prefix}{FirstCharToUpper(TypeNameOrAlias(typeof(U)))}";
            
            var m = FindMethodWithFirstArgumentOfType<int>(component, methodName);
            
            return (T) m?.CreateDelegate(typeof(T), component);
        }
        
        /// <summary>
        /// Resolve ambiguous methods by picking the one with the correct type as a first argument
        /// </summary>
        private static MethodInfo FindMethodWithFirstArgumentOfType<T>(object target, string methodName)
        {
            var type = target.GetType();
            
            return type.GetMethods().First(x => x.Name == methodName && x.GetParameters()[0].ParameterType == typeof(T));
        }

        /// <summary>
        /// Make the first letter Uppercase
        /// </summary>
        private static string FirstCharToUpper(string str) {
          
            if (string.IsNullOrEmpty(str)) 
            {
                throw new ArgumentException("Input string is Empty!");
            }
 
            return str.First().ToString().ToUpper() + str.Substring(1);
        }
        
        /// <summary>
        /// Make sure that system types are converted to their aliases,
        /// for example System.Single -> float, System.Int32 -> int etc
        /// https://stackoverflow.com/a/56352691
        /// </summary>
        // This is the set of types from the C# keyword list.
        static readonly Dictionary<Type, string> _typeAlias = new Dictionary<Type, string>
        {
                { typeof(bool), "bool" },
                { typeof(byte), "byte" },
                { typeof(char), "char" },
                { typeof(decimal), "decimal" },
                { typeof(double), "double" },
                { typeof(float), "float" },
                { typeof(int), "int" },
                { typeof(long), "long" },
                { typeof(object), "object" },
                { typeof(sbyte), "sbyte" },
                { typeof(short), "short" },
                { typeof(string), "string" },
                { typeof(uint), "uint" },
                { typeof(ulong), "ulong" },
                // Yes, this is an odd one.  Technically it's a type though.
                { typeof(void), "void" }
        };

        private static string TypeNameOrAlias(Type type)
        {
            // Lookup alias for type
            // Default to CLR type name
            return _typeAlias.TryGetValue(type, out var alias) ? alias : type.Name;
        }
    }