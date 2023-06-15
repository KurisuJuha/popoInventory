namespace JuhaKurisu.PopoTools.InventorySystem;

public interface IInventorySettings<in TSettings, TItem>
    where TSettings : IInventorySettings<TSettings, TItem>
{
    int GetMaxItemAmountInGrid(IInventoryGrid<TSettings, TItem> grid);
    int GetMaxItemAmountInItem(TItem item);
}