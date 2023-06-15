namespace JuhaKurisu.PopoTools.InventorySystem;

public interface IInventoryGrid<out TSettings, TItem>
    where TSettings : IInventorySettings<TSettings, TItem>
{
    TSettings Settings { get; }
    IReadOnlyCollection<TItem> Items { get; }
    bool IsAddable(TItem item);
    bool TryAdd(TItem item);
}