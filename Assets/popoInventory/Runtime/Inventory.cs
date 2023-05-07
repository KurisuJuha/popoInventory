namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class Inventory<ItemType> : IInventory<ItemType>
    {
        public readonly IInventorySettings<ItemType> inventorySettings;

        public Inventory(IInventorySettings<ItemType> inventorySettings)
        {
            this.inventorySettings = inventorySettings;
        }
    }
}