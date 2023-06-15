namespace JuhaKurisu.PopoTools.InventorySystem;

public interface IInventory<TSettings, TItem> : IDisposable
    where TSettings : IInventorySettings<TSettings, TItem>
{
    TSettings Settings { get; }
}