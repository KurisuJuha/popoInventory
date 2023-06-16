using System.Reactive.Subjects;

namespace JuhaKurisu.PopoTools.InventorySystem;

public sealed class InventoryGrid<TSettings, TItem> : IInventoryGrid<TSettings, TItem>
    where TSettings : IInventorySettings<TSettings, TItem>
{
    private readonly int _maxAmountInGrid;
    private readonly Subject<(IInventoryGrid<TSettings, TItem> grid, int index, TItem item)> _onAdded;
    private readonly Subject<(IInventoryGrid<TSettings, TItem> grid, int index, TItem item)> _onSubtracted;
    private List<TItem> _items;

    public InventoryGrid(TSettings settings, int maxAmountInGrid)
    {
        _maxAmountInGrid = maxAmountInGrid;
        _onAdded = new Subject<(IInventoryGrid<TSettings, TItem> grid, int index, TItem item)>();
        _onSubtracted = new Subject<(IInventoryGrid<TSettings, TItem> grid, int index, TItem item)>();
        _items = new List<TItem>();
        Settings = settings;
        Items = _items.AsReadOnly();
    }

    public IObservable<(IInventoryGrid<TSettings, TItem> grid, int index, TItem item)> OnAdded => _onAdded;
    public IObservable<(IInventoryGrid<TSettings, TItem> grid, int index, TItem item)> OnSubtracted => _onSubtracted;

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
        _onAdded.OnNext((this, _items.Count - 1, item));

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
        _onSubtracted.OnNext((this, _items.Count, item));

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

        var i = 0;
        foreach (var item in items)
        {
            _onAdded.OnNext((this, _items.Count - items.Count + i, item));
            i++;
        }

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

        for (var i = 0; i < amount; i++) _onSubtracted.OnNext((this, i, ret[i]));

        return true;
    }

    public void Exchange(IInventoryGrid<TSettings, TItem> otherGrid)
    {
        var buffer = _items;
        SetItems(otherGrid.Items);
        otherGrid.SetItems(buffer);
    }

    public void SetItems(IEnumerable<TItem> items)
    {
        _items = items.ToList();
    }

    public void Dispose()
    {
        _onAdded.Dispose();
        _onSubtracted.Dispose();
    }

    private int GetMaxAmount()
    {
        return _items.Count == 0
            ? _maxAmountInGrid
            : Math.Min(_maxAmountInGrid, Settings.GetMaxItemAmountInItem(_items.First()));
    }
}