namespace JuhaKurisu.PopoTools.InventorySystem;

public interface IInventoryGrid<TSettings, TItem> : IDisposable
    where TSettings : IInventorySettings<TSettings, TItem>
{
    IObservable<IInventoryGrid<TSettings, TItem>> OnAdded { get; }
    TSettings Settings { get; }
    IReadOnlyCollection<TItem> Items { get; }
    bool IsAddableItem(TItem item);
    bool TryAddItem(TItem item);
    bool IsSubtractableItem();
    bool TrySubtractItem(out TItem item);
    bool IsAddableItems(ICollection<TItem> items);
    bool TryAddItems(ICollection<TItem> items);
    bool IsSubtractableItems(int amount);
    bool TrySubtractItems(int amount, ICollection<TItem> subtractedItems);
    void Exchange(IInventoryGrid<TSettings, TItem> otherGrid);
    void SetItems(IEnumerable<TItem> items);
}