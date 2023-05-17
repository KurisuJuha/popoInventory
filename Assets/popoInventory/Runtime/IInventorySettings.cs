namespace JuhaKurisu.PopoTools.InventorySystem
{
    public interface IInventorySettings<TItem>
    {
        TItem CopyItem();
        bool IsSameItem(TItem itemA, TItem itemB);
        bool IsEmptyItem(TItem item);
        int ItemMaxAmount(TItem item);
        TItem GenerateEmptyItem();
    }
}