using System.Reactive.Subjects;

namespace JuhaKurisu.PopoTools.InventorySystem;

public sealed class InventoryGrid<TSettings, TItem> : IInventoryGrid<TSettings, TItem>
    where TSettings : IInventorySettings<TSettings, TItem>
{
    private readonly List<TItem> _items;
    private readonly int _maxAmount;
    private readonly Subject<IInventoryGrid<TSettings, TItem>> _onAdded;

    public InventoryGrid(TSettings settings, int maxAmount)
    {
        _maxAmount = maxAmount;
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
        if (_items.Count >= _maxAmount) return false;
        if (_items.Count >= Settings.GetMaxItemAmountInItem(item)) return false;
        return true;
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
        _items.RemoveAt(_items.Count - 1);
        return true;
    }

    public void Dispose()
    {
        _onAdded.Dispose();
    }
}