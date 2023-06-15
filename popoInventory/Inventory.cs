namespace JuhaKurisu.PopoTools.InventorySystem;

public class Inventory<TSettings, TItem> : IInventory<TSettings, TItem>
    where TSettings : IInventorySettings<TSettings, TItem>
{
    public Inventory(TSettings settings)
    {
        Settings = settings;
    }

    public TSettings Settings { get; }
}