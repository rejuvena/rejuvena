using System;
using System.Reflection;
using TomatoLib;

namespace Rejuvena.Core.Services.Transformers
{
    public class ModTransformationException : Exception
    {
        public ModTransformationException(MethodInfo transformedMethod) : base(GetFailureMessage(transformedMethod))
        {
        }

        public static string GetFailureMessage(MethodInfo transformedMethod) =>
            "Could not transform method " +
            $"({transformedMethod.DeclaringType?.FullName ?? "<no class>"}::{transformedMethod.Name}) " +
            $"because the associated Mod does not inherit {nameof(TomatoMod)}!";
    }
}