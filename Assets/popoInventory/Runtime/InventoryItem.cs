namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class InventoryItem<TItem>
    {
        public readonly IInventorySettings<TItem> inventorySettings;
        public readonly TItem item;

        public InventoryItem(IInventorySettings<TItem> inventorySettings, TItem item)
        {
            this.inventorySettings = inventorySettings;
            this.item = item;
        }

        public InventoryItem<TItem> Copy()
        {
            return new InventoryItem<TItem>(inventorySettings, item);
        }
    }
}