namespace JuhaKurisu.PopoTools.InventorySystem;

public interface IInventorySettings<TItem>
{
    int GetMaxItemAmountInItem(TItem item);
    TItem GetEmptyItem();
    bool AreSameItem(TItem item1, TItem item2);
}