namespace JuhaKurisu.PopoTools.InventorySystem;

public class InventoryGrid<TSettings, TItem> : IInventoryGrid<TSettings, TItem>
    where TSettings : IInventorySettings<TSettings, TItem>
{
    private readonly List<TItem> _items;

    public InventoryGrid(TSettings settings)
    {
        _items = new List<TItem>();
        Settings = settings;
        Items = _items.AsReadOnly();
    }

    public TSettings Settings { get; }
    public IReadOnlyCollection<TItem> Items { get; }

    public bool IsAddable(TItem item)
    {
        if (_items.Count >= Settings.GetMaxItemAmountInGrid(this)) return false;
        if (_items.Count >= Settings.GetMaxItemAmountInItem(item)) return false;
        return true;
    }

    public bool TryAdd(TItem item)
    {
        if (!IsAddable(item)) return false;

        _items.Add(item);

        return true;
    }
}