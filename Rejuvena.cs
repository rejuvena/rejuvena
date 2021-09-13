using System;
using JetBrains.Annotations;
using Terraria;
using TomatoLib;

namespace Rejuvena
{
    [UsedImplicitly]
    public class Rejuvena : TomatoMod
    {
        public class WhatTheFuck : Delegate
        {
            public WhatTheFuck([NotNull] object target, [NotNull] string method) : base(target, method)
            {
            }

            public WhatTheFuck([NotNull] Type target, [NotNull] string method) : base(target, method)
            {
            }
        }
    }
}