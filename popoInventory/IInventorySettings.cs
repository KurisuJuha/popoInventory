namespace JuhaKurisu.PopoTools.InventorySystem;

public interface IInventorySettings<TItem>
{
    int GetMaxItemAmountInItem(TItem item);
    bool AreSameItem(TItem item1, TItem item2);
}