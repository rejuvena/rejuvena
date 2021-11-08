using Terraria;

namespace Rejuvena.Core.Services.MenuModes
{
    public abstract class Menu
    {
        public int Id { get; }

        protected Menu()
        {
            // Theoretically should never have a duplicate ID
            Id = Main.rand.Next(10000000, int.MaxValue);
        }

        public virtual void Load()
        {
        }

        public virtual void Unload()
        {
        }

        public abstract void ModifyMenu(
            Main main,
            int selectedMenu,
            string[] buttonNames,
            float[] buttonScales,
            int[] buttonVerticalSpacing,
            ref int offY,
            ref int spacing,
            ref int numButtons,
            ref bool backButtonDown
        );
    }
}