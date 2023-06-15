namespace JuhaKurisu.PopoTools.InventorySystem;

public interface IInventory<TSettings, TItem>
    where TSettings : IInventorySettings<TSettings, TItem>
{
    TSettings Settings { get; }
}