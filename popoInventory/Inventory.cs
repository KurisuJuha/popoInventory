using System.Collections.ObjectModel;

namespace JuhaKurisu.PopoTools.InventorySystem;

public sealed class Inventory<TSettings, TItem> : IInventory<TSettings, TItem>
    where TSettings : IInventorySettings<TItem>
{
    public Inventory(TSettings settings, int size)
    {
        Grids = new ReadOnlyCollection<IInventoryGrid<TSettings, TItem>>(
            new IInventoryGrid<TSettings, TItem>[size]
        );
        Settings = settings;
    }

    public IReadOnlyCollection<IInventoryGrid<TSettings, TItem>> Grids { get; }
    public TSettings Settings { get; }

    public void Dispose()
    {
        foreach (var grid in Grids) grid.Dispose();
    }
}