namespace JuhaKurisu.PopoTools.InventorySystem
{
    public interface IInventoryGrid<TItem>
    {
        InventoryItem<TItem> inventoryItem { get; }
        int amount { get; }
        int maxAmount { get; }
        int AddItems(int addAmount);
        int SubtractItems(int subtractAmount);
    }
}