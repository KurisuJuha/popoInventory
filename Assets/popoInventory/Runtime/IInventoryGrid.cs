namespace JuhaKurisu.PopoTools.InventorySystem
{
    public interface IInventoryGrid<TItem>
    {
        InventoryItem<TItem> inventoryItem { get; }
        int amount { get; }
        int maxAmount { get; }
        void SetItems(TItem item, int amount);
        int AddItems(int addAmount);
        int SubtractItems(int subtractAmount);
    }
}