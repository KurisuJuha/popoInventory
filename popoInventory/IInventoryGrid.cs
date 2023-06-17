using System.Reactive.Linq;

namespace JuhaKurisu.PopoTools.InventorySystem;

public interface IInventoryGrid<TSettings, TItem> : IDisposable
    where TSettings : IInventorySettings<TItem>
{
    public IObservable<(IInventoryGrid<TSettings, TItem> grid, int index, TItem item)> OnAddedItem =>
        OnAddedItems.SelectMany(data => data.items.Select((item, i) => (data.grid, data.startIndex + i, item)));

    public IObservable<(IInventoryGrid<TSettings, TItem> grid, int index, TItem item)> OnSubtractedItem =>
        OnSubtractedItems.SelectMany(data => data.items.Select((item, i) => (data.grid, data.startIndex + i, item)));

    IObservable<(IInventoryGrid<TSettings, TItem> grid, int startIndex, int count, TItem[] items)> OnAddedItems { get; }

    IObservable<(IInventoryGrid<TSettings, TItem> grid, int startIndex, int count, TItem[] items)> OnSubtractedItems
    {
        get;
    }

    TSettings Settings { get; }
    IReadOnlyCollection<TItem> Items { get; }
    bool IsAddableItem(TItem item);
    bool TryAddItem(TItem item);
    bool IsSubtractableItem();
    bool TrySubtractItem(out TItem item);
    bool IsAddableItems(ICollection<TItem> items);
    bool TryAddItems(ICollection<TItem> items);
    bool IsSubtractableItems(int amount);
    bool TrySubtractItems(int amount, out ICollection<TItem> subtractedItems);
    void Exchange(IInventoryGrid<TSettings, TItem> otherGrid);
    void SetItems(IEnumerable<TItem> items);
}