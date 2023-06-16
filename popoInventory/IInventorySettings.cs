namespace JuhaKurisu.PopoTools.InventorySystem;

public interface IInventorySettings<in TSettings, TItem>
    where TSettings : IInventorySettings<TSettings, TItem>
{
    int GetMaxItemAmountInItem(TItem item);
    TItem GetEmptyItem();
    bool AreSameItem(TItem item1, TItem item2);
}