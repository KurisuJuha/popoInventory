namespace JuhaKurisu.PopoTools.InventorySystem;

public interface IInventoryGrid<out TSettings, TItem> : IDisposable
    where TSettings : IInventorySettings<TSettings, TItem>
{
    IObservable<IInventoryGrid<TSettings, TItem>> OnAdded { get; }
    TSettings Settings { get; }
    IReadOnlyCollection<TItem> Items { get; }
    bool IsAddable(TItem item);
    bool TryAdd(TItem item);
    bool IsSubtractable();
    bool TrySubtract(out TItem item);
    
}