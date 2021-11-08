using System.Reflection;
using Terraria.ModLoader;

namespace Rejuvena.Core.Services.Transformers
{
    public interface ITransformerMethod
    {
        Mod? Mod { get; set; }

        MethodInfo? MethodToTransform { get; }

        MethodInfo TransformingMethod { get; }

        void Transform(MethodInfo transformedMethod, MethodInfo transformingMethod);

        void Load();

        void Unload();
    }
}