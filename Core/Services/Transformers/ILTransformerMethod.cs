using System.Reflection;
using Terraria.ModLoader;
using TomatoLib;
using TomatoLib.Common.Utilities.Extensions;

namespace Rejuvena.Core.Services.Transformers
{
    public abstract class ILTransformerMethod : ITransformerMethod
    {
        public Mod? Mod { get; set; }

        public abstract MethodInfo? MethodToTransform { get; }

        public abstract MethodInfo TransformingMethod { get; }

        public virtual bool ThreadSafe => true;

        public virtual void Load()
        {
        }

        public virtual void Unload()
        {
        }

        public void Transform(MethodInfo transformedMethod, MethodInfo transformingMethod)
        {
            if (Mod is not TomatoMod tomato)
                throw new ModTransformationException(transformedMethod);

            tomato.CreateEdit(transformedMethod, transformingMethod.DeclaringType, transformingMethod.Name);
        }
    }
}