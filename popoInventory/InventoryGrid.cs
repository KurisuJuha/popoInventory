using System.Reactive.Subjects;

namespace JuhaKurisu.PopoTools.InventorySystem;

public sealed class InventoryGrid<TSettings, TItem> : IInventoryGrid<TSettings, TItem>
    where TSettings : IInventorySettings<TSettings, TItem>
{
    private readonly List<TItem> _items;
    private readonly int _maxAmountInGrid;
    private readonly Subject<IInventoryGrid<TSettings, TItem>> _onAdded;

    public InventoryGrid(TSettings settings, int maxAmountInGrid)
    {
        _maxAmountInGrid = maxAmountInGrid;
        _onAdded = new Subject<IInventoryGrid<TSettings, TItem>>();
        _items = new List<TItem>();
        Settings = settings;
        Items = _items.AsReadOnly();
    }

    public IObservable<IInventoryGrid<TSettings, TItem>> OnAdded => _onAdded;
    public TSettings Settings { get; }
    public IReadOnlyCollection<TItem> Items { get; }

    public bool IsAddableItem(TItem item)
    {
        if (_items.Count != 0 && !Settings.AreSameItem(item, _items[0])) return false;

        return _items.Count < GetMaxAmount();
    }

    public bool TryAddItem(TItem item)
    {
        if (!IsAddableItem(item)) return false;

        _items.Add(item);
        _onAdded.OnNext(this);

        return true;
    }

    public bool IsSubtractableItem()
    {
        return _items.Count > 0;
    }

    public bool TrySubtractItem(out TItem item)
    {
        if (!IsSubtractableItem())
        {
            item = Settings.GetEmptyItem();
            return false;
        }

        item = _items.Last();
        _items.RemoveAt(0);
        return true;
    }

    public bool IsAddableItems(ICollection<TItem> items)
    {
        if (_items.Count != 0 && items.Any(item => !Settings.AreSameItem(item, _items[0]))) return false;

        return GetMaxAmount() - _items.Count >= items.Count;
    }

    public bool TryAddItems(ICollection<TItem> items)
    {
        if (!IsAddableItems(items)) return false;

        _items.AddRange(items);
        return true;
    }

    public bool IsSubtractableItems(int amount)
    {
        return _items.Count >= amount;
    }

    public bool TrySubtractItems(int amount, ICollection<TItem> subtractedItems)
    {
        if (!IsSubtractableItems(amount)) return false;

        _items.RemoveRange(0, amount);
        return true;
    }

    public void Dispose()
    {
        _onAdded.Dispose();
    }

    private int GetMaxAmount()
    {
        return _items.Count == 0
            ? _maxAmountInGrid
            : Math.Min(_maxAmountInGrid, Settings.GetMaxItemAmountInItem(_items.First()));
    }
}