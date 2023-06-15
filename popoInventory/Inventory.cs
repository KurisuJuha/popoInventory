namespace JuhaKurisu.PopoTools.InventorySystem;

public sealed class Inventory<TSettings, TItem> : IInventory<TSettings, TItem>
    where TSettings : IInventorySettings<TSettings, TItem>
{
    public Inventory(TSettings settings)
    {
        Settings = settings;
    }

    public TSettings Settings { get; }

    public void Dispose()
    {
    }
}