using System.Linq;
using Rejuvena.Core.Services;
using TomatoLib;

namespace Rejuvena
{
    public class Rejuvena : TomatoMod
    {
        public TService GetService<TService>() where TService : Service => GetContent<TService>().FirstOrDefault();
    }
}