namespace JuhaKurisu.PopoTools.InventorySystem
{
    public interface IInventoryGrid<ItemType>
    {
        public InventoryItem<ItemType> inventoryItem { get; }
        public int amount { get; }
        public int maxAmount { get; }

        public int AddItems(int addAmount);

        public int SubtractItems(int subtractAmount);
    }
}