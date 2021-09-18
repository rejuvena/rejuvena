#region License
// Copyright (C) 2021 Tomat and Contributors
// GNU General Public License Version 3, 29 June 2007
#endregion

using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using TomatoLib.Common.Systems;
using TomatoLib.Core.Threading;

namespace Rejuvena.Common.Shaders
{
    [Autoload(false, Side = ModSide.Client)]
    public sealed class ShaderContainer : SingletonSystem<ShaderContainer>
    {
        public Ref<Effect> OutlineEffect { get; private set; }

        public ArmorShaderData OutlineShader { get; private set; }

        public override async void OnModLoad()
        {
            base.OnModLoad();

            if (Main.dedServ)
                return;

            OutlineEffect = await Get("Assets/Effects/OutlineShader");
            OutlineShader = new ArmorShaderData(OutlineEffect, "Outline");
        }

        public void Apply(ShaderData shader, [CanBeNull] Entity entity = null, DrawData? drawData = null)
        {
            switch (shader)
            {
                case null:
                    Main.pixelShader.CurrentTechnique.Passes[0].Apply();
                    break;

                case ArmorShaderData armorShader when armorShader.GetSecondaryShader(entity) is null:
                    armorShader.Apply(entity, drawData);
                    break;

                case ArmorShaderData armorShader:
                    armorShader.GetSecondaryShader(entity).Apply(entity, drawData);
                    break;
            }

            // TODO: Add more.
        }

        public async Task<Ref<Effect>> Get(string path) =>
            await GLCallLocker.Instance.InvokeAsync(() =>
                new Ref<Effect>(Mod.Assets.Request<Effect>(path, AssetRequestMode.ImmediateLoad).Value));
    }
}