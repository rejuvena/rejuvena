using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Content.Readers;
using Terraria;
using TomatoLib;
using TomatoLib.Common.Utilities.Extensions;

namespace Rejuvena
{
    [UsedImplicitly]
    public class Rejuvena : TomatoMod
    {
        public class EffectContentLoader : IAssetReader
        {
            public async ValueTask<T> FromStream<T>(Stream stream, MainThreadCreationContext mainThreadCtx) where T : class
            {
                if (typeof(T) != typeof(Effect))
                    return await new XnbReader(Main.instance.Services).FromStream<T>(stream, mainThreadCtx);

                await mainThreadCtx;

                T obj = FromStream<T>(stream);

                return obj;
            }

            protected T FromStream<T>(Stream stream) where T : class
            {
                using MemoryStream cloneStream = new();
                stream.CopyTo(cloneStream);
                byte[] effectCode = new byte[cloneStream.Length];
                cloneStream.Read(new byte[4], 0, 4);
                cloneStream.Seek(0L, SeekOrigin.Begin);
                cloneStream.Read(effectCode, 0, (int)cloneStream.Length);

                return new Effect(Main.graphics.GraphicsDevice, effectCode) as T;
            }
        }

        public override void Load()
        {
            base.Load();

            AssetReaderCollection assetReaders = Assets.GetFieldValue<AssetRepository, AssetReaderCollection>("_readers");
            //Dictionary<string, IAssetReader> byExtensions = assetReaders.GetFieldValue<AssetReaderCollection,
            //    Dictionary<string, IAssetReader>>("_readersByExtension");

            assetReaders.RegisterReader(new EffectContentLoader(), ".xnb", "xnb");
        }
    }
}