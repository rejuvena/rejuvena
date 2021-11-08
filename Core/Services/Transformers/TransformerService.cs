using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;

namespace Rejuvena.Core.Services.Transformers
{
    public class TransformerService : Service
    {
        public List<ITransformerMethod> TransformerMethods { get; set; } = new();

        public override void Load()
        {
            base.Load();

            List<Type> types = Mod.Code.GetTypes().Where(
                x => x.IsAssignableTo(typeof(ITransformerMethod))
                     && x.GetConstructor(Array.Empty<Type>()) != null
                     && !x.IsAbstract
            ).ToList();

            Mod.Logger.Debug($"Found {types.Count} transformation methods.");

            List<ITransformerMethod> extraTransformers = new();

            foreach (Type type in types)
            {
                if (Activator.CreateInstance(type) is not ITransformerMethod transformer)
                    continue;

                TransformerMethods.Add(transformer);

                transformer.Mod = Mod;
                transformer.Load();

                if (transformer.MethodToTransform is null)
                {
                    Mod.Logger.Debug("Found non-specific transformer: " + transformer.GetType().FullName);
                    extraTransformers.Add(transformer);
                    continue;
                }

                Mod.Logger.Debug("Applying transformer: " + transformer.GetType().FullName);
                transformer.Transform(transformer.MethodToTransform, transformer.TransformingMethod);
            }

            Mod.Logger.Debug("Finished applying transformers.");
            Mod.Logger.Debug("Non-specific transformer count: " + extraTransformers.Count);

            if (extraTransformers.Count <= 0)
                return;
            

            Mod.Logger.Debug("Applying non-specific transformers to all vanilla types...");

            foreach (Type type in typeof(Main).Assembly.GetTypes())
            foreach (MethodInfo method in type.GetMethods(
                    BindingFlags.Public
                    | BindingFlags.NonPublic
                    | BindingFlags.Instance
                    | BindingFlags.Static | BindingFlags.DeclaredOnly
                ))
            foreach (ITransformerMethod transformer in extraTransformers)
                transformer.Transform(method, transformer.TransformingMethod);
        }

        public override void Unload()
        {
            base.Unload();

            foreach (ITransformerMethod transformer in TransformerMethods)
                transformer.Unload();

            TransformerMethods.Clear();
        }
    }
}