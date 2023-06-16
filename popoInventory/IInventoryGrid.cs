namespace JuhaKurisu.PopoTools.InventorySystem;

public interface IInventoryGrid<out TSettings, TItem> : IDisposable
    where TSettings : IInventorySettings<TSettings, TItem>
{
    IObservable<IInventoryGrid<TSettings, TItem>> OnAdded { get; }
    TSettings Settings { get; }
    IReadOnlyCollection<TItem> Items { get; }
    bool IsAddableItem(TItem item);
    bool TryAddItem(TItem item);
    bool IsSubtractableItem();
    bool TrySubtractItem(out TItem item);
    
}