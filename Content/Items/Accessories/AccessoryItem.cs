namespace Rejuvena.Content.Items.Accessories
{
    /// <summary>
    ///     Abstract base class for accessories.
    /// </summary>
    public abstract class AccessoryItem : RejuvenaItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            Item.accessory = true;
        }
    }
}