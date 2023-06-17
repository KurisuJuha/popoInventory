using System.Reactive.Subjects;

namespace JuhaKurisu.PopoTools.InventorySystem;

public sealed class InventoryGrid<TSettings, TItem> : IInventoryGrid<TSettings, TItem>
    where TSettings : IInventorySettings<TItem>
{
    private readonly int _maxAmountInGrid;

    private readonly Subject<(IInventoryGrid<TSettings, TItem> grid, int startIndex, int count, TItem[] items)>
        _onAddedItems;

    private readonly Subject<IInventoryGrid<TSettings, TItem>> _onExchanged;

    private readonly Subject<(IInventoryGrid<TSettings, TItem> grid, int startIndex, int count, TItem[] items)>
        _onSubtractedItems;

    private List<TItem> _items;

    public InventoryGrid(TSettings settings, int maxAmountInGrid)
    {
        _maxAmountInGrid = maxAmountInGrid;
        _onAddedItems =
            new Subject<(IInventoryGrid<TSettings, TItem> grid, int startIndex, int count, TItem[] items)>();
        _onSubtractedItems =
            new Subject<(IInventoryGrid<TSettings, TItem> grid, int startIndex, int count, TItem[] items)>();
        _onExchanged = new Subject<IInventoryGrid<TSettings, TItem>>();
        _items = new List<TItem>();
        Settings = settings;
        Items = _items.AsReadOnly();
    }

    public IObservable<(IInventoryGrid<TSettings, TItem> grid, int startIndex, int count, TItem[] items)>
        OnAddedItems => _onAddedItems;

    public IObservable<(IInventoryGrid<TSettings, TItem> grid, int startIndex, int count, TItem[] items)>
        OnSubtractedItems => _onSubtractedItems;

    public IObservable<IInventoryGrid<TSettings, TItem>> OnExchanged => _onExchanged;

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
        _onAddedItems.OnNext((this, _items.Count - 1, 1, new[] { item }));

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
        _onSubtractedItems.OnNext((this, _items.Count, 1, new[] { item }));

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

        _onAddedItems.OnNext((this, _items.Count - items.Count, items.Count, items.ToArray()));

        return true;
    }

    public bool IsSubtractableItems(int amount)
    {
        return _items.Count >= amount;
    }

    public bool TrySubtractItems(int amount, out ICollection<TItem> subtractedItems)
    {
        if (!IsSubtractableItems(amount))
        {
            subtractedItems = Array.Empty<TItem>();
            return false;
        }

        var ret = new TItem[amount];
        _items.CopyTo(ret, 0);
        subtractedItems = ret;

        _items.RemoveRange(0, amount);

        _onSubtractedItems.OnNext((this, 0, amount, ret));

        return true;
    }

    public void Exchange(IInventoryGrid<TSettings, TItem> otherGrid)
    {
        var buffer = _items;
        SetItems(otherGrid.Items);
        otherGrid.SetItems(buffer);

        _onExchanged.OnNext(otherGrid);
    }

    public void SetItems(IEnumerable<TItem> items)
    {
        _items = items.ToList();
    }

    public void Dispose()
    {
        _onAddedItems.Dispose();
        _onSubtractedItems.Dispose();
        _onExchanged.Dispose();
    }

    private int GetMaxAmount()
    {
        return _items.Count == 0
            ? _maxAmountInGrid
            : Math.Min(_maxAmountInGrid, Settings.GetMaxItemAmountInItem(_items.First()));
    }
}