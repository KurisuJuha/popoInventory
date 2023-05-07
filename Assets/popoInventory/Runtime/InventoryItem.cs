namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class InventoryItem<ItemType>
    {
        public readonly IInventorySettings<ItemType> inventorySettings;
        public readonly ItemType item;

        public InventoryItem(IInventorySettings<ItemType> inventorySettings, ItemType item)
        {
            this.inventorySettings = inventorySettings;
            this.item = item;
        }

        public InventoryItem<ItemType> Copy()
            => new InventoryItem<ItemType>(inventorySettings, item);
    }
}