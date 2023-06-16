namespace JuhaKurisu.PopoTools.InventorySystem;

public interface IInventory<TSettings, TItem> : IDisposable
    where TSettings : IInventorySettings<TSettings, TItem>
{
    IReadOnlyCollection<IInventoryGrid<TSettings, TItem>> Grids { get; }
    TSettings Settings { get; }
}