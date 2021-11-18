using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;
using TomatoLib;

namespace Rejuvena.Common.Wrappers
{
    public readonly struct ToolTipWrapper
    {
        public readonly List<TooltipLine> Lines;

        public ToolTipWrapper(List<TooltipLine> lines)
        {
            Lines = lines;
        }

        public void Add(int index, TooltipLine line) => Lines.Insert(index, line);

        public void Add(TooltipLine line, bool front = false)
        {
            if (front)
                Lines.Insert(0, line);
            else
                Lines.Add(line);
        }

        public void Remove(int index) => Lines.RemoveAt(index);

        public void Remove(TooltipLine line) => Lines.Remove(line);

        public void Remove(Predicate<TooltipLine> predicate, bool all = false)
        {
            if (all)
                Lines.RemoveAll(predicate);
            else
                Lines.Remove(
                    Lines.FirstOrDefault(x => predicate?.Invoke(x) ?? false) ??
                    new TooltipLine(new TomatoMod(), "dummy", "dummy")
                );
        }

        public void InsertBefore(Identifier id, TooltipLine line)
        {
            TooltipLine? found = Lines.FirstOrDefault(x => x.mod == id.Mod && x.Name == id.Id);

            if (found is null)
                return;

            int index = Lines.IndexOf(found);

            if (index == -1)
                return;

            Lines.Insert(index, line);
        }

        public void InsertAfter(Identifier id, TooltipLine line)
        {
            TooltipLine? found = Lines.FirstOrDefault(x => x.mod == id.Id && x.Name == id.Id);

            if (found is null)
                return;

            int index = Lines.IndexOf(found);

            if (index == -1)
                return;

            index++;

            Lines.Insert(index, line);
        }
    }
}