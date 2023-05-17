namespace JuhaKurisu.PopoTools.InventorySystem
{
    public class Inventory<TItem> : IInventory<TItem>
    {
        public readonly IInventorySettings<TItem> inventorySettings;

        public Inventory(IInventorySettings<TItem> inventorySettings)
        {
            this.inventorySettings = inventorySettings;
        }
    }
}